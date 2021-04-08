using UnityEngine;
using System.Collections;

public class BFSnode
{

    public bool walkable;
    public Vector3 worldPosition;
    public int x;
    public int y;
    public BFSnode parent;
    public Renderer renderer;
    public GameObject game;

    public BFSnode(bool Walkable, Vector3 WorldPos, int X, int Y)
    {
        walkable = Walkable;
        worldPosition = WorldPos;
        x = X;
        y = Y;
    }
}