using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressStart : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isFade", true);
        Invoke("animcon", 1);
    }

    void animcon()
    {
        if(anim.GetBool("isFade") == true)
        {
            anim.SetBool("isFade", false);
        }
        else if (anim.GetBool("isFade") == false)
        {
            anim.SetBool("isFade", true);
        }
        Invoke("animcon", 1);
    }
}
