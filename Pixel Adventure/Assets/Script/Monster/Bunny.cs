using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny : Enemy
{
    private static Bunny instance;
    public float count;
    public float jumpPower;
    public GameObject point;
    public bool isjump = false;
    BoxCollider2D box;
    public bool ispointon = false;
    public float Attackcount;
    public GameObject pointTarget;
    public Transform Po;
    public bool tb = false;
    private bool isflying = false;

    public bool isa = false;
    public int think;
    public float distance;

    Vector3 Lpointtarget;
    Vector3 Rpointtarget;
    public GameObject lightR;
    public GameObject lightL;
    public ParticleSystem lightPar1;
    public ParticleSystem lightPar2;


    void Start()
    {
        instance = this;

        ThinkTime();
        isjump = false;

        MonsterType = 1;
        think = 1;
        Invoke("ThinkTime", 3);

        lightR.SetActive(false);
        lightL.SetActive(false);
        lightPar1.Stop();
        lightPar2.Stop();

    }
    void FixedUpdate()
    {
        UpdateTarget();
        if (Et.x < Pt.position.x - 1)      //플레이어보다 왼쪽
        {
            direction = 1;
        }
        else if (Et.x > Pt.position.x + 1)  //플레이어보다 오른쪽
        {
            direction = -1;
        }
        ///////////////----------------------방향 전환
        if (isBerserk == true)
        {
            spriteRenderer.color = new Color(1, 0.7f, 0.7f, 1);
        }
        //-----------------------버서크타임
        if (Health < 70 && Berserk == false && isBerserk == false)
        {
            Invoke("BerserkTime", 2);
            isBerserk = true;
        }
        else if (Health < 70 && Berserk == false && isBerserk == true)
        {
            return;
        }
        //발동


        if (Pt.position.x < 177 && Pt.position.x > 136 && Pt.position.y > -44 && Pt.position.y < -30)   //움직이는거
        {
            if (think <= 8 && think >= 7)   // 하늘 찍기
            {
                if (isjump == false)
                {
                    CancelInvoke();
                    JumpAttack();
                }
            }
            else if (think >= 3 && think >= 6)      //기본 점프 공격
            {
                if (isjump == false)
                {
                    if (Et.x > Pt.position.x + 1)      //플레이어보다 왼쪽
                    {
                        CancelInvoke();
                        StartCoroutine("Jumpleft");
                    }
                    else if (Et.x < Pt.position.x - 1)  //플레이어보다 오른쪽
                    {
                        CancelInvoke();
                        StartCoroutine("Jumpright");
                    }

                }
            }
            else
            {
                Move();
            }
        }
        Attackcount = 0;
    }

    void ThinkTime()                //생각 시간
    {
        think = Random.Range(1, 11);
        Invoke("ThinkTime", 2);
    }


    IEnumerator Jumpleft()
    {
        yield return null;
        direction = 1;
        anim.SetTrigger("isAttack");
        Debug.Log("왼쪽");
        if (ispointon == false)
        {
            Vector3 y0 = new Vector3(Pt.transform.position.x, -43, 0);
            Instantiate(point, y0, transform.rotation);

            ispointon = true;
        }
        pointTarget = GameObject.FindGameObjectWithTag("point");
        Po = pointTarget.transform;
        //rigid.velocity = new Vector2(-transform.localScale.x * 3f, jumpPower);
        Vector3 pointtarget = new Vector3(Po.transform.position.x, -39, 0);
        transform.position = Vector3.MoveTowards(transform.position, pointtarget, 0.4f);
        if (transform.position.x <= Po.transform.position.x + 3f)
        {
            StopCoroutine("Jumpright");
            think = 1;
            isjump = true;
            if (isBerserk == true)
            {
                if (transform.position.y < -40)
                {
                    LightOn();
                    Invoke("LightOff", 0.3f);
                }
            }
            Invoke("ThinkTime", 1);
            Invoke("AttackCoolTime", 2);
        }
    }

    IEnumerator Jumpright()
    {
        yield return null;
        direction = -1;
        anim.SetTrigger("isAttack");
        Debug.Log("오른쪽");
        if (ispointon == false)
        {
            Vector3 y0 = new Vector3(Pt.transform.position.x, -43, 0);
            Instantiate(point, y0, transform.rotation);
            ispointon = true;
        }
        pointTarget = GameObject.FindGameObjectWithTag("point");
        Po = pointTarget.transform;
        //rigid.velocity = new Vector2(transform.localScale.x * 3f, jumpPower);
        Vector3 pointtarget = new Vector3(Po.transform.position.x, -39, 0);
        transform.position = Vector3.MoveTowards(transform.position, pointtarget, 0.4f);
        if (transform.position.x >= Po.transform.position.x - 3f)
        {
            StopCoroutine("Jumpright");
            think = 1;
            isjump = true;
            if (isBerserk == true)
            {
                if (transform.position.y < -41)
                {
                    LightOn();
                    Invoke("LightOff", 0.3f);
                }
            }
            Invoke("ThinkTime", 1);
            Invoke("AttackCoolTime", 2);
        }
    }


    void JumpAttack()                           //하늘 찍기
    {
        if (ispointon == false)         //1.포인트 생성
        {
            Vector3 y0 = new Vector3(Pt.transform.position.x, -43, 0);
            Instantiate(point, y0, transform.rotation);
            ispointon = true;
        }
        pointTarget = GameObject.FindGameObjectWithTag("point");
        Po = pointTarget.transform;
        if (isflying == false)      //2.도약
        {
            Vector3 target = new Vector3(transform.position.x, -29, 0);
            transform.position = Vector3.MoveTowards(transform.position, target, 1f);
        }
        //--------------------------------------------------------------------------------------------------------------------
        if (transform.position.y >= -30)    //3.내려찍기
        {   
            Vector3 poup = new Vector3(Po.transform.position.x, -29, 0);                        //2.5 체공 시간
            rigid.constraints = RigidbodyConstraints2D.FreezePositionY;
            transform.position = Vector3.MoveTowards(transform.position, poup, 0.7f);
            if (transform.position.x < Po.transform.position.x + 2f && transform.position.x > Po.transform.position.x - 2f )
            {
                isflying = true;
                rigid.constraints = RigidbodyConstraints2D.None;
                rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
                Vector3 pointtarget = new Vector3(Po.transform.position.x, -43, 0);
                transform.position = Vector3.MoveTowards(transform.position, pointtarget, 0.7f);
                //transform.GetComponent<Rigidbody2D>().fixedAngle = true;
            }
        }
        if (isflying == true)
        {
            if (transform.position.y < -39)
            {
                think = 1;
                isjump = true;
                Debug.Log("1");
                if (isBerserk == true)
                {
                    Invoke("LightOn", 0.1f);
                    Invoke("LightOff", 0.4f);
                }
                Invoke("fall", 1f);
            }
        }
    }

    void fall()
    {
        Debug.Log("asdsad");
        isflying = false;
        Invoke("ThinkTime", 1);
        Invoke("AttackCoolTime", 2);
    }

    void AttackCoolTime()               //공격 쿨타임
    {
        isjump = false;
        tb = false;
    }

    void BerserkTime()
    {
        Invoke("BerserkDelay", 1);
    }

    void BerserkDelay()
    {
        Berserk = true;
    }

    void LightOn()
    {

        lightR.SetActive(true);
        lightL.SetActive(true);
        lightL.GetComponent<BoxCollider2D>().enabled = true;
        lightPar1.Play();
        lightPar2.Play();
    }

    void LightOff()
    {
        lightR.SetActive(false);
        lightL.SetActive(false);
        lightL.GetComponent<BoxCollider2D>().enabled = false;
        lightPar1.Stop();
        lightPar2.Stop();
    }
}