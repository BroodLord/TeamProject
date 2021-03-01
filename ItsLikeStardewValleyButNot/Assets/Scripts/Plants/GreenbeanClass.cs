using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GreenbeanClass : PlantAbstractClass
{
    public int CurrentDays;
    public int GrowthTime;
    public bool Harvestable;
    public bool Watered;
    public TileDictionaryClass Dictioary;
    PlantSeed Seeds;

    private void Start()
    {
        Dictioary = GameObject.FindGameObjectWithTag("TileMapManager").GetComponent<TileDictionaryClass>();
        mItemType = ItemTypes.Seed;
        CurrentDays = mCurrentDays;
        GrowthTime = mGrowthTime;
        Watered = mWatered;
    }
    public override void UpdatePlant(int DayAmount)
    {
        if (mHarvestable != true)
        {
            if (Dictioary == null)
            {
                Dictioary = GameObject.FindGameObjectWithTag("TileMapManager").GetComponent<TileDictionaryClass>();
            }
            PlantAbstractClass P = Dictioary.TileMapData.ElementAt(0).Value[ID].GetPlant();
            P.mGrowth = true;
            if (!P.mWatered) { DestoryPlant(); }
            else { Dictioary.TileMapData.ElementAt(0).Value[ID].SetWatered(false); }
            CurrentDays += DayAmount;
            if (CurrentDays >= 0 && CurrentDays < 1)
            {
                mSpriteIndex = 0;
            }
            if (CurrentDays >= 2 && CurrentDays < 4)
            {
                mSpriteIndex = 1;
            }
            if (CurrentDays >= 4)
            {
                mSpriteIndex = 2;
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
        Seeds = GameObject.FindObjectOfType<PlantSeed>();
        PlantAbstractClass PrefabTurnip = Dictioary.TileMapData.ElementAt(0).Value[ID].GetPlant();
        SpriteRenderer S = Dictioary.TileMapData.ElementAt(0).Value[ID].Clone.GetComponent<SpriteRenderer>();
        S.sprite = PrefabTurnip.mGrowthSprites[PrefabTurnip.mSpriteIndex];

    }
}
