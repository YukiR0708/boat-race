using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>�J�E���g�_�E���I�����̏���</summary>
public class CountDown : MonoBehaviour
    
{

    /// <summary>�J�E���g�_�E���̃A�j���[�V�����I�����ɌĂԃC�x���g </summary>
    void CompletedCountDown()
    {

        //NavMesh(8.0~9.0�̃X�s�[�h�œ������E�V�l�}�V�[��cart�������EPlayer����󂯕t����EBGM�炵�ă��[�v������
        GameManager.npc1Speed = Random.Range(8.0f, 9.0f);
        GameManager.npc2Speed = Random.Range(8.0f, 9.0f);
        GameManager.npc3Speed = Random.Range(8.0f, 9.0f);
        GameManager.targetSpeed = 10f;
        GameManager._audioSource.Play();
        Player.gameInputs.Enable();

    }

    void CountDownFalse()
    {
        this.gameObject.SetActive(false);
    }
}
