using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>　スクロール速度を上げるコンポーネント　</summary>
/// 
public class SpeedUp : ItemBase   // ItemBase2D を継承している
{
    /// <summary>増加スピード</summary>
    [SerializeField] int _upSpeed = 400;

    public override void Activate()
    {
        FindObjectOfType<Player>().SpeedUp(_upSpeed);
    }
}
