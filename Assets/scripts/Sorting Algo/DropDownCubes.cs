using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertionInputs : MonoBehaviour
{
    public int output;
    public InsertionSort insertionsorting;
    public float speed = 0.5f;
    void Awake()
    {
        insertionsorting = GetComponent<InsertionSort>();
    }
    public void HandleInputDataInsertion(int val)
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

    public void VisualizeInsertion()
    {
        insertionsorting.visualize = true;
    }

    public void speedChangeInserion(float newSpeed)
    {
        speed = newSpeed;
        insertionsorting.updateSpeed = speed;
    }

    public void newArrayInsertion()
    {
        insertionsorting.newArray = true;
    }

}
