using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{

    public GameObject sprite;

    public float scale = 2.0f;
    void FixedUpdate()
    {
        ScaleResolution();
        Invoke("ScaleReturn", 1f);
    }

    void ScaleResolution()

    {

        sprite.transform.localScale = new Vector3(1, scale, 1);
    }

    void ScaleReturn()
    {
        sprite.transform.localScale = new Vector3(1, 1, 1);
    }
  
}
