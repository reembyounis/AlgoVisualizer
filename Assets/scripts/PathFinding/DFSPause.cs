using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DFSPause : MonoBehaviour
{
    public GameObject Panel;
    public GameObject exitbutton;
    public GameObject pause;
    public GameObject play;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    public void info()
    {
        if (Panel.activeInHierarchy == false)
        {
            Panel.SetActive(true);
            exitbutton.SetActive(true);
        }
    }

    public void exit()
    {
        if (Panel.activeInHierarchy == true)
        {
            Panel.SetActive(false);
            exitbutton.SetActive(false);
        }
    }

    public void Pause()
    {
        pause.SetActive(false);
        play.SetActive(true);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Play()
    {
        pause.SetActive(true);
        play.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SortingMenu()
    {
        SceneManager.LoadScene("Sorting");
    }

    public void PathingMenu()
    {
        SceneManager.LoadScene("Pathing");
    }
}
