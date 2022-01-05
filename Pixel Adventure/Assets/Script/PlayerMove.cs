using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;

    public float maxSpeed;
    public float jumpPower;
    public float maxShotDelay;
    public float curShotDelay;
    public float bulletSpeed;
    public float RollPower;
    public float maxRollDelay;
    public float curRollDelay;

    public float StartHealth;       //최대 hp
    public float Health;            // 현재 hp
    public float StartMp;
    public float Mp;
    public float needExp;              //다음렙업을 위한 exp
    public float currentEXP;                 //지금 exp

    public int Ability;             //스탯 포인트
    public int DEF;
    public int STR;
    public float AS;
    public int character_Lv;        //렙

    private float bosstime;

    private bool isControll = true;

    private bool isScalding = false;
    private int ScaldingCheck = 0;
    private bool ScaldingOLCheck = false;

    private bool isBleeding = false;
    private int BleedingCheck = 0;
    private bool BleedingOLCheck = false;

    private bool isPoison = false;
    private int PoisonCheck = 0;
    private bool PoisonOLCheck = false;

    public bool isPlayerDie = false;      //플레이어 사망

    public GameObject HealthBar;
    public GameObject StateHealthBar;
    public GameObject MpBar;
    public GameObject StateMpBar;
    public GameObject ExpBar;
    public GameObject StateExpBar;

    public GameObject bullets;
    public GameObject GunPointRight;
    public GameObject GunPointLeft;
    public GameObject bosszone;
    public ParticleSystem ps;

    public GameObject prefabs_Floating_text;
    public GameObject parent;

    public Text HpStateText;
    public Text HpStatePoint;
    public Text MpStateText;            //스탯 위쪽마나
    public Text MpStatePoint;           //스탯창 아랫쪽 올리는 마나
    public Text STRStatePoint;          //스탯창에 힘포인트
    public Text DEFStatePoint;          //스탯창에 방어력포인트
    public Text ASStatePoint;
    public Text MainHpText;             //창에 있는 피
    public Text MainMpText;
    public Text AbilityPoint;           //스탯포인트
    public Text LvText;                 //렙 텍스트
    public Text ExpStateText;           //스테이트창에 경험치
    public Text EXPMainText;            //메인 창에 경험치

    public GameManager gameManager;

    Rigidbody2D rigid;
    public SpriteRenderer spriteRenderer;
    Animator anim;

    float h;


    void Start()
    {
        instance = this;
        ps.Stop();
    }

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
        else if (isPlayerDie == true)
        {
            return;
        }
        else
        {
            if (isControll == false)
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
                Lv_up();
                statecontrol();

            }
        }

    }

    void statecontrol()
    {
        HealthBar.GetComponent<Image>().fillAmount = Health / StartHealth;
        StateHealthBar.GetComponent<Image>().fillAmount = Health / StartHealth;
        MainHpText.text = Health + "/" + StartHealth;
        HpStateText.text = Health + "/" + StartHealth;

        EXPMainText.text = currentEXP + "/" + needExp;
        ExpStateText.text = currentEXP + "/" + needExp;
        ExpBar.GetComponent<Image>().fillAmount = currentEXP / needExp;
        StateExpBar.GetComponent<Image>().fillAmount = currentEXP / needExp;

        MainMpText.text = Mp + "/" + StartMp;
        MpStateText.text = Mp + "/" + StartMp;
        MpBar.GetComponent<Image>().fillAmount = Mp / StartMp;
        StateMpBar.GetComponent<Image>().fillAmount = Mp / StartMp;
                           

        HpStatePoint.text = " " + StartHealth;
        MpStatePoint.text = " " + StartMp;
        STRStatePoint.text = " " + STR;
        DEFStatePoint.text = " " + DEF;
        ASStatePoint.text = " " + AS;

        AbilityPoint.text = " " + Ability;
        LvText.text = "Lv. " + character_Lv;


    }

    void Lv_up()
    {
     ;
        if (currentEXP >= needExp)
        {
            currentEXP = currentEXP - needExp;
            character_Lv++;
            Ability = Ability + 3;


            for (int i = 1; i <= 10; i++)
            {
                if (character_Lv == i)
                {
                    needExp = needExp + (5 * i);
                }
            }
            Vector3 vector = this.transform.position;
            vector.y += 3f;

            GameObject clone = Instantiate(prefabs_Floating_text, vector, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingText>().text.text = "렙업";
            clone.GetComponent<FloatingText>().text.fontSize = 15;
            clone.transform.SetParent(parent.transform);
        }
    }

    void FixedUpdate()
    {
        if (gameManager.PlayerStop == true)
        {
            return;
        }
        else if (isPlayerDie == true)
        {
            return;
        }
        else
        {
            if (isControll == false)
            {
                if (transform.position.x <= 165)
                {
                    rigid.velocity = new Vector2(2, rigid.velocity.y);
                }
                else
                {
                    anim.SetBool("isRuning", false);
                    Invoke("controllreset", 13);
                }
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

                    RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
                    RaycastHit2D rayHit2 = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Elevator"));

                    if (rayHit.collider != null)
                    {
                        if (rayHit.distance < 1f)
                            anim.SetBool("isJumping", false);
                    }
                    if (rayHit2.collider != null)
                    {
                        if (rayHit2.distance < 1f)
                            anim.SetBool("isJumping", false);
                    }
                }
            }
        }
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            OnDamaged(collision.transform.position, enemy.dmg);
        }
        if (collision.gameObject.name == "Bat")
        {
            Bat bat = collision.gameObject.GetComponent<Bat>();
            OnDamaged(collision.transform.position, bat.dmg);
        }    

        if (collision.gameObject.name == "Ghost")
        {
            Ghost ghost = collision.gameObject.GetComponent<Ghost>();
            OnDamaged(collision.transform.position, ghost.dmg);
        } 
        if (collision.gameObject.tag == "bosszone")
        {
            playerControl();
        }
        if (collision.gameObject.tag == "edge")
        {
            PlayerDie();
        }
        if (collision.gameObject.name == "Slime")
        {
            if (PoisonCheck != 0)
            {
                PoisonOLCheck = true;
                Invoke("PoisonControll", 0.5f);
            }
            else
            {
                PoisonControll();
            }
        }
        if (collision.gameObject.tag == "Spike")
        {
            TrapDameged();
            Debug.Log("TD");
            if (BleedingCheck != 0)
            {
                BleedingOLCheck = true;
                Invoke("CoroutineControll", 0.5f);
                Debug.Log("재시작");
            }
            else
            {
                CoroutineControll();
                Debug.Log("BD");
            }
        }
        if (collision.gameObject.tag == "Trap")
        {
            TrapDameged();
            Debug.Log("TD");
            if (BleedingCheck != 0)
            {
                BleedingOLCheck = true;
                Invoke("CoroutineControll", 0.5f);
                Debug.Log("재시작");
            }
            else
            {
                CoroutineControll();
                Debug.Log("BD");
            }
        }
        if (collision.gameObject.tag == "Wheel")
        {
                Health = Health - 50;
                Destroy(GameObject.Find("Wheel"));
                TrapDameged();
        }
       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            EnemyBullets Ebullets = collision.gameObject.GetComponent<EnemyBullets>();
            OnDamaged(Ebullets.transform.position, Ebullets.Bulletdamage);
        }     
        if (collision.gameObject.tag == "Lava")
        {
            PlayerDie();
        }
        if (collision.gameObject.tag == "FireAttack")
        {
            SkullFire SF = collision.gameObject.GetComponent<SkullFire>();
            OnDamaged(collision.transform.position, SF.dmg);
        }
        if (collision.gameObject.tag == "Laser")
        {
            Laser laser = collision.gameObject.GetComponent<Laser>();
            OnDamaged(collision.transform.position, laser.dmg);
        }
        if (collision.gameObject.name == "Fire")
        {           
            FireDameged();         
            if (ScaldingCheck != 0)
            {
                ScaldingOLCheck = true;
                Invoke("FireControll", 0.5f);              
            }
            else
            {
                FireControll();              
            }      
        }
        // ---------------------------------------------------------------불 끝
        if (collision.gameObject.name == "FireRight") //인디언 불에 닿았을때 
        {           
              FireAttack FireRight = collision.gameObject.GetComponent<FireAttack>();
              OnDamaged(FireRight.transform.position, FireRight.dmg);       
            if (ScaldingCheck != 0)
            {
                ScaldingOLCheck = true;
                Invoke("FireControll", 0.5f);
            }
            else
            {
                FireControll();
            }
        }
        if (collision.gameObject.name == "FireLeft")
        {
         
              FireAttack FireLeft = collision.gameObject.GetComponent<FireAttack>();
              OnDamaged(FireLeft.transform.position, FireLeft.dmg);        
            if (ScaldingCheck != 0)
            {
                ScaldingOLCheck = true;
                Invoke("FireControll", 0.5f);
            }
            else
            {
                FireControll();
            }
        } // -------------------------------------------------------------불 끝


        if (collision.gameObject.tag == "Thunder")
        {
            Thunder th = collision.gameObject.GetComponent<Thunder>();
            OnDamaged(th.transform.position, th.Dmg);
        }
    }

    void CoroutineControll()
    {
        isBleeding = true;
        BleedingCheck = 0;
        StartCoroutine("Bd");
        ps.Play();
        Debug.Log("ps");
        Debug.Log("재시작");
    }

    void FireControll()
    {
        isScalding = true;
        ScaldingCheck = 0;
        StartCoroutine("Scald");       
    }
    void PoisonControll()
    {
        isPoison = true;
        PoisonCheck = 0;
        StartCoroutine("Poison");
    }

    void TrapDameged() //트랩에 맞으면 뒤로 반작용하는 로직
    {
        if (Health <= 0)
        {
            PlayerDie();            //죽는 모션 없시 먼저 죽을시 따로 빼서 함수 만들고 iNVOKE사용해서 디스트로이해줘야됨
        }
        gameObject.layer = 11;
        //Health = Health - 3;         //스파이크에 닿았을때 데미지

        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //반작용
        int dirc = transform.position.x - transform.position.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

        //맞는 모션
        anim.SetTrigger("doDamaged");

        Invoke("OffDamaged", 2);

        anim.SetBool("doDamaged", true);
    }

    void FireDameged() //불 트랩에 닿으면 반응하는 로직
    {
        if (Health <= 0)
        {
            PlayerDie();            //죽는 모션 없시 먼저 죽을시 따로 빼서 함수 만들고 iNVOKE사용해서 디스트로이해줘야됨
        }
        gameObject.layer = 11;
           
        //반작용
        int dirc = transform.position.x - transform.position.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

        //맞는 모션
        anim.SetTrigger("doDamaged");

        Invoke("OffDamaged", 2);

        anim.SetBool("doDamaged", true);
    }

    public void ScaldingStop()
    {
        isScalding = false;
        ScaldingCheck = 0;
        Debug.Log(ScaldingCheck);
    }

    public void BleedingStop()
    {
        isBleeding = false;
        BleedingCheck = 0;
        Debug.Log(BleedingCheck);
    }

    public void PoisonStop()
    {
        isPoison = false;
        PoisonCheck = 0;
        Debug.Log(PoisonCheck);
    }



    IEnumerator Bd()    //출혈데미지 코루틴
    {
        while (isBleeding == true)
        {
            if (Health <= 0)
            {
                PlayerDie();            //죽는 모션 없시 먼저 죽을시 따로 빼서 함수 만들고 iNVOKE사용해서 디스트로이해줘야됨
            }
            Health = Health - 2;        //출혈데미지(지속데미지)
            BleedingCheck++;
            if (BleedingOLCheck == true)
            {
                BleedingStop();
                Debug.Log("중복이다 임마");
                BleedingOLCheck = false;
            }
            Debug.Log(BleedingCheck);
            if (BleedingCheck == 10)
            {
                BleedingStop();
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator Scald()     //화상데미지 코루틴
    {
        while (isScalding == true)
        {
            if (Health <= 0)
            {
                PlayerDie();           //죽는 모션 없시 먼저 죽을시 따로 빼서 함수 만들고 iNVOKE사용해서 디스트로이해줘야됨
            }
            Health = Health - 1;        //출혈데미지(지속데미지)
            spriteRenderer.color = new Color(1, 0.7f, 0.7f, 1f); //화상 이펙트 표현하는 스프라이트렌더러
            ScaldingCheck++;
            if (ScaldingOLCheck == true)
            {
                ScaldingStop();
                Debug.Log("중복");
                ScaldingOLCheck = false;
            }
            Debug.Log(ScaldingCheck);
            if (ScaldingCheck == 10)
            {
                ScaldingStop();
                spriteRenderer.color = new Color(1, 1, 1, 1);
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator Poison()     //중독데미지 코루틴
    {
        while (isPoison == true)
        {
            if (Health <= 0)
            {
                PlayerDie();           //죽는 모션 없시 먼저 죽을시 따로 빼서 함수 만들고 iNVOKE사용해서 디스트로이해줘야됨
            }
            Health = Health - 1;        //중독데미지(지속데미지)
            spriteRenderer.color = new Color(0.7f, 1, 0.7f, 1f); //중독 이펙트 표현하는 스프라이트렌더러
            PoisonCheck++;
            if (PoisonOLCheck == true)
            {
                PoisonStop();
                Debug.Log("중복");
                PoisonOLCheck = false;
            }
            Debug.Log(PoisonCheck);
            if (PoisonCheck == 10)
            {
                PoisonStop();
                spriteRenderer.color = new Color(1, 1, 1, 1);
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    void OnDamaged(Vector2 targetPos, float damage) //피격 시 무적 상태
    {
        if (damage - DEF <= 0)
        {
            Health = Health - 1;
        }
        else
        {
            Health = Health - (damage - DEF);
        }
        if (Health <= 0)
        {
            PlayerDie();//죽는 모션 없시 먼저 죽을시 따로 빼서 함수 만들고 iNVOKE사용해서 디스트로이해줘야됨
        }
        gameObject.layer = 11;

        spriteRenderer.color = new Color(1, 1, 1, 0.4f);


        //반작용
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

        //맞는 모션
        anim.SetTrigger("doDamaged");

        Invoke("OffDamaged", 3f);
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
            if (curRollDelay < maxRollDelay)
            {
                return;
            }
            else
            {
                gameObject.layer = 11;
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
    void playerControl()
    {
        isControll = false;
        anim.SetBool("isRuning", true);
        anim.SetBool("isJumping", false);
        anim.SetBool("isRolling", false);
        bosszone.SetActive(false);
    }
    void controllreset()
    {
        isControll = true;
    }

    void PlayerDie()
    {
        if (isPlayerDie == false)
        {
            anim.SetTrigger("isDie");
            isPlayerDie = true;
        }
    }
}