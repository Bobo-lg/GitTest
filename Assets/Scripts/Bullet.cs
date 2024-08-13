using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f; // �ӵ��ٶ�
    private Vector2 direction;
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    private bool hasHitWall = false; // ����Ƿ�����ײ��ǽ��

    // �ӵ�������ʱ��
    public float timeToLive = 60f;

    // ��ʼ���ӵ�����
    public void Initialize(Vector2 shootDirection)
    {
        direction = shootDirection.normalized;
    }




    //------------�ڵ�Ԥ����------------
    [SerializeField] private GameObject _nodeLinePrefab;



    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        //edgeCollider = gameObject.AddComponent<EdgeCollider2D>();

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);

        // ���ø���ļ��ģʽ
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        Destroy(gameObject, timeToLive);
    }

    void Update()
    {
        if (!hasHitWall)
        {
            // �����ӵ�λ��
            transform.position += (Vector3)(direction * speed * Time.deltaTime);

            // ���¹켣��
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);

            // ������ײ��
            //UpdateCollider();
        }
    }

    public Vector2 colliderPoint;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            print("��ײ����ǽ");
            // �ӵ�����ǽ��ʱֹͣ�˶�
            hasHitWall = true;

            // ֹͣ�ӵ����˶�
            direction = Vector2.zero;
            //����ײ���������ⲿʹ��
            //colliderPoint = collision
            print(transform.position);
            //��ײ���λ�õ�������λ�þ���
            //Vector2 disLine = (transform.position - SpiderController.Instance.lastPoint.position).normalized;
            float disLine = Vector2.Distance(transform.position, SpiderController.Instance.lastPoint.position);
            print(disLine);
            //Vector2 startPoint = SpiderController.Instance.lastPoint.position;
            


            // ���ø���Ϊ��̬
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Static;
            }


            //-------------����ǽ���ڵ�ǰλ�÷�һ��β�ڵ㣬Ȼ���������ĵط���һ��ͷ�ڵ�-------------
            GameObject lgo = Instantiate(_nodeLinePrefab, transform.position, Quaternion.identity);
            GameObject tgo = lgo.GetComponentInChildren<NodeTrailControl>().gameObject;
            GameObject hgo = lgo.GetComponentInChildren<NodeHeadControl>().gameObject;
            tgo.transform.position = transform.position;
            hgo.transform.position = SpiderController.Instance.lastPoint.position;
            lgo.transform.parent = transform;
            Destroy(lgo, timeToLive-1);



            //-------------���������ƶ���-------------
            SpiderController.Instance.ResetMove();

        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            print("�䵽������");
            //ִ�е��˵��߼�

            // �ӵ�����ǽ��ʱֹͣ�˶�
            hasHitWall = true;

            // ֹͣ�ӵ����˶�
            direction = Vector2.zero;
            //����ײ���������ⲿʹ��


            // ���ø���Ϊ��̬
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Static;
            }


            //-------------���������ƶ���-------------
            SpiderController.Instance.ResetMove();

        }
    }



    /// <summary>
    /// �ռ��������ӵ�
    /// </summary>
    public void DestroySelfForCollected()
    {
        Destroy(gameObject, 0.1f);
    }

   

}
