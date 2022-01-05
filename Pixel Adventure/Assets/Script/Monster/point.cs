using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point : MonoBehaviour
{
    private Bunny bunny;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            bunny = FindObjectOfType<Bunny>();
            bunny.ispointon = false;
            //bunny.think = 1;
            //bunny.Invoke("ThinkTime", 1);
        }
    }

    void OnColliderEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            bunny = FindObjectOfType<Bunny>();
            bunny.ispointon = false;
            //bunny.think = 1;
            //bunny.Invoke("ThinkTime", 1);
        }
    }


}
