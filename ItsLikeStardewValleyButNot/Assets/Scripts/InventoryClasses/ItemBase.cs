using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public enum ItemTypes { Tool, Seed, Decoration }
    protected ItemTypes mItemType;

    protected string mName;
    protected int mAmount;
    protected bool mStackable;
    protected string mSrcImage;
    protected float mSellPrice;

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
