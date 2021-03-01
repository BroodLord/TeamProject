using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public class HoeScript : ToolScript
{
    public TileDictionaryClass Dictioary;
    public override void useTool()
    {
        Dictioary = GameObject.FindGameObjectWithTag("TileMapManager").GetComponent<TileDictionaryClass>();
        //TODO - when we add more grids and tilemaps, this will break
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int posInt = grid.LocalToCell(pos);
        if (!Dictioary.TileMapData.ElementAt(0).Value.ContainsKey(posInt))
        {
            // Shows the name of the tile at the specified coordinates#
            //Debug.Log(tileMap.GetTile(posInt).name);
            AudioSource Audio = gameObject.GetComponentInParent<AudioSource>();
            Audio.clip = GetSoundEffect();
            Audio.Play();
            Debug.Log("This is hoeing");
            TileDataClass Temp = new TileDataClass();
            Dictioary.TileMapData.ElementAt(0).Value.Add(posInt, Temp);
            Dictioary.TileMapData.ElementAt(0).Value[posInt].TileMap.SetTile(posInt, GetTile());
            Dictioary.TileMapData.ElementAt(0).Value[posInt].Tile = tileMap.GetTile(posInt);
        }
        else
        {
            Debug.Log(tileMap.GetTile(posInt).name);
            Debug.Log("This is already hoed");
        }
    }
}
