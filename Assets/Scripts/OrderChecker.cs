using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary> ���ʔ�������邽�߂̃N���X </summary>
public class OrderChecker : MonoBehaviour
{
    [SerializeField, Header("Player��NPC�̃��X�g")] List<Boat> boatList = new();
    [Tooltip("�ŏI�I�ȏ��ʏ��̃��X�g")] public List<string> lastOrder = new();
    [SerializeField, Header("�v���C���[�ENPC�̍��v��")] int _boatSum;
    [SerializeField, Header("���ʔ��\�p�l��")] GameObject _resultPanel;
    [SerializeField] Text _first, _second, _third, _fourth, _pScore;
    [SerializeField] Player _player;
    [SerializeField] GameManager _gm;
    [SerializeField] GameObject _btGoTitle;


    private void LateUpdate()
    {
        //�����X�g�����݂̏��ʏ��ɕ��ёւ���
        var order = boatList.OrderByDescending(x => x.CheckCount).ThenByDescending(x => x.Progress);
        int rank = 0;

        foreach (var boat in order)
        {
            boat.SetRank(rank++);
        }

        //��Player���S�[��������ړ����͂��~�߂�  
        if (lastOrder.Contains("Player"))
        {
            Player.gameInputs.Player.BoatMove.Disable();
            _player.canPlayerMove = false;
            _gm.state = GameManager.GameStatus.PlayerGole;
        }
        //�����ׂĂ�Player�ENPC���S�[��������
        if (lastOrder.Count == _boatSum)
        {
            _gm.state = GameManager.GameStatus.Finish;
            //���ʂ�\������
            _first.text = $"1st {lastOrder[0]}";
            _second.text = $"2nd {lastOrder[1]}";
            _third.text = $"3rd {lastOrder[2]}";
            _fourth.text = $"4th {lastOrder[3]}";
            _pScore.text = $"Player's Score : {_player.ScoreValue.ToString("D8")}";
            //�����I���{�^���̏�����
            EventSystem.current.SetSelectedGameObject(null);
            //�����I���{�^���̍Ďw��
            EventSystem.current.SetSelectedGameObject(_btGoTitle);

            _resultPanel.SetActive(true);
            
        }
    }
}
