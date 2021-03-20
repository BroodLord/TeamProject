using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CornClass : PlantAbstractClass
{
    public int CurrentDays;
    public int GrowthTime;
    public bool Harvestable;
    public bool Watered;
    public TileDictionaryClass Dictioary;
    PlantSeed Seeds;

    // Sets up the dictioary for the tilemap and the variables used for growth.
    private void Start()
    {
        Dictioary = GameObject.FindGameObjectWithTag("TileMapManager").GetComponent<TileDictionaryClass>();
        mItemType = ItemTypes.Seed;
        CurrentDays = mCurrentDays;
        GrowthTime = mGrowthTime;
        Watered = mWatered;
    }
    // Used to update the plant
    public override void UpdatePlant(int DayAmount)
    {
        // Only update if the plant isn't full grown.
        if (mHarvestable != true)
        {
            // Get the dictioary because we will be in another scene when this happens
            if (Dictioary == null)
            {
                Dictioary = GameObject.FindGameObjectWithTag("TileMapManager").GetComponent<TileDictionaryClass>();
            }
            // Gets the current plant
            PlantAbstractClass P = Dictioary.TileMapData.ElementAt(0).Value[ID].GetPlant();
            P.mGrowth = true;
            // If its not watered then destory the plant else set tile watered variable to false
            if (!P.mWatered) { DestoryPlant(); }
            else { Dictioary.TileMapData.ElementAt(0).Value[ID].SetWatered(false); }
            /*Update the sprite if a number of days have passed*/
            CurrentDays += DayAmount;
            if (CurrentDays >= 0 && CurrentDays < 1)
            {
                mSpriteIndex = 0;
            }
            if (CurrentDays >= 3 && CurrentDays < 6)
            {
                mSpriteIndex = 1;
            }
            if (CurrentDays >= 6)
            {
                mSpriteIndex = 2;
                mHarvestable = true;
            }
        }
    }
    // Destory this plant
    public override void DestoryPlant()
    {
        Destroy(this.gameObject);
    }

    public override void UpdatePlantSprite()
    {
        // Get the dictioary because we will be in another scene when this happens
        if (Dictioary == null)
        {
            Dictioary = GameObject.FindGameObjectWithTag("TileMapManager").GetComponent<TileDictionaryClass>();
        }
        /* Get the plant and update the sprite for the UI */
        //Seeds = GameObject.FindObjectOfType<PlantSeed>();
        PlantAbstractClass Prefab = Dictioary.TileMapData.ElementAt(0).Value[ID].GetPlant();
        SpriteRenderer S = Dictioary.TileMapData.ElementAt(0).Value[ID].Clone.GetComponent<SpriteRenderer>();
        S.sprite = Prefab.mGrowthSprites[Prefab.mSpriteIndex];

    }
}
