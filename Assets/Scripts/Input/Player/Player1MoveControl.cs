using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMoveControl2 : MonoBehaviour
{
    
    [SerializeField] private float _speed = 3f; //��ɫ�ƶ��ٶ�

    private Vector2 _direction = Vector2.zero; //��ɫ�ƶ�����

    float x, y = 0;



    private void FixedUpdate()
    {
        Player2Movement();
        Player2Direction();
    }



    private void Player2Movement()
    {
        x = GameInputManager.Instance.Movement1.x;
        y = GameInputManager.Instance.Movement1.y;

        _direction = new Vector2(x, y);


        //���ƽ�ɫ���ŷ����ƶ�
        transform.Translate(_direction * _speed * Time.deltaTime);
    }



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

}
