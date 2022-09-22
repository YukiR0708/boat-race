using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

/// <summary> ���ʔ�������邽�߂̃N���X </summary>
public class OrderChecker : MonoBehaviour
{
    [SerializeField, Header("Player��NPC�̃��X�g")] List<Boat> boatList = new();
    [Tooltip("�ŏI�I�ȏ��ʏ��̃��X�g")] public List<Boat> lastOrder = new();
    [SerializeField, Header("�v���C���[�ENPC�̍��v��")] int _boatSum;
    [SerializeField, Header("���ʔ��\�p�l��")] GameObject _resultPanel;
    [SerializeField] Text _first, _second, _third, _fourth, _pScore;
    [SerializeField] Player _player;

    private void LateUpdate()
    {
        //�����X�g�����݂̏��ʏ��ɕ��ёւ���
        var order = boatList.OrderByDescending(x => x.CheckCount).ThenByDescending(x => x.Progress);
        int rank = 0;

        foreach (var boat in order)
        {
            boat.SetRank(rank++);
        }

        //�����ׂĂ�Player�ENPC���S�[��������
        if(lastOrder.Count == _boatSum)
        {
            //���ʂ�\������
            _first.text = $"1st  {lastOrder[0].name}";
            _second.text = $"2nd  {lastOrder[1].name}";
            _third.text = $"3rd  {lastOrder[2].name}";
            _fourth.text = $"4th  {lastOrder[3].name}";
            _pScore.text = $"Player's Score : {_player.ScoreValue.ToString("D8")}";
            _resultPanel.SetActive(true);
        }
    }
}