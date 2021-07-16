using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleInput : MonoBehaviour
{
    public int output; // saving number of cubes
    public BubbleSort BubbleSorting; // bubblesort object
    public float speed = 0.5f; // variable for updating speed
    void Awake()
    {
        BubbleSorting = GetComponent<BubbleSort>();
    }
    public void HandleInputDataBubble(int val) // return dropdown option
    {
        if (val == 1)
        {
            output = 4;
            BubbleSorting.condition = true;
        }
        if (val == 2)
        {
            output = 10;
            BubbleSorting.condition = true;

        }
        if (val == 3)
        {
            output = 20;
            BubbleSorting.condition = true;
        }
    }

    public void VisualizeBubble() // indicates to start visualizing
    {
        BubbleSorting.visualize = true;
    }

    public void speedChangeBubble(float newSpeed) // updating visualizing speed
    {
        speed = newSpeed;
        BubbleSorting.updateSpeed = speed;
    }

    public void newArrayBubble() // indicates to generate new array
    {
        BubbleSorting.newArray = true;
    }
}
