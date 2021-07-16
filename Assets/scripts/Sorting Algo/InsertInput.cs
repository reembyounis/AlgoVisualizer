using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertInput : MonoBehaviour
{
    public int output; // saving number of cubes
    public InsertionSort insertionsorting; // insertionsort object
    public float speed = 0.5f; // variable for updating speed
    void Awake()
    {
        insertionsorting = GetComponent<InsertionSort>();
    }
    public void HandleInputDataInsertion(int val) // return dropdown option
    {
        if (val == 1)
        {
            output = 4;
            insertionsorting.condition = true;
        }
        if (val == 2)
        {
            output = 10;
            insertionsorting.condition = true;

        }
        if (val == 3)
        {
            output = 20;
            insertionsorting.condition = true;
        }
    }

    public void VisualizeInsertion() // indicates to start visualizing
    {
        insertionsorting.visualize = true;
    }

    public void speedChangeInsertion(float newSpeed) // updating visualizing speed
    {
        speed = newSpeed;
        insertionsorting.updateSpeed = speed;
    }

    public void newArrayInsertion() // indicates to generate new array
    {
        insertionsorting.newArray = true;
    }

}