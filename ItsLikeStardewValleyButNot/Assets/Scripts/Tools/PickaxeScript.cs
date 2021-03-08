using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PickaxeScript : ToolScript
{
    public GameObject ToolItems;
    public TileDictionaryClass Dictioary; 
    //public TileBase ChangedTile;
    public Tilemap TileMap;
    private int INDEX;
    public JunkPlacer rockPlacer;

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

    // This will get the ore that is attached to the rock we are mining and set it to the players item list
    public ItemBase GetRockItem(OreAbstractClass ore)
    {
        ToolItems = GameObject.Find("TemplateTools");
        XMLParser XML = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<XMLParser>();
        GameObject SubGameObject = new GameObject(ore.XMLName);
        SubGameObject.transform.parent = ToolItems.transform;
        ItemBase PlantItem = ore;
        return PlantItem;
    }

    public override void useTool()
    {
        /*Assigned index based on level*/
        if (SceneManager.GetActiveScene().name == "Mines")
        {
            INDEX = 1;
        }
        if (SceneManager.GetActiveScene().name == "Mines1")
        {
            INDEX = 2;
        }
        if(SceneManager.GetActiveScene().name == "Mines2")
        {
            INDEX = 3;
        }
        /*****************************************************/

        // Only use the tool if we are on any of the mines levels
        if (SceneManager.GetActiveScene().name == "Mines" || SceneManager.GetActiveScene().name == "Mines1" || SceneManager.GetActiveScene().name == "Mines2")
        {
            Dictioary = GameObject.FindGameObjectWithTag("TileMapManager").GetComponent<TileDictionaryClass>();
            //rockPlacer = GameObject.FindGameObjectWithTag("Rockplacer").GetComponent<JunkPlacer>();
            cInventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryClass>();
            //TODO - when we add more grids and tilemaps, this will break
            // Get the camera position to world.
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int posInt = grid.WorldToCell(pos);
            // check if the dictionary has an ore at the given location
            if (Dictioary.TileMapData.ElementAt(INDEX).Value[posInt].HasOre())
            {
                OreAbstractClass Ore = Dictioary.TileMapData.ElementAt(INDEX).Value[posInt].GetOre();
                AudioSource Audio = gameObject.GetComponentInParent<AudioSource>();
                /*Play the sound*/
                Audio.clip = GetSoundEffect();
                Audio.Play();
                Debug.Log("GATHERED ORE");
                /****************/
                // Assess the ore
                InventoryAssessment(GetRockItem(Ore), posInt);
                // Edit the dictionary
                Dictioary.TileMapData.ElementAt(INDEX).Value[posInt].TileMap.SetTile(posInt, GetTile());
                Dictioary.TileMapData.ElementAt(INDEX).Value.Remove(posInt);
            }
        }
    }
}
