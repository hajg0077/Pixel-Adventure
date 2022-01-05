using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : MonoBehaviour
{
    public GameObject Player;
    Rigidbody2D rigid;
    void Awake()
    {
        Player = GameObject.Find("Player");
        rigid = GetComponent<Rigidbody2D>();

    }

    void OnTriggerStay2D(Collider2D collision) //팬에 닿으면 위로 쭉 날라가는 로직
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.transform.GetComponent<Rigidbody2D>().gravityScale = -1.2f;
            Invoke("time", 2);
        }
    }

    void time()
    {
        Player.transform.GetComponent<Rigidbody2D>().gravityScale = 2;
    }
}
