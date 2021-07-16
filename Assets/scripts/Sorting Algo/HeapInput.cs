using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeapInput : MonoBehaviour
{
    public int output; // saving number of cubes
    public HeapSort HeapSorting; // insertionsort object
    public float speed = 0.5f; // variable for updating speed
    void Awake()
    {
        HeapSorting = GetComponent<HeapSort>();
    }
    public void HandleInputDataHeap(int val) // return dropdown option
    {
        if (val == 1)
        {
            output = 4;
            HeapSorting.condition = true;
        }
        if (val == 2)
        {
            output = 10;
            HeapSorting.condition = true;

        }
        if (val == 3)
        {
            output = 20;
            HeapSorting.condition = true;
        }
    }

    public void VisualizeHeap() // indicates to start visualizing
    {
        HeapSorting.visualize = true;
    }

    public void speedChangeHeap(float newSpeed) // updating visualizing speed
    {
        speed = newSpeed;
        HeapSorting.updateSpeed = speed;
    }

    public void newArrayHeap() // indicates to generate new array
    {
        HeapSorting.newArray = true;
    }
}
