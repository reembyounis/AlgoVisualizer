using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BFSgrid : MonoBehaviour
{

    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public BFSnode[,] grid;
    public bool[,] visited;
    float nodeDiameter;
    int gridSizeX, gridSizeY;
    public GameObject Cube;
    public GameObject grids;
    public Transform Spawn;
    public List<BFSnode> done = new List<BFSnode>();
    public Queue<int> rows = new Queue<int>();
    public Queue<int> cols = new Queue<int>();
    public int nodes_next = 0;
    public int moves = 0;
    public Transform start;
    public Transform end;
    int nodes_left = 1;
    public bool reached_end = false;
    public int getR, getC;
    List<BFSnode> neighbours = new List<BFSnode>();
    public List<BFSnode> path = new List<BFSnode>();

    public bool Obstacle = false;
    public bool newPlane = false;
    public List<GameObject> obstacles;

    public bool condition = false; // indicates that we chose one of the dropdown options

    public bool visualize = false; // button for starting to visualize

    public int sizeX = 0; // saving plane size
    public int sizeY = 0;
    public GameObject plane;
    public GameObject Sphere;
    public GameObject Capsule;
    GameObject current;
    public GameObject cube ;
    public List<GameObject> cube_array;


    void Awake()
    {
        Update();
    }

    void Update()
    {
        if (condition || !condition && newPlane)
        {
            if (newPlane) // generate new Plane
            {
                newPlane = false;
            }

            for (int i = 0; i < obstacles.Count; i++)
            {
                Destroy(obstacles[i]);
            }
            for (int i = 0; i < cube_array.Count; i++)
            {
                Destroy(cube_array[i]);
            }

            nodeDiameter = nodeRadius * 2;
            /*gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
            gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);*/

            gridSizeX = Mathf.RoundToInt(sizeX / nodeDiameter);
            gridSizeY = Mathf.RoundToInt(sizeY / nodeDiameter);
            grid = new BFSnode[gridSizeX, gridSizeY];
            visited = new bool[gridSizeX, gridSizeY];
            plane.transform.localScale = new Vector3(gridSizeX / 10, 1, gridSizeY / 10);
            Sphere.transform.localPosition = new Vector3(Random.Range(-sizeX / 2 + 2, sizeX / 2 - 2), 1.5f, Random.Range(-sizeY / 2 + 2, sizeY / 2 - 2));
            Capsule.transform.localPosition = new Vector3(Random.Range(-sizeX / 2 + 2, sizeX / 2 - 2), 1.5f, Random.Range(-sizeY / 2 + 2, sizeY / 2 - 2));

            condition = false;

        }

        if (Obstacle)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-sizeX / 2 + 1, sizeX / 2 - 1), 5, Random.Range(-sizeY / 2 + 1, sizeY / 2 - 1));
            current = Instantiate(Cube, spawnPos, Quaternion.identity);
            current.GetComponent<Renderer>().material.color = Color.red;
            obstacles.Add(current);
            Obstacle = false;
        }

        else if (visualize)
        {
            //Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
            Vector3 worldBottomLeft = transform.position - Vector3.right * sizeX / 2 - Vector3.forward * sizeY / 2;
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                    bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                    grid[x, y] = new BFSnode(walkable, worldPoint, x, y);
                }
            }
            visualize = false;
            CreateGrid();
        }
    }



    void CreateGrid()
    {
        startToEnd(start.position, end.position);
    }

    void startToEnd(Vector3 startPos, Vector3 endPos)
    {
        BFSnode startNode = NodeFromWorldPoint(startPos);
        BFSnode endNode = NodeFromWorldPoint(endPos);


        addFirstNode(startNode.x, startNode.y);
        done.Add(grid[startNode.x, startNode.y]);

        while (rows.Count > 0 || cols.Count > 0)
        {
            getR = rows.Dequeue();
            getC = cols.Dequeue();

            if (grid[getR, getC] == endNode)
            {
                reached_end = true;
                pathIt(startNode, endNode);
                break;
            }

            neighbours = Neighbours(grid[getR, getC]);
            nodes_left--;

            if (nodes_left == 0)
            {
                nodes_left = nodes_next;
                nodes_next = 0;
                moves++;
            }
        }

        visualizeit();
    }

    List<BFSnode> Neighbours(BFSnode node)
    {
        List<BFSnode> neighbours = new List<BFSnode>();
        int[] neighbourRow = { -1, 1, 0, 0 };
        int[] neighbourCol = { 0, 0, 1, -1 };

        int X, Y;

        for (int i = 0; i < 4; i++)
        {
            X = node.x + neighbourRow[i];
            Y = node.y + neighbourCol[i];

            if (X < 0 || Y < 0 || X >= gridSizeX || Y >= gridSizeY)
            {
                continue;
            }

            if (visited[X, Y] || !grid[X, Y].walkable)
            {
                continue;
            }

            rows.Enqueue(X);
            cols.Enqueue(Y);

            visited[X, Y] = true;
            done.Add(grid[X, Y]);
            grid[X, Y].parent = node;
            nodes_next++;
        }

        return neighbours;
    }

    void addFirstNode(int x, int y)
    {
        rows.Enqueue(x);
        cols.Enqueue(y);
        visited[x, y] = true;
    }

    BFSnode NodeFromWorldPoint(Vector3 worldPosition)
    {
        /*float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];*/

        float percentX = (worldPosition.x + sizeX / 2) / sizeX;
        float percentY = (worldPosition.z + sizeY / 2) / sizeY;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        int x = Mathf.RoundToInt((sizeX - 1) * percentX);
        int y = Mathf.RoundToInt((sizeY - 1) * percentY);
        return grid[x, y];
    }

    void pathIt(BFSnode startNode, BFSnode endNode)
    {
        BFSnode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
    }

    //void OnDrawGizmos()
    void visualizeit()
    {
        if (grid != null)
        {
            foreach (BFSnode n in grid)
            {
                cube = Instantiate(grids, n.worldPosition, Quaternion.identity);
                cube_array.Add(cube);
                cube.transform.localScale = Vector3.one * (nodeDiameter - .05f);

                n.game = cube;

                if (!n.walkable)
                {
                    n.game.GetComponent<Renderer>().material.color = Color.red;
                }
                if (done.Contains(n))
                {
                    n.game.GetComponent<Renderer>().material.color = Color.green;
                }
                if (path.Contains(n))
                {
                    n.game.GetComponent<Renderer>().material.color = Color.black;
                }
            }
        }
    }

}