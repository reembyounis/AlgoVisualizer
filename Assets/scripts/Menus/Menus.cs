using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menus : MonoBehaviour
{
    public void SortingMenu()
    {
        SceneManager.LoadScene("Sorting");
    }

    public void PathingMenu()
    {
        SceneManager.LoadScene("Pathing");
    }

}