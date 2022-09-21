using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary> ���ʔ�������邽�߂̃N���X </summary>
public class OrderChecker : MonoBehaviour
{
     [SerializeField,Header("Player��NPC�̃��X�g")] List<Boat> boatList;


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
