using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// InputSystem������͂��󂯎���ăv���C���[����𐧌䂷��N���X
/// </summary>

[RequireComponent(typeof(Rigidbody))]

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveForce;
    private Rigidbody _rigidbody;
    [Tooltip("�L�[����")]private Test _gameInputs;
    private Vector2 _moveInputValue;
    private int _scoreValue;
    [SerializeField, Tooltip("FreeLookCamera")] private Camera _tpsCamera;
    [SerializeField, Tooltip("�����")] private float _diveForce = 0f;
    [Tooltip("�Q�b�g�����A�C�e����ItemBase����󂯎��")]
    List<ItemBase> _itemList = new List<ItemBase>();
    [SerializeField, Tooltip("Score�e�L�X�g")] Text _scoreText;
    [Tooltip("���݂̃��b�v��")] private int _lapcount = 0;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _gameInputs = new Test();

        //�A�N�V�����̎擾���o�^
        _gameInputs.Player.BoatMove.started += OnBoatMove;
        _gameInputs.Player.BoatMove.performed += OnBoatMove;
        _gameInputs.Player.BoatMove.canceled += OnBoatMove;
        _gameInputs.Player.UseItem.performed += OnUseItem;

        _gameInputs.Enable();
    }

    private void Start()
    {
        _scoreText.GetComponent<Text>().text = "SCORE:" + _scoreValue.ToString("D8");

    }

    void Update()
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


    /// <summary>
    /// InputAction�Ƀv���C���[�̈ړ���n�����\�b�h
    /// </summary>
    /// <param name="context"></param>
    void OnBoatMove(InputAction.CallbackContext context)
    {
        //BoatMove�A�N�V�����̓��͂��擾
        _moveInputValue = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// InputAction�ɃA�C�e���̎g�p��n�����\�b�h
    /// </summary>
    /// <param name="context"></param>
    void OnUseItem(InputAction.CallbackContext context)
    {
        if (_itemList.Count > 0)
        {
            Debug.Log("OnUseItem���Ă΂ꂽ");
            // ���X�g�̐擪�ɂ���A�C�e�����g���āA�j������
            ItemBase item = _itemList[0];
            _itemList.RemoveAt(0);
            item.Activate();
            Destroy(item.gameObject);
        }
    }


    /// <summary>
    /// �X�R�A�𑝉������郁�\�b�h
    /// </summary>
    public void ScoreUp(int upScore)
    {
        _scoreValue += upScore;
        Debug.Log(_scoreValue);
        _scoreText.GetComponent<Text>().text = "SCORE:" + _scoreValue.ToString("D8");
    }

    /// <summary>
    ///���邽�߂̃��\�b�h
    /// </summary>
    public void Dive(float diveForce)
    {
        _diveForce += diveForce;
        Debug.Log(_diveForce);
    }

    /// <summary>
    /// ���x�𑝉������郁�\�b�h
    /// </summary>
    public void SpeedUp(float upSpeed)
    {
        _moveForce += upSpeed;
        Debug.Log(_moveForce);
    }

    /// <summary>
    /// �A�C�e�����A�C�e�����X�g�ɒǉ�����
    /// </summary>
    /// <param name="item"></param>
    public void GetItem(ItemBase item)
    {
        _itemList.Add(item);
    }

}
