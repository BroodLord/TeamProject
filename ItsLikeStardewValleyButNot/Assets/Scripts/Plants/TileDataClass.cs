using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine;

// I don't think this need commenting as its pretty easy to understand but ask me if you have any questions


public class TileDataClass : MonoBehaviour
{
    private PlantAbstractClass Plant;
    private OreAbstractClass Ore;
    private ItemBase Item;
    public Tilemap TileMap;
    public TileBase Tile;
    public GameObject Clone;
    private bool Watered;
    private bool NeedsFloor;

    public void GetTileMap()
    {
        TileMap = GameObject.Find("Floor").GetComponent<Tilemap>();
    }
    public void UpdateTile(Vector3Int posInt)
    {
        TileMap.SetTile(posInt, Tile);
    }

    private void Update()
    {
        if(NeedsFloor)
        {
            GetTileMap();
        }
    }

    public TileDataClass()
    {
        if (SceneManager.GetActiveScene().name != "LoadSaveScene")
        {
            TileMap = GameObject.Find("Floor").GetComponent<Tilemap>();
        }
        else
        {
            NeedsFloor = true;
        }
        Plant = null;
        Watered = false;
    }

    public bool HasItem()
    {
        if (Item != null)
        {
            return true;
        }
        return false;
    }

    public ItemBase GetItem()
    {
        return Item;
    }

    public void SetItem(ItemBase Item)
    {
        this.Item = Item;
    }

    public bool HasOre()
    {
        if (Ore != null)
        {
            return true;
        }
        return false;
    }

    public OreAbstractClass GetOre()
    {
        return Ore;
    }

    public void SetOre(OreAbstractClass P)
    {
        Ore = P;
    }

    public bool HasPlant()
    {
        if (Plant != null)
        {
            return true;
        }
        return false;
    }

    public PlantAbstractClass GetPlant()
    {
        return Plant;
    }

    public void SetPlant(PlantAbstractClass P)
    {
        Plant = P;
    }

    public bool IsWatered()
    {
        return Watered;
    }

    public void SetWatered(bool B)
    {
        Watered = B;
        if(Plant != null)
        {
            Plant.mWatered = B;
        }
    }

}
