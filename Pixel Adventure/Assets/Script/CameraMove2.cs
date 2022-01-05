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
    public Camera cm;
    private float xgab = 10;            //카메라 플레이어 x축 갭
    private float ygab = 5;             //카메라 플레이어 y축 갭
    private float Lxlimit = -8;        //카메라 왼쪽끝 최대치
    private float Rxlimit = 180;        //카메라 오른쪽끝 최대치

    private bool isBossAppear = false;
    private bool BossAppearcamera = false;
    private float camerax;

    public float turntime = 0;
    public int count = 0;

    Transform AT;       //플레이어 위치

    public GameObject bosshp;

    public Slider backVolume;       //사운드부분
    public AudioSource Audio;
    public AudioClip bgm2;
    public AudioClip bossbgm2;
    private float backVol = 1f;

    void Start()
    {
        backVolume =
        GameObject.Find("OptionCanvas").transform.Find("OptionEventSound").transform.Find("Option_Sound")
            .transform.Find("Slider").GetComponent<Slider>();
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


    void CameraStop()
    {
        isBossAppear = true;
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
            
            if (wall[1].activeSelf == false && AT.position.x < 176 && AT.position.y > -43 && MBoss.activeSelf == true)   // 중간보스존
            {
                wall[0].SetActive(true);
                wall[1].SetActive(true);
            }
            if (MBoss.activeSelf == false)
            {
                wall[0].SetActive(false);
            }
        }

        else if (AT.position.x > 25 && AT.position.x < 180 && AT.position.y >5) //4
        {
            if (AT.position.x < 103 && AT.position.x > 20 && AT.position.y > 5)
            {
                if (wall[2].activeSelf == false)
                {
                    wall[2].SetActive(true);
                }
                else if (wall[2].activeSelf == true)
                {
                    if (isBossAppear == true)
                    {
                        if (cm.orthographicSize < 13)
                        {
                            cm.orthographicSize = cm.orthographicSize + 0.01f;
                        }
                        if (ygab < 10)
                        {
                            ygab = ygab + 0.02f;
                        }
                        if (xgab < 0)
                        {
                            xgab = xgab + 0.01f;
                        }
                        transform.position = new Vector3(AT.position.x + xgab, AT.position.y + ygab, transform.position.z);
                        bosshp.SetActive(true);
                    }
                    else if (isBossAppear == false)
                    {
                        if (transform.position.x > 42)
                        {
                            camerax = transform.position.x;
                            camerax = camerax - 0.2f;
                            transform.position = new Vector3(camerax, 11, transform.position.z);
                        }
                        else if (BossAppearcamera == false && transform.position.x <= 42)
                        {
                            BossAppearcamera = true;
                            Invoke("CameraStop", 3f);
                        }
                        else
                        {
                            transform.position = new Vector3(42, 11, transform.position.z);
                        }
                    }
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
