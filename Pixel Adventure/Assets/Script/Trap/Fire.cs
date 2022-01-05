using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    public float maxfireDelay;
    public float middlefireDelay;
    public float curfireDelay;

    public GameObject fire;

    void Start()
    {
        fire.SetActive(true);
    }

    
    void Update()
    {
        fireTime();
        fireAttack();
        fireoff();
        
    }

   

    public void fireAttack() {

        if (curfireDelay < middlefireDelay)
        {
            return;
        }
        else{
            fire.SetActive(false);                      
            }
    
    }

    void fireoff()
    {
        if (curfireDelay < maxfireDelay)
        {
            return;
        }
        else
        {
            fire.SetActive(true);
            curfireDelay = 0;
        }
    }
    public void fireTime()
    {
        curfireDelay += Time.deltaTime;
    }
}

   
    
   

  

