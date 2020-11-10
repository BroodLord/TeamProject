﻿using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    public InventoryClass cInventory;
    public enum ItemTypes { Tool, Seed, Decoration }
    protected ItemTypes mItemType;
    protected bool ResetNeeded;
    protected Sprite mImage;
    protected string mName;
    protected int mAmount;
    protected bool mStackable;
    protected string mSrcImage;
    protected float mSellPrice;

    public bool CheckResetNeeded() { return ResetNeeded; }
    public void SetResetNeeded(bool Value) { ResetNeeded = Value; }

    public void ResetItem(int ID)
    {
        mImage = null;
        mName = null;
        mAmount = 0;
        mStackable = false;
        mSrcImage = null;
        mSellPrice = 0;
        cInventory.ItemList[ID] = null;
    }
    public void SetSpriteImage(Sprite ItemImage) { mImage = ItemImage; }
    public Sprite GetSpriteImage() { return mImage; }
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

}
