using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float monsterSpeed;
    public int direction = 0;               //랜덤 값을 받은 다음 움직임 변수

    public float StartHealth;       //몬스터 최대체력
    public float Health;            //몬스터 현재체력
    public float dmg;               //몬스터 공격력
    public int mexp;              //몬스터 처치시 경험치
 
    public int MonsterType;         //1:중보스,    2:보스

    public bool PHit;               //플레이어에게 공격 당할 시

    public bool Berserk = false;        //보스 전용 광폭화 패턴
    public bool isBerserk = false;

    public GameObject Target;       //타겟 지정
    public GameObject HealthBar;

    public Rigidbody2D rigid;
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public Transform Pt;
    public Vector2 Et;

    private PlayerMove Player;
    

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Move()
    {
        Vector2 frontVec = new Vector2(0,0);
        if (direction == 0)
        {
            anim.SetInteger("MonsterState", 0);
        }
        else
        {
            anim.SetInteger("MonsterState", 1);
            if (direction == -1)
            {
                spriteRenderer.flipX = false;
                frontVec = new Vector2(rigid.position.x- 0.3f, rigid.position.y);
                Debug.DrawRay(rigid.position, Vector3.down, new Color(1, 0, 0));
            }
            else if (direction == 1)
            {
                spriteRenderer.flipX = true;
                frontVec = new Vector2(rigid.position.x+0.3f, rigid.position.y);
                Debug.DrawRay(rigid.position, Vector3.down, new Color(1, 0, 0));
            }
            RaycastHit2D rayHit = Physics2D.Raycast
            (frontVec, Vector2.down, 4, LayerMask.GetMask("Platform"));
            if (rayHit.collider == null)
            {
                direction = 0;
            }
            rigid.velocity = new Vector2(direction * monsterSpeed, rigid.velocity.y);
        }
    }

    public void Hit(float damage)       //피격
    {
        PHit = true;
        Health -= damage;
        if (isBerserk == true)
        {
            spriteRenderer.color = new Color(1, 0.7f, 0.7f, 0.5f);
        }
        else
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);                    //피격시 흰색?
        }
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
        if (isBerserk == true)
        {
            spriteRenderer.color = new Color(1, 0.7f, 0.7f, 1);
        }
        else
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);                    //피격시 흰색?
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

    public void MonsterDeath()
    {
        if (MonsterType == 1 || MonsterType == 2)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
        Player = FindObjectOfType<PlayerMove>();
        Player.currentEXP = Player.currentEXP + mexp;
    }
    
    public void UpdateTarget()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        Pt = Target.transform;
        Et = this.gameObject.transform.position;
    }
}
