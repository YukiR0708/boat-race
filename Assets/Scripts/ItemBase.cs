using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�C�e���𐧌䂷����N���X
/// �A�C�e���̋��ʋ@�\����������
/// </summary>
/// 
public abstract class ItemBase : MonoBehaviour
{
    [Tooltip("�A�C�e���̉�]���x")] float _rotateSpeed = 100f;
    AudioSource _audioSource;
    [SerializeField, Tooltip("�A�C�e���l������SE")] AudioClip _sound = default;
    [Tooltip("Get ��I�ԂƁA��������Ɍ��ʂ���������BUse ��I�ԂƁA�A�C�e�����g�������ɔ�������")]
    [SerializeField, Header("�A�C�e���g�p�^�C�~���O")] ActivateTiming _whenActivated = ActivateTiming.Get;


    /// <summary>�@�A�C�e��������������ʂ�����(override)����@</summary>
    public abstract void Activate();

    private void Start()
    {
        _audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    void Update()
    {
        //���A�C�e�������邭��܂��
        transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        ///*****Player�ɓ��������Ƃ��̏���*****
        if (other.CompareTag("Player"))
        {
            //���A�C�e�����ƂɃA�T�C������Clip��炷
            if (_sound)
            {
                _audioSource.PlayOneShot(_sound);
            }

            // �A�C�e�������^�C�~���O�ɂ���ď����𕪂���
            //�������g�p�̃A�C�e��
            if (_whenActivated == ActivateTiming.Get)
            {
                Activate();
                Destroy(this.gameObject);
            }
            //���g�p�{�^���Ńv���C���[�̔C�ӂ̃^�C�~���O�Ŏg�p����A�C�e��
            else if (_whenActivated == ActivateTiming.Use)
            {
                Debug.Log("Use�ɕ��򂵂�");
                //�������Ȃ����Ɉړ�����
                this.transform.position = new Vector3(0, -50, 0);
                // ���ꉞ�R���C�_�[�𖳌��ɂ���
                GetComponent<Collider>().enabled = false;
                // ���v���C���[�ɃA�C�e����n��
                other.gameObject.GetComponent<Player>().GetItem(this);
            }
        }
    }

    /// <summary> �A�C�e�������A�N�e�B�x�[�g���邩 </summary>
    //�e�A�C�e����Prefab�̃C���X�y�N�^�[����w�肷��
    enum ActivateTiming
    {
        /// <summary>��������ɂ����g��</summary>
        Get,
        /// <summary>�A�C�e���g�p�{�^���Ŏg��</summary>
        Use,
    }

}

