using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Enemy
{
    public int temp;
    public float count;
    void Start()
    {
        direction = 1;
        PHit = false;
        thinkTime();
    }
    void FixedUpdate()
    {
        if (PHit == true)
        {
            Attack();
        }
        else
        {
            Move();
        }
    }

    void Turn()
    {
        direction = temp;
        if (direction == 1)
        {
            direction = -1;
        }
        else if (direction == -1)
        {
            direction = 1;
        }
        Invoke("thinkTime", 5);
    }

    void thinkTime()
    {
        temp = direction;
        direction = 0;
        Invoke("Turn", 2);
    }

    void Attack()
    {
        UpdateTarget();
        if (Et.x < Pt.position.x - 3)      //플레이어보다 왼쪽
        {
            direction = 1;
        }
        else if (Et.x > Pt.position.x + 3)  //플레이어보다 오른쪽
        {
            direction = -1;
        }
        Move();
    }
}
