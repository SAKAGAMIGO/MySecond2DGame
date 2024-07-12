using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameController _gameController;

    Rigidbody2D _rb;

    /// <summary>�J�n�ʒu</summary>
    private Vector2 _startPosition;

    /// <summary>���������ő勗��</summary>
    public float MaxPullDistance = 1;

    /// <summary>��΂���</summary>
    public float FlyForce = 8;

    /// <summary>��񂾔���</summary>
    public bool IsFly = false;

    int _dotCount = 5;

    //�`�悷��h�b�g�̐�
    private GameObject[] _dotObject = new GameObject[5];

    /// <summary>�h�b�gPrefab</summary>
    public GameObject DotPrefab;

    /// <summary>�h�b�g�̕`��Ԋu</summary>    
    public float DotTimeInterval = 0.05f;

    /// <summary>�A�j���[�^�[���擾</summary>    
    private Animator _animator;

    ///// <summary>�����Ă���A�C�e���̃��X�g</summary>
    //List<ItemBaceClass> _itemList = new List<ItemBaceClass>();

    void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        //RigidBody���擾
        _rb = GetComponent<Rigidbody2D>();
        ////��������Ƃ��͕������Z���K�v�Ȃ��̂�OFF�ɂ���
        _rb.isKinematic = true;
        //�O���r�e�B�X�P�[�����擾
        _rb.gravityScale = 0.5f;
        //�������Ȃ������ʒu
        _startPosition = transform.position;
        //�z��̗v�f�����h�b�g�̃I�u�W�F�N�g��\��
        for (int i = 0; i < _dotObject.Length; i++)
        {
            _dotObject[i] = Instantiate(DotPrefab);
            _dotObject[i].transform.localScale = _dotObject[i].transform.localScale * (1 - 0.06f * i);
            _dotObject[i].transform.parent = transform;
            //��������O�͔�\��
            _dotObject[i].SetActive(false);
        }
        //PlayerMotion�̃A�j���[�V�������i�[
        _animator = GameObject.Find("Player Motion").GetComponent<Animator>();
        //Player Motion��TransForm���擾
        GameObject.Find("Player Motion").GetComponent<Transform>();
    }

    void Update()
    {
        //// �A�C�e�����g��
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (_itemList.Count > 0)
        //    {
        //        // ���X�g�̐擪�ɂ���A�C�e�����g���āA�j������
        //        ItemBaceClass item = _itemList[0];
        //        _itemList.RemoveAt(0);
        //        item.Activate();
        //        Destroy(item.gameObject);
        //        Debug.Log("�A�C�e���g�p");
        //    }
        //}
    }

    ///// <summary>
    ///// �A�C�e�����A�C�e�����X�g�ɒǉ�����
    ///// </summary>
    ///// <param name="item"></param>
    //public void GetItem(ItemBaceClass item)
    //{
    //    _itemList.Add(item);
    //    Debug.Log("�A�C�e���擾");
    //}

    //�}�E�X�N���b�N���Ȃ���ړ�����֐�
    public void OnMouseDrag()
    {
        _animator.SetBool("Pull", true);

        //��x��񂾂珈�����s���Ȃ�����
        if (IsFly) return;

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
    private void OnMouseUp()
    {
        
        //�A�j���[�V������Pull���[�V�������Đ�
        _animator.SetBool("Pull", false);

        //��x��񂾂珈�����s���Ȃ�����
        if (IsFly) return;

        //�͂�������x�N�g���ɔ�΂��͂�������
        var Force = ((_startPosition - (Vector2)transform.position) * FlyForce);

        //�������Z��ON�ɂ���
        var RigidBody2D = GetComponent<Rigidbody2D>();
        RigidBody2D.isKinematic = false;

        //AddForce�֐��ŗ͂�������
        RigidBody2D.AddForce(Force, ForceMode2D.Impulse);

        //
        for (int i = 0; i < _dotObject.Length; i++)
        {
            _dotObject[i].SetActive(false);
        }

        IsFly = true;
    }

    /// <summary>
    /// �Փ˃C�x���g
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {

        //3�b��ɏ�����
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
        Debug.Log("override���Ă�������");
    }

    /// <summary>�h�b�g�̕\�����X�V</summary>
    public void UpdateDotObject()
    {
        //��΂��͂̃x�N�g�����Z�o
        var Force = ((_startPosition - (Vector2)transform.position) * FlyForce) * (1 - _rb.gravityScale + 1);
        //��΂��Ă���̎��Ԃ��Ƃ̃L�����N�^�[�̈ʒu�Ƀh�b�g��\��
        var CurrentTime = DotTimeInterval;
        for (int i = 0; i < _dotObject.Length; i++)
        {
            //�A�N�e�B�u�ɂ���
            _dotObject[i].SetActive(true);
            var Position = new Vector2();
            //X�����͎��Ԃɔ�Ⴕ�ĉE�ɐi��
            Position.x = (transform.position.x + Force.x * CurrentTime);
            //Y�����͎��R�����̌v�Z(�d��)
            Position.y = (transform.position.y + Force.y * CurrentTime) - (Physics2D.gravity.magnitude * CurrentTime * CurrentTime) / (1 + FlyForce / 10);

            _dotObject[i].transform.position = Position;
            CurrentTime += DotTimeInterval;
        }
    }

    public void AddFlyForce()
    {
        FlyForce += 5;
        Debug.Log("�X�e�[�^�XUP");
    }
}
