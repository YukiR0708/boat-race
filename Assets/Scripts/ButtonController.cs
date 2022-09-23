using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject _firstPanel;
    [SerializeField] GameObject _btSinglePlayer;
    [SerializeField] GameObject _btTwoPlayer;
    [SerializeField] GameObject _btHelp;
    [SerializeField] GameObject _btCloseHelp;

    [SerializeField] GameObject _secondPanel;
    [SerializeField] GameObject _btSingleStart;
    [SerializeField] GameObject _btRanking;
    [SerializeField] GameObject _btCloseRanking;
    [SerializeField] GameObject _btPageBack;
    [SerializeField] GameObject _rankingPanel;
    [SerializeField] GameObject _helpPanel;
    [SerializeField] AudioSource _as;
    GameManager _gm;
    [SerializeField, Header("ボタンクリック時のSE")] AudioClip _clickedSE;
    [Header("フェードアウト用イメージ"), SerializeField] Image _fadeOutImage;


    static bool _rkPanelActive = false;
    static bool _helpPanelActive = false;
    string _buttonName = "";

    void Start()
    {
        _gm = gameObject.AddComponent<GameManager>();
        var button = GetComponent<Button>();
        button.onClick.AddListener(() => _buttonName = this.gameObject.name);

    }

    private void Update()
    {
        if (_buttonName == "SinglePlayerButton")
        {
            
            _as.PlayOneShot(_clickedSE);
            ChangeSelectButton(_btSingleStart);
            _firstPanel.SetActive(false);
            _secondPanel.SetActive(true);

        }
        else if (_buttonName == "PageBackButton")
        {
            _as.PlayOneShot(_clickedSE);
            ChangeSelectButton(_btSinglePlayer);
            _secondPanel.SetActive(false);
            _firstPanel.SetActive(true);
        }
        else if (_buttonName == "TwoPlayerButton")
        {
            //  2人用画面へ（SceneManager完成したらそのメソッド呼ぶ）

        }
        else if (_buttonName == "StartButton")
        {
            _as.PlayOneShot(_clickedSE);
            //  シングルプレイ画面へ
            _gm.StartFadeOut(_fadeOutImage, "SinglePlay");

        }
        else if (_buttonName == "RankButton" || _buttonName == "CloseRank")
        {
            //RankingPanelの表示・非表示切り替え
            if (_rkPanelActive == false)
            {
                ChangeSelectButton(_btCloseRanking);
                _rkPanelActive = true;
            }
            else if (_rkPanelActive == true)
            {
                ChangeSelectButton(_btRanking);
                _rkPanelActive = false;
            }
            _as.PlayOneShot(_clickedSE);
            _rankingPanel.SetActive(_rkPanelActive);

        }
        else if (_buttonName == "HelpButton" || _buttonName == "CloseHelp")
        {
            //RankingPanelの表示・非表示切り替え
            if (_helpPanelActive == false)
            {
                ChangeSelectButton(_btCloseHelp);
                _helpPanelActive = true;
            }
            else if (_helpPanelActive == true)
            {
                ChangeSelectButton(_btHelp);
                _helpPanelActive = false;
            }

            _as.PlayOneShot(_clickedSE);
            _helpPanel.SetActive(_helpPanelActive);
        }

        _buttonName = "";
    }

    /// <summary>ゲームパッド操作時に、最初に選択しておくボタンを切り替える処理</summary>
    /// <param name="clickedButton"></param>
    private void ChangeSelectButton(GameObject clickedButton)
    {
        //初期選択ボタンの初期化
        EventSystem.current.SetSelectedGameObject(null);
        //初期選択ボタンの再指定
        EventSystem.current.SetSelectedGameObject(clickedButton);

    }
}
