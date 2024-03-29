using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public bool condition = false; // indicates that we chose one of the dropdown options

    public bool visualize = false; // button for starting to visualize

    public GameObject plane;

    public int sizeX; // saving plane size
    public int sizeY;

    public bool Obstacle = false;
    public bool newPlane = false;
    public List<GameObject> obstacles;

    public GameObject Sphere;
    public GameObject Capsule;
    GameObject current;

    // Start is called before the first frame update
    /*void Awake()
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
    }*/

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

            nodeDiameter = nodeRadius * 2;
            /*gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
            gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);*/

            gridSizeX = Mathf.RoundToInt(sizeX / nodeDiameter);
            gridSizeY = Mathf.RoundToInt(sizeY / nodeDiameter);
            grid = new DFSnode[gridSizeX, gridSizeY];
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
            Vector3 worldBottomLeft = transform.position - Vector3.right * sizeX / 2 - Vector3.forward * sizeY / 2;
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                    bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                    grid[x, y] = new DFSnode(walkable, worldPoint, x, y);
                }
            }

            //visualize = false;
        }
    }

    public void DFS(int x, int y, DFSnode end)
    {
        if (grid[x, y] == end)
        {
            return;
        }

        else
        {
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
        if (x < 0 || y < 0 || x >= gridSizeX || y >= gridSizeY || visited[x, y] || !grid[x, y].walkable)
        {
            return false;
        }
        return true;
    }

    public DFSnode NodeFromWorldPoint(Vector3 worldPosition)
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

    //void OnDrawGizmos()
    public void visualizeit()
    {
        /*Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if (grid != null)
        {
            foreach (DFSnode n in path)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }*/

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
        }
    }


    /*IEnumerator draw()
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
    }*/
}
