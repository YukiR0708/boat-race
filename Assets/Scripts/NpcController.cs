using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>NPCの動き・順位判定</summary>
public class NpcController : MonoBehaviour
{
    NavMeshAgent _agent;
    [SerializeField] GameObject _target;
    
    void Start()
    {
        _agent = gameObject.GetComponent<NavMeshAgent>();
    }

   
    void Update()
    {
        if (_target)_agent.destination = _target.transform.position;   //Targetを追いかける
    }

    /// <summary>チェックポイントを通った回数を計測するためのプロパティ。OrderCheckerで順位判定に使用する</summary>
    public int CheckCount { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        //CheckPointを通った回数を計測
        //NPCは逆走しない速度にしているので、今のところ逆走チェックはしていない。不具合があれば以下を修正する。
        if (other.gameObject.CompareTag("Check") || other.gameObject.CompareTag("Goal"))
        {
            CheckCount++;
            Debug.Log($"{ this.gameObject.name}のチェックポイント通過数は{CheckCount}");
        }

    }

}
