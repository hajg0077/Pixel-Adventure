using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChameleonDouble : Chameleon
{
    public GameObject Main;     //본체
    private bool movePlace = true;
    private bool moveStart = false;
    Transform MT;       //본체 위치
    public float hide = 0;
    void Start()
    {
        MonsterType = 0;
        think = 1;
        Invoke("ThinkTime", 3);
        isBerserk = true;
        spriteRenderer.color = new Color(1, 0.7f, 0.7f, 0);
    }

    void Update()
    {
        if (movePlace == true)
        {
            MT = Main.transform;
            transform.position = new Vector2(MT.position.x, MT.position.y);
            movePlace = false;
            Invoke("DarkSite", 0.1f);
        }
            if (Main.activeSelf == false)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (moveStart == true)
        {
            UpdateTarget();
            distance = Mathf.Abs(Et.x - Pt.position.x);
            if (isBerserk == true)
            {
                spriteRenderer.color = new Color(1, 0.7f, 0.7f, 1);
            }
            //이동로직
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
    }

    void DarkSite()
    {
        hide = hide + 0.1f;
        spriteRenderer.color = new Color(1, 0.7f, 0.7f, hide);
        transform.position = new Vector2(transform.position.x-0.4f, transform.position.y);
        if (hide < 1)
        {
            Invoke("DarkSite", 0.05f);
        }
        else
        {
            Invoke("MoveControll", 1);
            Debug.Log("등장끝");
        }
    }

    void MoveControll()
    {
        moveStart = true;
    }
    



}
