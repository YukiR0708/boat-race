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
    [Header("�t�F�[�h�p�C���[�W"), SerializeField] Image _fadeImage;
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
    public void StartFadeOut(string scene)//�t�F�[�h�A�E�g�֐�
    {
        _fadeImage.gameObject.SetActive(true);
        this._fadeImage.DOFade(duration: 1f, endValue: 1f).OnComplete(() => SceneManager.LoadScene(scene));
        //Image��Color�͓����ɐݒ� �������炾�񂾂񍕂�
    }
    public void StartFadeIn()//�t�F�[�h�C���֐�
    {
        this._fadeImage.DOFade(endValue: 0f, duration: 1f).OnComplete(() => _fadeImage.gameObject.SetActive(false));
        //Image��Color�͐^�����ɐݒ�@�����炾�񂾂񓧖���
    }


    private void Start()
    {
        ///*****�e�V�[�����ʂ̏���*****
        StartFadeIn();  // �t�F�[�h�C��
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = true;
        _audioSource.loop = true;

        ///*****�V�[���ɂ����Start�ōs���������قȂ�*****
    }

}
