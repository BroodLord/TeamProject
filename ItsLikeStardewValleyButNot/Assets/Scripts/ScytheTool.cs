using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class ScytheTool : ToolScript
{
    public TileDictionaryClass Dictioary;
    //public TileBase tilledTile;
    private void Start()
    {
        mAmount = 0;
    }

    public void InventoryAssessment(GameObject GO, Vector3Int ID)
    {
        ItemBase Item = GO.GetComponent<ItemBase>();
        if (!cInventory.HasItem(GO.name))
        {
            cInventory.AddItem(Item, 1);
        }
        else
        {
            cInventory.AddAmount(GO.name, Item.GetAmount());
        }
        //TileDataClass Temp = new TileDataClass();
        //Dictioary.TileMapData.Add(ID, Temp);
    }

    public override void useTool()
    {
        Dictioary = GameObject.FindGameObjectWithTag("TileMapManager").GetComponent<TileDictionaryClass>();
        //TODO - when we add more grids and tilemaps, this will break
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int posInt = grid.LocalToCell(pos);
        if (Dictioary.TileMapData[posInt].HasPlant())
        {
            PlantAbstractClass Plant = Dictioary.TileMapData[posInt].GetPlant();
            if (Plant.mHarvestable)
            {
                cInventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryClass>();
                // Shows the name of the tile at the specified coordinates            
                Debug.Log(tileMap.GetTile(posInt).name);
                Debug.Log("GATHERED PLANT");
                if(Plant is TurnipClass)
                {
                    InventoryAssessment(cInventory.Plants[0], posInt);
                }
                Plant.DestoryPlant();
            }
        }
        else
        {
            Debug.Log(tileMap.GetTile(posInt).name);
            Debug.Log("No Plant to Gather");
        }
    }
}
