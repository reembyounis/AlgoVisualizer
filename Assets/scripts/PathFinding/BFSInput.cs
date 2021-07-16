using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFSInput : MonoBehaviour
{
    public BFSgrid BFSgrid; // BFSgrid object
    void Awake()
    {
        BFSgrid = GetComponent<BFSgrid>();
    }
    public void HandleInputDataBFS(int val) // return dropdown option
    {
        if (val == 1)
        {
            BFSgrid.sizeX = 20;
            BFSgrid.sizeY = 10;
            BFSgrid.condition = true;
        }
        if (val == 2)
        {
            BFSgrid.sizeX = 30;
            BFSgrid.sizeY = 15;
            BFSgrid.condition = true;
        }
        if (val == 3)
        {
            BFSgrid.sizeX = 40;
            BFSgrid.sizeY = 20;
            BFSgrid.condition = true;
        }
    }

    public void AddObstaclesBFS()
    {
        BFSgrid.Obstacle = true;
    }

    public void VisualizeBFS() // indicates to start visualizing
    {
        BFSgrid.visualize = true;
    }

    public void newPlaneBFS() // indicates to generate new plane
    {
        BFSgrid.newPlane = true;
    }
}

