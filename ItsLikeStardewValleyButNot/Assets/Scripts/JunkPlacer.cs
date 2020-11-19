using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class JunkPlacer : MonoBehaviour
{
    public Grid grid;
    public Tilemap tileMap;
    public Sprite dirtSprite;
    public TileBase treeTile;
    public Tilemap nonWalkableTileMap;

    protected void awake()
    {
        grid = FindObjectOfType<Grid>();
        tileMap = FindObjectOfType<Tilemap>();
    }

    public void PlaceTrees()
    {
        Vector3 worldMin = tileMap.transform.TransformPoint(tileMap.localBounds.min);
        Vector3 worldMax = tileMap.transform.TransformPoint(tileMap.localBounds.max);


        for (int x = (int)worldMin.x; x < (int)worldMax.x; x++)
        {
            for (int y = (int)worldMin.y; y < (int)worldMax.y; y++)
            {

                if (Random.Range(1, 200) == 1 && tileMap.GetSprite(new Vector3Int(x, y, 0)) == dirtSprite)
                {
                    nonWalkableTileMap.SetTile(new Vector3Int(x, y, 0), treeTile);
                }
                
            }

        }
        
    }

    public void PlaceRocks()
    {

    }

    public void PlaceForage()
    {
        
    }
}
