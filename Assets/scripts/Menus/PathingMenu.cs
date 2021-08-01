using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PathingMenu : MonoBehaviour
{
    public void AStar()
    {
        SceneManager.LoadScene("Astar");
    }
    public void BFS()
    {
        SceneManager.LoadScene("BFS");
    }

    public void DFS()
    {
        SceneManager.LoadScene("DFS");
    }
    public void back()
    {
        SceneManager.LoadScene("Menu");
    }
}
