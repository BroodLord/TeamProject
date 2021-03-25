using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public Vector3 worldPosition;
    public Vector3Int gridPosition;

    public Node parent;

    public Node(Vector3Int _gridpos)
    {
        gridPosition = _gridpos;
    }
}
