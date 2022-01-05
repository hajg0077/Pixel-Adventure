using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaFrog : Enemy
{
    public float Pdistance;
    private bool isAttack = false;
    private bool appear = false;
    private bool appearEnd = false;

    public int RandomNumber;
    public int think;
    public int bulletSpeed;

    //은신
    public float hide;
    private bool isDarkSite = false;
    private bool StopDarkSite = false;

    private bool isPattern1 = false;
    private bool Pattern1Delay = false;

    private bool isBasic = false;       //기본패턴
    private bool isBasicDarkSite = false;

    public GameObject[] Double;
    public GameObject Dagger;
    public GameObject bosshp;

    void Start()
    {
        hide = 1;
        MonsterType = 2;
        think = 0;
        bulletSpeed = 15;
    }
    void FixedUpdate()
    {
        UpdateTarget();
        Pdistance = Mathf.Abs(Et.x - Pt.position.x);
        if(Et.x - Pt.position.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        if(Et.x - Pt.position.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        if (Pdistance < 24 && appear == false && Pt.position.y > 80)         //등장 로직
        {
            Invoke("AppearTime", 4);
            appear = true;
        }
       //-------------------------------------------------------------------------//

        if (isPattern1 == true && Pattern1Delay == false)             //광폭화 패턴 1
        {
            Invoke("Pattern1",1);
            Pattern1Delay = true;
        }

        
        //------------------------------------------------------------------------//  기본로직
        if(appear == true && appearEnd == true)
        {
            if (rigid.velocity.y < 0)           //바닥에 닿을 경우
            {
                Debug.DrawRay(rigid.position, Vector3.down * 2 , new Color(0, 1, 0));

                RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down * 2f, 2, LayerMask.GetMask("Platform"));

                if (rayHit.collider != null)
                {
                    if (rayHit.distance < 1.5f)
                    anim.SetBool("isJumping", false);
                    anim.SetBool("isAppear", false);
                }
            }
        
            if (isBasic == true && isBasicDarkSite == false)         //기본 패턴 들
            {
                if (isAttack == false)
                {
                    Attack();
                }
            }
            
        }
        //버서크 타임
        if (Health < 500 && Berserk == false && isBerserk == false)
        {
            Invoke("BerserkTime", 10);
            isBerserk = true;
            isBasic = false;
            StopDarkSite = true;
        }
        else if (Health < 500 && Berserk == false && isBerserk == true)
        {

        }

    }
    //------------------------------------------------------------------------//  처음 시작 관련
    void StartDelay()               //개구리 행동시작, 생각 시작
    {
        appearEnd = true;
        Invoke("DarkSiteOn", 3);
        isBasic = true;
    }

    void AppearTime()
    {
        rigid.velocity = new Vector2(-4, 13);
        anim.SetBool("isAppear", true);
        Invoke("AppearTimeEnd", 1.5f);
    }

    void AppearTimeEnd()
    {
        anim.SetBool("isAppear", false);
        Invoke("StartDelay", 8);
        bosshp.SetActive(true);
    }
    //------------------------------------------------------------------------//  기본로직
    void ThinkTime()
    {
        think++;
        Debug.Log(think);
        if (think < 5)
        {
            Invoke("DarkSiteOn", 5);
        }
        else
        {
            isAttack = false;
            Invoke("RollAttack", 2);
        }
        if (think == 8)
        {
            think = 0;
        }
        Debug.Log(think);
    }

    void BerserkTime()
    {
        StopDarkSite = false;
        Berserk = true;
        hide = 1;
        isDarkSite = false;
        isBasic = false;
        DarkSiteOn();
    }
    //------------------------------------------------------------------------//  기본 공격 로직

    void Attack()
    {
        if (think < 5)
        {
            isAttack = true;
            GameObject Ebullet = Instantiate(Dagger, transform.position, transform.rotation);
            Rigidbody2D brigid = Ebullet.GetComponent<Rigidbody2D>();
            Vector2 bdir = (Pt.position - transform.position).normalized;
            brigid.AddForce(bdir * bulletSpeed, ForceMode2D.Impulse);
            Invoke("AttackDelay", 1);
        }
        else
        {
            if (transform.position.y > 89)
            {
                isAttack = true;
                Debug.Log("발사");
                GameObject Ebullet1 = Instantiate(Dagger, transform.position, transform.rotation);
                Rigidbody2D brigid1 = Ebullet1.GetComponent<Rigidbody2D>();
                GameObject Ebullet2 = Instantiate(Dagger, transform.position, transform.rotation);
                Rigidbody2D brigid2 = Ebullet2.GetComponent<Rigidbody2D>();
                GameObject Ebullet3 = Instantiate(Dagger, transform.position, transform.rotation);
                Rigidbody2D brigid3 = Ebullet3.GetComponent<Rigidbody2D>();
                GameObject Ebullet4 = Instantiate(Dagger, transform.position, transform.rotation);
                Rigidbody2D brigid4 = Ebullet4.GetComponent<Rigidbody2D>();
                GameObject Ebullet5 = Instantiate(Dagger, transform.position, transform.rotation);
                Rigidbody2D brigid5 = Ebullet5.GetComponent<Rigidbody2D>();
                GameObject Ebullet6 = Instantiate(Dagger, transform.position, transform.rotation);
                Rigidbody2D brigid6 = Ebullet6.GetComponent<Rigidbody2D>();
                GameObject Ebullet7 = Instantiate(Dagger, transform.position, transform.rotation);
                Rigidbody2D brigid7 = Ebullet7.GetComponent<Rigidbody2D>();
                GameObject Ebullet8 = Instantiate(Dagger, transform.position, transform.rotation);
                Rigidbody2D brigid8 = Ebullet8.GetComponent<Rigidbody2D>();
                brigid1.AddForce(Vector2.up * bulletSpeed, ForceMode2D.Impulse);
                brigid2.AddForce((Vector2.up + Vector2.right) * bulletSpeed, ForceMode2D.Impulse);
                brigid3.AddForce((Vector2.up + Vector2.left) * bulletSpeed, ForceMode2D.Impulse);
                brigid4.AddForce(Vector2.right * bulletSpeed, ForceMode2D.Impulse);
                brigid5.AddForce(Vector2.left * bulletSpeed, ForceMode2D.Impulse);
                brigid6.AddForce((Vector2.down + Vector2.right) * bulletSpeed, ForceMode2D.Impulse);
                brigid7.AddForce((Vector2.down + Vector2.left) * bulletSpeed, ForceMode2D.Impulse);
                brigid8.AddForce(Vector2.down * bulletSpeed, ForceMode2D.Impulse);
            }
        }
    }

    void AttackDelay()
    {
        isAttack = false;
    }

    void RollAttack()
    {
        if (think == 5)
        {
            anim.SetBool("isAppear", true);
            rigid.velocity = new Vector2(-11, 13);
        }
        if (think == 6)
        {
            anim.SetBool("isAppear", true);
            rigid.velocity = new Vector2(11, 13);
        }
        ThinkTime();
    }


    //------------------------------------------------------------------------//  은신 로직
    void DarkSiteOn()             //은신 
    {
        if (Berserk == true)
        {
            hide = hide - 0.1f;
            spriteRenderer.color = new Color(1, 0.7f, 0.7f, hide);
        }
        else if (isBasic == true)
        {
            isBasicDarkSite = true;
            hide = hide - 0.1f;
            spriteRenderer.color = new Color(1, 1f, 1f, hide);
        }
        Invoke("DarkSiteControll", 0.05f);
    }
    void DarkSiteControll()             //은신 컨트롤
    {
        if (StopDarkSite == false)
        {
            if (isDarkSite == false)
            {
                if (hide < 0)
                {
                    isDarkSite = true;
                    if (Berserk == true)
                    {
                        spriteRenderer.color = new Color(1, 0.7f, 0.7f, 0);
                        Debug.Log("버서크은신끝");
                        transform.position = new Vector2(177, 95);
                        Invoke("DarkSiteOff", 4);
                        Invoke("DoubleAppear", 4);
                    }
                    else if (isBasic == true)
                    {
                        spriteRenderer.color = new Color(1, 1, 1, 0);
                        Debug.Log("은신끝");
                        switch (think)
                        {
                            case 0:
                                transform.position = new Vector2(192, 82);
                                break;
                            case 1:
                                transform.position = new Vector2(171, 86);
                                break;
                            case 2:
                                transform.position = new Vector2(180, 82);
                                break;
                            case 3:
                                transform.position = new Vector2(163, 82);
                                break;
                            case 4:
                                transform.position = new Vector2(183, 86);
                                break;
                        }
                        Invoke("DarkSiteOff", 1);
                    }
                }
                else
                {
                    Invoke("DarkSiteOn", 0.05f);
                }
            }
            else
            {
                if (hide > 1)
                {
                    if (Berserk == true)
                    {
                        spriteRenderer.color = new Color(1, 0.7f, 0.7f, 1);
                        isPattern1 = true;
                        Invoke("Double2Appear", 5);
                    }
                    else if (isBasic == true)
                    {
                        isDarkSite = false;
                        isBasicDarkSite = false;
                        ThinkTime();
                    }
                }
                else
                {
                    Invoke("DarkSiteOff", 0.05f);
                }
            }
        }
    }

    void DarkSiteOff()      //은신끝
    {
        hide = hide + 0.1f;
        if (Berserk == true)
        {
            spriteRenderer.color = new Color(1, 0.7f, 0.7f, hide);
        }
        else if(isBasic == true)
        {
            spriteRenderer.color = new Color(1, 1, 1, hide);
        }
        Invoke("DarkSiteControll", 0.05f);
    }


    //------------------------------------------------------------------------//  광폭화 세부 로직
    void DoubleAppear()     //광폭화 분신 소환
    {
        Double[0].SetActive(true);
        Double[1].SetActive(true);
    }

    void Pattern1()     //광폭화 패턴1
    {
        RandomNumber = Random.Range(1, 4);
        GameObject bullet1 = Instantiate(Dagger, transform.position, transform.rotation);
        Rigidbody2D brigid1 = bullet1.GetComponent<Rigidbody2D>();
        GameObject bullet2 = Instantiate(Dagger, transform.position, transform.rotation);
        Rigidbody2D brigid2 = bullet2.GetComponent<Rigidbody2D>();
        GameObject bullet3 = Instantiate(Dagger, transform.position, transform.rotation);
        Rigidbody2D brigid3 = bullet3.GetComponent<Rigidbody2D>();
        brigid1.AddForce(Vector2.down * bulletSpeed, ForceMode2D.Impulse);
        brigid2.AddForce((Vector2.down + Vector2.left * 0.2f * RandomNumber) * bulletSpeed, ForceMode2D.Impulse);
        brigid3.AddForce((Vector2.down + Vector2.right * 0.2f * RandomNumber) * bulletSpeed, ForceMode2D.Impulse);
        Invoke("Pattern1DelayControll", 1f);
    }

    void Pattern1DelayControll()
    {
        Pattern1Delay = false;
    }

    void Double2Appear()
    {
        Double[2].SetActive(true);
    }

    void BerserkEnd() 
    {
        isBasic = true;
        Berserk = false;
        think = 0;
        DarkSiteOn();
    }

    //------------------------------------------------------------------------//  콜라이더트리거
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullets")
        {
            Bullets bullets = collision.gameObject.GetComponent<Bullets>();
            Hit(bullets.Bulletdamage);
        }
        if (collision.gameObject.tag == "PDagger")
        {
            PDagger PDagger = collision.gameObject.GetComponent<PDagger>();
            Hit(PDagger.Damage);
            isPattern1 = false;
            Invoke("BerserkEnd", 5);
            anim.SetTrigger("Damaged");
            rigid.velocity = new Vector2(6, 10);
            Double[0].SetActive(false);
            Double[1].SetActive(false);
        }
    }
}
