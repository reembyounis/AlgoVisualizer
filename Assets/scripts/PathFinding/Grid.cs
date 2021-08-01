using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{

    public LayerMask unwalkableMask;
    Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;
    public GameObject grids;
    float nodeDiameter;
    int gridSizeX, gridSizeY;
    public GameObject Cube;
    public List<Node> pathing;
    public List<Node> open = new List<Node>();
    //public GameObject grids;
    public Transform spawnValue;
    public GameObject Sphere;
    public GameObject Capsule;
    GameObject cube;
    GameObject current;
    Vector3 newpos = new Vector3(0, 0, 0);
    public bool condition = false; // indicates that we chose one of the dropdown options

    public bool visualize = false; // button for starting to visualize

    public GameObject plane;

    public int sizeX; // saving plane size
    public int sizeY;

    public bool Obstacle = false;
    public bool newPlane = false;
    public List<GameObject> obstacles;
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
            grid = new Node[gridSizeX, gridSizeY];
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
                    grid[x, y] = new Node(walkable, worldPoint, x, y);
                }
            }
            //visualize = false;
        }
    }
    public List<Node> Neighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int X = node.x - 1; X <= node.x + 1; X++)
        {
            for (int Y = node.y - 1; Y <= node.y + 1; Y++)
            {
                if (X == node.x && Y == node.y)
                    continue;

                if (X >= 0 && X < gridSizeX && Y >= 0 && Y < gridSizeY)
                {
                    neighbours.Add(grid[X, Y]);
                }
            }
        }

        return neighbours;
    }


    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        /*float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);*/

        float percentX = (worldPosition.x + sizeX / 2) / sizeX;
        float percentY = (worldPosition.z + sizeY / 2) / sizeY;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((sizeX - 1) * percentX);
        int y = Mathf.RoundToInt((sizeY - 1) * percentY);
        return grid[x, y];
    }

    /*void OnDrawGizmos()
    {
        StartCoroutine(draw());
    }*/

    //void OnDrawGizmos()
    public void visualizing()
    {
        if (grid != null)
        {
            foreach (Node n in grid)
            {
                cube = Instantiate(grids, n.worldPosition, Quaternion.identity);
                cube_array.Add(cube);
                cube.transform.localScale = Vector3.one * (nodeDiameter - .05f);

                n.game = cube;

                if (!n.walkable)
                {
                    n.game.GetComponent<Renderer>().material.color = Color.red;
                }
                if (open.Contains(n))
                {
                    n.game.GetComponent<Renderer>().material.color = Color.green;
                }
                if (pathing.Contains(n))
                {
                    n.game.GetComponent<Renderer>().material.color = Color.black;
                }
            }
            /*foreach (Node n in open)
            {
                n.game.GetComponent<Renderer>().material.color = Color.green;
            }
            foreach (Node n in pathing)
            {
                n.game.GetComponent<Renderer>().material.color = Color.black;
            }*/
        }
    }

    /*IEnumerator draw()
    {
        List<Node> left = new List<Node>();
        List<Node> right = new List<Node>();

        for (int i = gridSizeX - 1; i >= 0; i--)
        {
            int flag = 0;
            for (int j = gridSizeY - 1; j >= 0; j--)
            {

                if (open.Contains(grid[i, j]) && flag == 0)
                {
                    left.Add(grid[i, j]);
                    if (pathing.Contains(grid[i, j]))
                    {
                        flag = 1;
                    }
                }

                else if (open.Contains(grid[i, j]) && flag == 1)
                {
                    right.Add(grid[i, j]);
                }
            }
            flag = 0;

            int l = 0, r = 0;
            left.Reverse();
            while (l < left.Count || r < right.Count)
            {
                if (l == left.Count)
                {

                    //right[r].game.GetComponent<Renderer>().material.color = Color.green;
                    r++;
                }

                else if (r == right.Count)
                {
                    //left[l].game.GetComponent<Renderer>().material.color = Color.green;

                    l++;
                }

                else
                {
                    /*right[r].game.GetComponent<Renderer>().material.color = Color.green;
                    left[l].game.GetComponent<Renderer>().material.color = Color.green;*/
    /*r++;
    l++;
}
yield return new WaitForSeconds(0.05f);
}

left.Clear();
right.Clear();
}

for (int i = 0; i < pathing.Count; i++)
{
float step = speed * Time.deltaTime;
Sphere.transform.position = Vector3.MoveTowards(Sphere.transform.position, new Vector3(pathing[i].worldPosition.x, 1, pathing[i].worldPosition.z), step);
yield return new WaitForSeconds(0.05f);
}
}*/
}


