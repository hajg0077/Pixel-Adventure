using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject State;
    public GameObject Option1;
    public GameObject Option2;
    public GameObject Option3;

    public bool PlayerStop = false;
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
            if (Input.GetKey(KeyCode.C))
            {
                SceneManager.LoadScene("3.GameClear");
            }

            else if (Input.GetKey(KeyCode.O))
            {
                SceneManager.LoadScene("4.GameOver");
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
            if (Option1.activeSelf)
            {
                Option1.SetActive(false);
                PlayerStop = false;
                Time.timeScale = 1F;
            }
            else
            {
                Option1.SetActive(true);
                PlayerStop = true;
                Time.timeScale = 0F;
            }
        }


    }
    public void GameExit()
    {
        Application.Quit();
    }

    public void Return()
    {
        Option1.SetActive(false);
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



}
