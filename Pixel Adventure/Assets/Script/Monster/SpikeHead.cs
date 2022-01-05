using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : Enemy
{
    private int moveDirect; //1왼 2오 3위 3아래
    public GameObject Turtle;
    public GameObject bullet;
    public GameObject twins;
    public float bulletSpeed;
    public int p2con = 0;
    private int summonnum = 1;
    private int Brs = 0;
    private int Bsummonnum = 1;
    private bool ismove = false;
    // Start is called before the first frame update
    void Start()
    {
        MonsterType = 1;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTarget();
        if (Pt.position.x > 260 && ismove == false)
        {
            ismove = true;
            Invoke("PatternStart", 1f);
        }
        else if (ismove == true)
        {
            if (twins.activeSelf == false && Berserk == false)
            {
                Berserk = true;
                isBerserk = true;
                spriteRenderer.color = new Color(1, 0.7f, 0.7f, 1);
                StartCoroutine("BerserkPattern");
            }
            switch (moveDirect)
            {
                case 1:
                    if (Et.x <= 263)
                    {
                        moveDirect = 0;
                    }
                    else
                    {
                        rigid.velocity = new Vector2(-1 * monsterSpeed, rigid.velocity.y);
                    }
                    break;
                case 2:
                    if (Et.x >= 293)
                    {
                        moveDirect = 0;
                    }
                    else
                    {
                        rigid.velocity = new Vector2(1 * monsterSpeed, rigid.velocity.y);
                    }
                    break;
                case 3:
                    if (Et.y >= -61)
                    {
                        moveDirect = 0;
                    }
                    else
                    {
                        rigid.velocity = new Vector2(rigid.velocity.x, 1 * monsterSpeed);
                    }
                    break;
                case 4:
                    if (Et.y <= -75)
                    {
                        moveDirect = 0;
                    }
                    else
                    {
                        rigid.velocity = new Vector2(rigid.velocity.x, -1 * monsterSpeed);
                    }
                    break;
                case 5:
                    if (Et.x >= 278)
                    {
                        moveDirect = 0;
                    }
                    else
                    {
                        rigid.velocity = new Vector2(15, -7).normalized * monsterSpeed;

                    }
                    break;
                case 6:
                    if (Et.x <= 263)
                    {
                        moveDirect = 0;
                    }
                    else
                    {
                        rigid.velocity = new Vector2(-15, 7).normalized * monsterSpeed;
                    }
                    break;
                default:
                    rigid.velocity = Vector2.zero;
                    break;
            }
        }
    }
    void PatternStart()
    {
        StartCoroutine("Pattern");
    }
    void BPatternStart()
    {
        StartCoroutine("BerserkPattern");
    }
    void BAgain()
    {
        StopCoroutine("BerserkPattern");
        Invoke("BPatternStart", 1f);
    }
    void Again()
    {
        StopCoroutine("Pattern");
        Invoke("PatternStart", 1f);
    }
    IEnumerator Pattern()
    {
        anim.SetTrigger("isLRmove");
        yield return new WaitForSeconds(1f);
        MoveRight();
        yield return new WaitForSeconds(1f);
        BAttack();
        yield return new WaitForSeconds(4f);
        anim.SetTrigger("isTBmove");
        yield return new WaitForSeconds(1f);
        MoveBottom();
        yield return new WaitForSeconds(5f);
        anim.SetTrigger("isLRmove");
        yield return new WaitForSeconds(1f);
        MoveLeft();
        yield return new WaitForSeconds(5f);
        anim.SetTrigger("isTBmove");
        yield return new WaitForSeconds(1f);
        MoveTop();
        yield return new WaitForSeconds(5f);
        //----------1순
        anim.SetTrigger("isLRmove");
        yield return new WaitForSeconds(1f);
        MoveRight();
        yield return new WaitForSeconds(1f);
        BAttack();
        yield return new WaitForSeconds(4f);
        anim.SetTrigger("isTBmove");
        yield return new WaitForSeconds(1f);
        MoveBottom();
        yield return new WaitForSeconds(5f);
        anim.SetTrigger("isLRmove");
        yield return new WaitForSeconds(1f);
        MoveLeft();
        yield return new WaitForSeconds(5f);
        anim.SetTrigger("isTBmove");
        yield return new WaitForSeconds(1f);
        MoveTop();
        yield return new WaitForSeconds(5f);
        //----------2순
        MoveCenter();
        yield return new WaitForSeconds(5f);
        BAttack2();
        yield return new WaitForSeconds(3f);
        BAttack2();
        yield return new WaitForSeconds(3f);
        BAttack2();
        yield return new WaitForSeconds(3f);
        TurtleSummon();
        yield return new WaitForSeconds(5f);
        MoveReturn();
        yield return new WaitForSeconds(5f);
        //----------3순
        Again();
        yield break;
    }
    IEnumerator BerserkPattern()
    {
        BTurtleSummon();
        yield return new WaitForSeconds(5f);
        BAgain();
        yield break;
    }

    void MoveLeft()
    {
        moveDirect = 1;
    }
    void MoveRight()
    {
        moveDirect = 2;
    }
    void MoveTop()
    {
        moveDirect = 3;
    }
    void MoveBottom()
    {
        moveDirect = 4;
    }
    void MoveCenter()
    {
        moveDirect = 5;
    }
    void MoveReturn()
    {
        moveDirect = 6;
    }
    void BAttack()   //탄환 플레이어방향
    {
        GameObject sb = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody2D brigid = sb.GetComponent<Rigidbody2D>();
        Vector2 bdir = (Pt.position - transform.position).normalized;
        brigid.AddForce(bdir * bulletSpeed, ForceMode2D.Impulse);
        p2con++;
        if (p2con < 5)
        {
            Invoke("BAttack", 0.3f);
        }
        else
        {
            p2con = 0;
        }
    }
    void BAttack2()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject sb = Instantiate(bullet, transform.position, transform.rotation); ;
            Rigidbody2D brigid1 = sb.GetComponent<Rigidbody2D>();
            Vector2 dirvec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / 30),
                                         Mathf.Sin(Mathf.PI * 2 * i / 30));
            brigid1.AddForce(dirvec.normalized * bulletSpeed, ForceMode2D.Impulse);
        }
    }

    void TurtleSummon()
    {
        if (summonnum == 1)
        {
            GameObject Summon1 = Instantiate(Turtle, transform.position, transform.rotation);
            Rigidbody2D sr1 = Summon1.GetComponent<Rigidbody2D>();
            sr1.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
        }
        if (summonnum == 2)
        {
            GameObject Summon2 = Instantiate(Turtle, transform.position, transform.rotation);
            Rigidbody2D sr2 = Summon2.GetComponent<Rigidbody2D>();
            sr2.AddForce(Vector2.up * 8 + Vector2.right * 5, ForceMode2D.Impulse);
        }
        if (summonnum == 3)
        {
            GameObject Summon3 = Instantiate(Turtle, transform.position, transform.rotation);
            Rigidbody2D sr3 = Summon3.GetComponent<Rigidbody2D>();
            sr3.AddForce(Vector2.up * 8 + Vector2.left * 5, ForceMode2D.Impulse);
        }
        summonnum++;
        if (summonnum > 3)
        {
            summonnum = 1;
        }
        else
        {
            Invoke("TurtleSummon", 1f);
        }
    }
    void BTurtleSummon()
    {
        Brs = Random.Range(-10, 11);
        if (Bsummonnum == 1)
        {
            GameObject BSummon1 = Instantiate(Turtle, transform.position, transform.rotation);
            Rigidbody2D Bsr1 = BSummon1.GetComponent<Rigidbody2D>();
            Bsr1.AddForce(Vector2.up * 8 + Vector2.right * Brs, ForceMode2D.Impulse);
        }
        if (Bsummonnum == 2)
        {
            GameObject BSummon2 = Instantiate(Turtle, transform.position, transform.rotation);
            Rigidbody2D Bsr2 = BSummon2.GetComponent<Rigidbody2D>();
            Bsr2.AddForce(Vector2.up * 8 + Vector2.right * Brs, ForceMode2D.Impulse);
        }
        if (Bsummonnum == 3)
        {
            GameObject BSummon3 = Instantiate(Turtle, transform.position, transform.rotation);
            Rigidbody2D Bsr3 = BSummon3.GetComponent<Rigidbody2D>();
            Bsr3.AddForce(Vector2.up * 8 + Vector2.right * Brs, ForceMode2D.Impulse);
        }
        Bsummonnum++;
        if (Bsummonnum > 3)
        {
            Bsummonnum = 1;
        }
        else
        {
            Brs = 0;
            Invoke("BTurtleSummon", 1f);
        }
    }
}
