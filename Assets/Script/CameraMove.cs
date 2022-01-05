using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraMove : MonoBehaviour
{
    public GameObject A;  //A라는 GameObject변수 선언
    private float xgab = 10;
    private float ygab = 5;
    private float Lxlimit = -17;
    private float Rxlimit = 240;
    public float turntime =0;
    public int count = 0;

    Transform AT;


    void Start()
    {
        AT = A.transform;
    }

    void LateUpdate()
    {
        if(AT.position.x > 240) //우측 경계선
        {
            transform.position = new Vector3(Rxlimit + xgab, AT.position.y + ygab, transform.position.z);
        }
        else if (AT.position.x < -17) //좌측 경계선
        {
            transform.position = new Vector3(Lxlimit + xgab, AT.position.y + ygab, transform.position.z);
        }
        else if (AT.position.x < 240 && AT.position.y > 38 && AT.position.y < 60) //2층
        {
            if(xgab > -10)
            {
                xgab = xgab -0.1f;
            }
            transform.position = new Vector3(AT.position.x + xgab, AT.position.y + ygab, transform.position.z);
        }
        else if (AT.position.x > -17 && AT.position.x < 167 && AT.position.y > 60) //3층
        {
            if (xgab < 10)
            {
                xgab = xgab + 0.1f;
            }
            transform.position = new Vector3(AT.position.x + xgab, AT.position.y + ygab, transform.position.z);
        }
        else if(AT.position.x >167 && AT.position.y >81) //보스존
        {
            if (ygab < 8)
            {
                ygab = ygab + 0.005f;
            }
            transform.position = new Vector3(167 + xgab, AT.position.y + ygab, transform.position.z);
        }
        else //1층
        {
            if (xgab < 10)
            {
                xgab = xgab + 0.1f;
            }
            transform.position = new Vector3(AT.position.x + xgab, AT.position.y + ygab, transform.position.z);
        }
    }
}
