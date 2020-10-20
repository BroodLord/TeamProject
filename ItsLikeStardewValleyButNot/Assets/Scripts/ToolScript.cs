using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

enum toolList
{
    hoe,
    axe,
    pick,
    wateringCan
}

public class ToolScript : MonoBehaviour
{
    public Grid grid;
    public Tilemap tileMap;
    
    private toolList currentTool;

    public virtual void useTool()
    {

    }
}

