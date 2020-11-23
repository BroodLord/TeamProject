using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WateringCanScript : ToolScript
{
    public TileBase wateredTile;

    public override void useTool()
    {
        //TODO - when we add more grids and tilemaps, this will break
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int posInt = grid.LocalToCell(pos);
        // Shows the name of the tile at the specified coordinates            
        Debug.Log(tileMap.GetTile(posInt).name);
        Debug.Log("This is watering");
        tileMap.SetTile(posInt, wateredTile);
    }
}
