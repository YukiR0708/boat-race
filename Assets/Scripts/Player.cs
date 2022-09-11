using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>InputSystemから入力を受け取ってプレイヤー操作を制御するクラス </summary>

[RequireComponent(typeof(Rigidbody))]

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveForce;
    private Rigidbody _rigidbody;
    [Tooltip("キー入力")] private Test _gameInputs;
    private Vector2 _moveInputValue;
    private int _scoreValue;
    [SerializeField, Tooltip("FreeLookCamera")] private Camera _tpsCamera;
    [SerializeField, Tooltip("潜る力")] private float _diveForce;
    [Tooltip("ゲットしたアイテムをItemBaseから受け取る")]
    List<ItemBase> _itemList = new List<ItemBase>();
    [SerializeField, Tooltip("Scoreテキスト")] Text _scoreText;
    [SerializeField, Tooltip("周回数テキスト")] Text _lapText;
    [Tooltip("現在のラップ数")] private int _lapcount;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _gameInputs = new Test();

        //*****InputActionの取得＆登録*****
        _gameInputs.Player.BoatMove.started += OnBoatMove;
        _gameInputs.Player.BoatMove.performed += OnBoatMove;
        _gameInputs.Player.BoatMove.canceled += OnBoatMove;
        _gameInputs.Player.UseItem.performed += OnUseItem;

        _gameInputs.Enable();
    }

    private void Start()
    {
        _scoreText.GetComponent<Text>().text = "SCORE:" + _scoreValue.ToString("D8");
        _lapText.GetComponent<Text>().text = "LAP:" + _lapcount.ToString() + "/3";
    }

    void Update()
    {
        //*****PlayerのTPS移動(cameraの前方を常に正面とする)処理*****
        //↓カメラのローカル空間のベクトルをワールド空間のベクトルへ変換
        Vector3 pForward = _tpsCamera.transform.TransformDirection(Vector3.forward);
        Vector3 pRight = _tpsCamera.transform.TransformDirection(Vector3.right);

        //↓ベクトルを加算して進行方向ベクトルを決定（y軸は無視）
        Vector3 moveDir = (_moveInputValue.x * pRight + _moveInputValue.y * pForward) * _moveForce;
        moveDir.y = 0;
        _rigidbody.AddForce(moveDir);

        //↓補完しながら進行方向を向く
        transform.LookAt(transform.position + moveDir);
    }

    //*****InputActionに入力を渡す処理*****
    /// <summary> InputActionに、プレイヤーの移動を渡すメソッド </summary>
    /// <param name="context"></param>
    void OnBoatMove(InputAction.CallbackContext context)
    {
        //↓BoatMoveアクションの入力を取得
        _moveInputValue = context.ReadValue<Vector2>();
    }

    /// <summary> InputActionに、アイテムの使用を渡すメソッド </summary>
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
        _scoreValue += upScore;
        _scoreText.GetComponent<Text>().text = "SCORE:" + _scoreValue.ToString("D8");
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
}
