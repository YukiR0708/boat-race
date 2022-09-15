using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>カウントダウン終了時の処理</summary>
public class CountDown : MonoBehaviour
    
{
    /// <summary>カウントダウンのアニメーション終了時に呼ぶイベント </summary>
    void CompletedCountDown()
    {
        //NavMesh(8.0~9.0のスピードで動かす・シネマシーンcart動かす・Player操作受け付ける・BGM鳴らしてループさせる
        Debug.Log("ここで動けるようにする");

    }

    void CountDownFalse()
    {
        this.gameObject.SetActive(false);
    }
}
