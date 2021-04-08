using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BFSgrid : MonoBehaviour
{

    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    BFSnode[,] grid;
    bool[,] visited;
    float nodeDiameter;
    int gridSizeX, gridSizeY;
    public GameObject Cube;
    public GameObject grids;
    public Transform Spawn;
    List<BFSnode> done = new List<BFSnode>();
    public Queue<int> rows = new Queue<int>();
    public Queue<int> cols = new Queue<int>();
    int nodes_next = 0;
    int moves=0;
    public Transform start;
    public Transform end;
    int nodes_left = 1;
    bool reached_end = false;
    int getR, getC;
    List<BFSnode> neighbours = new List<BFSnode>();
    List<BFSnode> path = new List<BFSnode>();

    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        grid = new BFSnode[gridSizeX, gridSizeY];
        visited = new bool[gridSizeX, gridSizeY];
        Update();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 spawnPos = new Vector3(Random.Range(-Spawn.position.x, Spawn.position.x), 5, Random.Range(-Spawn.position.z, Spawn.position.z));
            Instantiate(Cube, spawnPos, Quaternion.identity);

        }

        else if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateGrid();
        }
    }

    void CreateGrid()
    {
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new BFSnode(walkable, worldPoint, x, y);
            }
        }

        startToEnd(start.position,end.position);
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
                pathIt(startNode,endNode);
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
    }

    public List<BFSnode> Neighbours(BFSnode node)
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
            grid[X,Y].parent=node;
            nodes_next++;
        }

        return neighbours;
    }

    public void addFirstNode(int x, int y)
    {
        rows.Enqueue(x);
        cols.Enqueue(y);
        visited[x, y] = true;
    }

    public BFSnode NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
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

    void OnDrawGizmos()
    {
        StartCoroutine(draw());
    }

    IEnumerator draw()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if (grid != null)
        {
            foreach (BFSnode n in grid)
            {
                GameObject cube = Instantiate(grids, n.worldPosition, Quaternion.identity);
                cube.transform.localScale = Vector3.one * (nodeDiameter - .05f);

                n.game = cube;

                if (!n.walkable)
                {
                    n.game.GetComponent<Renderer>().material.color = Color.red;
                }
            }
        }

        foreach (BFSnode n in done)
        {
            n.game.GetComponent<Renderer>().material.color = Color.green;
            yield return new WaitForSeconds(0.0005f);
        }

        foreach (BFSnode n in path)
        {
            n.game.GetComponent<Renderer>().material.color = Color.black;
        }
    }
}