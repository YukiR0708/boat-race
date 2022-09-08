using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// InputSystemから入力を受け取ってプレイヤー操作を制御するクラス
/// </summary>

[RequireComponent(typeof(Rigidbody))]

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveForce;

    private Rigidbody _rigidbody;
    private Test _gameInputs;
    private Vector2 _moveInputValue;
    private float _score;
    [SerializeField, Tooltip("FreeLookCamera")] private Camera _tpsCamera;
    [SerializeField, Tooltip("潜る力")] private float _diveForce = 0f;
    [Tooltip("ゲットしたアイテムをItemBaseから受け取る")]
    List<ItemBase> _itemList = new List<ItemBase>();

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _gameInputs = new Test();

        //アクションの取得＆登録
        _gameInputs.Player.BoatMove.started += OnMove;
        _gameInputs.Player.BoatMove.performed += OnMove;
        _gameInputs.Player.BoatMove.canceled += OnMove;

        _gameInputs.Enable();
    }

    void Start()
    {
    }

    /// <summary>
    /// InputActionにプレイヤーの移動を渡すメソッド
    /// </summary>
    /// <param name="context"></param>
    void OnMove(InputAction.CallbackContext context)
    {
        //BoatMoveアクションの入力を取得
        _moveInputValue = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// スコアを増加させるメソッド
    /// </summary>
    public void ScoreUp(float upScore)
    {
        _score += upScore;
    }

    /// <summary>
    ///潜るためのメソッド
    /// </summary>
    public void Dive(float diveForce)
    {
        _diveForce += diveForce;
    }

    /// <summary>
    /// 速度を増加させるメソッド
    /// </summary>
    public void SpeedUp(float upSpeed)
    {
        _moveForce += upSpeed;
    }

    /// <summary>
    /// アイテムをアイテムリストに追加する
    /// </summary>
    /// <param name="item"></param>
    public void GetItem(ItemBase item)
    {
        _itemList.Add(item);
    }




    void Update()
    {
        Move();
        // アイテムを使う
        if (Input.GetButtonDown("Fire1"))
        {
            if (_itemList.Count > 0)
            {
                // リストの先頭にあるアイテムを使って、破棄する
                ItemBase item = _itemList[0];
                _itemList.RemoveAt(0);
                item.Activate();
                Destroy(item.gameObject);
            }
        }

    }


    /// <summary>
    /// プレイヤーの移動を制御するクラス
    /// </summary>
    void Move()
    {
        //カメラのローカル空間のベクトルをワールド空間のベクトルへ変換
        Vector3 pForward = _tpsCamera.transform.TransformDirection(Vector3.forward);
        Vector3 pRight = _tpsCamera.transform.TransformDirection(Vector3.right);

        //ベクトルを加算して進行方向ベクトルを決定（y軸は無視）
        Vector3 moveDir = (_moveInputValue.x * pRight + _moveInputValue.y * pForward) * _moveForce;
        moveDir.y = _diveForce;
        _rigidbody.AddForce(moveDir);

        //補完しながら進行方向を向く
        transform.LookAt(transform.position + moveDir);
    }
}
