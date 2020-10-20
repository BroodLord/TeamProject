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

public abstract class ToolScript : MonoBehaviour
{
    public Grid grid;
    public Tilemap tileMap;

    private toolList currentTool;

    private void Start()
    {
        grid = FindObjectOfType<Grid>();
        tileMap = FindObjectOfType<Tilemap>();
    }
    public virtual void useTool() { }

    public Grid GetGrid() { return grid; }
    public Tilemap GetTileMap () { return tileMap; }
}

