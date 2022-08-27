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


    void OnMove(InputAction.CallbackContext context)
    {
        //BoatMoveアクションの入力を取得
        _moveInputValue = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        _rigidbody.AddForce(new Vector3(_moveInputValue.x, 0, _moveInputValue.y) * _moveForce);
    }
}
