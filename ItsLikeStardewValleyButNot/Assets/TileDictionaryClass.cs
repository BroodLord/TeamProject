using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDictionaryClass : MonoBehaviour
{

    public Dictionary<Vector3Int, TileDataClass> TileMapData;
    // Start is called before the first frame update
    void Start()
    {
        TileMapData = new Dictionary<Vector3Int, TileDataClass>();
    }
}
