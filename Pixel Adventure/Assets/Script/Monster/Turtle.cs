using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : Enemy
{
    void Start()
    {
        Invoke("SpikeOut", 2f);
    }

    void SpikeOut()
    {
        anim.SetTrigger("isSpikeOut");
        Invoke("Idle2", 1f);
    }
    
    void Idle2()
    {
        anim.SetBool("isIdle2", true);
    }
}
