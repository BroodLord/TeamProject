using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class WateringCanScript : ToolScript
{
    public TileDictionaryClass Dictioary;
    public override void useTool()
    {
        if (SceneManager.GetActiveScene().name == "Farmland")
        {
            Dictioary = GameObject.FindGameObjectWithTag("TileMapManager").GetComponent<TileDictionaryClass>();
            //TODO - when we add more grids and tilemaps, this will break
            // Get the mouse pos to world
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int posInt = grid.LocalToCell(pos);
            if (Dictioary.TileMapData.ElementAt(0).Value.ContainsKey(posInt))
            {
                if (!Dictioary.TileMapData.ElementAt(0).Value[posInt].IsWatered())
                {
                    PlantAbstractClass P = Dictioary.TileMapData.ElementAt(0).Value[posInt].GetPlant();
                    // Shows the name of the tile at the specified coordinates    
                    // Check if it has already been watered
                    if (!P.mHarvestable)
                    {
                        //Debug.Log(tileMap.GetTile(posInt).name);
                        /*Play the sound effect*/
                        AudioSource Audio = gameObject.GetComponentInParent<AudioSource>();
                        Audio.clip = GetSoundEffect();
                        Audio.Play();
                        /*************************/
                        Debug.Log("This is watered");
                        ToolUsed = true;
                        /*Edit the tile so it is watered*/
                        Dictioary.TileMapData.ElementAt(0).Value[posInt].TileMap.SetTile(posInt, GetTile());
                        Dictioary.TileMapData.ElementAt(0).Value[posInt].Tile = tileMap.GetTile(posInt);
                        Dictioary.TileMapData.ElementAt(0).Value[posInt].SetWatered(true);
                        P = Dictioary.TileMapData.ElementAt(0).Value[posInt].GetPlant();
                    }
                }
                else
                {
                    Debug.Log("This is already watered");
                }
            }
            else
            {
                Debug.Log(tileMap.GetTile(posInt).name);
                Debug.Log("Needs to be Hoed first");
            }
        }
    }
}
