using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRock : Enemy
{
    public int temp;
    public float count;
    private PlayerMove Player;
    public GameObject MRock;
    Vector2 Dropleft, Dropright;

    void Start()
    {
        direction = 1;
        PHit = false;
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
        Dropleft = new Vector2(-2, 8);
        Dropright = new Vector2(2, 8);
        Destroy(gameObject);
        GameObject mrock = Instantiate(MRock, transform.position + Vector3.up * 1f + Vector3.left * 2f, transform.rotation);
        mrock.GetComponent<Rigidbody2D>().AddForce(Dropleft, ForceMode2D.Impulse);
        Instantiate(MRock, transform.position + Vector3.up * 1f + Vector3.right * 2f, transform.rotation);
        mrock.GetComponent<Rigidbody2D>().AddForce(Dropright, ForceMode2D.Impulse);
        Player = FindObjectOfType<PlayerMove>();
        Player.currentEXP = Player.currentEXP + mexp;
    }
}
