using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    public int temp;
    public float count;
    public float DHp;
    public float Size;

    void Start()
    {
        monsterSpeed = 2;
        direction = 1;
        PHit = false;
        thinkTime();
    }
    void FixedUpdate()
    {
        SlimeSize();
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

    void SlimeSize()
    {
        DHp = Health / StartHealth;
        if (DHp <= 0.25f)
        {
            DHp = 0.25f;
        }
        Size = 8 * DHp;
        monsterSpeed = 2/DHp;
        transform.localScale = new Vector3(Size, Size, 0);
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
