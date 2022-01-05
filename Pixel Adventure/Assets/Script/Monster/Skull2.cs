using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull2 : Enemy
{
    public GameObject bullet;
    public ParticleSystem ps1;
    public ParticleSystem ps2;
    public GameObject ops1;
    public GameObject ops2;
    public ParticleSystem[] fire;
    public GameObject[] gfire;

    public float bulletSpeed;
    private int rsp = 0;
    private int fireRandom1 = 0;
    private int fireRandom2 = 0;
    public int p1con = 0;
    private bool ismove = false;
    private bool isp3 = false;
    public float LaserAngle = 0;
    void Start()
    {
        ps1.Stop();
        ps2.Stop();
        UpdateTarget();
        Invoke("SP", 1f);
        for (int i = 0; i < 8; i++)
        {
            gfire[i].SetActive(false);
            fire[i].Stop();
        }
    }

    void Update()
    {
        if (spriteRenderer.flipX == false && ismove == true)
        {
            if(isp3 == true)
            {
                if(Et.x < 338)
                {
                    rigid.velocity = new Vector2(1 * monsterSpeed, rigid.velocity.y);
                }
                else
                {
                    rigid.velocity = new Vector2(0, rigid.velocity.y);
                    isp3 = false;
                    ismove = false;
                    spriteRenderer.flipX = true;
                }
            }
            else
            {
                rigid.velocity = new Vector2(1 * monsterSpeed, rigid.velocity.y);
            }
        }
        else if (spriteRenderer.flipX == true && ismove == true)
        {
            if (isp3 == true)
            {
                if (Et.x > 326)
                {
                    rigid.velocity = new Vector2(-1 * monsterSpeed, rigid.velocity.y);
                }
                else
                {
                    rigid.velocity = new Vector2(0, rigid.velocity.y);
                    isp3 = false;
                    ismove = false;
                    spriteRenderer.flipX = false;
                }
            }
            else
            {
                rigid.velocity = new Vector2(-1 * monsterSpeed, rigid.velocity.y);
            }
        }
        UpdateTarget();
    }
    void PatternStart()
    {
        StartCoroutine("Pattern");
    }
    void Again()
    {
        StopCoroutine("Pattern");
        Invoke("PatternStart", 1f);
    }

    IEnumerator Pattern()
    {
        P1();
        yield return new WaitForSeconds(5f);
        P1();
        yield return new WaitForSeconds(5f);
        P1();
        yield return new WaitForSeconds(5f);
        P1();
        yield return new WaitForSeconds(5f);
        P3();
        yield return new WaitForSeconds(0.5f);
        P3();
        yield return new WaitForSeconds(2.5f);
        P2();
        yield return new WaitForSeconds(5f);
        P3();
        yield return new WaitForSeconds(0.5f);
        P3();
        yield return new WaitForSeconds(2.5f);
        Again();
        yield break;
    }

    void P1()
    {
        ismove = true;
        GameObject sb = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody2D brigid = sb.GetComponent<Rigidbody2D>();
        brigid.AddForce(Vector2.down * bulletSpeed, ForceMode2D.Impulse);
        p1con++;
        if (p1con < 10)
        {
            Invoke("P1", 0.3f);
        }
        else
        {
            ismove = false;
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            if (spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
            }
            else if (spriteRenderer.flipX == false)
            {
                spriteRenderer.flipX = true;
            }
            p1con = 0;
        }
    }

    void P2()       //레이저 쏘기
    {
        ops1.SetActive(true);
        ops2.SetActive(true);
        ps1.Play();
        ps1.transform.Rotate(new Vector3(0, 0, LaserAngle));
        ps2.Play();
        ps2.transform.Rotate(new Vector3(0, 0, LaserAngle));
        Invoke("LAControll", 1f);
    }

    void P3()
    {
        ismove = true;
        isp3 = true;
        for (int i = 0; i < 30; i++)
        {
            GameObject sb = Instantiate(bullet, transform.position, transform.rotation); ;
            Rigidbody2D brigid1 = sb.GetComponent<Rigidbody2D>();
            Vector2 dirvec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / 30),
                                         Mathf.Sin(Mathf.PI * 2 * i / 30));
            brigid1.AddForce(dirvec.normalized * bulletSpeed, ForceMode2D.Impulse);
        }
    }

    void LAControll()
    {
        if (LaserAngle < 180)
        {
            LaserAngle = LaserAngle + 1f;
            ps1.transform.Rotate(new Vector3(0, 0, 1));
            ps2.transform.Rotate(new Vector3(0, 0, 1));
            Invoke("LAControll", 0.01f);
        }
        else
        {
            ps1.Stop();
            ps2.Stop();
            LaserAngle = 0;
            ps1.transform.Rotate(new Vector3(0, 0, 180));
            ps2.transform.Rotate(new Vector3(0, 0, 180));
            ops1.SetActive(false);
            ops2.SetActive(false);
        }
    }
    void SP()       // 스타트 패턴 탄환 랜덤으로 막 쏘기
    {
        int r;
        r = Random.Range(1, 50);
        GameObject sb = Instantiate(bullet, transform.position, transform.rotation); ;
        Rigidbody2D brigid1 = sb.GetComponent<Rigidbody2D>();
        Vector2 dirvec = new Vector2(Mathf.Cos(Mathf.PI * 2 * r / 50),
                                     Mathf.Sin(Mathf.PI * 2 * r / 50));
        brigid1.AddForce(dirvec.normalized * bulletSpeed, ForceMode2D.Impulse);
        rsp++;
        if (rsp < 200)
        {
            Invoke("SP", 0.02f);
        }
        else
        {
            rsp = 0;
            Again();
            Invoke("FireStart", 2f);
        }
    }

    void FireStart()
    {
        fireRandom1 = Random.Range(0, 8);
        fireRandom2 = Random.Range(0, 8);
        while(fireRandom1 == fireRandom2)
        {
            fireRandom2 = Random.Range(0, 8);
        }
        gfire[fireRandom1].SetActive(true);
        fire[fireRandom1].Play();
        gfire[fireRandom2].SetActive(true);
        fire[fireRandom2].Play();
        Invoke("FireStop", 3f);
    }

    void FireStop()
    {
        gfire[fireRandom1].SetActive(false);
        fire[fireRandom1].Stop();
        gfire[fireRandom2].SetActive(false);
        fire[fireRandom2].Stop();
        Invoke("FireStart", 2f);
    }

}
