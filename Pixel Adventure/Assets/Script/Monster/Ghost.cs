using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ghost : MonoBehaviour
{
    Color color;

    public float speed;
    public float stoppingDistance;
    public float retreatDostance;
    public float dmg;
    public float StartHealth;       //몬스터 최대체력
    public float Health;            //몬스터 현재체력
    public int mexp;              //몬스터 처치시 경험치
    public bool PHit;               //플레이어에게 공격 당했을 시

    private bool isDisAppear = false;
    private bool isStop = false;

    private Animator anim;
    private Transform player;
    public Rigidbody2D rigid;

    public GameObject ghost;
    public GameObject Target;
    public GameObject HealthBar;
    public GameObject Canvas;
    public SpriteRenderer sp;

    private bool touch = false;

    private PlayerMove Player;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Target = GameObject.FindGameObjectWithTag("Player");
        sp = GetComponent<SpriteRenderer>();
        Canvas.SetActive(true);

    }
    void Update()
    {
        if (touch == false)
        {
            Move();
        }
         else
        {
            Move2();
            Invoke("touchfalse", 1.5f);
        } 
        if ((transform.position.x > player.position.x))
        {
            sp.flipX = false;
        }
        if ((transform.position.x < player.position.x))
        {
            sp.flipX = true;
        }

    }
    public void Hit(float damage)       //피격
    {
        PHit = true;
        Health -= damage;
        sp.color = new Color(1, 1, 1, 0.5f);                    //피격시 흰색?
        Invoke("ReturnSprite", 0.2f);
        HealthBar.GetComponent<Image>().fillAmount = Health / StartHealth;

        if (Health <= 0)
        {
            rigid.velocity = new Vector2(0, 0);
            anim.SetTrigger("Damaged");
            Invoke("MonsterDeath", 0.4f);      //죽는 모션 없시 먼저 죽을시 따로 빼서 함수 만들고 iNVOKE사용해서 디스트로이해줘야됨

        }
    }
    void ReturnSprite()
    {
        sp.color = new Color(1, 1, 1, 1);                    //피격시 흰색?
    }
    void MonsterDeath()
    {
        if (Health <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            Player = FindObjectOfType<PlayerMove>();
            Player.currentEXP = Player.currentEXP + mexp;
        }
    }
    void Move()
    {
        if (touch == false)
        {
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                if (transform.position.x - player.position.x < 27 && transform.position.y - player.position.y < 40)
                {
                    if (isDisAppear == false)
                    {
                        Invoke("Hidingtrue", 0.5f);
                        isDisAppear = true;
                    }
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                   if (Target.gameObject.layer == 10)
                    {
                        Moving();
                    } 
                }
            }
        }
     }
    void Move2()
    {
        isStop = true;
        if (touch == true)
        {
            if (Vector2.Distance(transform.position, player.position) > retreatDostance)
            {
                transform.position = Vector2.MoveTowards(transform.position, transform.position + player.position, speed * Time.deltaTime);
                if (Target.gameObject.layer == 11)
                {
                    Invoke("MoveStop", 1f);
                } 
            }
        }
    } 
    void touchfalse()
    {
        touch = false;
    }
    void Hidingtrue()                   //사라지는 애니메이션
    {      
        anim.SetBool("isHiding", true);
        Invoke("Objectfalse", 1f);
    }
    void Objectfalse()          //  스프라이트 투명화
    {
        if (anim.GetBool("isHiding") == true)
        {
            Canvas.SetActive(false);
            sp.material.color = new Color(sp.material.color.r, sp.material.color.g, sp.material.color.b, 0f);
            Invoke("Objecttrue", 2f);
        }
    }
    void Objecttrue()           //스프라이트 복구 && 나타나는 애니메이션
    {
        Canvas.SetActive(true);
        sp.material.color = new Color(sp.material.color.r, sp.material.color.g, sp.material.color.b, 1f);
        if (anim.GetBool("isHiding") == true)
        {
            anim.SetBool("isHiding", false);
            anim.SetBool("isMoving", true);
            Invoke("HideReturn", 2f);
            Debug.Log("2");
        }
    }
    void HideReturn()       // 원상복구 
    {
        anim.SetBool("isMoving", false);
        isDisAppear = false;
    }
    void MoveStop()
    {
        if (isStop == true)
        {
            Debug.Log("앙대");
            speed = 0;
            isStop = false;
        }

    }
    void Moving()
    {
        if (isStop == false)
        {
            speed = 8;
            Debug.Log("동작");
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            touch = true;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullets")
        {
            Bullets bullets = collision.gameObject.GetComponent<Bullets>();
            Hit(bullets.Bulletdamage);
        }
    }
}

