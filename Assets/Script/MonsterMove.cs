using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterMove : MonoBehaviour
{
    public float monsterSpeed;
    public int nextMove;               //랜덤 값을 받은 다음 움직임 변수

    public float StartHealth;       //몬스터 최대체력
    public float Health;            //몬스터 현재체력
    public float dmg;               //몬스터 공격력

    public GameObject HealthBar;        //HP바

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;

        void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("Think", 2);

    }

    void FixedUpdate()              //이동
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);


        // 몬스터가 앞이 낭떠러지인지 확인하는 로직
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        //Debug.DrawRay(rigid.position, Vector3.down, new Color(1, 0, 0));
        RaycastHit2D rayHit = Physics2D.Raycast
                (frontVec, Vector3.down, 1, LayerMask.GetMask("Platfrom"));
        if (rayHit.collider == null)
        {
            Turn();
        }

        // 몬스터가 앞이벽인지 확인하는 로직
        RaycastHit2D rayHitR = Physics2D.Raycast
                (frontVec, Vector3.right, 0.3f, LayerMask.GetMask("Platfrom"));
        RaycastHit2D rayHitL = Physics2D.Raycast
               (frontVec, Vector3.left, 0.3f, LayerMask.GetMask("Platfrom"));
        if (rayHitR.collider != null)
        {
            Turn();
        }
        if (rayHitL.collider != null)
        {
            Turn();
        }
    }



    void Think()            //이동 랜덤
    {
        nextMove = Random.Range(-1, 2);
        anim.SetInteger("MonsterState", nextMove);

        if (nextMove != 0)
        {
            spriteRenderer.flipX = nextMove == 1;
            anim.SetInteger("MonsterState", 1);
        }
        float nextThinkTime = Random.Range(1f, 4f);
        Invoke("Think", nextThinkTime);
    }


    void Turn()                 //방향 전환
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;
        CancelInvoke();
        Invoke("Think", 2);

    }

     void Hit(float damage)       //피격
    {

        Health -= damage;
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);                    //피격시 흰색?
        Invoke("ReturnSprite", 0.2f);
        HealthBar.GetComponent<Image>().fillAmount = Health / StartHealth;
        
        if (Health <= 0)
        {
            rigid.velocity = new Vector2(0,0);
            anim.SetTrigger("Damaged");          
            Invoke("MonsterDeath",0.4f);      //죽는 모션 없시 먼저 죽을시 따로 빼서 함수 만들고 iNVOKE사용해서 디스트로이해줘야됨
        }       
    }

    void ReturnSprite()         
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerBullets")
        {
            Bullets bullets = collision.gameObject.GetComponent<Bullets>();
            Hit(bullets.Bulletdamage);

        }
    }

    void MonsterDeath()
    {
        Destroy(gameObject);
    }

}
