using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public int gridX;
    public int gridY;

    public bool IsWall;

    public Vector3 Position;

    public Node Parent;
    public int gCost;
    public int hCost;
    public int FCost { get { return gCost + hCost; } }

    public Node(bool a_isWall,Vector3 a_Pos,int a_gridX, int a_gridY)
    {
        IsWall = a_isWall;
        Position = new Vector3(a_Pos.x,0,a_Pos.z);
        gridX = a_gridX;
        gridY = a_gridY;
    }
}
