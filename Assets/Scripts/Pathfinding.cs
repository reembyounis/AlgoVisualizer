using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    GridMaker grid;
    public Transform start;
    public Transform end;

    void Awake()
    {
        grid = GetComponent<GridMaker>();
    }

    void Update()
    {
        startToEnd(start.position, end.position);
    }

    void startToEnd(Vector3 startPos, Vector3 endPos)
    {
        Node start = grid.NodeFromWorldPoint(startPos);
        Node end = grid.NodeFromWorldPoint(endPos);

        List<Node> opened = new List<Node>();
        List<Node> closed = new List<Node>();
        opened.Add(start);

        while (opened.Count != 0)
        {
            Node currentPos = opened[0];

            for (int i = 0; i < opened.Count; i++)
            {
                int fCostCurrent = currentPos.HCost + currentPos.GCost;
                int fCostOpened = opened[i].HCost + opened[i].GCost;
                if (fCostOpened <= fCostCurrent && opened[i].HCost < opened[i].HCost)
                {
                    currentPos = opened[i];
                }
            }
            grid.open.Add(currentPos);
            opened.Remove(currentPos);
            closed.Add(currentPos);

            if (currentPos == end)
            {
                pathIt(start,end);
                return;
            }

            foreach(Node neighbour in grid.Neighbours(currentPos))
            {
                if(closed.Contains(neighbour) || !neighbour.walkable)
                {
                    continue;
                }

                int newStep = currentPos.GCost + Distance(currentPos, neighbour);
                if(newStep < neighbour.GCost || !opened.Contains(neighbour))
                {
                    neighbour.GCost = newStep;
                    neighbour.HCost = Distance(neighbour,end);

                    neighbour.parent = currentPos;

                    if (!opened.Contains(neighbour))
                    {
                        opened.Add(neighbour);
                    }
                }
            }
        }

    }

    int Distance(Node start,Node end)
    {
        int xDistance = Mathf.Abs(start.x - end.x);
        int yDistance = Mathf.Abs(start.y - end.y);

        if (xDistance > yDistance)
        {
            return 14 * yDistance + 10 * (xDistance - yDistance);
        }

        else
        {
            return 14 * xDistance + 10 * (yDistance - xDistance);
        }
    }

    void pathIt(Node start,Node end)
    {
        List<Node> path = new List<Node>();
        Node current = end;
        while (current != start)
        {
            path.Add(current);
            current = current.parent;
        }
        path.Reverse();
        grid.path = path;
    }
   
}
