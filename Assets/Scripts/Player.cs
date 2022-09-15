using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

/// <summary>InputSystemから入力を受け取ってプレイヤー操作を制御するクラス </summary>

[RequireComponent(typeof(Rigidbody))]

public class Player : MonoBehaviour
{
    //*****移動関連*****
    private Rigidbody _rigidbody;
    [SerializeField] private float _moveForce;
    [Tooltip("キー入力")] private Test _gameInputs;
    private Vector2 _moveInputValue;
    [SerializeField, Tooltip("FreeLookCamera")] private Camera _tpsCamera;

    //*****UI・DOTween関連*****
    [SerializeField, Tooltip("Scoreテキスト")] Text _scoreText;
    [SerializeField, Tooltip("周回数テキスト")] Text _lapText;
    [SerializeField] float _scoreChangeInterval = 0.5f; //何秒かけて変化させるか
     AudioSource _audioSource;

    //*****アイテム関連*****
    [SerializeField, Tooltip("潜る力")] private float _diveForce;
    [Tooltip("ゲットしたアイテムをItemBaseから受け取る")]
    List<ItemBase> _itemList = new List<ItemBase>();
    private int _scoreValue;

    //*****周回計算関連*****
    [Tooltip("現在のラップ数")] private int _lapcount = 0;
    [Tooltip("チェックポイントの名前リスト")] List<string> _checkPoints = new();
    [Tooltip("チェックポイントの名前が正規ルート通りに入ってる配列")] //リストリセット時の書き換え用 
    string[] _checkPoint = { "CheckPoint1", "CheckPoint2", "CheckPoint3", "Goal" };

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _gameInputs = new Test();
        _audioSource = GetComponent<AudioSource>();

        //*****InputActionの取得＆登録*****
        _gameInputs.Player.BoatMove.started += OnBoatMove;
        _gameInputs.Player.BoatMove.performed += OnBoatMove;
        _gameInputs.Player.BoatMove.canceled += OnBoatMove;
        _gameInputs.Player.UseItem.performed += OnUseItem;
        _gameInputs.Enable();
        ///*****各種デフォルト値設定*****
        _scoreText.text = "SCORE:" + _scoreValue.ToString("D8");
        _lapText.text = "LAP:1/3";
        _checkPoints = _checkPoint.ToList();

    }

    void Update()
    {
        //*****PlayerのTPS移動(cameraの前方を常に正面とする)処理*****
        //↓カメラのローカル空間のベクトルをワールド空間のベクトルへ変換
        Vector3 pForward = _tpsCamera.transform.TransformDirection(Vector3.forward);
        Vector3 pRight = _tpsCamera.transform.TransformDirection(Vector3.right);

        //↓ベクトルを加算して進行方向ベクトルを決定（y軸は無視）
        Vector3 moveDir = (_moveInputValue.x * pRight + _moveInputValue.y * pForward) * _moveForce * Time.deltaTime;
        moveDir.y = 0;
        _rigidbody.AddForce(moveDir);

        //↓補完しながら進行方向を向く
        transform.LookAt(transform.position + moveDir);
    }

    //*****InputActionに入力を渡す処理*****
    /// <summary> プレイヤー移動コールバック </summary>
    /// <param name="context"></param>
    void OnBoatMove(InputAction.CallbackContext context)
    {
        //↓BoatMoveアクションの入力を取得
        _moveInputValue = context.ReadValue<Vector2>();
    }

    /// <summary>アイテム使用コールバック</summary>
    /// <param name="context"></param>
    void OnUseItem(InputAction.CallbackContext context)
    {
        if (_itemList.Count > 0)
        {
            Debug.Log("OnUseItemが呼ばれた");
            //↓リストの先頭にあるアイテムを使って、破棄する
            ItemBase item = _itemList[0];
            _itemList.RemoveAt(0);
            item.Activate();
            Destroy(item.gameObject);
        }
    }

    //*****即時使用でないアイテムの処理*****
    /// <summary> アイテムをアイテムリストに追加する </summary>
    /// <param name="item"></param>
    public void GetItem(ItemBase item)
    {
        _itemList.Add(item);
    }

    //*****以下、即時使用アイテムの処理*****
    /// <summary> スコアを増加させるメソッド </summary>
    public void ScoreUp(int upScore)
    {
        int oldScore = _scoreValue; //追加前のスコアを保存
        _scoreValue += upScore;
        DOTween.To(() => oldScore,  //DOTweenで連続的に変化させる対象の値
               x => _scoreText.text = "SCORE:" + x.ToString("D8"),
           _scoreValue,
           _scoreChangeInterval)
           .OnUpdate(() => _audioSource.Play())
           .OnComplete(() => _scoreText.text = "SCORE:" + _scoreValue.ToString("D8"));
    }

    /// <summary> 潜るメソッド </summary>
    public void Dive(float diveForce)
    {
        _diveForce += diveForce;
        Debug.Log(_diveForce);
    }

    /// <summary> 速度を増加させるメソッド </summary>
    public void SpeedUp(float upSpeed)
    {
        _moveForce += upSpeed;
        Debug.Log(_moveForce);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == _checkPoints[0] && other.gameObject.CompareTag("Check"))
        {
            Debug.Log($"{_checkPoints[0]}を通過しました");
            _checkPoints.RemoveAt(0);
        }
        else if (other.gameObject.name == _checkPoints[0] && other.gameObject.CompareTag("Goal"))
        {
            Debug.Log($"{_checkPoints[0]}を通過しました");
            _checkPoints.RemoveAt(0);
            //↓次の周分入れ直す
            _checkPoints = _checkPoint.ToList();
            _lapcount++;
            _lapText.text = "LAP:" + _lapcount.ToString() + "/3";

        }
        else if ((_lapcount == 0) && (other.gameObject.name != _checkPoints[0]) && (other.gameObject.CompareTag("Goal")))
        {
            _lapcount++;
        }
        else if ((other.gameObject.name != _checkPoints[0]) && (other.gameObject.CompareTag("Goal") || other.gameObject.CompareTag("Check")))
        {
            Debug.Log("正規ルートに戻ってください");
        }
    }
}
