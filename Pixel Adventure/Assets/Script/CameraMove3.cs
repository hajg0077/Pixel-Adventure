using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove3 : MonoBehaviour
{
    public static Sound Soundinstance;

    public GameObject A;  //A라는 GameObject변수 선언
    public GameObject[] wall;
    public GameObject MBoss;
    public GameObject Boss1;
    public GameObject Boss2;
    public GameObject BossR;

    public GameObject uptile;
    private float xgab = 10;
    private float ygab = 5;
    private float Lxlimit = -21;
    private float Rxlimit = 179;

    public float turntime = 0;
    public int count = 0;

    Transform AT;

    public GameObject bosshp;
    public GameObject bosshp2;

    public Slider backVolume;       //사운드부분
    public AudioSource Audio;
    public AudioClip bgm3;
    public AudioClip bossbgm3;
    private float backVol = 1f;

    void Start()
    {
        A = GameObject.Find("Player");
        backVolume =
        GameObject.Find("OptionCanvas").transform.Find("OptionEventSound").transform.Find("Option_Sound")
            .transform.Find("Slider").GetComponent<Slider>();
        Soundinstance = FindObjectOfType<Sound>();
        AT = A.transform;
        AT.position = new Vector2(-5, 6);


        Audio = GetComponent<AudioSource>();        //사운드 부분
        Audio.clip = bgm3;
        backVol = PlayerPrefs.GetFloat("backvol", 1f);
        backVolume.value = backVol;
        Audio.volume = backVolume.value;                   //오류 뜨는 부분
    }

    void Update()
    {
        SoundSlider();                         //사운드바
        if (Boss1.activeSelf == false)
        {
            Boss2.SetActive(true);
            bosshp2.SetActive(true);
        }
        if (Boss1.activeSelf == false && Boss2.activeSelf == false)
        {
            BossR.SetActive(false);
        }
    }

    void LateUpdate()
    {
        if (AT.position.x > 260 && AT.position.x <300 && AT.position.y > -80)
        {
            transform.position = new Vector3(278, -68f, transform.position.z);
        }
        else if (AT.position.x > 300 && AT.position.y > -80)
        {
            transform.position = new Vector3(325, -67.5f, transform.position.z);
            bosshp.SetActive(true);
        }

        else
        {
            if (AT.position.x > 179) //B1층 우측 경계선
            {
                transform.position = new Vector3(Rxlimit + xgab, AT.position.y + ygab, transform.position.z);
            }

            else if (AT.position.x < -21) //B1층 좌측 경계선
            {
                transform.position = new Vector3(Lxlimit + xgab, AT.position.y + ygab, transform.position.z);
            }

            else if (AT.position.x < 98 && AT.position.x > 80 && AT.position.y > -22) //B1충 구덩이
            {
                if (xgab < 10)
                {
                    xgab = xgab + 0.1f;
                }
                transform.position = new Vector3(AT.position.x + xgab, AT.position.y + ygab, transform.position.z);
            }

            else if (AT.position.x < 179 && AT.position.y > -55 && AT.position.y < -20) // B2층 
            {
                if (xgab > -10)
                {
                    xgab = xgab - 0.1f;
                }
                transform.position = new Vector3(AT.position.x + xgab, AT.position.y + ygab, transform.position.z);

            }

            else if (AT.position.x > -20 && AT.position.y > -79 && AT.position.y < -55) //돌 생성 구간
            {
                if (xgab < 10)
                {
                    xgab = xgab + 0.1f;
                }
                transform.position = new Vector3(AT.position.x + xgab, AT.position.y + ygab, transform.position.z);
            }

            else if (AT.position.y > -105 && AT.position.y < -79) // 돌 왼쪽으로 꺽는 지점
            {
                Lxlimit = 8;
                if (AT.position.x < 8) //B1층 좌측 경계선
                {
                    transform.position = new Vector3(Lxlimit + xgab, AT.position.y + ygab, transform.position.z);
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

            else if (AT.position.y < -105) //맨 밑
            {
                if (AT.position.y < -118)
                {
                    uptile.SetActive(true);
                }
                Lxlimit = 8;
                if (AT.position.x < 8) //B1층 좌측 경계선
                {
                    transform.position = new Vector3(Lxlimit + xgab, AT.position.y + ygab, transform.position.z);
                }
                else
                {
                    if (xgab < 10)
                    {
                        xgab = xgab + 0.1f;
                    }
                    transform.position = new Vector3(AT.position.x + xgab, AT.position.y + ygab, transform.position.z);
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
    }
    public void SoundSlider()       //슬라이스바
    {
        Audio.volume = backVolume.value;
        backVol = backVolume.value;
        PlayerPrefs.SetFloat("backvol", backVol);
    }
}
