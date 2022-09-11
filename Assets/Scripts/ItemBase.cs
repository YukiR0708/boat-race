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


    /// <summary>　アイテムが発動する効果を実装(override)する　</summary>
    public abstract void Activate();

    private void Start()
    {
        _audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    void Update()
    {
        //↓アイテムがくるくるまわる
        transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        ///*****Playerに当たったときの処理*****
        if (other.CompareTag("Player"))
        {
            //↓アイテムごとにアサインしたClipを鳴らす
            if (_sound)
            {
                _audioSource.PlayOneShot(_sound);
            }

            // アイテム発動タイミングによって処理を分ける
            //↓即時使用のアイテム
            if (_whenActivated == ActivateTiming.Get)
            {
                Activate();
                Destroy(this.gameObject);
            }
            //↓使用ボタンでプレイヤーの任意のタイミングで使用するアイテム
            else if (_whenActivated == ActivateTiming.Use)
            {
                Debug.Log("Useに分岐した");
                //↓見えない所に移動する
                this.transform.position = new Vector3(0, -50, 0);
                // ↓一応コライダーを無効にする
                GetComponent<Collider>().enabled = false;
                // ↓プレイヤーにアイテムを渡す
                other.gameObject.GetComponent<Player>().GetItem(this);
            }
        }
    }

    /// <summary> アイテムをいつアクティベートするか </summary>
    //各アイテムのPrefabのインスペクターから指定する
    enum ActivateTiming
    {
        /// <summary>取った時にすぐ使う</summary>
        Get,
        /// <summary>アイテム使用ボタンで使う</summary>
        Use,
    }

}

