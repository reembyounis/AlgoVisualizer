using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class InsertionPause : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public bool play = false;
    public bool pause = false;


    public GameObject pauseMenuUI;
    void Update()
    {
        if (pause)
        {
            Resume();
            pause = false;
        }
        else if (play)
        {
            Pause();
            play = false;
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void HandlePauseButton()
    {
        pause = true;
    }

    public void HandlePlayButton()
    {
        play = true;
    }
}
