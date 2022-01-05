using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;

public class Plant : Enemy
{
    public float ShotSpeed;
    public GameObject bullets;
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
        if (curShotDelay > maxShotDelay)
        {
                Attack();
                curShotDelay = 0;
        }

    }

    void Turn()
    {
        direction = temp;
        if (direction == 1)
        {
            direction = -1;
            spriteRenderer.flipX = false;
        }
        else if (direction == -1)
        {
            direction = 1;
            spriteRenderer.flipX = true;
        }
        Invoke("thinkTime", 0.5f);
    }

    void thinkTime()
    {
        temp = direction;
        direction = 0;
    }

    void Attack()
    {
        UpdateTarget();
        if (hit == false)
        {
            if (Et.x < Pt.position.x - 3)      //플레이어보다 왼쪽
            {
                direction = 1;
                spriteRenderer.flipX = true;
                anim.SetTrigger("Attack");
                Invoke("right", 0.6f);
                hit = true;
            }
            else if (Et.x > Pt.position.x + 3)  //플레이어보다 오른쪽
            {
                direction = -1;
                spriteRenderer.flipX = false;
                anim.SetTrigger("Attack");
                Invoke("left", 0.6f);
                hit = true;
            }
        }
        else
        {
            hit = false;
        }
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void right()
    {
        GameObject bullet = Instantiate(bullets, ShotRight.transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.right * ShotSpeed, ForceMode2D.Impulse);
    }

    void left()
    {
        GameObject bullet = Instantiate(bullets, ShotLeft.transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.left * ShotSpeed, ForceMode2D.Impulse);
    }


}
