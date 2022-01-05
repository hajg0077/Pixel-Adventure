using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public static Sound Soundinstance;
    public Slider backVolume;
    public AudioSource Audio;
    public AudioClip bgm;
    public AudioClip bossbgm1;
    public AudioClip bossbgm2;
    public AudioClip bossbgm3;
    private float backVol = 1f;

    private void Start()
    {
        Audio = GetComponent<AudioSource>();
        Audio.clip = bgm;
        backVol = PlayerPrefs.GetFloat("backvol", 1f);
        backVolume.value = backVol;
        //Audio.playOnAwake = true;  //활성화시 해당씬 실행시 바로 사운드 재생이 시작됩니다.
        //비활성화시 Play()명령을 통해서만 재생됩니다.
        // Audio.Play(); //오디오 재생
        Audio.volume = backVolume.value;                   //오류 뜨는 부분
        //Audio.priority = 0;
        //씬안에 모든 오디오소스중 현재 오디오 소스의 우선순위를 정한다.
        // 0 : 최우선, 256 : 최하, 128 : 기본값
    }

    void Update()
    {
        SoundSlider();
    }

    public void SoundSlider()
    {
        Audio.volume = backVolume.value;
        backVol = backVolume.value;
        PlayerPrefs.SetFloat("backvol", backVol);
    }
}