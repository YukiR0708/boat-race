using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>�J�E���g�_�E���̃A�j���[�V�����C�x���g</summary>
public class CountDown : MonoBehaviour
    
{
    [SerializeField, Header("�J�E���g�_�E���pSE")] AudioClip _countdownSE;

    /// <summary>�J�E���g�_�E���pSE��炷</summary>
    void PlayCountDownSE()
    {
        GameManager._audioSource.PlayOneShot(_countdownSE, 1.0f);
    }


    /// <summary>�J�E���g�_�E���̃A�j���[�V�����I�����ɌĂԃC�x���g </summary>
    void CompletedCountDown()
    {

        //NavMesh(8.0~9.0�̃X�s�[�h�œ������E�V�l�}�V�[��cart�������EPlayer����󂯕t����EBGM�炵�ă��[�v������
        GameManager.npc1Speed = Random.Range(8.0f, 9.0f);
        GameManager.npc2Speed = Random.Range(8.0f, 9.0f);
        GameManager.npc3Speed = Random.Range(8.0f, 9.0f);
        GameManager.targetSpeed = 10f;
        Player.gameInputs.Enable();

    }
    
    /// <summary>�J�E���g�_�E���p�l�����\���ɂ���</summary>
    void CountDownFalse()
    {
        GameManager._audioSource.volume = 0.1f;
        GameManager._audioSource.Play();
        this.gameObject.SetActive(false);
    }
}
