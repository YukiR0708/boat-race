using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary> 順位判定をするためのクラス </summary>
public class OrderChecker : MonoBehaviour
{
    [SerializeField, Header("PlayerとNPCのリスト")] List<Boat> boatList = new();
    [Tooltip("最終的な順位順のリスト")] public List<string> lastOrder = new();
    [SerializeField, Header("プレイヤー・NPCの合計数")] int _boatSum;
    [SerializeField, Header("結果発表パネル")] GameObject _resultPanel;
    [SerializeField] Text _first, _second, _third, _fourth, _pScore;
    [SerializeField] Player _player;
    [SerializeField] GameManager _gm;
    [SerializeField] GameObject _btGoTitle;


    private void LateUpdate()
    {
        //↓リストを現在の順位順に並び替える
        var order = boatList.OrderByDescending(x => x.CheckCount).ThenByDescending(x => x.Progress);
        int rank = 0;

        foreach (var boat in order)
        {
            boat.SetRank(rank++);
        }

        //↓Playerがゴールしたら移動入力を止める  
        if (lastOrder.Contains("Player"))
        {
            Player.gameInputs.Player.BoatMove.Disable();
            _player.canPlayerMove = false;
            _gm.state = GameManager.GameStatus.PlayerGole;
        }
        //↓すべてのPlayer・NPCがゴールしたら
        if (lastOrder.Count == _boatSum)
        {
            _gm.state = GameManager.GameStatus.Finish;
            //結果を表示する
            _first.text = $"1st {lastOrder[0]}";
            _second.text = $"2nd {lastOrder[1]}";
            _third.text = $"3rd {lastOrder[2]}";
            _fourth.text = $"4th {lastOrder[3]}";
            _pScore.text = $"Player's Score : {_player.ScoreValue.ToString("D8")}";
            //初期選択ボタンの初期化
            EventSystem.current.SetSelectedGameObject(null);
            //初期選択ボタンの再指定
            EventSystem.current.SetSelectedGameObject(_btGoTitle);

            _resultPanel.SetActive(true);
            
        }
    }
}
