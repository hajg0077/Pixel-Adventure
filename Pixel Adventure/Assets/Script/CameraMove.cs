using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class CameraMove : MonoBehaviour
{

    public GameObject A;  //플레이어
    public GameObject[] wall;
    public GameObject MBoss;
    public GameObject Boss;
    private float xgab = 10;            //카메라 플레이어 x축 갭
    private float ygab = 5;             //카메라 플레이어 y축 갭
    private float Lxlimit = -17;        //카메라 왼쪽끝 최대치
    private float Rxlimit = 240;        //카메라 오른쪽끝 최대치

    public float turntime =0;
    public int count = 0;

    Transform AT;       //플레이어 위치


    public Slider backVolume;       //사운드부분
    public AudioSource Audio;
    public AudioClip bgm1;
    public AudioClip bossbgm1;
    private float backVol = 1f;

    void Start()
    {
        A = GameObject.Find("Player");
        AT = A.transform;

        Audio = GetComponent<AudioSource>();        //사운드부분
        Audio.clip = bgm1;
        backVol = PlayerPrefs.GetFloat("backvol", 1f);
        backVolume.value = backVol;
        Audio.volume = backVolume.value;                   //오류 뜨는 부분
        Audio.Play();
    }

    void Update()
    {
        SoundSlider();
    }

    void LateUpdate()
    {
        if (AT.position.x > 240) //우측 경계선
        {
            transform.position = new Vector3(Rxlimit + xgab, AT.position.y + ygab, transform.position.z);
        }

        else if (AT.position.x < -17) //좌측 경계선
        {
            transform.position = new Vector3(Lxlimit + xgab, AT.position.y + ygab, transform.position.z);
        }

        else if (AT.position.x < 240 && AT.position.y > 38 && AT.position.y < 60) //2층
        {
            if (MBoss.activeSelf == false)
            {
                wall[0].SetActive(false);
                wall[1].SetActive(false);
                if (xgab < -10.2)
                {
                    xgab = xgab + 0.1f;
                }
                else if(xgab > -9.8)
                {
                    xgab = xgab - 0.1f;
                }
                else
                {
                    xgab = -10;
                }
                transform.position = new Vector3(AT.position.x + xgab, AT.position.y + ygab, transform.position.z);
            }
            else
            {
                if (wall[0].activeSelf == true)         //중보스존 카메라 조작
                {
                    if(transform.position.x > 115)
                    {
                        transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
                    }
                    else if (transform.position.y > 50.1)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
                    }
                    else if(transform.position.y < 49.9)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
                    }
                    else
                    {
                        transform.position = new Vector3(115, 50, transform.position.z);
                    }
                    
                }
                else
                {
                    if (xgab > -10)
                    {
                        xgab = xgab - 0.1f;
                    }
                    transform.position = new Vector3(AT.position.x + xgab, AT.position.y + ygab, transform.position.z);
                }

                if (wall[0].activeSelf == false && AT.position.x < 133 && MBoss.activeSelf == true)   // 중간보스존
                {
                    wall[0].SetActive(true);
                    wall[1].SetActive(true);
                }
            }
        }

        else if (AT.position.x > -17 && AT.position.x < 165 && AT.position.y > 60) //3층
        {
            if (wall[2].activeSelf == true)
            {
                return;
            }
            if (xgab < 10)
            {
                xgab = xgab + 0.1f;
            }
            transform.position = new Vector3(AT.position.x + xgab, AT.position.y + ygab, transform.position.z);
        }

        else if(AT.position.x >165 && AT.position.y >81) //보스존 연출
        {
            if(wall[2].activeSelf == false)
            {
                wall[2].SetActive(true);
            }
            if (ygab < 8)
            {
                Audio.clip = bossbgm1;
                Audio.Play();
                Audio.loop = true;


                ygab = ygab + 0.005f;
                transform.position = new Vector3(AT.position.x + xgab, AT.position.y + ygab, transform.position.z);
            }
            else
            {
                return;
            }
            
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


    public void SoundSlider()           //사운드바
    {
        Audio.volume = backVolume.value;
        backVol = backVolume.value;
        PlayerPrefs.SetFloat("backvol", backVol);
    }
}
