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
    GameManager _gm;

    static bool _rkPanelActive = false;
    static bool _helpPanelActive = false;
    string _buttonName = "";

    void Start()
    {
        _gm = new GameManager();
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
            //  2�l�p�D�I����ʂցiSceneManager���������炻�̃��\�b�h�Ăԁj

        }
        else if (_buttonName == "StartButton")
        {
            //  �V���O���v���C��ʂ�
            _gm.StartFadeOut("SinglePlay");

        }
        else if (_buttonName == "RankButton" || _buttonName == "CloseRank")
        {
            //RankingPanel�̕\���E��\���؂�ւ�
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
        else if (_buttonName == "HelpButton" || _buttonName == "CloseHelp")
        {
            //RankingPanel�̕\���E��\���؂�ւ�
            if (_helpPanelActive == false)
            {
                _helpPanelActive = true;
                Debug.Log("�\��");
                Debug.Log(_helpPanelActive);
            }
            else if (_helpPanelActive == true)
            {
                _helpPanelActive = false;
                Debug.Log("��\��");
                Debug.Log(_helpPanelActive);
            }

            _helpPanel.SetActive(_helpPanelActive);

        }

        _buttonName = "";
    }
}
