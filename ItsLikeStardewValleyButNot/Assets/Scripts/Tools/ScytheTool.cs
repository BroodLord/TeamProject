using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class ScytheTool : ToolScript
{
    public GameObject ToolItems;
    public TileDictionaryClass Dictioary;

    // This function will assess if an item is already in the inventory, either adding it or increasing the amount.

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

    // This will get the planted item that was planted in the dictionary
    public ItemBase GetPlantItem(string Name)
    {
        ToolItems = GameObject.Find("TemplateTools");
        XMLParser XML = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<XMLParser>();
        GameObject SubGameObject = new GameObject(Name);
        SubGameObject.transform.parent = ToolItems.transform;
        // Loop through all the XML items
        foreach (var i in XML.items)
        {
            // if we find the plant on the xml then we want to set up the item with the base value of the xml item.
            if(i.Key.Equals(Name))
            {
                ItemBase PlantItem = SubGameObject.gameObject.AddComponent<ItemBase>() as ItemBase;
                PlantItem.SetUpThisItem(i.Value.bItemType, i.Value.bName, i.Value.bAmount, i.Value.bStackable, i.Value.bSrcImage, 
                                        i.Value.bSoundEffect, i.Value.bTile, i.Value.bPrefab, i.Value.bSellPrice, i.Value.bCustomData);
                return PlantItem;
            }
        }
        return null;
    }

    public override void useTool()
    {
        if (SceneManager.GetActiveScene().name == "Farmland")
        {
            Dictioary = GameObject.FindGameObjectWithTag("TileMapManager").GetComponent<TileDictionaryClass>();
            //TODO - when we add more grids and tilemaps, this will break
            // Get the mouse positon in world
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int posInt = grid.LocalToCell(pos);
            // Check if the dictionary has a plant there
            if (Dictioary.TileMapData.ElementAt(0).Value[posInt].HasPlant())
            {
                // check if that plant is harvestable
                PlantAbstractClass Plant = Dictioary.TileMapData.ElementAt(0).Value[posInt].GetPlant();
                if (Plant.mHarvestable)
                {
                    cInventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryClass>();
                    // Play the sound effect
                    AudioSource Audio = gameObject.GetComponentInParent<AudioSource>();
                    Audio.clip = GetSoundEffect();
                    Audio.Play();
                    ToolUsed = true;
                    /************************/
                    //Debug.Log("GATHERED PLANT");
                    // Asset the plant and destory the planted one.
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
}
