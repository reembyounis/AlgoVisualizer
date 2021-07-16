using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickInput : MonoBehaviour
{
    public int output; // saving number of cubes
    public QuickSort QuickSorting; // insertionsort object
    public float speed = 0.5f; // variable for updating speed
    void Awake()
    {
        QuickSorting = GetComponent<QuickSort>();
    }
    public void HandleInputDataQuick(int val) // return dropdown option
    {
        if (val == 1)
        {
            output = 4;
            QuickSorting.condition = true;
        }
        if (val == 2)
        {
            output = 10;
            QuickSorting.condition = true;

        }
        if (val == 3)
        {
            output = 20;
            QuickSorting.condition = true;
        }
    }

    public void VisualizeQuick() // indicates to start visualizing
    {
        QuickSorting.visualize = true;
    }

    public void speedChangeQuick(float newSpeed) // updating visualizing speed
    {
        speed = newSpeed;
        QuickSorting.updateSpeed = speed;
    }

    public void newArrayQuick() // indicates to generate new array
    {
        QuickSorting.newArray = true;
    }
}
