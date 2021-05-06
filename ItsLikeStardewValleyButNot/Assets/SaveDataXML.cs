﻿using System;
using System.Collections;
using System.Xml;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;

public struct RockData
{
    public string ItemType;
    public string OreName;
    public float[] ID;
    public float[] Pos;
    public string Tile;
    public string XMLName;
    public float Amount;

    public void SetUp(string itemType, string oreName, float[] id, 
                      float[] pos, string tile, string xmlName, float amount)
    {
        ID = new float[3];
        Pos = new float[3];
        ItemType = itemType;
        OreName = oreName;
        ID[0] = id[0]; ID[1] = id[1]; ID[2] = id[2];
        Pos[0] = pos[0]; Pos[1] = pos[1]; Pos[2] = pos[2];
        Tile = tile;
        XMLName = xmlName;
        Amount = amount;
    }
}

public struct TreeData
{
    public string ItemType;
    public string TreeName;
    public float[] ID;
    public float[] Pos;
    public string Tile;
    public string XMLName;
    public float Amount;

    public void SetUp(string itemType, string treeName, float[] id,
                      float[] pos, string tile, string xmlName, float amount)
    {
        ID = new float[3];
        Pos = new float[3];
        ItemType = itemType;
        TreeName = treeName;
        ID[0] = id[0]; ID[1] = id[1]; ID[2] = id[2];
        Pos[0] = pos[0]; Pos[1] = pos[1]; Pos[2] = pos[2];
        Tile = tile;
        XMLName = xmlName;
        Amount = amount;
    }
}

public struct PlantData
{
    public string ItemType;
    public int Amount;
    public int CurrentDays;
    public int GrowthTime;
    public string[] GrowthSprite;
    public float[] ID;
    public float[] Pos;
    public string Tile;
    public bool Harvestable;
    public bool Growth;
    public string XMLName;
    public bool Watered;

    public void SetUp(Vector3Int id, Vector3Int pos, string tile, bool watered)
    {
        ID = new float[3];
        Pos = new float[3];
        ID[0] = id[0]; ID[1] = id[1]; ID[2] = id[2];
        Pos[0] = pos[0]; Pos[1] = pos[1]; Pos[2] = pos[2];
        Tile = tile;
        Watered = watered;
    }

    public void SetUp(int currentDays, int growthTime, string type, string image, int amount, string[] growthSprite, 
                      Vector3Int id, Vector3Int pos, string tile, bool harvestable, bool growth, string xmlName, bool watered)
    {
        ID = new float[3];
        Pos = new float[3];
        GrowthSprite = new string[3];
        ItemType = type;
        CurrentDays = currentDays;
        Amount = amount;
        GrowthTime = growthTime;
        GrowthSprite[0] = growthSprite[0]; GrowthSprite[1] = growthSprite[1]; GrowthSprite[2] = growthSprite[2];
        ID[0] = id[0]; ID[1] = id[1]; ID[2] = id[2];
        Pos[0] = pos[0]; Pos[1] = pos[1]; Pos[2] = pos[2];
        Tile = tile;
        Harvestable = harvestable;
        Growth = growth;
        XMLName = xmlName;
        Watered = watered;
    }

}

public struct ItemData
{
    public string Name;
    public string Type;
    public string Src_Image;
    public string Tile_Src_Image;
    public string Sound_Effect;
    public string Prefab;
    public int Amount;
    public int CustomData;
    public bool Stackable;
    public float SellPrice;
    public string Desc;

    public void SetUp(string name, string type, string image, string tile_image, string sound_effect, string prefab, int amount, bool stackable, float price, int customdata, string desc)
    {
        Name = name;
        Type = type;
        Src_Image = image;
        Tile_Src_Image = tile_image;
        Sound_Effect = sound_effect;
        Prefab = prefab;
        Amount = amount;
        Stackable = stackable;
        SellPrice = price;
        CustomData = customdata;
        Desc = desc;
    }
}

