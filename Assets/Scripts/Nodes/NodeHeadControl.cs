using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeHeadControl : MonoBehaviour
{
    [SerializeField] private Transform _Player2;
    [SerializeField] private Transform _Player1;

    [SerializeField] private float _duration;

    private Vector3 _destination;

    [SerializeField] private LayerMask _player2Layer;

    [SerializeField] private Transform _nodeTailTran;


    private void Start()
    {
        _Player2 = GameObject.FindWithTag("Player2").transform;
        _Player1 = GameObject.FindWithTag("Enemy").transform;
    }

    private void Update()
    {
        OnPlayer2InThisNode(); //�ж����2�Ƿ��ڴ˽ڵ㸽��
        
        BeCollected(); //���ռ��ж�
    }




    /*֩�����ƶ� ������*/
    //private void OnMouseDown()
    //{
    //    MoveToDirection();
    //}

    //private void MoveToDirection()
    //{
    //    if (!CanMoveToThisNode()) return;

    //    _destination = transform.position;
    //    _Player2.DOMove(_destination, _duration);
    //}


    //private bool CanMoveToThisNode()
    //{
    //    if (Vector3.Distance(_Player2.position, transform.position) < .4f)
    //    {
    //        return true;
    //    }

    //    return false;
    //}


    private void OnPlayer2InThisNode()
    {
        if (Vector3.Distance(_Player2.position, transform.position) < 1.8f)
        {
            NodeManager.Instance.ChangeCurrentNodes(transform, _nodeTailTran);
        }
    }


    /// <summary>
    /// ͷ�ڵ㱻�ռ�
    /// </summary>
    private void BeCollected()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Vector3.Distance(_Player1.position, transform.position) < 1.8f)
        {
            Transform lineGo = gameObject.transform.parent.parent;


            //---------�ж����2�Ƿ�����������-------
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (_nodeTailTran.position - transform.position).normalized,
            Vector3.Distance(_nodeTailTran.position, transform.position), _player2Layer);

            if(hit.collider != null)
            {
                SpiderController.Instance.EnableGravity(true);
            }


            //TODO:�ռ���



            lineGo.GetComponent<Bullet>().DestroySelfForCollected();
        }
    }


}
