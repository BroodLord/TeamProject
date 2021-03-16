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

    private toolList currentTool;

    protected void awake()
    {
        mItemType = ItemTypes.Tool;
        grid = FindObjectOfType<Grid>();
        tileMap = FindObjectOfType<Tilemap>();
    }
}

