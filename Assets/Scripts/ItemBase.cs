using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムを制御する基底クラス
/// アイテムの共通機能を実装する
/// </summary>
/// 
public abstract class ItemBase : MonoBehaviour
{
    [Tooltip("アイテムの回転速度")] float _rotateSpeed = 100f;
    AudioSource _audioSource;
    [SerializeField, Tooltip("アイテム獲得時のSE")] AudioClip _sound = default;
    [Tooltip("Get を選ぶと、取った時に効果が発動する。Use を選ぶと、アイテムを使った時に発動する")]
    [SerializeField, Header("アイテム使用タイミング")] ActivateTiming _whenActivated = ActivateTiming.Get;


    /// <summary>
    /// アイテムが発動する効果を実装(override)する
    /// </summary>
    public abstract void Activate();



    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (_sound)
            {
                AudioSource.PlayClipAtPoint(_sound, Camera.main.transform.position);
            }
            // アイテム発動タイミングによって処理を分ける
            if (_whenActivated == ActivateTiming.Get)
            {
                Activate();
                Destroy(this.gameObject);
            }
            else if (_whenActivated == ActivateTiming.Use)
            {
                // 見えない所に移動する
                this.transform.position = new Vector3(0, -50, 0);
                // コライダーを無効にする
                GetComponent<Collider2D>().enabled = false;
                // プレイヤーにアイテムを渡す
                collision.gameObject.GetComponent<Player>().GetItem(this);
            }
        }
    }

    /// <summary>
    /// アイテムをいつアクティベートするか
    /// </summary>
    enum ActivateTiming
    {
        /// <summary>取った時にすぐ使う</summary>
        Get,
        /// <summary>「使う」コマンドで使う</summary>
        Use,
    }

}

