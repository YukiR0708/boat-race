using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>順位計算のためにPlayerとNPC両方につけるスクリプト</summary>
public class Boat : MonoBehaviour
{
    /// <summary>チェックポイントを通った回数を計測するためのプロパティ。OrderCheckerで順位判定に使用する</summary>
    public int CheckCount { get; private set; }
    /// <summary>チェックポイントを通ってからの移動量を計測するためのプロパティ。OrderCheckerで順位判定に使用する</summary>
    public float Progress { get; private set; }

    [SerializeField] List<GameObject> checkPoint;
    [Tooltip("保存用のチェックポイントリスト")] List<GameObject> checkPoints = new();
    [SerializeField, Tooltip("周回数テキスト")] Text _lapText;
    [SerializeField, Tooltip("移動量テキスト")] Text _progressText;
    [Tooltip("現在のラップ数")] private int _lapcount = 0;
    [Tooltip("移動量を計測する際、基準とするチェックポイント")] GameObject _currentCheckPoint;
    [Tooltip("移動量を計測する際、次に通るチェックポイント")] GameObject _nextCheckPoint;
    [SerializeField, Header("コライダーと座標の差分補完のためのオブジェクト")] GameObject _boatPos;

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
            Debug.Log($"{this.gameObject}が{checkPoint[0]}を通過しました");
            //↓前のチェックポイントからの移動量をリセットし、今通ったのと次のチェックポイントを保存
            //Progress = 0f;
            _currentCheckPoint = checkPoint[0];
            checkPoint.RemoveAt(0);
            //↓1周したら
            if (other.gameObject.CompareTag("Goal"))
            {
                //↓次の周分入れ直す
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
            //↓スタートのとき、ゴールを通ったタイミングでLap数をかぞえる　表記は最初から1/3周
            _lapcount++;
        }
        else if ((this.gameObject.name == "Player") && (other.gameObject != checkPoint[0]) && (other.gameObject.CompareTag("Goal") || other.gameObject.CompareTag("Check")))
        {
            Debug.Log("正規ルートに戻ってください");
        }
    }

    private void Update()
    {
        Progress = GetProgress(_currentCheckPoint, _nextCheckPoint);
    }

    private float GetProgress(GameObject cCheck, GameObject nCheck)
    {
        //↓移動量を求める際基準とする単位ベクトル
        Vector3 uniVec = (nCheck.transform.position - cCheck.transform.position).normalized;
        
        //↓現在のPlayer・NPCの、チェックポイントを通ってから移動したベクトル
        Vector3 vec = _boatPos.transform.position - cCheck.transform.position;

        // ↓内積から移動量を求める
        return Vector3.Dot(uniVec, vec);
    }
}
