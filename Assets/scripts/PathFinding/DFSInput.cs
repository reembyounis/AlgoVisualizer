using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFSInput : MonoBehaviour
{
    public DFSgrid grid; // grid object
    void Awake()
    {
        grid = GetComponent<DFSgrid>();
    }
    public void HandleInputDataDFS(int val) // return dropdown option
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

    public void AddObstaclesDFS()
    {
        grid.Obstacle = true;
    }

    public void VisualizeDFS() // indicates to start visualizing
    {
        grid.visualize = true;
    }

    public void newPlaneDFS() // indicates to generate new plane
    {
        grid.newPlane = true;
    }
}
