using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject Player;
    Rigidbody2D rigid;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //anim = GetComponent<Animator>();

    }

    void OnTriggerStay2D(Collider2D collision) //팬에 닿으면 위로 쭉 날라가는 로직
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.transform.GetComponent<Rigidbody2D>().gravityScale = -1.2f;
            //rigid.AddForce(Vector2.up * 28, ForceMode2D.Impulse);
            Invoke("time", 2);
        }
    }

    void time()
    {
        Player.transform.GetComponent<Rigidbody2D>().gravityScale = 2;
    }
}
