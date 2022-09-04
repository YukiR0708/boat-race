using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void SinglePlayButtonClicked()
    {
        _btSinglePlayer.SetActive(false);
        _btTwoPlayer.SetActive(false);
        _btHelp.SetActive(false);
        _btSingleStart.SetActive(true);
        _btRanking.SetActive(true);
        _btPageBack.SetActive(true);
    }

    public void PageBackButtonClicked()
    {
        _btSinglePlayer.SetActive(true);
        _btTwoPlayer.SetActive(true);
        _btHelp.SetActive(true);
        _btSingleStart.SetActive(false);
        _btRanking.SetActive(false);
        _btPageBack.SetActive(false);
    }


    public void TwoPlayerButtonClicked()
    {
        //  2�l�p�D�I����ʂցiSceneManager���������炻�̃��\�b�h�Ăԁj
    }

    public void SignleStartButtonClicked()
    {
        //  1�l�p�D�I����ʂցiSceneManager���������炻�̃��\�b�h�Ăԁj
    }

    public void RankingButtonClicked()
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

    public void HelpButtonClicked()
    {
        //RankingPanel�̕\���E��\���؂�ւ�
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
