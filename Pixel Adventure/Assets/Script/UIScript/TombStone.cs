using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombStone : MonoBehaviour
{
    public Rigidbody2D rigid;
    public float x;
    public float y;
    // Update is called once per frame

    void Start()
    {
        x = transform.position.x;
        y = transform.position.y;
    }
    void Update()
    {
        if (y >= 27)
        {
            y = y - 1;
            transform.position = new Vector2(x, y);
        }
    }
}
