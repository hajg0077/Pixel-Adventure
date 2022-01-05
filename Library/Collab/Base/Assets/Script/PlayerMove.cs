using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public float maxShotDelay;
    public float curShotDelay;
    public float bulletSpeed;
    public float RollPower;
    public float maxRollDelay;
    public float curRollDelay;
    public float StartHealth;
    public float Health;


    public GameObject HealthBar;
    public GameObject bullets;
    public GameObject GunPointRight;
    public GameObject GunPointLeft;

    public GameManager gameManager;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    float h;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (gameManager.PlayerStop == true)
        {
            return;
        }
        else
        {
            //Stop Speed
            if (anim.GetBool("isRolling") == true)
            {
                return;
            }
            else
            {
                if (Input.GetButtonUp("Horizontal"))
                {
                    rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
                }

                //방향 전환
                if (Input.GetButton("Horizontal"))
                    spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            }
            //애니메이션
            if (Mathf.Abs(rigid.velocity.x) < 0.3)
            {
                anim.SetBool("isRuning", false);
            }
            else
            {
                anim.SetBool("isRuning", true);
            }

            if (anim.GetBool("isRolling") == true)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    return;
                }
            }
            if (anim.GetBool("isJumping") == true)
            {
                if (Input.GetButtonDown("Roll"))
                {
                    return;
                }
            }

            Jump();
            Fire();
            Reload();
            Roll();
            Rolltime();
        }

    }
    void FixedUpdate()
    {
        if (gameManager.PlayerStop == true)
        {
            return;
        }
        else
        {
            //움직임 조작
            if (anim.GetBool("isRolling") == true)
            {
                return;
            }
            else
            {
                h = Input.GetAxisRaw("Horizontal");
                rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
                if (rigid.velocity.x > maxSpeed) // 오른쪽 최대 속도
                    rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);

                else if (rigid.velocity.x < maxSpeed * (-1)) // 왼쪽 최대 속도
                    rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
            }
            //Landing Platform
            if (rigid.velocity.y < 0)
            {
                Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

                RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platfrom"));

                if (rayHit.collider != null)
                {
                    if (rayHit.distance < 1f)
                        anim.SetBool("isJumping", false);
                }
            }
        }
    }
     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            MonsterMove Monster = collision.gameObject.GetComponent<MonsterMove>();
            OnDamaged(collision.transform.position, Monster.dmg);
            
        }
        
    }


    void OnDamaged(Vector2 targetPos, float damage) //피격 시 무적 상태
    {
        Health = Health - damage;
        HealthBar.GetComponent<Image>().fillAmount = Health / StartHealth;
        if (Health <= 0)
        {
            Destroy(gameObject);            //죽는 모션 없시 먼저 죽을시 따로 빼서 함수 만들고 iNVOKE사용해서 디스트로이해줘야됨
        }
        gameObject.layer = 11;

        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //반작용
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1)*7 ,ForceMode2D.Impulse);

        //맞는 모션
        anim.SetTrigger("doDamaged");

        Invoke("OffDamaged", 1);
      
    }

    void OffDamaged() //무적시간 풀기
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    void Fire() //공격
    {
        if (!Input.GetButton("Fire1"))
            return;

        if (curShotDelay < maxShotDelay)
            return;



        if (spriteRenderer.flipX == true)
        {
            GameObject bullet = Instantiate(bullets, GunPointLeft.transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            rigid.AddForce(Vector2.left * bulletSpeed, ForceMode2D.Impulse);

        }

        else
        {
            GameObject bullet = Instantiate(bullets, GunPointRight.transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            rigid.AddForce(Vector2.right * bulletSpeed, ForceMode2D.Impulse);
        }
            curShotDelay = 0;
        


    }

    void Roll() // 구르기
    {
        if (Input.GetButtonDown("Roll"))
        {
            gameObject.layer = 11;
            if (curRollDelay < maxRollDelay)
            {
                return;
            }
            else
            {
                anim.SetBool("isRolling", true);
                if (spriteRenderer.flipX == true)
                {
                    anim.SetBool("isRuning", false);
                    rigid.AddForce(Vector2.left * RollPower, ForceMode2D.Impulse);
                }
                else
                {
                    anim.SetBool("isRuning", false);
                    rigid.AddForce(Vector2.right * RollPower, ForceMode2D.Impulse);
                }
                anim.SetTrigger("isRoll");
                Invoke("deActivate", 0.5f);
                curRollDelay = 0;
                
            }
            Invoke("OffDamaged", 0.75f);
        }
    }

    void Rolltime()
    {
        curRollDelay += Time.deltaTime;
    }

    void deActivate()
    {
        anim.SetBool("isRolling", false);
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    //void OnTriggerStay2D(Collider2D collision) //팬에 닿으면 위로 쭉 날라가는 로직
    //{
    //    if (collision.tag == "Pan")
    //    {
    //        rigid.AddForce(Vector2.up * 28, ForceMode2D.Impulse);
    //    }
    //}


}