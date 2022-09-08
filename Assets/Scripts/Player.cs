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
    private float _score;
    [SerializeField, Tooltip("FreeLookCamera")] private Camera _tpsCamera;
    [SerializeField, Tooltip("�����")] private float _diveForce = 0f;
    [Tooltip("�Q�b�g�����A�C�e����ItemBase����󂯎��")]
    List<ItemBase> _itemList = new List<ItemBase>();

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
    }

    /// <summary>
    /// InputAction�Ƀv���C���[�̈ړ���n�����\�b�h
    /// </summary>
    /// <param name="context"></param>
    void OnMove(InputAction.CallbackContext context)
    {
        //BoatMove�A�N�V�����̓��͂��擾
        _moveInputValue = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// �X�R�A�𑝉������郁�\�b�h
    /// </summary>
    public void ScoreUp(float upScore)
    {
        _score += upScore;
    }

    /// <summary>
    ///���邽�߂̃��\�b�h
    /// </summary>
    public void Dive(float diveForce)
    {
        _diveForce += diveForce;
    }

    /// <summary>
    /// ���x�𑝉������郁�\�b�h
    /// </summary>
    public void SpeedUp(float upSpeed)
    {
        _moveForce += upSpeed;
    }

    /// <summary>
    /// �A�C�e�����A�C�e�����X�g�ɒǉ�����
    /// </summary>
    /// <param name="item"></param>
    public void GetItem(ItemBase item)
    {
        _itemList.Add(item);
    }




    void Update()
    {
        Move();
        // �A�C�e�����g��
        if (Input.GetButtonDown("Fire1"))
        {
            if (_itemList.Count > 0)
            {
                // ���X�g�̐擪�ɂ���A�C�e�����g���āA�j������
                ItemBase item = _itemList[0];
                _itemList.RemoveAt(0);
                item.Activate();
                Destroy(item.gameObject);
            }
        }

    }


    /// <summary>
    /// �v���C���[�̈ړ��𐧌䂷��N���X
    /// </summary>
    void Move()
    {
        //�J�����̃��[�J����Ԃ̃x�N�g�������[���h��Ԃ̃x�N�g���֕ϊ�
        Vector3 pForward = _tpsCamera.transform.TransformDirection(Vector3.forward);
        Vector3 pRight = _tpsCamera.transform.TransformDirection(Vector3.right);

        //�x�N�g�������Z���Đi�s�����x�N�g��������iy���͖����j
        Vector3 moveDir = (_moveInputValue.x * pRight + _moveInputValue.y * pForward) * _moveForce;
        moveDir.y = _diveForce;
        _rigidbody.AddForce(moveDir);

        //�⊮���Ȃ���i�s����������
        transform.LookAt(transform.position + moveDir);
    }
}
