using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullets : MonoBehaviour
{
    public int Bulletdamage;
    public Rigidbody2D rigid;
    public GameObject Target;
    public Transform Pt;
    void Start()
    {
        Invoke("DestroyBullet", 4f);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
