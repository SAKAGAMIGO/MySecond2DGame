using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>GameController�X�N���v�g</summary>
    GameController _gameController;

    /// <summary>Rigidbody2D���擾</summary>
    Rigidbody2D _rb;

    /// <summary>�J�n�ʒu</summary>
    private Vector2 _startPosition;

    /// <summary>���������ő勗��</summary>
    private float MaxPullDistance = 1;

    /// <summary>��΂���</summary>
    [SerializeField] static float FlyForce = 8;

    /// <summary>��񂾔���</summary>
    public bool IsFly = false;

    //private GameObject[] _dotObject = new GameObject[5];

    //�`�悷��h�b�g�̐�
    [SerializeField] List<GameObject> _dotObj = new List<GameObject>();
    /// <summary>�h�b�gPrefab</summary>
    public GameObject DotPrefab;
    /// <summary>�h�b�g�̕`��Ԋu</summary>    
    private float _dotTimeInterval = 0.05f;

    /// <summary>�A�j���[�^�[���擾</summary>    
    private Animator _animator;

    Muzzl _muzzle;

    //�������Ƃ��̃G�t�F�N�g�^�U
    bool _isShoot = false;

    PlayerHealth _playerHealth;

    Muzzl m_chergeEffect;

    private bool _isSound = false;

    void Start()
    {
        _muzzle = GameObject.FindAnyObjectByType<Muzzl>();
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        //RigidBody���擾
        _rb = GetComponent<Rigidbody2D>();
        ////��������Ƃ��͕������Z���K�v�Ȃ��̂�OFF�ɂ���
        _rb.isKinematic = true;
        //�O���r�e�B�X�P�[�����O�ɂ���
        _rb.gravityScale = 0.5f;
        //�������Ȃ������ʒu
        _startPosition = transform.position;
        //�z��̗v�f�����h�b�g�̃I�u�W�F�N�g��\��
        for (int i = 0; i < _dotObj.Count; i++)
        {
            _dotObj[i] = Instantiate(DotPrefab);
            _dotObj[i].transform.localScale = _dotObj[i].transform.localScale * (1 - 0.06f * i);
            _dotObj[i].transform.parent = transform;
            //��������Ȃ��Ƃ��͔�\��
            _dotObj[i].SetActive(false);
        }

        //PlayerMotion�̃A�j���[�V�������i�[
        _animator = GameObject.Find("Man_Gun").GetComponent<Animator>();

        //Player Motion��TransForm���擾
        GameObject.Find("Man_Gun").GetComponent<Transform>();


        //PlayerHealth���i�[
        //_playerHealth = GameObject.Find("Player_ShootStanding").GetComponent<PlayerHealth>(); 

        m_chergeEffect = GameObject.FindAnyObjectByType<Muzzl>();
    }

    //�}�E�X�N���b�N���Ȃ���ړ�����֐�
    public void OnMouseDrag()
    {
        if (!_isSound)
        {
            SoundManager.Instance.PlaySE(SESoundData.SE.Set);
            _isSound = true;
        }

        //Animation���Đ�
        _animator.SetBool("Set", true);

        //��x��񂾂珈�����s���Ȃ�����
        if (IsFly)
        {
            return;
        }

        //Mouse�̈ʒu���擾����Player�̈ʒu�𓯂��ɂ���
        Vector2 Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //�}�E�X�̈ʒu���ő勗���𒴂����ꍇ�ő勗���̈ʒu�ɂȂ�悤�Ɏ���
        if (Vector2.Distance(_startPosition, Position) > MaxPullDistance)
        {
            Position = (Position - _startPosition).normalized * MaxPullDistance + _startPosition;

        }

        //�}�E�X�ʒu�̂����W���J�n�ʒu���E�ɂ������ꍇ�J�n�ʒu�̂����W�ɂ���
        if (Position.x > _startPosition.x)
        {
            Position.x = _startPosition.x;
        }

        transform.position = Position;

        UpdateDotObject();
    }

    //�}�E�X�𗣂������̊֐�
    public void OnMouseUp()
    {
        SoundManager.Instance.PlaySE(SESoundData.SE.Shoot);
        _isSound = false;

        //�A�j���[�V�������Đ�    
        _animator.SetBool("Shoot", true);
        _animator.SetBool("Set", false);

        //��x��񂾂珈�����s���Ȃ�����
        if (IsFly) return;

        //�͂�������x�N�g���ɔ�΂��͂�������
        var Force = ((_startPosition - (Vector2)transform.position) * FlyForce);

        //�������Z��ON�ɂ���
        var RigidBody2D = GetComponent<Rigidbody2D>();
        RigidBody2D.isKinematic = false;

        //AddForce�֐��ŗ͂�������
        RigidBody2D.AddForce(Force, ForceMode2D.Impulse);

        //�h�b�g�I�u�W�F�N�g��Length�̐����\��������
        for (int i = 0; i < _dotObj.Count; i++)
        {
            _dotObj[i].SetActive(false);
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
    /// �Փ˃C�x���g
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //3�b��ɏ�����
        Destroy(gameObject, 3);
        _animator.SetBool("Shoot", false);
    }

    public virtual void OnDestroy()
    {
        _gameController._isPlayerCount = true;
        Detonate();


        //_playerHealth.AddDamage(10);

        m_chergeEffect._flag = true;


    }



    public virtual void Detonate()
    {
        Debug.Log("override���Ă�������");
    }

    /// <summary>�h�b�g�̕\�����X�V</summary>
    public void UpdateDotObject()
    {
        //��΂��͂̃x�N�g�����Z�o
        var Force = ((_startPosition - (Vector2)transform.position) * FlyForce) * (1 - _rb.gravityScale + 1);
        //��΂��Ă���̎��Ԃ��Ƃ̃L�����N�^�[�̈ʒu�Ƀh�b�g��\��
        var CurrentTime = _dotTimeInterval;
        for (int i = 0; i < _dotObj.Count; i++)
        {
            //�A�N�e�B�u�ɂ���
            _dotObj[i].SetActive(true);
            var Position = new Vector2();
            //X�����͎��Ԃɔ�Ⴕ�ĉE�ɐi��
            Position.x = (transform.position.x + Force.x * CurrentTime);
            //Y�����͎��R�����̌v�Z(�d��)
            Position.y = (transform.position.y + Force.y * CurrentTime) - (Physics2D.gravity.magnitude * CurrentTime * CurrentTime) / (1 + FlyForce / 10);

            _dotObj[i].transform.position = Position;
            CurrentTime += _dotTimeInterval;
        }
    }

    public void AddFlyForce()
    {
        FlyForce += 3;
        Debug.Log("�X�e�[�^�XUP");
    }

    public void AddDotLength()
    {
        _dotObj.Add(DotPrefab);
        _dotObj.Add(DotPrefab);
        _dotObj.Add(DotPrefab);
        _dotObj.Add(DotPrefab);
        _dotObj.Add(DotPrefab);
    }
}