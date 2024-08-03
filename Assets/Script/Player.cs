using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>GameControllerスクリプト</summary>
    GameController _gameController;

    /// <summary>Rigidbody2Dを取得</summary>
    Rigidbody2D _rb;

    /// <summary>開始位置</summary>
    private Vector2 _startPosition;

    /// <summary>引っ張れる最大距離</summary>
    private float MaxPullDistance = 1;

    /// <summary>飛ばす力</summary>
    [SerializeField] static float FlyForce = 8;

    /// <summary>飛んだ判定</summary>
    public bool IsFly = false;

    int _dotCount = 5;

    //描画するドットの数
    private GameObject[] _dotObject = new GameObject[5];

    /// <summary>ドットPrefab</summary>
    public GameObject DotPrefab;

    /// <summary>ドットの描画間隔</summary>    
    private float _dotTimeInterval = 0.05f;

    /// <summary>アニメーターを取得</summary>    
    private Animator _animator;

    Muzzl _muzzle;
    //撃ったときのエフェクト真偽
    bool _isShoot = false;

    PlayerHealth _playerHealth;


    void Start()
    {
        _muzzle = GameObject.FindAnyObjectByType<Muzzl>();
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        //RigidBodyを取得
        _rb = GetComponent<Rigidbody2D>();
        ////引っ張るときは物理演算が必要ないのでOFFにする
        _rb.isKinematic = true;
        //グラビティスケールを０にする
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
        _animator = GameObject.Find("Player_ShootStanding").GetComponent<Animator>();
        //Player MotionのTransFormを取得
        GameObject.Find("Player_ShootStanding").GetComponent<Transform>();
        _playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    //マウスクリックしながら移動する関数
    public void OnMouseDrag()
    {
        //Animationを再生
        _animator.SetBool("Standing", true);

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
    public void OnMouseUp()
    {

        //アニメーションを再生
        _animator.SetBool("Shoot", true);
        _animator.SetBool("Standing", false);

        //一度飛んだら処理を行えなくする
        if (IsFly) return;

        //力を加えるベクトルに飛ばす力をかける
        var Force = ((_startPosition - (Vector2)transform.position) * FlyForce);

        //物理演算をONにする
        var RigidBody2D = GetComponent<Rigidbody2D>();
        RigidBody2D.isKinematic = false;

        //AddForce関数で力を加える
        RigidBody2D.AddForce(Force, ForceMode2D.Impulse);

        //ドットオブジェクトをLengthの数分表示させる
        for (int i = 0; i < _dotObject.Length; i++)
        {
            _dotObject[i].SetActive(false);
        }

        IsFly = true;

        _isShoot = true;
    }

    public void Shoot()
    {
        if (_isShoot == true)
        {
            _muzzle.ToShoot();
            _isShoot = false;
        }
    }

    private void Update()
    {
        Shoot();
    }

    /// <summary>
    /// 衝突イベント
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //3秒後に消える
        Destroy(gameObject, 3);
    }

    public virtual void OnDestroy()
    {
        _animator.SetBool("Shoot", false);
        _gameController.IsPlayerCount = true;
        Detonate();
        _playerHealth.AddDamage(10);
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
        var CurrentTime = _dotTimeInterval;
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
            CurrentTime += _dotTimeInterval;
        }
    }

    public void AddFlyForce()
    {
        FlyForce += 3;
        Debug.Log("ステータスUP");
    }
}
