using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;

public class Chameleon : Enemy
{
    public float distance;
    public bool isAttack = false;
    public bool Attacking = false;
    public GameObject child;
    public GameObject Double;
    public int think;
    
    void Start()
    {
        MonsterType = 1;
        think = 1;
        Invoke("ThinkTime", 3);
    }
    void FixedUpdate()
    {
        UpdateTarget();
        distance = Mathf.Abs(Et.x - Pt.position.x);
        if(isBerserk == true)
        {
            spriteRenderer.color = new Color(1, 0.7f, 0.7f, 1);
        }

        if (Health < 250 && Berserk == false && isBerserk == false)
        {
            Invoke("BerserkTime", 5);
            isBerserk = true;
        }
        else if (Health < 250 && Berserk == false && isBerserk == true)
        {
            return;
        }

        if (Pt.position.x < 133 && Pt.position.y > 38)   //움직이는거
        {
            if (think == 9)
            {
                direction = -1;
                monsterSpeed = 15;
                Move();
            }
            else if (think == 10)
            {
                direction = 1;
                monsterSpeed = 15;
                Move();
            }
            else
            {
                if (isAttack == false && Attacking == false)
                {
                    if (Et.x < Pt.position.x)
                    {
                        direction = 1;
                    }
                    else if (Et.x > Pt.position.x)
                    {
                        direction = -1;
                    }
                    if (distance > 5)
                    {
                        monsterSpeed = 5;
                    }
                    else
                    {
                        monsterSpeed = 0;
                        isAttack = true;
                    }
                    Move();
                }
                if (isAttack == true && Attacking == false)
                {
                    Attack();
                    Attacking = true;
                }
            }
        }
    }
    void ThinkTime()
    {
        think = Random.Range(1, 11);
        Invoke("ThinkTime", 2);
    }

    void BerserkTime()
    {
        Invoke("BerserkDelay", 1);
        Double.SetActive(true);
    }

    void BerserkDelay()
    {
        Berserk = true;
    }

    public void Attack()
    {
        anim.SetTrigger("isAttack");
        Invoke("AttackColiderOn", 0.4f);
        Invoke("AttackCoolTime", 2);
    }

    void AttackCoolTime()
    {
        Attacking = false;
        isAttack = false;
    }

    void AttackColiderOn()
    {
        GameObject child = transform.Find("Attack").gameObject;
        child.SetActive(true);
        if(direction == 1)
        {
            child.GetComponent<BoxCollider2D>().offset = new Vector2(0.6f, -0.1f);
        }
        else if(direction == -1)
        {
            child.GetComponent<BoxCollider2D>().offset = new Vector2(-0.6f, -0.1f);
        }
        child.GetComponent<BoxCollider2D>().size = new Vector2(1.3f, 0.1f);
        Invoke("AttackColiderOff", 0.3f);
    }
    void AttackColiderOff()
    {
        GameObject child = transform.Find("Attack").gameObject;
        child.GetComponent<BoxCollider2D>().size = new Vector2(0.1f, 0.1f);
        child.GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0f);
        child.SetActive(false);
    }

}
