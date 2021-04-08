using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFSnode
{
    public bool walkable;
    public Vector3 worldPosition;
    public int x;
    public int y;
    public DFSnode parent;
    public Renderer renderer;
    public GameObject game;

    public DFSnode(bool Walkable, Vector3 WorldPos, int X, int Y)
    {
        walkable = Walkable;
        worldPosition = WorldPos;
        x = X;
        y = Y;
    }
}
