using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class ScytheTool : ToolScript
{
    public TileDictionaryClass Dictioary;

    public void InventoryAssessment(ItemBase Item, Vector3Int ID)
    {
        //ItemBase Item = GO.GetComponent<ItemBase>();
        if (!cInventory.HasItem(Item.GetName()))
        {
            cInventory.AddItem(Item);
        }
        else
        {
            cInventory.AddAmount(Item.GetName(), Item.GetAmount());
        }
        //TileDataClass Temp = new TileDataClass();
        //Dictioary.TileMapData.Add(ID, Temp);
    }

    public ItemBase GetPlantItem(string Name)
    {
        XMLParser XML = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<XMLParser>();
        foreach(var i in XML.items)
        {
            if(i.Key.Equals(Name))
            {
                ItemBase PlantItem = new ItemBase();
                PlantItem.SetUpThisItem(i.Value.bItemType, i.Value.bName, i.Value.bAmount, i.Value.bStackable, i.Value.bSrcImage, i.Value.bTile, i.Value.bSellPrice);
                return PlantItem;
            }
        }
        return null;
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
                //Debug.Log(tileMap.GetTile(posInt).name);
                Debug.Log("GATHERED PLANT");
                InventoryAssessment(GetPlantItem(Plant.XMLName), posInt);
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
