using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Item item;
    public GameObject Player;
    public GameObject PlayerCanvas;
    public GameObject StateCanvas;
    public GameObject SenceManager;
    public GameObject OptionCanvas;

    public GameObject State;
    public GameObject Black;
    public GameObject Option1;
    public GameObject Option2;
    public GameObject Option3;
    public GameObject Boss;

    public Animator fadeAnim;

    public PlayerMove playerMove;

    public int StageNumber;

    public bool PlayerStop = false;
    public bool isGameOver = false;
    private bool TurnDelay = false;
    private bool isS1Start = false;

    void Awake()
    {
        item = FindObjectOfType<Item>();
    }
    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "0.Title")
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("1.Guide");
            }
        }

        if (SceneManager.GetActiveScene().name == "1.Guide")
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("2.Stage1");
            }
        }

        if (SceneManager.GetActiveScene().name == "2.Stage1")
        {
            StageNumber = 1;
            Boss = GameObject.Find("NinjaFrog");
            if (isS1Start == false)
            {
                PlayerStop = true;
                Invoke("StageStart", 3);
                isS1Start = true;
            }
            if (Boss == null)
            {
                if(TurnDelay == false)
                {
                    fadeAnim.SetTrigger("Out");
                    PlayerStop = true;
                    Invoke("DealyTime", 3);
                    TurnDelay = true;
                }
            }

            else if (playerMove.isPlayerDie == true)
            {
                if (isGameOver == false)
                {
                    Invoke("GameOver", 1);
                    isGameOver = true;
                }
            }
        }

        if (SceneManager.GetActiveScene().name == "2-1.Stage2")
        {
            StageNumber = 2;
            Boss = GameObject.Find("MaskDude");
            if (Boss == null)
            {
                if (TurnDelay == false)
                {
                    fadeAnim.SetTrigger("Out");
                    PlayerStop = true;
                    Invoke("DealyTime", 3);
                    TurnDelay = true;
                }
            }

            else if (playerMove.isPlayerDie == true)
            {
                if(isGameOver == false)
                {
                    Invoke("GameOver", 1);
                    isGameOver = true;
                }
            }
        }

        if (SceneManager.GetActiveScene().name == "2-2.Stage3")
        {
            StageNumber = 3;
            Boss = GameObject.Find("BossR");
            if (Boss == null)
            {
                if (TurnDelay == false)
                {
                    fadeAnim.SetTrigger("Out");
                    PlayerStop = true;
                    Invoke("DealyTime", 3);
                    TurnDelay = true;
                }
            }

            else if (playerMove.isPlayerDie == true)
            {
                if (isGameOver == false)
                {
                    Invoke("GameOver", 1);
                    isGameOver = true;
                }
            }
        }

        if (SceneManager.GetActiveScene().name == "3.GameClear")
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("0.Title");
            }
        }

        if (SceneManager.GetActiveScene().name == "4.GameOver")
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("2.Stage1");
            }
        }


        if (Input.GetButtonDown("State"))
        {
            if (State.activeSelf)
            {
                State.SetActive(false);
            }
            else
            {
                State.SetActive(true);
            }
        }


        if (Input.GetButtonDown("Option"))
        {

            if (Option1.activeSelf == false && Option2.activeSelf == false && Option3.activeSelf == false)
            {
                Option1.SetActive(true);
                PlayerStop = true;
                Time.timeScale = 0F;
            }
            
            else if(Option1.activeSelf == true || Option2.activeSelf == true || Option3.activeSelf == true)
            {
                Option1.SetActive(false);
                Option2.SetActive(false);
                Option3.SetActive(false);
                PlayerStop = false;
                Time.timeScale = 1F;
            }

            
        }

        if (Input.GetButtonDown("1"))
        {
            item.HpHeal(); 
        }

        if (Input.GetButtonDown("2"))
        {
            item.MpHeal();
        }
    }

    public void DealyTime()
    {
        if (StageNumber == 1)
        {
            S1toS2();
        }
        if (StageNumber == 2)
        {
            S2toS3();
        }
        if (StageNumber == 3)
        {
            GameClear();
        }
        TurnDelay = false;
    }
    public void GameExit()
    {
        Application.Quit();
    }

    public void Return()
    {
        Option1.SetActive(false);
        Option2.SetActive(false);
        Option3.SetActive(false);
        PlayerStop = false;
        Time.timeScale = 1F;
    }

    public void Option1Start()
    {
        Option1.SetActive(true);
        Option2.SetActive(false);
        Option3.SetActive(false);
        PlayerStop = true;
        Time.timeScale = 0F;
    }

    public void Option2Start()
    {
        Option1.SetActive(false);
        Option2.SetActive(true);
        Option3.SetActive(false);
        PlayerStop = true;
        Time.timeScale = 0F;
    }

    public void Option3Start()
    {
        Option2.SetActive(false);
        Option1.SetActive(false);
        Option3.SetActive(true);
        PlayerStop = true;
        Time.timeScale = 0F;
    }

    public void ResolutionFull()
    {
        Screen.SetResolution(1920, 1280, true);
    }

    public void Resolution()
    {
        Screen.SetResolution(1280, 720, false);
    }

    void S1toS2()
    {
        SceneManager.LoadScene("2-1.Stage2");
        StageStart();
        DontDestroyOnLoad(Player);
        DontDestroyOnLoad(PlayerCanvas);
        DontDestroyOnLoad(OptionCanvas);
        DontDestroyOnLoad(StateCanvas);
        DontDestroyOnLoad(SenceManager);
        DontDestroyOnLoad(Black);
    }

    void S2toS3()
    {
        SceneManager.LoadScene("2-2.Stage3");
        StageStart();
        DontDestroyOnLoad(Player);
        DontDestroyOnLoad(PlayerCanvas);
        DontDestroyOnLoad(OptionCanvas);
        DontDestroyOnLoad(StateCanvas);
        DontDestroyOnLoad(SenceManager);
        DontDestroyOnLoad(Black);
    }

    void StageStart()
    {
        fadeAnim.SetTrigger("In");
        PlayerStop = false;
    }

    void GameOver()
    {
        Destroy(Player);
        Destroy(PlayerCanvas);
        Destroy(OptionCanvas);
        Destroy(StateCanvas);
        Destroy(SenceManager);
        Destroy(Black);
        SceneManager.LoadScene("4.GameOver");
    }

    void GameClear()
    {
        Destroy(Player);
        Destroy(PlayerCanvas);
        Destroy(OptionCanvas);
        Destroy(StateCanvas);
        Destroy(SenceManager);
        Destroy(Black);
        SceneManager.LoadScene("3.GameClear");
    }

    public void Retry()
    {
        SceneManager.LoadScene("2.Stage1");
    }
}
