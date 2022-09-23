using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>カメラが水面に入ったら行う処理のクラス</summary>
public class CameraEffect : MonoBehaviour
{
    [SerializeField, Header("水のエフェクトパネル")] Image _waterPanel;
    [SerializeField, Header("水面判定の基準とするY軸")] float _waterBorder;
    [Tooltip("現在のカメラのY軸")] float _currentY;
    [Tooltip("前のフレームのカメラのY軸")] float _oldY;
    [SerializeField, Header("Playerについてる、SE用のAudioSource")] AudioSource _as;
    [SerializeField, Header("GameManaerについてる、BGM用のAudioLowPassFilter")]AudioLowPassFilter _bgmLowFilter;
    [SerializeField, Header("水に入ったときのSE")] AudioClip _enterWater;
    [SerializeField, Header("水から出たときのSE")] AudioClip _exitWater;
    [SerializeField, Header("何秒待ってから水滴を消すか")] float _waitSecond;

    private void Start()
    {
        _oldY = this.gameObject.transform.position.y;    
    }

    void Update()
    {
        _currentY = this.gameObject.transform.position.y;

        if(_oldY > _waterBorder && _currentY < _waterBorder)
        {
            //水に入ったときの処理    音をこもらせる・ドボンSE
            _as.PlayOneShot(_enterWater);
            _bgmLowFilter.enabled = true;
            _waterPanel.color = new Color(255, 255, 255, 0.4f);
            _waterPanel.enabled = true;
        }
        else if(_oldY < _waterBorder && _currentY > _waterBorder)
        {
            //水から出たときの処理　音をもとに戻す・ポチャンSE・DoTweenで徐々に水滴パネルを消す
            _as.PlayOneShot(_exitWater);
            _waterPanel.DOFade(endValue: 0f, duration: _waitSecond).OnComplete(() => _waterPanel.enabled = false).SetAutoKill();
            _bgmLowFilter.enabled = false;
        }
        _oldY = _currentY;
    }

}
