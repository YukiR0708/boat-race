using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> スコアを上げるコンポーネント </summary>

public class Coin : ItemBase   // ItemBase2D を継承している
{
    /// <summary>増加スコア</summary>
    [SerializeField] int _upScore;
    public override void Activate()

    {
        FindObjectOfType<Player>().ScoreUp(_upScore);
    }
}
