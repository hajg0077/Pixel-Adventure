using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject State;
    public GameObject Option;
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
            if (Option.activeSelf)
            {
                Option.SetActive(false);
                PlayerStop = false;
                Time.timeScale = 1F;
                Debug.Log("start");
            }
            else
            {
                Option.SetActive(true);
                PlayerStop = true;
                Time.timeScale = 0F;
                Debug.Log("false");
            }
        }


    }
    public void GameExit()
    {
        Application.Quit();
    }

    public void Return()
    {
        Option.SetActive(false);
        PlayerStop = false;
        Time.timeScale = 1F;
        Debug.Log("stop");
    }

    public void OptionStart()
    {
        Option.SetActive(true);
        PlayerStop = true;
        Time.timeScale = 0F;
        Debug.Log("start");
    }
}
