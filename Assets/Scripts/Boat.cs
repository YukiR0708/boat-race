using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>���ʌv�Z�̂��߂�Player��NPC�����ɂ���X�N���v�g</summary>
public class Boat : MonoBehaviour
{
    /// <summary>�`�F�b�N�|�C���g��ʂ����񐔂��v�����邽�߂̃v���p�e�B�BOrderChecker�ŏ��ʔ���Ɏg�p����</summary>
    public int CheckCount { get; private set; }
    /// <summary>�`�F�b�N�|�C���g��ʂ��Ă���̈ړ��ʂ��v�����邽�߂̃v���p�e�B�BOrderChecker�ŏ��ʔ���Ɏg�p����</summary>
    public float Progress { get; private set; }

    [SerializeField] List<GameObject> checkPoint;
    [Tooltip("�ۑ��p�̃`�F�b�N�|�C���g���X�g")] List<GameObject> checkPoints = new();
    [SerializeField, Tooltip("���񐔃e�L�X�g")] Text _lapText;
    [SerializeField, Tooltip("�ړ��ʃe�L�X�g")] Text _progressText;
    [Tooltip("���݂̃��b�v��")] private int _lapcount = 0;
    [Tooltip("�ړ��ʂ��v������ہA��Ƃ���`�F�b�N�|�C���g")] GameObject _currentCheckPoint;
    [Tooltip("�ړ��ʂ��v������ہA���ɒʂ�`�F�b�N�|�C���g")] GameObject _nextCheckPoint;
    [SerializeField, Header("�R���C�_�[�ƍ��W�̍����⊮�̂��߂̃I�u�W�F�N�g")] GameObject _boatPos;

    private void Start()
    {
        checkPoints.AddRange(checkPoint);
        if (this.gameObject.name == "Player") _lapText.text = $"LAP:1/3";
        _currentCheckPoint = checkPoint[0];
        _nextCheckPoint = checkPoint[1];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == checkPoint[0] && (other.gameObject.CompareTag("Check") || (other.gameObject.CompareTag("Goal"))))
        {
            CheckCount++;
            Debug.Log($"{this.gameObject}��{checkPoint[0]}��ʉ߂��܂���");
            //���O�̃`�F�b�N�|�C���g����̈ړ��ʂ����Z�b�g���A���ʂ����̂Ǝ��̃`�F�b�N�|�C���g��ۑ�
            //Progress = 0f;
            _currentCheckPoint = checkPoint[0];
            checkPoint.RemoveAt(0);
            //��1��������
            if (other.gameObject.CompareTag("Goal"))
            {
                //�����̎������꒼��
                checkPoint.AddRange(checkPoints);
                if (this.gameObject.name == "Player")
                {
                    _lapcount++;
                    _lapText.text = $"LAP:{_lapcount.ToString()}/3";
                }

            }
            _nextCheckPoint = checkPoint[0];

        }
        else if ((this.gameObject.name == "Player") && (_lapcount == 0) && (other.gameObject != checkPoint[0]) && (other.gameObject.CompareTag("Goal")))
        {
            //���X�^�[�g�̂Ƃ��A�S�[����ʂ����^�C�~���O��Lap������������@�\�L�͍ŏ�����1/3��
            _lapcount++;
        }
        else if ((this.gameObject.name == "Player") && (other.gameObject != checkPoint[0]) && (other.gameObject.CompareTag("Goal") || other.gameObject.CompareTag("Check")))
        {
            Debug.Log("���K���[�g�ɖ߂��Ă�������");
        }
    }

    private void Update()
    {
        Progress = GetProgress(_currentCheckPoint, _nextCheckPoint);
    }

    private float GetProgress(GameObject cCheck, GameObject nCheck)
    {
        //���ړ��ʂ����߂�ۊ�Ƃ���P�ʃx�N�g��
        Vector3 uniVec = (nCheck.transform.position - cCheck.transform.position).normalized;
        
        //�����݂�Player�ENPC�́A�`�F�b�N�|�C���g��ʂ��Ă���ړ������x�N�g��
        Vector3 vec = _boatPos.transform.position - cCheck.transform.position;

        // �����ς���ړ��ʂ����߂�
        return Vector3.Dot(uniVec, vec);
    }
}
