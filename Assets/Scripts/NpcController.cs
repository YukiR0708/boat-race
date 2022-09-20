using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>NPC‚Ì“®‚«</summary>
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
        if (_target)_agent.destination = _target.transform.position;   //Target‚ð’Ç‚¢‚©‚¯‚é
    }

}
