using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskDude : Enemy
{
    public bool isBossStop = false;
    public bool Drop = false;
    private bool bossapeear = false;
    public int temp;
    public float stoppingDistance;   

    public ParticleSystem fireRight;  
    public ParticleSystem fireLeft;
    public GameObject firer;
    public GameObject firel;
    public GameObject fatbird;
    public GameObject bosszone;
 
    private bool isFire = false;
    private bool isTouch = false;

    void Start()
    {   
       firer.SetActive(false);
       firel.SetActive(false);     
       fireRight.Stop();
       fireLeft.Stop();
       spriteRenderer.color = new Color(1, 1, 1, 0);
    }  

    void FixedUpdate()
    {      
        if(bosszone.activeSelf == true && bossapeear == false)
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
            bossapeear = true;
        }
        if (PHit == true)
        {               
            isFire = true;
            Attack();
            Invoke("Stop", 3f);        
        }

        if (Health <= 500) //---------------------광폭화
        {
            Berserk = true;
            isBerserk = true;
            Berserkmode();
        }

        if(Health <= 0)
        {
            firer.SetActive(false);
            firel.SetActive(false);
            firer.GetComponent<BoxCollider2D>().enabled = false; //이거 안끄면 버그? 생겨서 아무것도 없는데 맞음
            firel.GetComponent<BoxCollider2D>().enabled = false; //이거 안끄면 버그? 생겨서 아무것도 없는데 맞음
        } 
    }

    void Berserkmode()
    {
        if(isBerserk == true)
        {
            monsterSpeed = 7;
          //  spriteRenderer.color = new Color(1, 0.7f, 0.7f, 1);
            if (Vector2.Distance(transform.position, Pt.position) > stoppingDistance) { 
                transform.position = Vector2.MoveTowards(transform.position, Pt.position, monsterSpeed * Time.deltaTime);
            anim.SetBool("isRunning", true);
            if (transform.position.x < Pt.position.x)
            {
                spriteRenderer.flipX = false;
            }
                if (transform.position.x > Pt.position.x)
                {
                    spriteRenderer.flipX = true;
                }
            }
        }
    }

    void Stop() //---------------------------------불 뿜다가 멈추는 로직
    {
        PHit = false;
        isBossStop = true;
        direction = 0;
        if (isBerserk == false) {       
        anim.SetBool("isRunning", false);
    }     
     
    }
  
    public void Moving()    //----------------------기존 Enemy Move() 로직 이름만 바꿈
    {
        Vector2 frontVec = new Vector2(0, 0);
        if (direction == 0)
        {
          //  anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
            if (direction == -1)
            {             
                spriteRenderer.flipX = true;
                frontVec = new Vector2(rigid.position.x - 0.3f, rigid.position.y);
                Debug.DrawRay(rigid.position, Vector3.down, new Color(1, 0, 0));
            }
            else if (direction == 1)
            {              
                spriteRenderer.flipX = false;
                frontVec = new Vector2(rigid.position.x + 0.3f, rigid.position.y);
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

    void Attack() //---------------------------플레이어 방향에 따라 불뿜는 방향(공격) 바뀌는 로직
    {      
        UpdateTarget();
        Moving();
        if (transform.position.x < Pt.position.x)      //플레이어보다 왼쪽 
        {
            direction = 1;                     
                fireR();
        }
        else if (transform.position.x > Pt.position.x)  //플레이어보다 오른쪽 
        {
            direction = -1;         
                fireL();                    
        }
        isTouch = true;    
    } 
    void fireR()
    {    
        firer.SetActive(true);
        firel.SetActive(false);     
        fireRight.Play();
        fireLeft.Stop();
        Invoke("FireRightStop", 3f);
    }

    void fireL()
    {      
        firel.SetActive(true);
        firer.SetActive(false);    
        fireLeft.Play();
        fireRight.Stop();
        Invoke("FireLeftStop", 3f);
    } 

    void FireRightStop() //오른쪽 불 공격 멈추는 로직
    {     
        firer.SetActive(false);
        firer.GetComponent<BoxCollider2D>().enabled = false; //이거 안끄면 버그? 생겨서 아무것도 없는데 맞음
        Invoke("FireRightOn", 0f);
    } 

     void FireRightOn() // 오른쪽 불 공격 콜라이더 켜기 
    {
        firer.GetComponent<BoxCollider2D>().enabled = true;
    }

    void FireLeftStop() // 왼쪽 불 공격 멈추는 로직
    {       
        firel.SetActive(false);
        firel.GetComponent<BoxCollider2D>().enabled = false; //이거 안끄면 버그? 생겨서 아무것도 없는데 맞음
        Invoke("FireLeftOn", 0f);
    }

    void FireLeftOn() //왼쪽 불 공격 콜라이더 켜기 
    {
        firel.GetComponent<BoxCollider2D>().enabled = true;
    }
 
}



