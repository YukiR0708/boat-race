using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����悤�ɂȂ�A�C�e���̃R���|�[�l���g
/// </summary>
/// 
public class Bubble : ItemBase   // ItemBase2D ���p�����Ă���
{
    /// <summary>�����X�R�A</summary>
    [SerializeField] int _diveForce;


    public override void Activate()
    {
        FindObjectOfType<Player>().Dive(_diveForce);
    }
}
