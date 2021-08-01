using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DFSpathing : MonoBehaviour
{
    public Transform start;
    public Transform end;
    DFSgrid grid;

    void Awake()
    {
        grid = GetComponent<DFSgrid>();
    }

    void Update()
    {
        if(grid.visualize){
            grid.visualize=false;
            startToEnd(start.position, end.position);
        }
    }

    void startToEnd(Vector3 startPos, Vector3 endPos)
    {
        DFSnode startNode = grid.NodeFromWorldPoint(startPos);
        DFSnode endNode = grid.NodeFromWorldPoint(endPos);

        grid.DFS(startNode.x, startNode.y, endNode);
        grid.visualizeit();
    }

}
