using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Gate : MonoBehaviour
{
    public GameObject gateon;
    private PlayerMove player;
    public float warpx;
    public float warpy;

    void Awake()
    {
        player = FindObjectOfType<PlayerMove>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(Input.GetButtonDown("Open"))
            {
                Open();
                player.transform.position = new Vector2(warpx, warpy);
            }
        }
    }
    void Open()
    {
        Instantiate(gateon, transform.position, transform.rotation);
        Destroy(gameObject);
    }


}
