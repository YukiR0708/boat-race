using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>�J���������ʂɓ�������s�������̃N���X</summary>
public class CameraEffect : MonoBehaviour
{
    [SerializeField, Header("���̃G�t�F�N�g�p�l��")] Image _waterPanel;
    [SerializeField, Header("���ʔ���̊�Ƃ���Y��")] float _waterBorder;
    [Tooltip("���݂̃J������Y��")] float _currentY;
    [Tooltip("�O�̃t���[���̃J������Y��")] float _oldY;
    [SerializeField, Header("Player�ɂ��Ă�ASE�p��AudioSource")] AudioSource _as;
    [SerializeField, Header("GameManaer�ɂ��Ă�ABGM�p��AudioLowPassFilter")]AudioLowPassFilter _bgmLowFilter;
    [SerializeField, Header("���ɓ������Ƃ���SE")] AudioClip _enterWater;
    [SerializeField, Header("������o���Ƃ���SE")] AudioClip _exitWater;
    [SerializeField, Header("���b�҂��Ă��琅�H��������")] float _waitSecond;

    private void Start()
    {
        _oldY = this.gameObject.transform.position.y;    
    }

    void Update()
    {
        _currentY = this.gameObject.transform.position.y;

        if(_oldY > _waterBorder && _currentY < _waterBorder)
        {
            //���ɓ������Ƃ��̏���    ���������点��E�h�{��SE
            _as.PlayOneShot(_enterWater);
            _bgmLowFilter.enabled = true;
            _waterPanel.color = new Color(255, 255, 255, 0.4f);
            _waterPanel.enabled = true;
        }
        else if(_oldY < _waterBorder && _currentY > _waterBorder)
        {
            //������o���Ƃ��̏����@�������Ƃɖ߂��E�|�`����SE�EDoTween�ŏ��X�ɐ��H�p�l��������
            _as.PlayOneShot(_exitWater);
            _waterPanel.DOFade(endValue: 0f, duration: _waitSecond).OnComplete(() => _waterPanel.enabled = false).SetAutoKill();
            _bgmLowFilter.enabled = false;
        }
        _oldY = _currentY;
    }

}
