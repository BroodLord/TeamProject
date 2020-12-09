﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HoeScript : ToolScript
{
    public TileDictionaryClass Dictioary;
    public TileBase tilledTile;
    private void Start()
    {
        mAmount = 0;

    }

    public override void useTool()
    {
        Dictioary = GameObject.FindGameObjectWithTag("TileMapManager").GetComponent<TileDictionaryClass>();
        //TODO - when we add more grids and tilemaps, this will break
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int posInt = grid.LocalToCell(pos);
        if (!Dictioary.TileMapData.ContainsKey(posInt))
        {
            // Shows the name of the tile at the specified coordinates            
            Debug.Log(tileMap.GetTile(posInt).name);
            Debug.Log("This is hoeing");
            TileDataClass Temp = new TileDataClass();
            Dictioary.TileMapData.Add(posInt, Temp);
            Dictioary.TileMapData[posInt].TileMap.SetTile(posInt, tilledTile);
            Dictioary.TileMapData[posInt].Tile = tileMap.GetTile(posInt);
        }
        else
        {
            Debug.Log(tileMap.GetTile(posInt).name);
            Debug.Log("This is already hoed");
        }
    }
}
