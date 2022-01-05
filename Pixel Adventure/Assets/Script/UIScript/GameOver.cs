using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Image fadeImg;
    private float alpha;
    private bool istime = false;

    void Awake()
    {
        fadeImg = GetComponent<Image>();
        Debug.Log(alpha);
        alpha = 1;
    }

    void Update()
    {
        if(alpha >= 0 && istime == false)
        {
            alpha = alpha - 0.01f;
            Invoke("time", 0.02f);
            istime = true;
        }
        fadeImg.color = new Color(0, 0, 0, alpha);
    }

    void time()
    {
        istime = false;
    }
}
