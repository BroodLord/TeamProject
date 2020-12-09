//using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public InventoryClass cInventory;
    public Grid grid;
    public Tilemap tileMap;
    public enum ItemTypes { Tool, Seed, Decoration }
    public ItemTypes mItemType;
    protected bool ResetNeeded;
    protected Sprite mImage;
    protected string mName;
    [SerializeField]
    protected int mAmount;
    protected bool mStackable;
    protected string mSrcImage;
    protected float mSellPrice;

    public bool CheckResetNeeded() { return ResetNeeded; }
    public void SetResetNeeded(bool Value) { ResetNeeded = Value; }

    public void SetSpriteImage(Sprite ItemImage) { mImage = ItemImage; }
    public Sprite GetSpriteImage() { SpriteRenderer s = this.GetComponent<SpriteRenderer>(); return s.sprite; }
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
