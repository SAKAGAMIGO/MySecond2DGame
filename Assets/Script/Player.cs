using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameController _gameController;

    Rigidbody2D _rb;

    /// <summary>開始位置</summary>
    private Vector2 _startPosition;

    /// <summary>引っ張れる最大距離</summary>
    public float MaxPullDistance = 1;

    /// <summary>飛ばす力</summary>
    public float FlyForce = 8;

    /// <summary>飛んだ判定</summary>
    public bool IsFly = false;

    int _dotCount = 5;

    //描画するドットの数
    private GameObject[] _dotObject = new GameObject[5];

    /// <summary>ドットPrefab</summary>
    public GameObject DotPrefab;

    /// <summary>ドットの描画間隔</summary>    
    public float DotTimeInterval = 0.05f;

    /// <summary>アニメーターを取得</summary>    
    private Animator _animator;

    ///// <summary>持っているアイテムのリスト</summary>
    //List<ItemBaceClass> _itemList = new List<ItemBaceClass>();

    void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        //RigidBodyを取得
        _rb = GetComponent<Rigidbody2D>();
        ////引っ張るときは物理演算が必要ないのでOFFにする
        _rb.isKinematic = true;
        //グラビティスケールを取得
        _rb.gravityScale = 0.5f;
        //動かせない初期位置
        _startPosition = transform.position;
        //配列の要素数分ドットのオブジェクトを表示
        for (int i = 0; i < _dotObject.Length; i++)
        {
            _dotObject[i] = Instantiate(DotPrefab);
            _dotObject[i].transform.localScale = _dotObject[i].transform.localScale * (1 - 0.06f * i);
            _dotObject[i].transform.parent = transform;
            //引っ張る前は非表示
            _dotObject[i].SetActive(false);
        }
        //PlayerMotionのアニメーションを格納
        _animator = GameObject.Find("Player Motion").GetComponent<Animator>();
        //Player MotionのTransFormを取得
        GameObject.Find("Player Motion").GetComponent<Transform>();
    }

    void Update()
    {
        //// アイテムを使う
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (_itemList.Count > 0)
        //    {
        //        // リストの先頭にあるアイテムを使って、破棄する
        //        ItemBaceClass item = _itemList[0];
        //        _itemList.RemoveAt(0);
        //        item.Activate();
        //        Destroy(item.gameObject);
        //        Debug.Log("アイテム使用");
        //    }
        //}
    }

    ///// <summary>
    ///// アイテムをアイテムリストに追加する
    ///// </summary>
    ///// <param name="item"></param>
    //public void GetItem(ItemBaceClass item)
    //{
    //    _itemList.Add(item);
    //    Debug.Log("アイテム取得");
    //}

    //マウスクリックしながら移動する関数
    public void OnMouseDrag()
    {
        _animator.SetBool("Pull", true);

        //一度飛んだら処理を行えなくする
        if (IsFly) return;

        //Mouseの位置を取得してPlayerの位置を同じにする
        Vector2 Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        

        //マウスの位置が最大距離を超えた場合最大距離の位置になるように実装
        if (Vector2.Distance(_startPosition, Position) > MaxPullDistance)
        {
            Position = (Position - _startPosition).normalized * MaxPullDistance + _startPosition;
            
        }

        //マウス位置のｘ座標が開始位置より右にあった場合開始位置のｘ座標にする
        if (Position.x > _startPosition.x)
        {
            Position.x = _startPosition.x;
        }

        transform.position = Position;

        UpdateDotObject();
    }

    //マウスを離した時の関数
    private void OnMouseUp()
    {
        
        //アニメーションのPullモーションを再生
        _animator.SetBool("Pull", false);

        //一度飛んだら処理を行えなくする
        if (IsFly) return;

        //力を加えるベクトルに飛ばす力をかける
        var Force = ((_startPosition - (Vector2)transform.position) * FlyForce);

        //物理演算をONにする
        var RigidBody2D = GetComponent<Rigidbody2D>();
        RigidBody2D.isKinematic = false;

        //AddForce関数で力を加える
        RigidBody2D.AddForce(Force, ForceMode2D.Impulse);

        //
        for (int i = 0; i < _dotObject.Length; i++)
        {
            _dotObject[i].SetActive(false);
        }

        IsFly = true;
    }

    /// <summary>
    /// 衝突イベント
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {

        //3秒後に消える
        Destroy(gameObject, 3);
    }

    private void OnDestroy()
    {
        _gameController.IsPlayerCount = true;
        _gameController.Count();
        Detonate();
    }

    public virtual void Detonate()
    {
        Debug.Log("overrideしてください");
    }

    /// <summary>ドットの表示を更新</summary>
    public void UpdateDotObject()
    {
        //飛ばす力のベクトルを算出
        var Force = ((_startPosition - (Vector2)transform.position) * FlyForce) * (1 - _rb.gravityScale + 1);
        //飛ばしてからの時間ごとのキャラクターの位置にドットを表示
        var CurrentTime = DotTimeInterval;
        for (int i = 0; i < _dotObject.Length; i++)
        {
            //アクティブにする
            _dotObject[i].SetActive(true);
            var Position = new Vector2();
            //X方向は時間に比例して右に進む
            Position.x = (transform.position.x + Force.x * CurrentTime);
            //Y方向は自由落下の計算(重力)
            Position.y = (transform.position.y + Force.y * CurrentTime) - (Physics2D.gravity.magnitude * CurrentTime * CurrentTime) / (1 + FlyForce / 10);

            _dotObject[i].transform.position = Position;
            CurrentTime += DotTimeInterval;
        }
    }

    public void AddFlyForce()
    {
        FlyForce += 5;
        Debug.Log("ステータスUP");
    }
}
