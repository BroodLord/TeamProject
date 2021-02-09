using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class JunkPlacer : MonoBehaviour
{
    private Grid grid;
    private Tilemap tileMap;
    public TileBase dirtSprite;
    public TileBase treeTile;
    private Tilemap nonWalkableTileMap;

    protected void start()
    {
        grid = FindObjectOfType<Grid>();
        tileMap = GameObject.FindGameObjectWithTag("Floor").GetComponent<Tilemap>();
        nonWalkableTileMap = GameObject.FindGameObjectWithTag("Non-Walkable").GetComponent<Tilemap>();

    }

    public void PlaceTrees()
    {
        Vector3 worldMin = tileMap.transform.TransformPoint(grid.LocalToCell(tileMap.localBounds.min));
        Vector3 worldMax = tileMap.transform.TransformPoint(grid.LocalToCell(tileMap.localBounds.max));


        for (int x = (int)worldMin.x; x < (int)worldMax.x; x++)
        {
            for (int y = (int)worldMin.y; y < (int)worldMax.y; y++)
            {

                if (Random.Range(1, 100) <= 10 && tileMap.GetSprite(new Vector3Int(x, y, 0)) == dirtSprite)
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
