using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class DefaultItemBase : MonoBehaviour
{
    public enum ItemTypes { Tool, Plant, Ore, Seed, Decoration }
    public ItemTypes bItemType;
    public Sprite bImage;
    public string bName;
    [SerializeField]
    public int bAmount;
    public bool bStackable;
    public string bSrcImage;
    public GameObject bPrefab;
    public AudioClip bSoundEffect;
    public TileBase bTile;
    public float bSellPrice;

    public ItemTypes GetBaseType()
    {
        return ItemTypes.Tool;
    }

    public void SetUpBaseItem(ItemTypes ItemType, string Name, int Amount, bool Stackable, string SrcImage, AudioClip Audio, TileBase Tile, GameObject prefab, float SellPrice)
    {
        bItemType = ItemType;
        if (ItemType != ItemTypes.Plant && ItemType != ItemTypes.Seed)
        {
            string Path = "TEMP ASSETS/" + SrcImage;
            bImage = Resources.Load<Sprite>(Path);
        }
        else
        {
            bImage = GetSpriteFromSheet(SrcImage);
        }
        bTile = Tile;
        bPrefab = prefab;
        bSoundEffect = Audio;
        bName = Name;
        bAmount = Amount;
        bSrcImage = SrcImage;
        bStackable = Stackable;
        bSellPrice = SellPrice;


    }

    private Sprite GetSpriteFromSheet(string SrcImage)
    {
        Sprite[] Sprites;
        Sprites = Resources.LoadAll<Sprite>("TEMP ASSETS/Crop_Spritesheet");
        for (int i = 0; i < Sprites.Length; i++)
        {
            if (Sprites[i].name == SrcImage)
                return Sprites[i];
        }
        return null;
    }

}
