using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public int Bulletdamage;
    private PlayerMove Player;

    void Start()
    {
        Invoke("DestroyBullet", 1f);
        Player = FindObjectOfType<PlayerMove>();
        Bulletdamage = Player. STR;
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
