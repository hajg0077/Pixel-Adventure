using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float Bulletdamage;

    void Start()
    {
        Invoke("DestroyBullet", 1.5f);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        } 

        if(collision.gameObject.tag == "Platform")
        {
            Destroy(gameObject);
        }
    }
 
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
