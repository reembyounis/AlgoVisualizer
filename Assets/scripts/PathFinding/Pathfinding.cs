using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Pathfinding : MonoBehaviour {

	public Transform start;
	public Transform end;
	Grid grid;

	void Awake() {
		grid = GetComponent<Grid> ();
	}

	void Update() {
		startToEnd (start.position, end.position);
	}

	void startToEnd(Vector3 startPos, Vector3 endPos) {
		Node startNode = grid.NodeFromWorldPoint(startPos);
		Node endNode = grid.NodeFromWorldPoint(endPos);

		List<Node> opened = new List<Node>();
		List<Node> closed = new List<Node>();
		opened.Add(startNode);
        grid.open.Add(startNode);

		while (opened.Count > 0) {
			Node current = opened[0];
			for (int i = 1; i < opened.Count; i ++) {
				int fCostCurrent = current.hCost + current.gCost;
                int fCostOpened = opened[i].hCost + opened[i].gCost;
				if (fCostOpened< fCostCurrent || fCostOpened == fCostCurrent) {
					if (opened[i].hCost < current.hCost)
						current = opened[i];
				}
			}
			opened.Remove(current);
			closed.Add(current);

			if (current == endNode) {
				pathIt(startNode,endNode);
				return;
			}

			foreach (Node neighbour in grid.Neighbours(current)) {
				if (!neighbour.walkable || closed.Contains(neighbour)) {
					continue;
				}

				int newCostToNeighbour = current.gCost + GetDistance(current, neighbour);
				if (newCostToNeighbour < neighbour.gCost || !opened.Contains(neighbour)) {
					neighbour.gCost = newCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, endNode);
					neighbour.parent = current;

					if (!opened.Contains(neighbour))
						opened.Add(neighbour);
                        grid.open.Add(neighbour);
				}
			}
		}
	}

	void pathIt(Node startNode, Node endNode) {
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse();
		
		grid.pathing = path;

	}

	int GetDistance(Node nodeA, Node nodeB) {
		int xDistance = Mathf.Abs(nodeA.x- nodeB.x);
		int yDistance = Mathf.Abs(nodeA.y - nodeB.y);

		if (xDistance > yDistance)
			return 14*yDistance + 10* (xDistance-yDistance);
		return 14*xDistance + 10 * (yDistance-xDistance);
	}
}