public class SaveData : MonoBehaviour
{
    public int[] Date = new int[3];
    public int[] Time = new int[3];
    public float[] PlayerPos = new float[3];
    public bool[] WeeklyResets = new bool[4];
    public int[] InventorySlotAmount = new int[3];

    public ItemData[] Inventory;
    public ItemData[] HotBar;
    public ItemData[] Chest;

    public PlantData[] Farm;

    public RockData[] Mines;
    public RockData[] Mines1;
    public RockData[] Mines2;

    public TreeData[] Forest;

    public float PlayerGold;
    public int PlayerEnergy;
    public float[] Lighting;

    public SaveData(Vector3Int date, Vector3 pos , Vector3Int time, float[] lighting, bool[] resets, int[] slots, float playerGold, int playerEnergy, ItemBase[] inventory, 
                    ItemBase[] hotbar, ItemBase[] chest, Dictionary<string, Dictionary<Vector3Int, TileDataClass>> dataBase)
    {
        // Set up all the arrays we will be saving to
        Inventory = new ItemData[slots[0]];
        HotBar = new ItemData[slots[1]];
        Chest = new ItemData[slots[2]];
        Lighting = new float[4];
        Farm = new PlantData[dataBase.ElementAt(0).Value.Count];
        Mines = new RockData[dataBase.ElementAt(1).Value.Count];
        Mines1 = new RockData[dataBase.ElementAt(2).Value.Count];
        Mines2 = new RockData[dataBase.ElementAt(3).Value.Count];
        Forest = new TreeData[dataBase.ElementAt(4).Value.Count];

        Date[0] = date.x; Date[1] = date.y; Date[2] = date.z;
        Time[0] = time.x; Time[1] = time.y; Time[2] = time.z;
        PlayerPos[0] = pos.x; PlayerPos[1] = pos.y; PlayerPos[2] = pos.z;
        WeeklyResets = resets;
        InventorySlotAmount = slots;
        PlayerGold = playerGold;
        PlayerEnergy = playerEnergy;
        Lighting = lighting;

        string TileName = "";
        string PrefabName = "";
        string SoundEffectName = "";
        for (int i = 0; i < slots[0]; i++)
        {
            if (inventory[i] != null)
            {
                if (inventory[i].GetTile() != null)
                {
                    TileName = inventory[i].GetTile().name;
                }
                if (inventory[i].GetPrefab() != null)
                {
                    PrefabName = inventory[i].GetPrefab().name;
                }
                if (inventory[i].GetSoundEffect() != null)
                {
                    SoundEffectName = inventory[i].GetSoundEffect().name;
                }

                    Inventory[i].SetUp(inventory[i].GetName(), inventory[i].mItemType.ToString(),
                    inventory[i].GetSrcImage(), TileName,
                    SoundEffectName, PrefabName, inventory[i].GetAmount(),
                    inventory[i].GetStackable(), inventory[i].GetSellPrice(), inventory[i].GetCustomData(), inventory[i].GetDesc());
            }

            TileName = "";
            PrefabName = "";
            SoundEffectName = "";
        }
        for (int i = 0; i < slots[1]; i++)
        {
            if (hotbar[i] != null)
            {
                if (hotbar[i].GetTile() != null)
                {
                    TileName = hotbar[i].GetTile().name;
                }
                if (hotbar[i].GetPrefab() != null)
                {
                    PrefabName = hotbar[i].GetPrefab().name;
                }
                if (hotbar[i].GetSoundEffect() != null)
                {
                    SoundEffectName = hotbar[i].GetSoundEffect().name;
                }


                HotBar[i].SetUp(hotbar[i].GetName(), hotbar[i].mItemType.ToString(), hotbar[i].GetSrcImage(),
                    TileName, SoundEffectName, PrefabName, hotbar[i].GetAmount(),
                    hotbar[i].GetStackable(),hotbar[i].GetSellPrice(), hotbar[i].GetCustomData(), hotbar[i].GetDesc());
            }

            TileName = "";
            PrefabName = "";
            SoundEffectName = "";
        }
        for (int i = 0; i < slots[2]; i++)
        {
            if (chest[i] != null)
            {
                if (chest[i].GetTile() != null)
                {
                    TileName = chest[i].GetTile().name;
                }
                if (chest[i].GetPrefab() != null)
                {
                    PrefabName = chest[i].GetPrefab().name;
                }
                if (chest[i].GetSoundEffect() != null)
                {
                    SoundEffectName = chest[i].GetSoundEffect().name;
                }

                Chest[i].SetUp(chest[i].GetName(), chest[i].mItemType.ToString(), chest[i].GetSrcImage(),
                    TileName, SoundEffectName, PrefabName, chest[i].GetAmount(),
                    chest[i].GetStackable(), chest[i].GetSellPrice(), chest[i].GetCustomData(), chest[i].GetDesc());
            }

            TileName = "";
            PrefabName = "";
        }

        int counter = 0;
        foreach (var var in dataBase.ElementAt(0).Value)
        {
            if (var.Value.GetPlant() != null)
            {
                PlantAbstractClass Plant = var.Value.GetPlant();
                Vector3Int Key = new Vector3Int(var.Key.x, var.Key.y, var.Key.z);
                string[] GrowthSprites = new string[Plant.mGrowthSprites.Length];
                GrowthSprites[0] = Plant.mGrowthSprites[0].name;
                GrowthSprites[1] = Plant.mGrowthSprites[1].name;
                GrowthSprites[2] = Plant.mGrowthSprites[2].name;
                Farm[counter].SetUp(Plant.mCurrentDays, Plant.mGrowthTime, Plant.GetType().ToString(),
                    Plant.GetSrcImage(), Plant.GetAmount(), GrowthSprites, Key, Key, var.Value.Tile.name,
                    Plant.mHarvestable, Plant.mGrowth, Plant.XMLName, Plant.mWatered);
            }
            else
            {
                Vector3Int Key = new Vector3Int(var.Key.x, var.Key.y, var.Key.z);
                Farm[counter].SetUp(Key, Key, var.Value.Tile.name, var.Value.IsWatered());
            }
            counter++;
        }
        counter = 0;
        foreach (var var in dataBase.ElementAt(1).Value)
        {
            OreAbstractClass Ore = var.Value.GetOre();
            float[] Key = new float[3]; Key[0] = var.Key.x; Key[1] = var.Key.y; Key[2] = var.Key.z;
            Mines[counter].SetUp(Ore.mItemType.ToString(), Ore.GetName(), Key, Key, var.Value.Tile.name, Ore.XMLName, Ore.GetAmount());
            counter++;
        }

        counter = 0;
        foreach (var var in dataBase.ElementAt(2).Value)
        {
            OreAbstractClass Ore = var.Value.GetOre();
            float[] Key = new float[3]; Key[0] = var.Key.x; Key[1] = var.Key.y; Key[2] = var.Key.z;
            Mines1[counter].SetUp(Ore.mItemType.ToString(), Ore.GetName(), Key, Key, var.Value.Tile.name, Ore.XMLName, Ore.GetAmount());
            counter++;
        }
        counter = 0;
        foreach (var var in dataBase.ElementAt(3).Value)
        {
            OreAbstractClass Ore = var.Value.GetOre();
            float[] Key = new float[3]; Key[0] = var.Key.x; Key[1] = var.Key.y; Key[2] = var.Key.z;
            Mines2[counter].SetUp(Ore.mItemType.ToString(), Ore.GetName(), Key, Key, var.Value.Tile.name, Ore.XMLName, Ore.GetAmount());
            counter++;
        }
        counter = 0;
        foreach (var var in dataBase.ElementAt(4).Value)
        {
            ItemBase Wood = var.Value.GetItem();
            float[] Key = new float[3]; Key[0] = var.Key.x; Key[1] = var.Key.y; Key[2] = var.Key.z;
            Forest[counter].SetUp(Wood.mItemType.ToString(), Wood.GetName(), Key, Key, var.Value.Tile.name, Wood.GetName(), Wood.GetAmount());
            counter++;
        }
        counter = 0;
    }
}
