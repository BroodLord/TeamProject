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
    protected string mSrcImage;
    protected float mSellPrice;
    private string TempAssetPath = "TEMP ASSETS/";

    public void SetUpThisItem(ItemTypes ItemType, string Name, int Amount, bool Stackable, string SrcImage, TileBase Tile, GameObject prefab, float SellPrice)
    {
        SetUpBaseItem(ItemType, Name, Amount, Stackable, SrcImage, Tile, prefab, SellPrice);
        mItemType = bItemType;
        mImage = bImage;
        mPrefab = prefab;
        mTile = bTile;
        mName = bName;
        mAmount = bAmount;
        mSrcImage = bSrcImage;
        mStackable = bStackable;
        mSellPrice = bSellPrice;
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
        //string Path = TempAssetPath + GetSrcImage();
        //Sprite Sprite = Resources.Load<Sprite>(Path);
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
    public void SubstractAmount(int ItemAmount) { mAmount -= ItemAmount; }
    public void SetType(ItemTypes Type) { mItemType = Type; }
    public void SetImage(string image) { mSrcImage = image; }
    public void SetStackable(bool stackable) { mStackable = stackable; }

    public void SetSellPrice(float sellPrice) { mSellPrice = sellPrice; }

    public virtual void useTool() { }
}
