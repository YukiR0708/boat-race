using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class GameManager : MonoBehaviour
{
    GameStatus _state = GameStatus.Start;
    [Header("�t�F�[�h�C���p�C���[�W"), SerializeField] Image _fadeInImage;
    AudioSource _audioSource;

    /// <summary>���݂̃Q�[����ԊǗ��p</summary>
    public enum GameStatus
    {
        Start,  //�^�C�g��,��������� 
        CountDown,  //3�E2�E1�EStart�̃A�j���[�V����
        GameMode, //�Q�[��
        Finish, //�Q�[���I��
    }




    ///*****�V�[���ړ��֘A*****
    /// <summary>�t�F�[�h</summary>
    public void StartFadeOut(Image fadeOutImage, string scene)//�t�F�[�h�A�E�g�֐�
    {
        fadeOutImage.gameObject.SetActive(true);
        fadeOutImage.DOFade(duration: 1f, endValue: 1f).OnComplete(() => SceneManager.LoadScene(scene)).SetAutoKill();
        //Image��Color�͓����ɐݒ� �������炾�񂾂񍕂�
    }
    public void StartFadeIn(Image fadeInImage)//�t�F�[�h�C���֐�
    {
        fadeInImage.DOFade(endValue: 0f, duration: 1f).OnComplete(() => fadeInImage.gameObject.SetActive(false)).SetAutoKill();
        //Image��Color�͐^�����ɐݒ�@�����炾�񂾂񓧖���
    }


    private void Start()
    {
        ///*****�e�V�[�����ʂ̏���*****
        StartFadeIn(_fadeInImage);  // �t�F�[�h�C��
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;

        ///*****�V�[���ɂ����Start�ōs���������قȂ�*****
        //�^�C�g���V�[���̂Ƃ�


        //�Q�[���V�[���̂Ƃ�

        //
    }

}
