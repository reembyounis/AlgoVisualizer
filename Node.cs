using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
	public bool walkable;
	public Vector3 worldPosition;
	public int x, y;
	public int GCost, HCost;
	public Node parent;

	public Node(bool _walkable, Vector3 _worldPos,int _x,int _y)
	{
		walkable = _walkable;
		worldPosition = _worldPos;
		x = _x;
		y = _y;
	}

}