using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;
    public Transform desPos; 
    public float speed;

     void Start()
    {
        transform.position = startPos.position;
        desPos = endPos;
    }

    void FixedUpdate()
    {    
        transform.position = Vector2.MoveTowards(transform.position, desPos.position, Time.deltaTime * speed);

        if(Vector2.Distance(transform.position, desPos.position) <= 0.05f)
        {
            if (desPos == endPos) desPos = startPos;
            else desPos = endPos;
        }
    }

}
