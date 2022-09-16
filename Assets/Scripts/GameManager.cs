using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]

public class GameManager : MonoBehaviour
{
    GameStatus _state = GameStatus.Start;
    [Header("フェードイン用イメージ"), SerializeField] Image _fadeInImage;
    AudioSource _audioSource;
    [SerializeField, Header("ルール説明用パネル")] GameObject _rulePanel;
    [SerializeField, Header("カウントダウン用パネル")] GameObject _countDownPanel;
    [SerializeField, Header("NPC１")] NavMeshAgent _npc1;
    [Tooltip("NPC1のスピード")] public static float npc1Speed = 0f;
    [SerializeField, Header("NPC2")] NavMeshAgent _npc2;
    [Tooltip("NPC2のスピード")] public static float npc2Speed = 0f;
    [SerializeField, Header("NPC3")] NavMeshAgent _npc3;
    [Tooltip("NPC3のスピード")] public static float npc3Speed = 0f;

    /// <summary>現在のゲーム状態管理用</summary>
    public enum GameStatus
    {
        Start,  //タイトル,操作説明等 
        Rule,   //ルール説明
        CountDown,  //3・2・1・Startのアニメーション
        GameMode, //ゲーム
        Finish, //ゲーム終了
    }




    ///*****シーン移動関連*****
    /// <summary>フェード</summary>
    public void StartFadeOut(Image fadeOutImage, string scene)//フェードアウト関数
    {
        fadeOutImage.gameObject.SetActive(true);
        fadeOutImage.DOFade(duration: 1f, endValue: 1f).OnComplete(() => SceneManager.LoadScene(scene)).SetAutoKill();
        //ImageのColorは透明に設定 透明からだんだん黒へ
    }
    public void StartFadeIn(Image fadeInImage)//フェードイン関数
    {
        fadeInImage.DOFade(endValue: 0f, duration: 1f).OnComplete(() => fadeInImage.gameObject.SetActive(false)).SetAutoKill();
        //ImageのColorは真っ黒に設定　黒からだんだん透明へ
    }


    private void Start()
    {
        ///*****各シーン共通の処理*****
        StartFadeIn(_fadeInImage);  // フェードイン
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;

        ///*****シーンによってStartで行う処理が異なる*****
        //タイトルシーンのとき


        //ゲームシーンのとき

        //NavMesh止めておく・シネマシーンcart止めておく・Player操作受け付けない（UIのみ受け付ける）
        _npc1.speed = npc1Speed;
        _npc2.speed = npc2Speed;
        _npc3.speed = npc3Speed;
    }

    private void Update()
    {
        ///*****シーンによってUpdateで行う処理が異なる*****
        //タイトルシーンのとき


        //ゲームシーンのとき

        //NavMeshのSpeedを取得する
        _npc1.speed = npc1Speed;
        _npc2.speed = npc2Speed;
        _npc3.speed = npc3Speed;

    }


    /// <summary>ルールパネルを閉じるボタンで作動。カウントダウンスタート。</summary>
    public void StartCountDown()
    {
        _countDownPanel.SetActive(true);
        _rulePanel.SetActive(false);
;    }
}
