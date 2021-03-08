using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class DefaultItemBase : MonoBehaviour
{
    // This is the default items properties for each items
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

    // Gets the base tool type
    public ItemTypes GetBaseType()
    {
        return ItemTypes.Tool;
    }

    // This will set up the default value
    public void SetUpBaseItem(ItemTypes ItemType, string Name, int Amount, bool Stackable, string SrcImage, AudioClip Audio, TileBase Tile, GameObject prefab, float SellPrice)
    {
        bItemType = ItemType;
        // If the tool type isn't a plant or ore then we are using just a base png image for the tool so just load the image, NOTE: This might change later.
        if (ItemType != ItemTypes.Plant && ItemType != ItemTypes.Seed && ItemType != ItemTypes.Ore)
        {
            string Path = "XML Loaded Assets/" + SrcImage;
            bImage = Resources.Load<Sprite>(Path);
        }
        // If its an ore or plant then load the sprite from a sprite sheet
        else if (ItemType == ItemTypes.Ore)
        {
            bImage = GetOreFromSheet(SrcImage);
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
    // These are the same function but just load from a different sprite sheet, NOTE: This will be azir dead but the base code for it will stay the same
    private Sprite GetOreFromSheet(string SrcImage)
    {
        // Loads all the sprite sheet into an array of sprites.
        Sprite[] Sprites;
        Sprites = Resources.LoadAll<Sprite>("XML Loaded Assets/roguelikeitems");
        // Loop through all sprites and if we find the sprite then return it.
        for (int i = 0; i < Sprites.Length; i++)
        {
            if (Sprites[i].name == SrcImage)
                return Sprites[i];
        }
        return null;
    }

    private Sprite GetSpriteFromSheet(string SrcImage)
    {
        Sprite[] Sprites;
        Sprites = Resources.LoadAll<Sprite>("XML Loaded Assets/Crop_Spritesheet");
        for (int i = 0; i < Sprites.Length; i++)
        {
            if (Sprites[i].name == SrcImage)
                return Sprites[i];
        }
        return null;
    }

}
