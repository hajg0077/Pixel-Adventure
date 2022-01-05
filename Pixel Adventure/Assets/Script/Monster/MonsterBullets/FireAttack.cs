using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttack : MonoBehaviour
{
    public int aa;
    public GameObject Boss;
    public float dmg;
    public Transform Ts;
    // Start is called before the first frame update
    void Start()
    {
        Ts = Boss.transform;
    }
    void Update()
    {
        if(aa == 1)
        {

            transform.position = new Vector2(Ts.position.x + 2, Ts.position.y);
        }

        else if(aa == 2)
        {
            transform.position = new Vector2(Ts.position.x - 2, Ts.position.y);
        }
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("아야");
        }
    }
}
