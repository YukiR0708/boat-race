using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>NPC�̓����E���ʔ���</summary>
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
        if (_target)_agent.destination = _target.transform.position;   //Target��ǂ�������
    }

    /// <summary>�`�F�b�N�|�C���g��ʂ����񐔂��v�����邽�߂̃v���p�e�B�BOrderChecker�ŏ��ʔ���Ɏg�p����</summary>
    public int CheckCount { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        //CheckPoint��ʂ����񐔂��v��
        //NPC�͋t�����Ȃ����x�ɂ��Ă���̂ŁA���̂Ƃ���t���`�F�b�N�͂��Ă��Ȃ��B�s�������Έȉ����C������B
        if (other.gameObject.CompareTag("Check") || other.gameObject.CompareTag("Goal"))
        {
            CheckCount++;
            Debug.Log($"{ this.gameObject.name}�̃`�F�b�N�|�C���g�ʉߐ���{CheckCount}");
        }

    }

}
