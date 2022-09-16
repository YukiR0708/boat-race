using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>カウントダウン終了時の処理</summary>
public class CountDown : MonoBehaviour
    
{

    /// <summary>カウントダウンのアニメーション終了時に呼ぶイベント </summary>
    void CompletedCountDown()
    {

        //NavMesh(8.0~9.0のスピードで動かす・シネマシーンcart動かす・Player操作受け付ける・BGM鳴らしてループさせる
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
