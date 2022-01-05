using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBird : Enemy
{
    public GameObject Boss;
    public GameObject Wall;

    public Transform Ts;

    public bool PPHit = false;
    public bool isRino = false;

    private Transform play;

    private bool isTouch;  
 
    private MaskDude MaskDude;

    void Start()
    {
        play = GameObject.FindGameObjectWithTag("Player").transform;      
        Ts = Wall.transform;     

        this.rigid.gravityScale = 0;    //안떨어지게 무게 0 줘서 공중에 떠있게 함
        spriteRenderer.material.color = new Color(spriteRenderer.material.color.r, spriteRenderer.material.color.g, spriteRenderer.material.color.b, 0f); //setActivy(false)안먹힘 이렇게 투명값 줘야함.
        gameObject.GetComponent<BoxCollider2D>().enabled = false; // 콜라이더 꺼야 안보이는 상태에서 플레이어랑 충돌 안 일으킴.
    } 
    void Update()
    {    
        BossStop();       
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")

                anim.SetBool("isIdle", true);     
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullets")
        {
            isTouch = true;
            Bullets bullets = collision.gameObject.GetComponent<Bullets>();
            HHit(bullets.Bulletdamage);
        }
    }
    public void HHit(float damage)       //피격
    {
        PPHit = true;
        Health -= damage;                

        if (Health <= 0)
        {
            rigid.velocity = new Vector2(0, 0);
            anim.SetTrigger("Damaged");
            Invoke("MMonsterDeath", 0.1f);      //죽는 모션 없시 먼저 죽을시 따로 빼서 함수 만들고 iNVOKE사용해서 디스트로이해줘야됨

        }
    }
    public void MMonsterDeath()
    {
        if (MonsterType == 1 || MonsterType == 2)
        {
            gameObject.SetActive(false);
        }
        else
        {
            isRino = true;
            isTouch = true;
            Up();     
        }           
    }
    public void Up() 
    {
        if(isTouch == true)
        {
            this.rigid.gravityScale = 0;    //안떨어지게 무게 0 줘서 공중에 떠있게 함
            spriteRenderer.material.color = new Color(spriteRenderer.material.color.r, spriteRenderer.material.color.g, spriteRenderer.material.color.b, 0f); //setActivy(false)안먹힘 이렇게 투명값 줘야함.
            gameObject.GetComponent<BoxCollider2D>().enabled = false; // 콜라이더 꺼야 안보이는 상태에서 플레이어랑 충돌 안 일으킴.
            Invoke("ActiveTrue", 2f);                   
        } 
    }
    public void ActiveTrue() //죽으면 플레이어 위치에 따라 위로 떨어지는 로직 
    {
        this.rigid.gravityScale = 1;
        spriteRenderer.material.color = new Color(spriteRenderer.material.color.r, spriteRenderer.material.color.g, spriteRenderer.material.color.b, 1f); //setActivy(false)안먹힘 이렇게 투명값 줘야함.
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        transform.position = new Vector2(play.position.x, play.position.y + 23);
        isTouch = false;
    }
    public void BossStop()
    {         
        if (GameObject.Find("MaskDude").GetComponent<MaskDude>().isBossStop) // MaskDude 컴포넌트에서 isBossStop 함수 가져와서 비교하는 로직
        {
            this.rigid.gravityScale = 1;
            spriteRenderer.material.color = new Color(spriteRenderer.material.color.r, spriteRenderer.material.color.g, spriteRenderer.material.color.b, 1f); //setActivy(false)안먹힘 이렇게 투명값 줘야함.
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            Invoke("isBossStop", 0.1f);
        }
    }  

    void isBossStop()
    {
        GameObject.Find("MaskDude").GetComponent<MaskDude>().isBossStop = false;

    }
}
