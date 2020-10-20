﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HoeScript : ToolScript
{
    public TileBase tilledTile;

    public override void useTool()
    {
        grid = FindObjectOfType<Grid>();
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int posInt = base.GetGrid().LocalToCell(pos);
        // Shows the name of the tile at the specified coordinates            
        Debug.Log(base.GetTileMap().GetTile(posInt).name);
        Debug.Log("This is hoeing");
        base.GetTileMap().SetTile(posInt, tilledTile);
    }
}
