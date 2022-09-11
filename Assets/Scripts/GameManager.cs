using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>現在のゲーム状況</summary>
    public enum GameStatus
    {
        Start,  //タイトル,操作説明等 
        GameMode, //ゲーム
        Finish, //ゲーム終了
    }
}
