using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>NPC��NavMeshAgent�Ƃ��āA�����Ȃ�Target��ǂ�����</summary>
public class NpcController : MonoBehaviour
{
    NavMeshAgent _agent;
    [SerializeField] GameObject _target;

    // Start is called before the first frame update
    void Start()
    {
        _agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_target)_agent.destination = _target.transform.position;   //Target��ǂ�������
    }

}
