using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaFrogDouble : NinjaFrog
{
    private bool isMove = false;
    private bool isDoubleAttack = false;
    public int doubleType;
    public int Rplace;
    public GameObject PDagger;
    // Start is called before the first frame update
    void Start()
    {
        hide = 0;
        DarkSite1();
        bulletSpeed = 12;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        UpdateTarget();
        if (Health <= 0)
        {
            Invoke("MonsterDeath", 0.4f);
        }
        if (isMove == true && isDoubleAttack == false && doubleType == 1)
        {
            Invoke("DoubleAttack", 1);
            isDoubleAttack = true;
        }
        if(doubleType == 2 && isMove == true)
        {
            if (isDoubleAttack == false)
            {
                Invoke("DoubleAttack", 1);
                isDoubleAttack = true;
            }
        }
    }
    void DarkSite1()
    {
        hide = hide + 0.1f;
        spriteRenderer.color = new Color(1, 0.7f, 0.7f, hide);
        if (hide < 1)
        {
            Invoke("DarkSite1", 0.05f);
        }
        else
        {
            Debug.Log("등장끝");
            Invoke("MoveControll", 0.5f);
            if (doubleType == 2)
            {
                Invoke("DarkSite2", 3);
            }
        }
    }

    void DarkSite2()             //은신 
    {
        isMove = false;
        hide = hide - 0.1f;
        spriteRenderer.color = new Color(1, 0.7f, 0.7f, hide);
        if(hide > 0)
        {
            Invoke("DarkSite2", 0.05f);
        }
        else
        {
            Invoke("DarkSite1", 5);
            Rplace = Random.Range(1, 5);
            switch (Rplace)
            {
                case 1:
                    transform.position = new Vector2(192, 82);
                    break;
                case 2:
                    transform.position = new Vector2(182, 86);
                    break;
                case 3:
                    transform.position = new Vector2(171, 86);
                    break;
                case 4:
                    transform.position = new Vector2(167, 82);
                    break;
            }
        }
    }

    void DoubleAttack()
    {
        if (doubleType == 1)
        {
            anim.SetTrigger("isRoll");
            if (spriteRenderer.flipX == true)
            {
                rigid.AddForce(Vector2.left * 9, ForceMode2D.Impulse);
                spriteRenderer.flipX = false;
            }
            else
            {
                rigid.AddForce(Vector2.right * 9, ForceMode2D.Impulse);
                spriteRenderer.flipX = true;
            }
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
            Invoke("DoubleAttackTime", 1);
        }
        else if(doubleType == 2)
        {
            GameObject Ebullet = Instantiate(Dagger, transform.position, transform.rotation);
            Rigidbody2D brigid = Ebullet.GetComponent<Rigidbody2D>();
            Vector2 bdir = (Pt.position - transform.position).normalized;
            brigid.AddForce(bdir * bulletSpeed, ForceMode2D.Impulse);
            Invoke("DoubleAttackTime", 1);
        }
    }

    void DoubleAttackTime()
    {
        isDoubleAttack = false;
    }

    void MoveControll()
    {
        isMove = true;
    }
    void MonsterDeath()
    {
        PDagger.transform.position = transform.position;
        PDagger.SetActive(true);
        Destroy(gameObject);
    }
}
