using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class TurnipClass : PlantAbstractClass
{
    public int CurrentDays;
    public int GrowthTime;
    public bool Harvestable;
    public bool Watered;
    public TileDictionaryClass Dictioary;
    TurnipSeed Seeds;

    private void Start()
    {
        Dictioary = GameObject.FindGameObjectWithTag("TileMapManager").GetComponent<TileDictionaryClass>();
        mItemType = ItemTypes.Seed;
        CurrentDays = mCurrentDays;
        GrowthTime = mGrowthTime;
        Watered = mWatered;
    }
    public override void UpdatePlant()
    {
        if (mHarvestable != true)
        {
            if (Dictioary == null)
            {
                Dictioary = GameObject.FindGameObjectWithTag("TileMapManager").GetComponent<TileDictionaryClass>();
            }
            PlantAbstractClass P = Dictioary.TileMapData[ID].GetPlant();
            if (!P.mWatered) { DestoryPlant(); }
            else { Dictioary.TileMapData[ID].SetWatered(false); }
            ++CurrentDays;
            if (CurrentDays >= 0 && CurrentDays < 1)
            {
                SpriteIndex = 0;
            }
            if (CurrentDays >= 1 && CurrentDays < 3)
            {
                SpriteIndex = 1;
            }
            if (CurrentDays >= 3 && CurrentDays < 6)
            {
                SpriteIndex = 2;
            }
            if (CurrentDays >= 6)
            {
                SpriteIndex = 3;
                mHarvestable = true;
            }
        }
    }
    public override void DestoryPlant()
    {
        Destroy(this.gameObject);
    }

    public override void UpdatePlantSprite()
    {
        if (Dictioary == null)
        {
            Dictioary = GameObject.FindGameObjectWithTag("TileMapManager").GetComponent<TileDictionaryClass>();
        }
        Seeds = GameObject.FindObjectOfType<TurnipSeed>();
        PlantAbstractClass PrefabTurnip = Dictioary.TileMapData[ID].GetPlant();
        SpriteRenderer S = Dictioary.TileMapData[ID].Clone.GetComponent<SpriteRenderer>();
        S.sprite = PrefabTurnip.mGrowthSprites[PrefabTurnip.SpriteIndex];

    }
}
