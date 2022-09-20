using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

/// <summary>InputSystemから入力を受け取ってプレイヤー操作を制御するクラス </summary>

[RequireComponent(typeof(Rigidbody))]

public class Player : MonoBehaviour
{
    //*****移動関連*****
    private Rigidbody _rigidbody;
    [SerializeField] private float _moveForce;
    [Tooltip("キー入力")] public static Test gameInputs;
    private Vector2 _moveInputValue;
    [SerializeField, Tooltip("FreeLookCamera")] private Camera _tpsCamera;

    //*****UI・DOTween関連*****
    [SerializeField, Tooltip("Scoreテキスト")] Text _scoreText;
    [SerializeField] float _scoreChangeInterval = 0.5f; //何秒かけて変化させるか
     AudioSource _audioSource;

    //*****アイテム関連*****
    [SerializeField, Tooltip("潜る力")] private float _diveForce;
    [Tooltip("ゲットしたアイテムをItemBaseから受け取る")]
    List<ItemBase> _itemList = new List<ItemBase>();
    private int _scoreValue;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        gameInputs = new Test();
        _audioSource = GetComponent<AudioSource>();

        //*****InputActionの取得＆登録*****
        gameInputs.Player.BoatMove.started += OnBoatMove;
        gameInputs.Player.BoatMove.performed += OnBoatMove;
        gameInputs.Player.BoatMove.canceled += OnBoatMove;
        gameInputs.Player.UseItem.performed += OnUseItem;
        //gameInputs.Enable();
        ///*****各種デフォルト値設定*****
        _scoreText.text = "SCORE:" + _scoreValue.ToString("D8");

    }

    void Update()
    {
        //*****PlayerのTPS移動(cameraの前方を常に正面とする)処理*****
        //↓カメラのローカル空間のベクトルをワールド空間のベクトルへ変換
        Vector3 pForward = _tpsCamera.transform.TransformDirection(Vector3.forward);
        Vector3 pRight = _tpsCamera.transform.TransformDirection(Vector3.right);

        //↓ベクトルを加算して進行方向ベクトルを決定（y軸は無視）
        Vector3 moveDir = (_moveInputValue.x * pRight + _moveInputValue.y * pForward) * _moveForce * Time.deltaTime;
        moveDir.y = 0;
        _rigidbody.AddForce(moveDir);

        //↓補完しながら進行方向を向く
        transform.LookAt(transform.position + moveDir);
    }

    //*****InputActionに入力を渡す処理*****
    /// <summary> プレイヤー移動コールバック </summary>
    /// <param name="context"></param>
    void OnBoatMove(InputAction.CallbackContext context)
    {
        //↓BoatMoveアクションの入力を取得
        _moveInputValue = context.ReadValue<Vector2>().normalized;
    }

    /// <summary>アイテム使用コールバック</summary>
    /// <param name="context"></param>
    void OnUseItem(InputAction.CallbackContext context)
    {
        if (_itemList.Count > 0)
        {
            Debug.Log("OnUseItemが呼ばれた");
            //↓リストの先頭にあるアイテムを使って、破棄する
            ItemBase item = _itemList[0];
            _itemList.RemoveAt(0);
            item.Activate();
            Destroy(item.gameObject);
        }
    }

    //*****即時使用でないアイテムの処理*****
    /// <summary> アイテムをアイテムリストに追加する </summary>
    /// <param name="item"></param>
    public void GetItem(ItemBase item)
    {
        _itemList.Add(item);
    }

    //*****以下、即時使用アイテムの処理*****
    /// <summary> スコアを増加させるメソッド </summary>
    public void ScoreUp(int upScore)
    {
        int oldScore = _scoreValue; //追加前のスコアを保存
        _scoreValue += upScore;
        DOTween.To(() => oldScore,  //DOTweenで連続的に変化させる対象の値
               x => _scoreText.text = "SCORE:" + x.ToString("D8"),
           _scoreValue,
           _scoreChangeInterval)
           .OnUpdate(() => _audioSource.Play())
           .OnComplete(() => _scoreText.text = "SCORE:" + _scoreValue.ToString("D8"));
    }

    /// <summary> 潜るメソッド </summary>
    public void Dive(float diveForce)
    {
        _diveForce += diveForce;
        Debug.Log(_diveForce);
    }

    /// <summary> 速度を増加させるメソッド </summary>
    public void SpeedUp(float upSpeed)
    {
        _moveForce += upSpeed;
        Debug.Log(_moveForce);
    }

}
