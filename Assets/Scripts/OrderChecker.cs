using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary> 順位判定をするためのクラス </summary>
public class OrderChecker : MonoBehaviour
{
     [SerializeField,Header("PlayerとNPCのリスト")] List<Boat> boatList;


    private void LateUpdate()
    {
        var order = boatList.OrderByDescending(x => x.CheckCount).ThenByDescending(x => x.Progress);
        int rank = 0;

        foreach (var boat in order)
        {
            boat.SetRank(rank++);
        }
    }
}
