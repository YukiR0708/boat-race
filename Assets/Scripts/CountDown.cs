using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�J�E���g�_�E���I�����̏���</summary>
public class CountDown : MonoBehaviour
    
{
    /// <summary>�J�E���g�_�E���̃A�j���[�V�����I�����ɌĂԃC�x���g </summary>
    void CompletedCountDown()
    {
        //NavMesh(8.0~9.0�̃X�s�[�h�œ������E�V�l�}�V�[��cart�������EPlayer����󂯕t����EBGM�炵�ă��[�v������
        Debug.Log("�����œ�����悤�ɂ���");

    }

    void CountDownFalse()
    {
        this.gameObject.SetActive(false);
    }
}
