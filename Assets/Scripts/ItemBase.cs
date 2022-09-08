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


    /// <summary>
    /// �A�C�e��������������ʂ�����(override)����
    /// </summary>
    public abstract void Activate();



    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (_sound)
            {
                AudioSource.PlayClipAtPoint(_sound, Camera.main.transform.position);
            }
            // �A�C�e�������^�C�~���O�ɂ���ď����𕪂���
            if (_whenActivated == ActivateTiming.Get)
            {
                Activate();
                Destroy(this.gameObject);
            }
            else if (_whenActivated == ActivateTiming.Use)
            {
                // �����Ȃ����Ɉړ�����
                this.transform.position = new Vector3(0, -50, 0);
                // �R���C�_�[�𖳌��ɂ���
                GetComponent<Collider2D>().enabled = false;
                // �v���C���[�ɃA�C�e����n��
                collision.gameObject.GetComponent<Player>().GetItem(this);
            }
        }
    }

    /// <summary>
    /// �A�C�e�������A�N�e�B�x�[�g���邩
    /// </summary>
    enum ActivateTiming
    {
        /// <summary>��������ɂ����g��</summary>
        Get,
        /// <summary>�u�g���v�R�}���h�Ŏg��</summary>
        Use,
    }

}

