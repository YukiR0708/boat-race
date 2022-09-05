using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject _btSinglePlayer;
    [SerializeField] GameObject _btTwoPlayer;
    [SerializeField] GameObject _btHelp;

    [SerializeField] GameObject _btSingleStart;
    [SerializeField] GameObject _btRanking;
    [SerializeField] GameObject _btPageBack;
    [SerializeField] GameObject _rankingPanel;
    [SerializeField] GameObject _helpPanel;

    bool _rkPanelActive = false;
    bool _helpPanelActive = false;
    string _buttonName = "";

    void Awake()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(() => _buttonName = this.gameObject.name);
    }

    private void Update()
    {
        if (_buttonName == "SinglePlayerButton")
        {
            _btSinglePlayer.SetActive(false);
            _btTwoPlayer.SetActive(false);
            _btHelp.SetActive(false);
            _btSingleStart.SetActive(true);
            _btRanking.SetActive(true);
            _btPageBack.SetActive(true);
        }
        else if (_buttonName == "PageBackButton")
        {
            _btSinglePlayer.SetActive(true);
            _btTwoPlayer.SetActive(true);
            _btHelp.SetActive(true);
            _btSingleStart.SetActive(false);
            _btRanking.SetActive(false);
            _btPageBack.SetActive(false);
        }
        else if (_buttonName == "TwoPlayerButton")
        {
            //  2人用船選択画面へ（SceneManager完成したらそのメソッド呼ぶ）

        }
        else if (_buttonName == "StartButton")
        {
            //  1人用船選択画面へ（SceneManager完成したらそのメソッド呼ぶ）

        }
        else if (_buttonName == "RankButton")
        {
            //RankingPanelの表示・非表示切り替え
            if (_rkPanelActive == false)
            {
                _rkPanelActive = true;
            }
            else if (_rkPanelActive == true)
            {
                _rkPanelActive = false;
            }

            _rankingPanel.SetActive(_rkPanelActive);

        }
        else if (_buttonName == "HelpButton")
        {
            //RankingPanelの表示・非表示切り替え
            if (_helpPanelActive == false)
            {
                _helpPanelActive = true;
            }
            else if (_helpPanelActive == true)
            {
                _helpPanelActive = false;
            }

            _helpPanel.SetActive(_helpPanelActive);

        }
    }
}
