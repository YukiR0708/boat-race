using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ���ʔ�������邽�߂̃N���X </summary>
public class OrderChecker : MonoBehaviour
{
    [SerializeField] GameObject _npc1, _npc2, _npc3, _player;
    NpcController _npc1Con, _npc2Con, _npc3Con;
    Player _playerCon;
    [Tooltip("�eNPC�̃`�F�b�N�|�C���g�ʉߐ�")] int _count1, _count2, _count3, _countPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _npc1Con = _npc1.GetComponent<NpcController>();
        _npc2Con = _npc2.GetComponent<NpcController>();
        _npc3Con = _npc3.GetComponent<NpcController>();
        _playerCon = _player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //�eNPC�̃`�F�b�N�|�C���g�ʉߐ����m�F
        _count1 = _npc1Con.CheckCount;
        _count2 = _npc2Con.CheckCount;
        _count3 = _npc3Con.CheckCount;
        _countPlayer = _playerCon.PlayerCheckCount;
    }
}
