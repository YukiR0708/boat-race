using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// InputSystem������͂��󂯎���ăv���C���[����𐧌䂷��N���X
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

        //�A�N�V�����̎擾���o�^
        _gameInputs.Player.BoatMove.started += OnMove;
        _gameInputs.Player.BoatMove.performed += OnMove;
        _gameInputs.Player.BoatMove.canceled += OnMove;

        _gameInputs.Enable();
    }


    void OnMove(InputAction.CallbackContext context)
    {
        //BoatMove�A�N�V�����̓��͂��擾
        _moveInputValue = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        _rigidbody.AddForce(new Vector3(_moveInputValue.x, 0, _moveInputValue.y) * _moveForce);
    }
}
