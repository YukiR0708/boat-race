using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;
using Cinemachine;

[RequireComponent(typeof(AudioSource))]

public class GameManager : MonoBehaviour
{
    public GameStatus state = GameStatus.Title;
    //[Tooltip("���݂̃V�[����")] string _currentScene;
    [SerializeField, Header("�J��������")] CinemachineInputProvider _freeLookCamera;
    //*****UI�֘A*****
    [Header("�t�F�[�h�C���p�C���[�W"), SerializeField] Image _fadeInImage;
    public static AudioSource _audioSource;
    [SerializeField, Header("���[�������p�p�l��")] GameObject _rulePanel;
    [SerializeField, Header("�J�E���g�_�E���p�p�l��")] GameObject _countDownPanel;

    //***** NPC�֘A*****
    [SerializeField] NavMeshAgent _npc1;
    [Tooltip("NPC1�̃X�s�[�h")] public static float npc1Speed = 0f;
    [SerializeField] NavMeshAgent _npc2;
    [Tooltip("NPC2�̃X�s�[�h")] public static float npc2Speed = 0f;
    [SerializeField, Header("NPC3")] NavMeshAgent _npc3;
    [Tooltip("NPC3�̃X�s�[�h")] public static float npc3Speed = 0f;
    [SerializeField] CinemachineDollyCart _npcTarget;
    [Tooltip("NPC�̃^�[�Q�b�g�̃X�s�[�h")] public static float targetSpeed = 0f;


    /// <summary>���݂̃Q�[����ԊǗ��p</summary>
    public enum GameStatus
    {
        Title,  //�^�C�g��,��������� 
        InGame, //�Q�[��
        Pause, //�|�[�Y
        UnPause,    //�|�[�Y����
        PlayerGole, //Player���S�[��
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
        //_currentScene = SceneManager.GetActiveScene().name;
        //if (_currentScene == "Title") state = GameStatus.Title;
        //else if (_currentScene == "SinglePlay") state = GameStatus.InGame;

        ///*****�V�[���ɂ����Start�ōs���������قȂ�*****
        //�^�C�g���V�[���̂Ƃ�
        if (state == GameStatus.Title)
        {

        }
        //�Q�[���V�[���̂Ƃ�
        else if (state == GameStatus.InGame)
        {
            //NavMesh�iNPC)�~�߂Ă����E�V�l�}�V�[��cart�i�^�[�Q�b�g�j�~�߂Ă����EPlayer����󂯕t���Ȃ��iUI�̂ݎ󂯕t����j
            _npc1.speed = 0f;
            _npc2.speed = 0f;
            _npc3.speed = 0f;
            _npcTarget.m_Speed = 0f;
        }
    }

    private void Update()
    {
        ///*****�V�[���ɂ����Update�ōs���������قȂ�*****
        //�^�C�g���V�[���̂Ƃ�
        if (state == GameStatus.Title)
        {

        }

        //�Q�[�����̂Ƃ�
        else if (state == GameStatus.InGame || state == GameStatus.PlayerGole)
        {
            //NPC�ƃ^�[�Q�b�g��Speed���擾����
            _npc1.speed = npc1Speed;
            _npc2.speed = npc2Speed;
            _npc3.speed = npc3Speed;
            _npcTarget.m_Speed = targetSpeed;
        }
        //���ʌ��莞
        else if(state == GameStatus.Finish)
        {
            _npc1.speed = 0f;
            _npc2.speed = 0f;
            _npc3.speed = 0f;
            _npcTarget.m_Speed = 0f;
        }
    }


    /// <summary>���[���p�l�������{�^���ō쓮�B�J�E���g�_�E���X�^�[�g</summary>
    public void StartCountDown()
    {
        _countDownPanel.SetActive(true);
        _rulePanel.SetActive(false);
        _freeLookCamera.enabled = true;
    }

    /// <summary>static�ɂ����ϐ��������Z�b�g����</summary>
    void ReStart()
    {
        
    }
}
