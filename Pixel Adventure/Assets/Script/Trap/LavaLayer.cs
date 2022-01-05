using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LavaLayer : MonoBehaviour
{
    Tilemap ti;
    void Start()
    {
        ti = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ti.color = new Color(1, 1, 1, 0.4f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ti.color = new Color(1, 1, 1, 1f);
        }

    }
}
