using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public class JunkPlacer : MonoBehaviour
{
    public Grid grid;
    public Tilemap tileMap;
    public Sprite dirtSprite;
    public TileBase treeTile;
    public Tilemap nonWalkableTileMap;
    public XMLParser XML;
    public TileDictionaryClass Dictioary;
    //public Dictionary<Vector3Int, ItemBase> DataBase;

    protected void awake()
    {
        //grid = FindObjectOfType<Grid>();
        //tileMap = FindObjectOfType<Tilemap>();
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        Dictioary = GameObject.FindGameObjectWithTag("TileMapManager").GetComponent<TileDictionaryClass>();
        XML = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<XMLParser>();
        //DataBase = new Dictionary<Vector3Int, ItemBase>();
        PlaceRocks();
    }

    async void SetUp()
    {
    }

    public void PlaceTrees()
    {
        Vector3 worldMin = tileMap.transform.TransformPoint(tileMap.localBounds.min);
        Vector3 worldMax = tileMap.transform.TransformPoint(tileMap.localBounds.max);


        for (int x = (int)worldMin.x; x < (int)worldMax.x; x++)
        {
            for (int y = (int)worldMin.y; y < (int)worldMax.y; y++)
            {
                // 35 - 43
                if (Random.Range(1, 100) <= 40 && tileMap.GetSprite(new Vector3Int(x, y, 0)) == dirtSprite)
                {
                    nonWalkableTileMap.SetTile(new Vector3Int(x, y, 0), treeTile);
                }
                
            }

        }
        
    }

    public void PlaceRocks()
    {
        Vector3 worldMin = tileMap.transform.TransformPoint(tileMap.localBounds.min);
        Vector3 worldMax = tileMap.transform.TransformPoint(tileMap.localBounds.max);


        for (int x = (int)worldMin.x; x < (int)worldMax.x; x++)
        {
            for (int y = (int)worldMin.y; y < (int)worldMax.y; y++)
            {
                Vector3 pos = new Vector3(x, y, 0);
                Vector3Int posInt = grid.WorldToCell(pos);
                if (Random.Range(1, 100) <= 40 && tileMap.GetSprite(posInt) == dirtSprite)
                {
                    //nonWalkableTileMap.SetTile(posInt, treeTile);
                    TileDataClass Temp = new TileDataClass();
                    Dictioary.TileMapData.Add(posInt, Temp);
                    Dictioary.TileMapData[posInt].TileMap.SetTile(posInt, treeTile);
                    Dictioary.TileMapData[posInt].Tile = tileMap.GetTile(posInt);
                    int ItemIndex = Random.Range(35, 43);
                    int RandAmount = Random.Range(1, 5);
                    Ore Item = new Ore();
                    Item = this.gameObject.AddComponent<Ore>() as Ore;

                    Item.SetUpThisItem(XML.items.ElementAt(ItemIndex).Value.bItemType, XML.items.ElementAt(ItemIndex).Value.bName, RandAmount,
                        XML.items.ElementAt(ItemIndex).Value.bStackable, XML.items.ElementAt(ItemIndex).Value.bSrcImage, XML.items.ElementAt(ItemIndex).Value.bSoundEffect,
                        XML.items.ElementAt(ItemIndex).Value.bTile, XML.items.ElementAt(ItemIndex).Value.bPrefab, XML.items.ElementAt(ItemIndex).Value.bSellPrice);

                    Dictioary.TileMapData[posInt].SetOre(Item);
                    //DataBase.Add(posInt, Item);
                }

            }

        }

    }


    public void PlaceForage()
    {
        
    }
}
