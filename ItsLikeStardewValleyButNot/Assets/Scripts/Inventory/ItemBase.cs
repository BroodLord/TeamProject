//using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class ItemBase : DefaultItemBase
{
    public InventoryClass cInventory;
    public Grid grid;
    public Tilemap tileMap;
    //public enum ItemTypes { Tool, Seed, Decoration }
    public ItemTypes mItemType;
    protected bool ResetNeeded;
    protected Sprite mImage;
    protected TileBase mTile;
    protected string mName;
    [SerializeField]
    protected int mAmount;
    protected bool mStackable;
    protected GameObject mPrefab;
    public AudioClip mSoundEffect;
    protected string mSrcImage;
    protected float mSellPrice;
    private string TempAssetPath = "XML Loaded Assets/";
    private int mCustomData;

    // Sets up the an item that is in the inventory
    public void SetUpThisItem(ItemTypes ItemType, string Name, int Amount, bool Stackable, string SrcImage, AudioClip Audio, TileBase Tile, GameObject prefab, float SellPrice, int CustomData)
    {
        SetUpBaseItem(ItemType, Name, Amount, Stackable, SrcImage, Audio, Tile, prefab, SellPrice, CustomData);
        mItemType = bItemType;
        mImage = bImage;
        mPrefab = prefab;
        mTile = bTile;
        mName = bName;
        mAmount = bAmount;
        mSoundEffect = bSoundEffect;
        mSrcImage = bSrcImage;
        mStackable = bStackable;
        mSellPrice = bSellPrice;
        mCustomData = CustomData;
    }

    public int GetCustomData()
    {
        return mCustomData;
    }

    public AudioClip GetSoundEffect()
    {
        return mSoundEffect;
    }

    public TileBase GetTile()
    {
        return mTile;
    }

    public GameObject GetPrefab()
    {
        return mPrefab;
    }

    public bool CheckResetNeeded() { return ResetNeeded; }
    public void SetResetNeeded(bool Value) { ResetNeeded = Value; }

    public void SetSpriteImage(Sprite ItemImage) { mImage = ItemImage; }
    public Sprite GetSpriteImage()
    {
        return mImage;
    }
    public string GetName() { return mName; }
    public int GetAmount() { return mAmount; }
    public ItemTypes GetType() { return mItemType; }

    public bool GetStackable() { return mStackable; }

    public string GetSrcImage() { return mSrcImage; }

    public float GetSellPrice() { return mSellPrice; }
    public void SetName(string ItemName) { mName = ItemName; }
    public void SetAmount(int ItemAmount) { mAmount = ItemAmount; }
    public void AddAmount(int ItemAmount) { mAmount += ItemAmount; }
    public void SubstractAmount(int ItemAmount)
    { 
        mAmount -= ItemAmount; 

    }
    public void SetType(ItemTypes Type) { mItemType = Type; }
    public void SetImage(string image) { mSrcImage = image; }
    public void SetStackable(bool stackable) { mStackable = stackable; }

    public void SetSellPrice(float sellPrice) { mSellPrice = sellPrice; }

    public virtual void useTool() { }
}
