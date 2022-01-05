using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;

public class Trunk : Enemy
{
    public float ShotSpeed;
    public GameObject bulletsRight;
    public GameObject bulletsLeft;
    public GameObject ShotRight;
    public GameObject ShotLeft;
    public int temp;
    public float count;
    public float maxShotDelay;
    public float curShotDelay;
    public bool hit;

    void Start()
    {
        direction = 1;
        PHit = false;
        thinkTime();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Reload();
        if (PHit == true)
        {
            if (curShotDelay > maxShotDelay)
            {
                Attack();
                curShotDelay = 0;
            }
            else
            {
                Move();
            }
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
        if (hit == false)
        {
            if (Et.x < Pt.position.x - 3)      //플레이어보다 왼쪽
            {
                direction = 1;
                anim.SetTrigger("Attack");
                Invoke("right", 0.6f);
                hit = true;
            }
            else if (Et.x > Pt.position.x + 3)  //플레이어보다 오른쪽
            {
                direction = -1;
                anim.SetTrigger("Attack");
                Invoke("left", 0.6f);
                hit = true;
            }
        }
        else
        {
            hit = false;
            Move();
        }
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void right()
    {
        GameObject bullet = Instantiate(bulletsRight, ShotRight.transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.right * ShotSpeed, ForceMode2D.Impulse);
    }

    void left()
    {
                        GameObject bullet = Instantiate(bulletsLeft, ShotLeft.transform.position, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.left * ShotSpeed, ForceMode2D.Impulse);
    }
}
