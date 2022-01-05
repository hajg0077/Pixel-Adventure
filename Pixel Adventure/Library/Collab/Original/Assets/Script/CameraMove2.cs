using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove2 : MonoBehaviour
{
    public static Sound Soundinstance;

    public GameObject A;  //플레이어
    public GameObject[] wall;
    public GameObject MBoss;
    public GameObject Boss;
    private float xgab = 10;            //카메라 플레이어 x축 갭
    private float ygab = 5;             //카메라 플레이어 y축 갭
    private float Lxlimit = -8;        //카메라 왼쪽끝 최대치
    private float Rxlimit = 180;        //카메라 오른쪽끝 최대치

    public float turntime = 0;
    public int count = 0;

    Transform AT;       //플레이어 위치



    public Slider backVolume;       //사운드부분
    public AudioSource Audio;
    public AudioClip bgm2;
    public AudioClip bossbgm2;
    private float backVol = 1f;

    void Start()
    {
        A = GameObject.Find("Player");
        Soundinstance = FindObjectOfType<Sound>();
        AT = A.transform;
        AT.position = new Vector2(-5, 6);

        Audio = GetComponent<AudioSource>();        //사운드 부분
        Audio.clip = bgm2;
        backVol = PlayerPrefs.GetFloat("backvol", 1f);
        backVolume.value = backVol;
        Audio.volume = backVolume.value;                   //오류 뜨는 부분
    }

    void Update()
    {
        SoundSlider();
    }

    void LateUpdate()
    {
        if (AT.position.x > 180) //우측 경계선
        {
            transform.position = new Vector3(Rxlimit + xgab, AT.position.y + ygab, transform.position.z);
        }

        else if (AT.position.x < -8) //좌측 경계선
        {
            transform.position = new Vector3(Lxlimit + xgab, AT.position.y + ygab, transform.position.z);
        }

        else if (AT.position.x < 136 && AT.position.y > -33 && AT.position.y < -17) //1
        {
            if (AT.position.x < 19)
            {
                transform.position = new Vector3(19 + xgab, AT.position.y + ygab, transform.position.z);
            }
            else 
            {
                if (xgab > -10)
                {
                    xgab = xgab - 0.1f;
                }
                transform.position = new Vector3(AT.position.x + xgab, AT.position.y + ygab, transform.position.z);
            }
        }

        else if (AT.position.x < 136 && AT.position.x > 6 && AT.position.y < -33)  //2
        {
            if (xgab < 0)
            {
                xgab = 0;
            }
            if (xgab < 10)
            {
                xgab = xgab + 0.1f;
            }
            transform.position = new Vector3(AT.position.x + xgab, AT.position.y + ygab, transform.position.z);
        }

        else if (AT.position.x  > 136 && AT.position.x < 180 && AT.position.y < -44)  //2-2
        {
            if (xgab < 0)
            {
                xgab = 0;
            }
            if (xgab < 10)
            {
                xgab = xgab + 0.1f;
            }
            transform.position = new Vector3(AT.position.x + xgab, AT.position.y + ygab, transform.position.z);
        }

        else if (AT.position.x > 136 && AT.position.x < 180 && AT.position.y > -44 && AT.position.y < -25) //3
        {
            if (wall[0].activeSelf == true)         //중보스존 카메라 조작
            {
                if (transform.position.x > 159)
                {
                    transform.position = new Vector3(transform.position.x - 0.2f, transform.position.y, transform.position.z);
                }
                else if (transform.position.y > -34.9)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
                }
                else if (transform.position.y < -35.1)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(159, -35, transform.position.z);
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
            
            if (wall[0].activeSelf == false && AT.position.x < 176 && AT.position.y > -43 && MBoss.activeSelf == true)   // 중간보스존
            {
                wall[0].SetActive(true);
                wall[1].SetActive(true);
            }
        }

        else if (AT.position.x > 25 && AT.position.x < 180 && AT.position.y >5) //4
        {
            if (xgab > -10)
            {
                xgab = xgab - 0.1f;
            }
            transform.position = new Vector3(AT.position.x + xgab, AT.position.y + ygab, transform.position.z);
        }

        else if (AT.position.x > 165 && AT.position.y > 81) //보스존 연출
        {
            if (wall[2].activeSelf == false)
            {
                wall[2].SetActive(true);
            }
            if (ygab < 8)
            {
                Soundinstance.Audio.clip = Soundinstance.bossbgm2;
                Soundinstance.Audio.Play();
                Soundinstance.Audio.loop = true;
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


    public void SoundSlider()       //슬라이스바
    {
        Audio.volume = backVolume.value;
        backVol = backVolume.value;
        PlayerPrefs.SetFloat("backvol", backVol);
    }
}
