using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSeed : ToolScript
{
    private Vector3Int ID;
    private Vector3 pos;
    public TileDictionaryClass Dictioary;
    public GameObject PlantPrefab;
    public override void useTool()
    {
        Dictioary = GameObject.FindGameObjectWithTag("TileMapManager").GetComponent<TileDictionaryClass>();
        //TODO - when we add more grids and tilemaps, this will break
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int posInt = grid.LocalToCell(pos);
        if (Dictioary.TileMapData.ContainsKey(posInt) && !Dictioary.TileMapData[posInt].HasPlant())
        {
            // Shows the name of the tile at the specified coordinates  
            ID = posInt;
            //Debug.Log(tileMap.GetTile(posInt).name);
            Debug.Log("Seed Planted");
            GameObject Clone;
            Clone = Instantiate(PlantPrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
            PlantAbstractClass TempPlant = Clone.GetComponent<PlantAbstractClass>();
            TempPlant.ID = ID;
            TempPlant.pos = pos;
            TempPlant.AddAmount(1);
            Dictioary.TileMapData[posInt].SetPlant(TempPlant);
            Dictioary.TileMapData[posInt].Clone = Clone;
            if (Dictioary.TileMapData[posInt].IsWatered()) { TempPlant.mWatered = true; }
            else { TempPlant.mWatered = false; }
        }
        else
        {
            Debug.Log(tileMap.GetTile(posInt).name);
            Debug.Log("Plot already has a plant placed there");
        }
    }
}
