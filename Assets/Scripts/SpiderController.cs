using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    private static SpiderController instance;
    public static SpiderController Instance => instance;
    public GameObject bulletPrefab; // �ӵ���Prefab
    public Transform firePoint; // �ӵ������

    public float moveSpeed=5;

    public Transform lastPoint;


    /*---------------------*/

    float x, y = 0;
    //��ɫ�ƶ�����
    private Vector2 _direction = Vector2.zero;

    //��ɫ�ƶ��ٶ�
    [SerializeField] private float _speed = 3f;

    //��ɫ�ƶ��ٶ�
    [SerializeField] private float _speciaSpeed = 1f;

    [SerializeField] bool _canMove; //�����������Ҷ�
    [SerializeField] bool _canNormalMove; //�����������Ҷ�

    private Rigidbody2D rgd;


    //������
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private float _checkRadius;




    private void Awake()
    {
        instance = this;

        rgd = GetComponent<Rigidbody2D>();

        //----------------һ��ʼ���������ƶ�----------------
        _canMove = true;
        _canNormalMove = true;
    }


    void Update()
    {
        // ������������
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        //�����ƶ�
        Player2Movement();

        //�����ƶ�
        Player2SpeciaMovement();

        //����
        Player2Direction();

        //��������
        ResetGravity();
    }


    /// <summary>
    /// �����ƶ�
    /// </summary>
    private void Player2Movement()
    {
        if (!_canMove) return;

        if (!_canNormalMove) return;

        x = GameInputManager.Instance.Movement2.x;

        _direction = new Vector2(x, y);


        //���ƽ�ɫ���ŷ����ƶ�
        transform.Translate(_direction * _speed * Time.deltaTime);
    }


    /// <summary>
    /// �ܷ������ƶ�
    /// </summary>
    /// <returns></returns>
    private bool CanSpeciaMove()
    {
        return NodeManager.Instance.CanSpeciaMoveInCurrentNode();
    }


    /// <summary>
    /// �����ƶ�
    /// </summary>
    private void Player2SpeciaMovement()
    {
        if (!CanSpeciaMove()) return;
        if (!_canMove) return;

        _direction = NodeManager.Instance.CurrentNodeTail().position - NodeManager.Instance.CurrentNodeHead().position;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            //���ƽ�ɫ���ŷ����ƶ�
            transform.Translate(_direction * _speciaSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //���ƽ�ɫ���ŷ����ƶ�
            transform.Translate(-_direction * _speciaSpeed * Time.deltaTime);
        }
    }


    /// <summary>
    /// �������
    /// </summary>
    private void Player2Direction()
    {
        if (x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }







    void Shoot()
    {
        //----------------һ��Ͳ�����-------------------
        _canMove = false;

       


        // ��ȡ��������������е�λ��
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; 

        // ����ӷ���㵽���λ�õķ���
        Vector2 direction = (mousePosition - firePoint.position).normalized;

        // ʵ�����ӵ����������ʼλ��
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        lastPoint = firePoint;

        // ��ʼ���ӵ�����
        bullet.GetComponent<Bullet>().Initialize(direction);

        // ʹ�ӵ������ƶ�����
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        //��������ӵ���ײ��ľ���  ������˿

    }



    /// <summary>
    /// �ָ��ƶ� ���䵽ǽ�ڻ���Ӻ�
    /// </summary>
    public void ResetMove()
    {
        _canMove = true;
    }



    /// <summary>
    /// �ָ������ƶ�
    /// </summary>
    public void EnableNormalMove(bool enable)
    {
        _canNormalMove = enable;
    }




    public bool Player2IsInSlide()
    {
        if (Mathf.Abs(GameInputManager.Instance.Movement2.y) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="enable"></param>
    public void EnableGravity(bool enable)
    {
        if(enable)
        {
            rgd.gravityScale = 1;
        }
        else
        {
            rgd.gravityScale = 0;
        }
        
    }


    private bool IsOnGround()
    {
        Collider2D hit = Physics2D.OverlapCircle(_groundCheckPoint.position, _checkRadius);

        if (hit != null && hit.tag == "Wall")
        {
            Debug.Log(hit.name);
            return true;
        }
        else
        {
            return false;
        }
    }



    private void ResetGravity()
    {
        if(IsOnGround())
        {
            EnableGravity(false);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheckPoint.position, _checkRadius);
    }

}
