﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class TileDataClass : MonoBehaviour
{
    private PlantAbstractClass Plant;
    private OreAbstractClass Ore;
    public Tilemap TileMap;
    public TileBase Tile;
    public GameObject Clone;
    private bool Watered;

    public void GetTileMap()
    {
        TileMap = GameObject.Find("Floor").GetComponent<Tilemap>();
    }
    public void UpdateTile(Vector3Int posInt)
    {
        TileMap.SetTile(posInt, Tile);
    }

    public TileDataClass()
    {
        TileMap = GameObject.Find("Floor").GetComponent<Tilemap>();
        Plant = null;
        Watered = false;
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
