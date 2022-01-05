using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : Enemy
{
    public GameObject bullet;
    public ParticleSystem ps1;
    public ParticleSystem[] fire;
    public GameObject[] gfire;
    public float bulletSpeed;
    public float LaserAngle = 0;
    public int p2con = 0;
    public int p4con = 0;
    private bool isLAControll = true;
    private bool isMove = false;
    private int bp1 = 30;
    private int bp2 = 4;
    // Start is called before the first frame update
    void Start()
    {
        UpdateTarget();
        ps1.Stop();
        for(int i=0; i<8; i++)
        {
            fire[i].Stop();
        }
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

    // Update is called once per frame
    void Update()
    {
        UpdateTarget();
        if (Pt.position.x > 305)
        {
            if(isMove == false)
            {
                isMove = true;
                Invoke("PatternStart", 5f);
            }
        }

        if (Health < 500 && Berserk == false && isBerserk == false)
        {
            StopCoroutine("Pattern");
            Invoke("BerserkTime", 5);
            isBerserk = true;
        }
        else if (Health < 500 && Berserk == false && isBerserk == true)
        {

        }
    }

    IEnumerator Pattern()
    {
        P4();
        yield return new WaitForSeconds(5f);
        P1();
        yield return new WaitForSeconds(1f);
        P1();
        yield return new WaitForSeconds(1f);
        P1();
        yield return new WaitForSeconds(1f);
        P1();
        yield return new WaitForSeconds(1f);
        P4();
        yield return new WaitForSeconds(5f);
        P3();
        yield return new WaitForSeconds(3f);
        P2();
        yield return new WaitForSeconds(2f);
        P2();
        yield return new WaitForSeconds(3f);
        P3();
        yield return new WaitForSeconds(3f);
        Again();
        yield break;
    }

    IEnumerator BerserkPattern()
    {
        BP1();
        yield return new WaitForSeconds(2f);
        BP3();
        yield return new WaitForSeconds(1f);
        BP2();
        yield return new WaitForSeconds(2f);
        BP3();
        yield return new WaitForSeconds(1f);
        BP1();
        yield return new WaitForSeconds(2f);
        BP3();
        yield return new WaitForSeconds(1f);
        BP2();
        yield return new WaitForSeconds(2f);
        BP3();
        yield return new WaitForSeconds(1f);
        BP1();
        yield return new WaitForSeconds(2f);
        BP3();
        yield return new WaitForSeconds(1f);
        BP2();
        yield return new WaitForSeconds(2f);
        BP3();
        yield return new WaitForSeconds(2f);
        Again();
        yield break;
    }

    void P1()       //탄환 큰 원
    {
        for(int i=0; i<bp1; i++)
        {
            GameObject sb = Instantiate(bullet, transform.position, transform.rotation); ;
            Rigidbody2D brigid1 = sb.GetComponent<Rigidbody2D>();
            Vector2 dirvec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / bp1),
                                         Mathf.Sin(Mathf.PI * 2 * i / bp1));
            brigid1.AddForce(dirvec.normalized * bulletSpeed, ForceMode2D.Impulse);
        }
    }
    void P2()   //탄환 플레이어방향
    {
        GameObject sb = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody2D brigid = sb.GetComponent<Rigidbody2D>();
        Vector2 bdir = (Pt.position - transform.position).normalized;
        brigid.AddForce(bdir * bulletSpeed, ForceMode2D.Impulse);
        p2con++;
        if(p2con < 3)
        {
            Invoke("P2", 0.3f);
        }
        else
        {
            p2con = 0;
        }
    }

    void P3()       //레이저 쏘기
    {
        ps1.Play();
        ps1.transform.Rotate(new Vector3(0, 0, LaserAngle));
        Invoke("LAControll", 1f);
    }

    void P4()       //탄환 랜덤으로 막 쏘기
    {
        int r;
        r = Random.Range(1, 50);
        GameObject sb = Instantiate(bullet, transform.position, transform.rotation); ;
        Rigidbody2D brigid1 = sb.GetComponent<Rigidbody2D>();
        Vector2 dirvec = new Vector2(Mathf.Cos(Mathf.PI * 2 * r / 50),
                                     Mathf.Sin(Mathf.PI * 2 * r / 50));
        brigid1.AddForce(dirvec.normalized * bulletSpeed, ForceMode2D.Impulse);
        p4con++;
        if (p4con < 100)
        {
            Invoke("P4", 0.04f);
        }
        else
        {
            p4con = 0;
        }
    }
    void LAControll()
    {
        if (LaserAngle < 180)
        {
            LaserAngle = LaserAngle + 1f;
            ps1.transform.Rotate(new Vector3(0, 0, 1));
            Invoke("LAControll", 0.01f);
        }
        else
        {
            ps1.Stop();
            LaserAngle = 0;
            ps1.transform.Rotate(new Vector3(0, 0, 180));
        }
    }

    void BerserkTime()
    {
        Berserk = true;
        StartCoroutine("BerserkPattern");
    }

    void BP1()
    {
        for (int i = 0; i < 4; i++)
        {
            gfire[i * 2].SetActive(true);
            fire[i * 2].Play();
        }
    }
    void BP2()
    {
        for (int i = 0; i < 4; i++)
        {
            gfire[i * 2 + 1].SetActive(true);
            fire[i * 2 + 1].Play();
        }
    }
    void BP3()
    {
        for (int i = 0; i < 8; i++)
        {
            fire[i].Stop();
            gfire[i].SetActive(false);
        }
    }
}
