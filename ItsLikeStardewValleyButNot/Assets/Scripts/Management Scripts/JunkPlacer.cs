using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class JunkPlacer : MonoBehaviour
{
    public Grid grid;
    public Tilemap tileMap;
    public Sprite dirtSprite;
    public TileBase treeTile;
    public Tilemap nonWalkableTileMap;
    public XMLParser XML;
    public TileDictionaryClass Dictioary;

    public Clock cClock;


    IEnumerator Start()
    {
        // Wait for a set time so the game can register what we have loaded into the level
        yield return new WaitForSeconds(0.1f);
        // Find all the components need.
        Dictioary = GameObject.FindGameObjectWithTag("TileMapManager").GetComponent<TileDictionaryClass>();
        XML = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<XMLParser>();
        cClock = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Clock>();
        //DataBase = new Dictionary<Vector3Int, ItemBase>();
        // If the level is the same as the one we want to check and the reset hasn't been done
        if (SceneManager.GetActiveScene().name == "Forest" && cClock.WeeklyReset[3])
        {
            cClock.WeeklyReset[3] = false;
            PlaceTrees();
        }
        else if (SceneManager.GetActiveScene().name == "Forest")
        {
            foreach (var Childv in Dictioary.TileMapData.ElementAt(4).Value)
            {
                Childv.Value.GetTileMap();
                Childv.Value.TileMap.SetTile(Childv.Key, Childv.Value.Tile);
            }
        }

        if (SceneManager.GetActiveScene().name == "Mines" && cClock.WeeklyReset[0])
        {
            // Reset the reset to false so it doesn't do it again
            cClock.WeeklyReset[0] = false;
            // place rocks
            PlaceRocks();
        }
        // if the level is the same and we have placed rocks before then we want to loop through all the dictionary and place the rocks
        else if(SceneManager.GetActiveScene().name == "Mines")
        {
            foreach (var Childv in Dictioary.TileMapData.ElementAt(1).Value)
            {
                Childv.Value.GetTileMap();
                Childv.Value.TileMap.SetTile(Childv.Key, Childv.Value.Tile);
            }
        }
        // Same as above
        if (SceneManager.GetActiveScene().name == "Mines1" && cClock.WeeklyReset[1])
        {
            cClock.WeeklyReset[1] = false;
            PlaceRocks();
        }
        else if (SceneManager.GetActiveScene().name == "Mines1")
        {
            foreach (var Childv in Dictioary.TileMapData.ElementAt(2).Value)
            {
                Childv.Value.GetTileMap();
                Childv.Value.TileMap.SetTile(Childv.Key, Childv.Value.Tile);
            }
        }
        // Same as above
        if (SceneManager.GetActiveScene().name == "Mines2" && cClock.WeeklyReset[2])
        {
            cClock.WeeklyReset[2] = false;
            PlaceRocks();
        }
        else if (SceneManager.GetActiveScene().name == "Mines2")
        {
            foreach (var Childv in Dictioary.TileMapData.ElementAt(3).Value)
            {
                Childv.Value.GetTileMap();
                Childv.Value.TileMap.SetTile(Childv.Key, Childv.Value.Tile);
            }
        }
    }

    // Loop through the max and mines of the tilemap and set the tile to a tree if we can. (random chance)
    public void PlaceTrees()
    {
        Vector3 worldMin = tileMap.transform.TransformPoint(tileMap.localBounds.min);
        Vector3 worldMax = tileMap.transform.TransformPoint(tileMap.localBounds.max);


        for (int x = (int)worldMin.x; x < (int)worldMax.x; x++)
        {
            for (int y = (int)worldMin.y; y < (int)worldMax.y; y++)
            {

                Vector3 pos = new Vector3(x, y, 0);
                Vector3Int posInt = grid.WorldToCell(pos);
                if (Random.Range(1, 100) <= 40 && tileMap.GetSprite(posInt) == dirtSprite)
                {
                    //nonWalkableTileMap.SetTile(posInt, treeTile);
                    // Create a new slot
                    TileDataClass Temp = new TileDataClass();
                    // Added the new slot
                    Dictioary.TileMapData.ElementAt(4).Value.Add(posInt, Temp);
                    // Set that tile sprite 
                    Dictioary.TileMapData.ElementAt(4).Value[posInt].TileMap.SetTile(posInt, treeTile);
                    // Set the tile to be the one we set
                    Dictioary.TileMapData.ElementAt(4).Value[posInt].Tile = tileMap.GetTile(posInt);
                    ItemBase Item = new ItemBase();
                    Item = this.gameObject.AddComponent<ItemBase>() as ItemBase;
                    // Sets up and ore item
                    Item.SetUpThisItem(XML.items.ElementAt(44).Value.bItemType, XML.items.ElementAt(44).Value.bName, 1,
                        XML.items.ElementAt(44).Value.bStackable, XML.items.ElementAt(44).Value.bSrcImage, XML.items.ElementAt(44).Value.bSoundEffect,
                        XML.items.ElementAt(44).Value.bTile, XML.items.ElementAt(44).Value.bPrefab, XML.items.ElementAt(44).Value.bSellPrice, XML.items.ElementAt(44).Value.bCustomData);
                    // Set up the item in the rock at the given location.
                    Dictioary.TileMapData.ElementAt(4).Value[posInt].SetItem(Item);
                    //DataBase.Add(posInt, Item);
                }

            }

        }
        
    }

    // Same as the above but with a few edits
    public void PlaceRocks()
    {
        // we want to have a index so determine what level we are on
        int INDEX = 0;
        if (SceneManager.GetActiveScene().name == "Mines") { INDEX = 1;}
        else if (SceneManager.GetActiveScene().name == "Mines1") { INDEX = 2; }
        else if (SceneManager.GetActiveScene().name == "Mines2") { INDEX = 3; }

        Vector3 worldMin = tileMap.transform.TransformPoint(tileMap.localBounds.min);
        Vector3 worldMax = tileMap.transform.TransformPoint(tileMap.localBounds.max);


        for (int x = (int)worldMin.x; x < (int)worldMax.x; x++)
        {
            for (int y = (int)worldMin.y; y < (int)worldMax.y; y++)
            {
                Vector3 pos = new Vector3(x, y, 0);
                Vector3Int posInt = grid.WorldToCell(pos);
                if (Random.Range(1, 100) <= 40 && tileMap.GetSprite(posInt) == dirtSprite)
                {
                    //nonWalkableTileMap.SetTile(posInt, treeTile);
                    // Create a new slot
                    TileDataClass Temp = new TileDataClass();
                    // Added the new slot
                    Dictioary.TileMapData.ElementAt(INDEX).Value.Add(posInt, Temp);
                    // Set that tile sprite 
                    Dictioary.TileMapData.ElementAt(INDEX).Value[posInt].TileMap.SetTile(posInt, treeTile);
                    // Set the tile to be the one we set
                    Dictioary.TileMapData.ElementAt(INDEX).Value[posInt].Tile = tileMap.GetTile(posInt);
                    // Generate two random numbers, one for what ore will spawn and then what amount will spawn with it.
                    int ItemIndex = Random.Range(35, 43);
                    int RandAmount = Random.Range(1, 5);
                    Ore Item = new Ore();
                    Item = this.gameObject.AddComponent<Ore>() as Ore;
                    // Sets up and ore item
                    Item.SetUpThisItem(XML.items.ElementAt(ItemIndex).Value.bItemType, XML.items.ElementAt(ItemIndex).Value.bName, RandAmount,
                        XML.items.ElementAt(ItemIndex).Value.bStackable, XML.items.ElementAt(ItemIndex).Value.bSrcImage, XML.items.ElementAt(ItemIndex).Value.bSoundEffect,
                        XML.items.ElementAt(ItemIndex).Value.bTile, XML.items.ElementAt(ItemIndex).Value.bPrefab, XML.items.ElementAt(ItemIndex).Value.bSellPrice, XML.items.ElementAt(ItemIndex).Value.bCustomData);
                    // Set up the item in the rock at the given location.
                    Dictioary.TileMapData.ElementAt(INDEX).Value[posInt].SetOre(Item);
                    //DataBase.Add(posInt, Item);
                }

            }

        }

    }


    public void PlaceForage()
    {
        
    }
}
