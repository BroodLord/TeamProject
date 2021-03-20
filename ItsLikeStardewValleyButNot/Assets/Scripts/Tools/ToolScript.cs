using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

enum toolList
{
    hoe,
    axe,
    pick,
    wateringCan, 
    fishingRod
}

public abstract class ToolScript : ItemBase
{
    public bool ToolUsed;
    private toolList currentTool;

    protected void awake()
    {
        ToolUsed = false;
        mItemType = ItemTypes.Tool;
        grid = FindObjectOfType<Grid>();
        tileMap = FindObjectOfType<Tilemap>();
    }
}

