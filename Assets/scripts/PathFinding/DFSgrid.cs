using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFSgrid : MonoBehaviour
{

    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    DFSnode[,] grid;
    bool[,] visited;
    float nodeDiameter;
    int gridSizeX, gridSizeY;
    public GameObject Cube;
    public GameObject grids;
    public Transform Spawn;
    public Transform start;
    public Transform end;
    List<DFSnode> path = new List<DFSnode>();

    // Start is called before the first frame update
    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        grid = new DFSnode[gridSizeX, gridSizeY];
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
                grid[x, y] = new DFSnode(walkable, worldPoint, x, y);
            }
        }

        startToEnd(start.position, end.position);
    }

    void startToEnd(Vector3 startPos, Vector3 endPos)
    {
        DFSnode startNode = NodeFromWorldPoint(startPos);
        DFSnode endNode = NodeFromWorldPoint(endPos);

        DFS(startNode.x, startNode.y, endNode);

    }


    void DFS(int x, int y, DFSnode end)
    {
        if (grid[x, y] == end)
        {
            return;
        }

        else{
            visited[x, y] = true;
            path.Add(grid[x, y]);

            if (isValid(x - 1, y))
            {
                DFS(x - 1, y, end);
            }

            else if (isValid(x, y + 1))
            {
                DFS(x, y + 1, end);
            }

            else if (isValid(x + 1, y))
            {
                DFS(x + 1, y, end);
            }

            else if (isValid(x, y - 1))
            {
                DFS(x, y - 1, end);
            }
        }

    }

    bool isValid(int x, int y)
    {
        if (x < 0 || y < 0 || x >= gridSizeX || y >= gridSizeY || visited[x,y] || !grid[x,y].walkable)
        {
            return false;
        }
        return true;
    }

    public DFSnode NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
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
            foreach (DFSnode n in grid)
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

        foreach (DFSnode n in path)
        {
            n.game.GetComponent<Renderer>().material.color = Color.green;
            yield return new WaitForSeconds(0.0005f);
        }
    }
}
