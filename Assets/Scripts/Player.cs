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
    private Transform _transform;
    private Vector3 _prePosition;

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

    void Start()
    {
        _transform = this.transform;
        _prePosition = _transform.position;
    }


    void OnMove(InputAction.CallbackContext context)
    {
        //BoatMove�A�N�V�����̓��͂��擾
        _moveInputValue = context.ReadValue<Vector2>();
    }

    void Update()
    {
        _rigidbody.AddForce(new Vector3(_moveInputValue.x, 0, _moveInputValue.y) * _moveForce);

        //���݂̃|�W�V�������擾
        var currentPosition = _transform.position;

        //�ړ��ʂ��v�Z
        var delta = currentPosition - _prePosition;
        delta.y = 0;

        // �Î~���Ă����Ԃ��ƁA�i�s���������ł��Ȃ����߉�]���Ȃ�
        if (delta == Vector3.zero)
            return;

        // �i�s�����i�ړ��ʃx�N�g���j�Ɍ����悤�ȃN�H�[�^�j�I�����擾
        var rotation = Quaternion.LookRotation(delta * _moveForce, Vector3.up);

        // �I�u�W�F�N�g�̉�]�ɔ��f
        _transform.rotation = rotation;

        //���݂̃|�W�W������ۑ�
        _prePosition = currentPosition;

    }
}
