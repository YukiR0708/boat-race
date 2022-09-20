using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

/// <summary>InputSystem������͂��󂯎���ăv���C���[����𐧌䂷��N���X </summary>

[RequireComponent(typeof(Rigidbody))]

public class Player : MonoBehaviour
{
    //*****�ړ��֘A*****
    private Rigidbody _rigidbody;
    [SerializeField] private float _moveForce;
    [Tooltip("�L�[����")] public static Test gameInputs;
    private Vector2 _moveInputValue;
    [SerializeField, Tooltip("FreeLookCamera")] private Camera _tpsCamera;

    //*****UI�EDOTween�֘A*****
    [SerializeField, Tooltip("Score�e�L�X�g")] Text _scoreText;
    [SerializeField] float _scoreChangeInterval = 0.5f; //���b�����ĕω������邩
     AudioSource _audioSource;

    //*****�A�C�e���֘A*****
    [SerializeField, Tooltip("�����")] private float _diveForce;
    [Tooltip("�Q�b�g�����A�C�e����ItemBase����󂯎��")]
    List<ItemBase> _itemList = new List<ItemBase>();
    private int _scoreValue;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        gameInputs = new Test();
        _audioSource = GetComponent<AudioSource>();

        //*****InputAction�̎擾���o�^*****
        gameInputs.Player.BoatMove.started += OnBoatMove;
        gameInputs.Player.BoatMove.performed += OnBoatMove;
        gameInputs.Player.BoatMove.canceled += OnBoatMove;
        gameInputs.Player.UseItem.performed += OnUseItem;
        //gameInputs.Enable();
        ///*****�e��f�t�H���g�l�ݒ�*****
        _scoreText.text = "SCORE:" + _scoreValue.ToString("D8");

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
    /// <summary> �v���C���[�ړ��R�[���o�b�N </summary>
    /// <param name="context"></param>
    void OnBoatMove(InputAction.CallbackContext context)
    {
        //��BoatMove�A�N�V�����̓��͂��擾
        _moveInputValue = context.ReadValue<Vector2>().normalized;
    }

    /// <summary>�A�C�e���g�p�R�[���o�b�N</summary>
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
        int oldScore = _scoreValue; //�ǉ��O�̃X�R�A��ۑ�
        _scoreValue += upScore;
        DOTween.To(() => oldScore,  //DOTween�ŘA���I�ɕω�������Ώۂ̒l
               x => _scoreText.text = "SCORE:" + x.ToString("D8"),
           _scoreValue,
           _scoreChangeInterval)
           .OnUpdate(() => _audioSource.Play())
           .OnComplete(() => _scoreText.text = "SCORE:" + _scoreValue.ToString("D8"));
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

}
