using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WateringCanScript : ToolScript
{
    public TileDictionaryClass Dictioary;
    public TileBase wateredTile;

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
        if (Dictioary.TileMapData.ContainsKey(posInt))
        {
            // Shows the name of the tile at the specified coordinates    
            if (!Dictioary.TileMapData[posInt].IsWatered())
            {
                Debug.Log(tileMap.GetTile(posInt).name);
                Debug.Log("This is watered");
                Dictioary.TileMapData[posInt].TileMap.SetTile(posInt, wateredTile);
                Dictioary.TileMapData[posInt].Tile = tileMap.GetTile(posInt);
                Dictioary.TileMapData[posInt].SetWatered(true);
                PlantAbstractClass P = Dictioary.TileMapData[posInt].GetPlant();
            }
            else
            {
                Debug.Log("This is already watered");
            }
        }
        else
        {
            Debug.Log(tileMap.GetTile(posInt).name);
            Debug.Log("Needs to be Hoed first");
        }
    }
}
