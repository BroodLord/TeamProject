using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public abstract class PlantAbstractClass : ItemBase
{
    public Sprite[] mGrowthSprites;
    public int mCurrentDays;
    public int mGrowthTime;
    public bool mHarvestable;
    public bool mWatered;
    public int SpriteIndex;
    public Vector3Int ID;
    public Vector3 pos;

    public abstract void UpdatePlant();
    public abstract void DestoryPlant();

    public abstract void UpdatePlantSprite();
}
