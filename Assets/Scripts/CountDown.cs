using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>カウントダウンのアニメーションイベント</summary>
public class CountDown : MonoBehaviour
    
{
    [SerializeField, Header("カウントダウン用SE")] AudioClip _countdownSE;
    [SerializeField] Player _player;

    /// <summary>カウントダウン用SEを鳴らす</summary>
    void PlayCountDownSE()
    {
        GameManager._audioSource.PlayOneShot(_countdownSE, 1.0f);
    }


    /// <summary>カウントダウンのアニメーション終了時に呼ぶイベント </summary>
    void CompletedCountDown()
    {

        //NavMesh(8.0~9.0のスピードで動かす・シネマシーンcart動かす・Player操作受け付ける・BGM鳴らしてループさせる
        GameManager.npc1Speed = Random.Range(10.0f, 11.0f);
        GameManager.npc2Speed = Random.Range(10.0f, 11.0f);
        GameManager.npc3Speed = Random.Range(10.0f, 11.0f);
        GameManager.targetSpeed = 12f;
        Player.gameInputs.Enable();
        _player.canPlayerMove = true;
        

    }
    
    /// <summary>カウントダウンパネルを非表示にする</summary>
    void CountDownFalse()
    {
        GameManager._audioSource.volume = 0.1f;
        GameManager._audioSource.Play();
        this.gameObject.SetActive(false);
    }
}
