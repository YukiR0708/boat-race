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
    private Transform _transform;
    private Vector3 _prePosition;

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
        _transform = this.transform;
        _prePosition = _transform.position;
    }


    void OnMove(InputAction.CallbackContext context)
    {
        //BoatMoveアクションの入力を取得
        _moveInputValue = context.ReadValue<Vector2>();
    }

    void Update()
    {
        _rigidbody.AddForce(new Vector3(_moveInputValue.x, 0, _moveInputValue.y) * _moveForce);

        //現在のポジションを取得
        var currentPosition = _transform.position;

        //移動量を計算
        var delta = currentPosition - _prePosition;
        delta.y = 0;

        // 静止している状態だと、進行方向を特定できないため回転しない
        if (delta == Vector3.zero)
            return;

        // 進行方向（移動量ベクトル）に向くようなクォータニオンを取得
        var rotation = Quaternion.LookRotation(delta * _moveForce, Vector3.up);

        // オブジェクトの回転に反映
        _transform.rotation = rotation;

        //現在のポジジョンを保存
        _prePosition = currentPosition;

    }
}
