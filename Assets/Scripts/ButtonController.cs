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
    [SerializeField, Header("�{�^���N���b�N����SE")] AudioClip _clickedSE;
    [Header("�t�F�[�h�A�E�g�p�C���[�W"), SerializeField] Image _fadeOutImage;


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
            //  2�l�p��ʂցiSceneManager���������炻�̃��\�b�h�Ăԁj

        }
        else if (_buttonName == "StartButton")
        {
            _as.PlayOneShot(_clickedSE);
            //  �V���O���v���C��ʂ�
            _gm.StartFadeOut(_fadeOutImage, "SinglePlay");

        }
        else if (_buttonName == "RankButton" || _buttonName == "CloseRank")
        {
            //RankingPanel�̕\���E��\���؂�ւ�
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
            //RankingPanel�̕\���E��\���؂�ւ�
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

    /// <summary>�Q�[���p�b�h���쎞�ɁA�ŏ��ɑI�����Ă����{�^����؂�ւ��鏈��</summary>
    /// <param name="clickedButton"></param>
    private void ChangeSelectButton(GameObject clickedButton)
    {
        //�����I���{�^���̏�����
        EventSystem.current.SetSelectedGameObject(null);
        //�����I���{�^���̍Ďw��
        EventSystem.current.SetSelectedGameObject(clickedButton);

    }
}
