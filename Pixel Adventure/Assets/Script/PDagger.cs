using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PDagger : MonoBehaviour
{
    private float distancex;
    private float distancey;
    public Rigidbody2D rigid;
    public int Damage;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Damage = 200;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (transform.position.x)
            {
                case 192:
                    transform.position = new Vector2(192, 82);
                    rigid.velocity = new Vector2(-15, 13);
                    break;
                case 182:
                    transform.position = new Vector2(182, 86);
                    rigid.velocity = new Vector2(-5, 9);
                    break;
                case 171:
                    transform.position = new Vector2(171, 86);
                    rigid.velocity = new Vector2(6, 9);
                    break;
                case 167:
                    transform.position = new Vector2(167, 82);
                    rigid.velocity = new Vector2(10, 13);
                    break;
            }
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
