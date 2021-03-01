using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDictionaryClass : MonoBehaviour
{

    //public Dictionary<Vector3Int, TileDataClass> TileMapData;
    public Dictionary<string, Dictionary<Vector3Int, TileDataClass>> TileMapData;
    // Start is called before the first frame update
    void Start()
    {
        //TileMapData = new Dictionary<Vector3Int, TileDataClass>();
        TileMapData = new Dictionary<string, Dictionary<Vector3Int, TileDataClass>>();
        TileMapData.Add("Farm", new Dictionary<Vector3Int, TileDataClass>());
        TileMapData.Add("MinesL1", new Dictionary<Vector3Int, TileDataClass>());
        TileMapData.Add("MinesL2", new Dictionary<Vector3Int, TileDataClass>());
        TileMapData.Add("MinesL3", new Dictionary<Vector3Int, TileDataClass>());
        TileMapData.Add("Trees", new Dictionary<Vector3Int, TileDataClass>());
    }
}
