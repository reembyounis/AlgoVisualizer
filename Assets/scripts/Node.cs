using UnityEngine;
using System.Collections;

public class Node {
	
	public bool walkable;
	public Vector3 worldPosition;
	public int x;
	public int y;
	public int gCost;
	public int hCost;
	public Node parent;
	
	public Node(bool Walkable, Vector3 WorldPos, int X, int Y) {
		walkable = Walkable;
		worldPosition = WorldPos;
		x = X;
		y = Y;
	}
}