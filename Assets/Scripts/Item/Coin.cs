using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> �X�R�A���グ��R���|�[�l���g </summary>

public class Coin : ItemBase   // ItemBase2D ���p�����Ă���
{
    /// <summary>�����X�R�A</summary>
    [SerializeField] int _upScore;
    public override void Activate()

    {
        FindObjectOfType<Player>().ScoreUp(_upScore);
    }
}
