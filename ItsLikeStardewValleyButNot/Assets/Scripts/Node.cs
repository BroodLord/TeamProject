using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public Vector3Int worldPosition;

    public Node parent;

    public Node(Vector3Int _worldpos)
    {
        worldPosition = _worldpos;
    }
}
