using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public GameObject Target;       //타겟 지정
    public Transform Pt;
    public Vector2 Et;
    Rigidbody2D rigid;
    public float maxSpeed;

    public GameObject PlaPo; //플레이어 위치  

    Transform PP;
    void Start()
    {
        PlaPo = GameObject.Find("Player");
        rigid = GetComponent<Rigidbody2D>();
        PP = PlaPo.transform;

    }

    void Update()
    {

        if (PP.position.x < 18 && PP.position.y < -55)
        {
            if (rigid.velocity.x > maxSpeed) // 오른쪽 최대 속도
            {
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);

            }
            else
            {
                rigid.AddForce(Vector2.right * 2f, ForceMode2D.Impulse);
            }
        }
        if(PP.position.x > 18 && PP.position.y < -65)
        {
            if (rigid.velocity.x > maxSpeed) // 오른쪽 최대 속도
            {
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);

            }
            else
            {
                rigid.AddForce(Vector2.right * 2f, ForceMode2D.Impulse);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Lava")
        {
            Destroy(gameObject, 1.2f);
        }
    }

}
