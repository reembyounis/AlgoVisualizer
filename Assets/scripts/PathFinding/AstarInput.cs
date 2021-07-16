using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarInput : MonoBehaviour
{
    public Grid grid; // grid object
    void Awake()
    {
        grid = GetComponent<Grid>();
    }
    public void HandleInputDataAstar(int val) // return dropdown option
    {
        if (val == 1)
        {
            grid.sizeX = 20;
            grid.sizeY = 10;
            grid.condition = true;
        }
        if (val == 2)
        {
            grid.sizeX = 30;
            grid.sizeY = 15;
            grid.condition = true;
        }
        if (val == 3)
        {
            grid.sizeX = 40;
            grid.sizeY = 20;
            grid.condition = true;
        }
    }

    public void AddObstaclesAstar()
    {
        grid.Obstacle = true;
    }

    public void VisualizeAstar() // indicates to start visualizing
    {
        grid.visualize = true;
    }

    public void newPlaneAstar() // indicates to generate new plane
    {
        grid.newPlane = true;
    }
}

