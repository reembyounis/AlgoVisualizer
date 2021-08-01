using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResumeMenu : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Sorting;
    public GameObject Pathfinding;
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void SortingMenu()
    {
        MainMenu.SetActive(false);
        Sorting.SetActive(true);
    }
    public void PathfindingMenu()
    {
        MainMenu.SetActive(false);
        Pathfinding.SetActive(true);

    }
}
