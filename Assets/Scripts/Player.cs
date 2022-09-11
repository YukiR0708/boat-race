using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Linq;

/// <summary>InputSystem������͂��󂯎���ăv���C���[����𐧌䂷��N���X </summary>

[RequireComponent(typeof(Rigidbody))]

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveForce;
    private Rigidbody _rigidbody;
    [Tooltip("�L�[����")] private Test _gameInputs;
    private Vector2 _moveInputValue;
    private int _scoreValue;
    [SerializeField, Tooltip("FreeLookCamera")] private Camera _tpsCamera;
    [SerializeField, Tooltip("�����")] private float _diveForce;
    [Tooltip("�Q�b�g�����A�C�e����ItemBase����󂯎��")]
    List<ItemBase> _itemList = new List<ItemBase>();
    [SerializeField, Tooltip("Score�e�L�X�g")] Text _scoreText;
    [SerializeField, Tooltip("���񐔃e�L�X�g")] Text _lapText;
    [Tooltip("���݂̃��b�v��")] private int _lapcount = 0;
    [Tooltip("�`�F�b�N�|�C���g�̖��O���X�g")] List<string> _checkPoints = new();
    [Tooltip("�`�F�b�N�|�C���g�̖��O�����K���[�g�ʂ�ɓ����Ă�z��")]//���X�g���Z�b�g���̏��������p 
    string[] _checkPoint = { "CheckPoint1", "CheckPoint2", "CheckPoint3", "Goal" };

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _gameInputs = new Test();

        //*****InputAction�̎擾���o�^*****
        _gameInputs.Player.BoatMove.started += OnBoatMove;
        _gameInputs.Player.BoatMove.performed += OnBoatMove;
        _gameInputs.Player.BoatMove.canceled += OnBoatMove;
        _gameInputs.Player.UseItem.performed += OnUseItem;
        _gameInputs.Enable();
        ///*****�e��f�t�H���g�l�ݒ�*****
        _scoreText.text = "SCORE:" + _scoreValue.ToString("D8");
        _lapText.text = "LAP:" + _lapcount.ToString() + "/3";
        _checkPoints = _checkPoint.ToList();

    }

    void Update()
    {
        //*****Player��TPS�ړ�(camera�̑O������ɐ��ʂƂ���)����*****
        //���J�����̃��[�J����Ԃ̃x�N�g�������[���h��Ԃ̃x�N�g���֕ϊ�
        Vector3 pForward = _tpsCamera.transform.TransformDirection(Vector3.forward);
        Vector3 pRight = _tpsCamera.transform.TransformDirection(Vector3.right);

        //���x�N�g�������Z���Đi�s�����x�N�g��������iy���͖����j
        Vector3 moveDir = (_moveInputValue.x * pRight + _moveInputValue.y * pForward) * _moveForce * Time.deltaTime;
        moveDir.y = 0;
        _rigidbody.AddForce(moveDir);

        //���⊮���Ȃ���i�s����������
        transform.LookAt(transform.position + moveDir);
    }

    //*****InputAction�ɓ��͂�n������*****
    /// <summary> InputAction�ɁA�v���C���[�̈ړ���n�����\�b�h </summary>
    /// <param name="context"></param>
    void OnBoatMove(InputAction.CallbackContext context)
    {
        //��BoatMove�A�N�V�����̓��͂��擾
        _moveInputValue = context.ReadValue<Vector2>();
    }

    /// <summary> InputAction�ɁA�A�C�e���̎g�p��n�����\�b�h </summary>
    /// <param name="context"></param>
    void OnUseItem(InputAction.CallbackContext context)
    {
        if (_itemList.Count > 0)
        {
            Debug.Log("OnUseItem���Ă΂ꂽ");
            //�����X�g�̐擪�ɂ���A�C�e�����g���āA�j������
            ItemBase item = _itemList[0];
            _itemList.RemoveAt(0);
            item.Activate();
            Destroy(item.gameObject);
        }
    }

    //*****�����g�p�łȂ��A�C�e���̏���*****
    /// <summary> �A�C�e�����A�C�e�����X�g�ɒǉ����� </summary>
    /// <param name="item"></param>
    public void GetItem(ItemBase item)
    {
        _itemList.Add(item);
    }

    //*****�ȉ��A�����g�p�A�C�e���̏���*****
    /// <summary> �X�R�A�𑝉������郁�\�b�h </summary>
    public void ScoreUp(int upScore)
    {
        _scoreValue += upScore;
        _scoreText.text = "SCORE:" + _scoreValue.ToString("D8");
    }

    /// <summary> ���郁�\�b�h </summary>
    public void Dive(float diveForce)
    {
        _diveForce += diveForce;
        Debug.Log(_diveForce);
    }

    /// <summary> ���x�𑝉������郁�\�b�h </summary>
    public void SpeedUp(float upSpeed)
    {
        _moveForce += upSpeed;
        Debug.Log(_moveForce);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == _checkPoints[0] && other.gameObject.CompareTag("Check"))
        {
            Debug.Log($"{_checkPoints[0]}��ʉ߂��܂���");
            _checkPoints.RemoveAt(0);
        }   
        else if(other.gameObject.name == _checkPoints[0] && other.gameObject.CompareTag("Goal"))
        {
            Debug.Log($"{_checkPoints[0]}��ʉ߂��܂���");
            _checkPoints.RemoveAt(0);
            //�����̎������꒼��
            _checkPoints = _checkPoint.ToList();
            _lapcount++;
            _lapText.text = "LAP:" + _lapcount.ToString() + "/3";

        }
        else if((other.gameObject.name != _checkPoints[0]) && (other.gameObject.CompareTag("Goal") || other.gameObject.CompareTag("Check")))
        {
            Debug.Log("���K���[�g�ɖ߂��Ă�������");
        }
    }
}
