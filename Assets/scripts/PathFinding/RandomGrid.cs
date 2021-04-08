using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomGrid : MonoBehaviour
{

    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;
    float nodeDiameter;
    int gridSizeX, gridSizeY;
    public GameObject Cube;
    public List<Node> pathing;
    public List<Node> open = new List<Node>();
    public GameObject grids;
    public Transform spawnValue;

    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        grid = new Node[gridSizeX, gridSizeY];
        Update();
    }

    /*void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnValue.position.x, spawnValue.position.x), 5, Random.Range(-spawnValue.position.z, spawnValue.position.z));
            Instantiate(Cube, spawnPos, Quaternion.identity);
        }
        Update();
    }*/

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
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

    IEnumerator draw(){
		//Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
		open.Reverse();
		if (grid != null)
		{
			foreach (Node n in grid)
			{
				GameObject cube = Instantiate(grids, n.worldPosition, Quaternion.identity);
				cube.transform.localScale = Vector3.one * (nodeDiameter- .05f);
				//Renderer rend = cube.GetComponent<Renderer>();
				
				//n.renderer=rend;
				n.game=cube;

				if (!n.walkable)
				{
					//rend.material.color = Color.red;
					n.game.GetComponent<Renderer>().material.color=Color.red;
				}
			}

			/*Node[,] newGrid = new Node[gridSizeX,gridSizeY];
			int row=0;
			int col=0;*/

			List<Node> left = new List<Node>();
			List<Node> right = new List<Node>();

			for(int i=gridSizeX-1;i>=0;i--){
				int flag = 0;
				for(int j=gridSizeY-1;j>=0;j--){
					//newGrid[row,col]=grid[i,j];

					if(open.Contains(grid[i,j]) && flag==0){
						left.Add(grid[i,j]);
						if(pathing.Contains(grid[i,j])){
							flag=1;
						}
					}

					else if(open.Contains(grid[i,j]) && flag==1){
						right.Add(grid[i,j]);
					}

					/*col++;
					if(col==gridSizeY){
						row++;
						col=0;
					}*/
				}
				flag = 0;

				int l=0,r=0;
				left.Reverse();
				while(l<left.Count || r<right.Count){
					if(l==left.Count){

						//right[r].renderer.material.color = Color.green;
						right[r].game.GetComponent<Renderer>().material.color=Color.green;

						r++;
					}

					else if(r==right.Count){
						//left[l].renderer.material.color = Color.green;
						left[l].game.GetComponent<Renderer>().material.color=Color.green;

						l++;
					}

					else{


						//right[r].renderer.material.color = Color.green;
						//left[l].renderer.material.color = Color.green;
						right[r].game.GetComponent<Renderer>().material.color=Color.green;
						left[l].game.GetComponent<Renderer>().material.color=Color.green;

						r++;
						l++;
					}
					yield return new WaitForSeconds(0.05f);
				}

				left.Clear();
				right.Clear();	
			}


			/*
			foreach(Node n in newGrid){
				if(pathing.Contains(n)){
					n.game.GetComponent<Renderer>().material.color=Color.black;
				}
				else{ 
					n.renderer.material.color = Color.green;
				}
				yield return new WaitForSeconds(0.05f);
			}*/
		}
	}
}