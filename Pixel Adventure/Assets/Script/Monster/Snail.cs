using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : Enemy
{
    public int temp;
    public float count;
    void Start()
    {
        direction = 0;
    }
    void FixedUpdate()
    {
        Attack();
        Move();
    }

    void Attack()
    {
        UpdateTarget();
        if (Et.x - Pt.position.x < 30 && Et.y < 30)
        {
            direction = -1;
        }
        else if (Pt.position.x - Et.x >30 && Pt.position.y >= 38)
        {
            direction = 1;
        }
    }

}
