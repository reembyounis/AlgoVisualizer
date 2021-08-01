using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SortingMenu : MonoBehaviour
{
    public void BubbleSort()
    {
        SceneManager.LoadScene("BubbleSort");
    }

    public void HeapSort()
    {
        SceneManager.LoadScene("HeapSort");
    }
    public void InsertionSort()
    {
        SceneManager.LoadScene("InsertionSort");
    }

    public void QuickSort()
    {
        SceneManager.LoadScene("QuickSort");
    }

    public void back()
    {
        SceneManager.LoadScene("Menu");
    }
}
