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
    GameStatus _state = GameStatus.Start;
    //*****UI�֘A*****
    [Header("�t�F�[�h�C���p�C���[�W"), SerializeField] Image _fadeInImage;
    public static AudioSource _audioSource;
    [SerializeField, Header("���[�������p�p�l��")] GameObject _rulePanel;
    [SerializeField, Header("�J�E���g�_�E���p�p�l��")] GameObject _countDownPanel;

    //***** NPC�֘A*****
    [SerializeField, Header("NPC�P")] NavMeshAgent _npc1;
    [Tooltip("NPC1�̃X�s�[�h")] public static float npc1Speed = 0f;
    [SerializeField, Header("NPC2")] NavMeshAgent _npc2;
    [Tooltip("NPC2�̃X�s�[�h")] public static float npc2Speed = 0f;
    [SerializeField, Header("NPC3")] NavMeshAgent _npc3;
    [Tooltip("NPC3�̃X�s�[�h")] public static float npc3Speed = 0f;
    [SerializeField, Header("NPC�̃^�[�Q�b�g")] CinemachineDollyCart _npcTarget;
    [Tooltip("NPC�̃^�[�Q�b�g�̃X�s�[�h")] public static float targetSpeed = 0f;
    [SerializeField, Header("�J��������")] CinemachineInputProvider _freeLookCamera;

    /// <summary>���݂̃Q�[����ԊǗ��p</summary>
    public enum GameStatus
    {
        Start,  //�^�C�g��,��������� 
        Rule,   //���[������
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

        //NavMesh�iNPC)�~�߂Ă����E�V�l�}�V�[��cart�i�^�[�Q�b�g�j�~�߂Ă����EPlayer����󂯕t���Ȃ��iUI�̂ݎ󂯕t����j
        _npc1.speed = npc1Speed;
        _npc2.speed = npc2Speed;
        _npc3.speed = npc3Speed;
        _npcTarget.m_Speed = targetSpeed;
    }

    private void Update()
    {
        ///*****�V�[���ɂ����Update�ōs���������قȂ�*****
        //�^�C�g���V�[���̂Ƃ�


        //�Q�[���V�[���̂Ƃ�

        //NPC�ƃ^�[�Q�b�g��Speed���擾����
        _npc1.speed = npc1Speed;
        _npc2.speed = npc2Speed;
        _npc3.speed = npc3Speed;
        _npcTarget.m_Speed = targetSpeed;
    }


    /// <summary>���[���p�l�������{�^���ō쓮�B�J�E���g�_�E���X�^�[�g</summary>
    public void StartCountDown()
    {
        _countDownPanel.SetActive(true);
        _rulePanel.SetActive(false);
        _freeLookCamera.enabled = true;
        
;    }
}
