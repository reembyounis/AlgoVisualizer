using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridMaker : MonoBehaviour
{

	public LayerMask unwalkableMask;
	public Vector2 gridWorldSize;
	public float nodeRadius;
	public GameObject grids;
	Node[,] grid;
	float nodeDiameter;
	int gridSizeX, gridSizeY;
	public List<Node> path;
	public List<Node> open=new List<Node>();
	public GameObject Cube;

	void Start()
	{
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
		Update();
	}

    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = Random.Range(-9f, 9f);
			Vector3 spawnPos = Camera.main.ScreenToWorldPoint(mousePos);

			Instantiate(Cube, spawnPos, Quaternion.identity);
		}

		else if (Input.GetKeyDown(KeyCode.Space))
        {
			CreateGrid();
        }
	}

    void CreateGrid()
    {
		grid = new Node[gridSizeX, gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
				grid[x, y] = new Node(walkable, worldPoint,x,y);
			}
		}

	}

	public List<Node> Neighbours(Node node)
	{
		List<Node> neighbours = new List<Node>();
		for(int X = node.x - 1; X <= node.x+1; X++)
        {
			for(int Y = node.y; Y <= node.y+1; Y++)
            {
				if(X == node.x && Y == node.y)
                {
					continue;
                }

				if(X >= 0 && X < gridSizeX && Y >= 0 && Y < gridSizeY)
                {
					neighbours.Add(grid[X, Y]);
                }
            }
        }
		return neighbours;
	}

	public Node NodeFromWorldPoint(Vector3 worldPosition)
	{
		float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
		float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
		return grid[x, y];
	}

	void DrawIt()
	{
		//Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

		if (grid != null)
		{
			foreach (Node n in grid)
			{
				GameObject cube = Instantiate(grids, n.worldPosition, Quaternion.identity);
				cube.transform.localScale = Vector3.one * (nodeDiameter - .1f);
				Renderer rend = cube.GetComponent<Renderer>();
				
				if (!n.walkable)
                {
					rend.material.color = Color.red;

				}
				else if (open.Contains(n) && !path.Contains(n))
				{
					rend.material.color = Color.green;

				}

				else if(path.Contains(n))
                {
					rend.material.color = Color.black;
				}
			}
		}
	}
}
