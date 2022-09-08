using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 潜れるようになるアイテムのコンポーネント
/// </summary>
/// 
public class Bubble : ItemBase   // ItemBase2D を継承している
{
    /// <summary>増加スコア</summary>
    [SerializeField] int _diveForce;


    public override void Activate()
    {
        FindObjectOfType<Player>().Dive(_diveForce);
    }
}
