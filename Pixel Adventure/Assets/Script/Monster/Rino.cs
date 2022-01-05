using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rino : Enemy
{
    public GameObject FatBird;
    private Transform trans;
    private Transform trans1;

    public int temp;
    public float count;
    private float distance;
    private bool isAttack;
    private bool attacking;
    private bool atkstart;
    private bool isStrating = false;
    
    void Start()
    {
        trans = GameObject.Find("RinoWallLeft").transform;
        trans1 = GameObject.Find("RinoWallRight").transform;
        direction = 1;
        this.rigid.gravityScale = 0;
        spriteRenderer.material.color = new Color(spriteRenderer.material.color.r, spriteRenderer.material.color.g, spriteRenderer.material.color.b, 0f);
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
    }
    void FixedUpdate()
    {       
        UpdateTarget();
        distance = Mathf.Abs(Et.x - Pt.position.x);
        if (distance < 80 && atkstart == false)
        {
            attacking = true;
            if (Et.x <= Pt.position.x && isAttack == false)
            {
                direction = 1;
                isAttack = true;
                spriteRenderer.flipX = true;
            }
            else if (Et.x > Pt.position.x && isAttack == false)
            {
                direction = -1;
                isAttack = true;
                spriteRenderer.flipX = false;
            }
            if (attacking == true)
            {
                Invoke("Attack", 2f);
            }
        }
    }
    void Attack()
    {
        if (GameObject.Find("FatBird 1").GetComponent<FatBird>().isRino == true)
        {           
            this.rigid.gravityScale = 1;
            spriteRenderer.material.color = new Color(spriteRenderer.material.color.r, spriteRenderer.material.color.g, spriteRenderer.material.color.b, 1f);
            gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
            Vector2 frontVec = new Vector2(0, 0);
            anim.SetBool("Attack", true);
            monsterSpeed = 25;
            if (direction == 1)      //플레이어보다 왼쪽
            {             
                frontVec = new Vector2(rigid.position.x + 1f, rigid.position.y);
                if (Et.x - Pt.position.x > 5)
                {
                    atkstart = true;
                    attacking = false;
                    Invoke("AttackDelay", 2f);
                }
            }
            else if (direction == -1)  //플레이어보다 오른쪽
            {            
                frontVec = new Vector2(rigid.position.x - 1f, rigid.position.y);
                if (Pt.position.x - Et.x > 5)
                {
                    atkstart = true;
                    attacking = false;
                    Invoke("AttackDelay", 2f);
                }
            }
            RaycastHit2D rayHit = Physics2D.Raycast
                (frontVec, Vector2.down, 4, LayerMask.GetMask("Platform"));
            if (rayHit.collider == null)
            {
                monsterSpeed = 0;
            }
            rigid.velocity = new Vector2(direction * monsterSpeed, rigid.velocity.y);
            Debug.DrawRay(rigid.position, Vector3.down, new Color(1, 0, 0));
        }
    }
    void AttackDelay()
    {
        if (Et.x <= Pt.position.x && isAttack == false)
        {
            transform.position = new Vector2(trans.position.x, trans.position.y);
        }
        else if (Et.x > Pt.position.x && isAttack == false)
        {
            transform.position = new Vector2(trans1.position.x, trans1.position.y);
        }

        isStrating = true;
        anim.SetBool("Attack", false);
        isAttack = false;
        atkstart = false;
        GameObject.Find("FatBird 1").GetComponent<FatBird>().isRino = false;      
        spriteRenderer.material.color = new Color(spriteRenderer.material.color.r, spriteRenderer.material.color.g, spriteRenderer.material.color.b, 0f);
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        this.rigid.gravityScale = 0;
        this.gameObject.SetActive(false);
        Invoke("RinoTrue", 0.2f);
    }
    void RinoTrue()
    {
        this.gameObject.SetActive(true);
    }
}
