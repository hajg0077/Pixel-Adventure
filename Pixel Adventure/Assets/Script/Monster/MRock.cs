using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MRock : Enemy
{
    public int temp;
    public float count;
    private PlayerMove Player;
    public GameObject SRock;
    Vector2 Dropleft, Dropleftleft, Dropright, Droprightright;

    void Start()
    {
        direction = 1;
        PHit = true;
        thinkTime();
    }
    void FixedUpdate()
    {
        if (PHit == true)
        {
            Attack();
        }
        else
        {
            Move();
        }
    }

    void Turn()
    {
        direction = temp;
        if (direction == 1)
        {
            direction = -1;
        }
        else if (direction == -1)
        {
            direction = 1;
        }
        Invoke("thinkTime", 5);
    }

    void thinkTime()
    {
        temp = direction;
        direction = 0;
        Invoke("Turn", 2);
    }

    void Attack()
    {
        UpdateTarget();
        if (Et.x < Pt.position.x - 3)      //플레이어보다 왼쪽
        {
            direction = 1;
        }
        else if (Et.x > Pt.position.x + 3)  //플레이어보다 오른쪽
        {
            direction = -1;
        }
        Move();
    }

    new void MonsterDeath()
    {
        Dropleft = new Vector2(-1, 8);
        Dropright = new Vector2(1, 8);
        Dropleftleft = new Vector2(-3, 8);
        Droprightright = new Vector2(3, 8);

        Destroy(gameObject);
        GameObject srock = Instantiate(SRock, transform.position + Vector3.up * 1f + Vector3.left * 1f, transform.rotation);
        srock.GetComponent<Rigidbody2D>().AddForce(Dropleft, ForceMode2D.Impulse);
        Instantiate(SRock, transform.position + Vector3.up + Vector3.left * 3f, transform.rotation);
        srock.GetComponent<Rigidbody2D>().AddForce(Dropleftleft, ForceMode2D.Impulse);
        Instantiate(SRock, transform.position + Vector3.up * 1f + Vector3.right * 1f, transform.rotation);
        srock.GetComponent<Rigidbody2D>().AddForce(Dropright, ForceMode2D.Impulse);
        Instantiate(SRock, transform.position + Vector3.up  + Vector3.right * 3f, transform.rotation);
        srock.GetComponent<Rigidbody2D>().AddForce(Droprightright, ForceMode2D.Impulse);
        Player = FindObjectOfType<PlayerMove>();
        Player.currentEXP = Player.currentEXP + mexp;
    }
}
