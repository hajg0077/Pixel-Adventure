using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Box : MonoBehaviour
{
    Rigidbody2D rigid;
    CircleCollider2D circle;
    public GameObject HpPotion;
    public GameObject MpPotion;
    public GameObject OpenBox;
    Vector2 Dropleft, Dropright;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullets")
        {
            Open();
        }
        else if (collision.gameObject.tag == "Player")
        {
            Open();
        }
    }

    void Open()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        circle = gameObject.GetComponent<CircleCollider2D>();

        Dropleft = new Vector2(-1, 5);
        Dropright = new Vector2(1, 5);
        //this.rigid.AddForce(Dropleft, ForceMode2D.Impulse);
        //this.rigid.AddForce(Dropright, ForceMode2D.Impulse);

        Instantiate(OpenBox, transform.position, transform.rotation);
        GameObject hp = Instantiate(HpPotion, transform.position + Vector3.up * 1f, transform.rotation);
        hp.GetComponent<Rigidbody2D>().AddForce(Dropleft, ForceMode2D.Impulse);
        GameObject mp = Instantiate(MpPotion, transform.position + Vector3.up * 1f, transform.rotation);
        mp.GetComponent<Rigidbody2D>().AddForce(Dropright, ForceMode2D.Impulse);

        Destroy(gameObject);
    }
}
