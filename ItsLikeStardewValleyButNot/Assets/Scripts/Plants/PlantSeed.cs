using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlantSeed : ToolScript
{
    private Vector3Int ID;
    private Vector3 pos;
    public TileDictionaryClass Dictioary;
    public GameObject PlantPrefab;
    public override void useTool()
    {
        // Set the plant prefab to be the base one
        PlantPrefab = bPrefab;
        Dictioary = GameObject.FindGameObjectWithTag("TileMapManager").GetComponent<TileDictionaryClass>();
        //TODO - when we add more grids and tilemaps, this will break
        // Convert mouse to world point
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int posInt = grid.LocalToCell(pos);
        // Check that point doesn't have a plant and contains the key
        if (Dictioary.TileMapData.ElementAt(0).Value.ContainsKey(posInt) && !Dictioary.TileMapData.ElementAt(0).Value[posInt].HasPlant())
        {
            /*Subtract one as we only use 1 seed at a time, if it drops below 0 then remove the item*/
            this.SubstractAmount(1);
            HotBarClass HotBar = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<HotBarClass>();
            if (this.GetAmount() <= 0) { HotBar.RemoveItem(this.GetName()); }
            /********************************************************************************************/
            ID = posInt;

            /*Get the audio the play it*/
            AudioSource Audio = gameObject.GetComponentInParent<AudioSource>();
            Audio.clip = GetSoundEffect();
            Audio.Play();
            /***************************/

            Debug.Log("Seed Planted");
            /*Create a clone and instantiate it*/
            GameObject Clone;
            Clone = Instantiate(PlantPrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
            /*****************************/
            /* Set up the plant for the clone on that spot in the database */
            PlantAbstractClass TempPlant = Clone.GetComponent<PlantAbstractClass>();
            TempPlant.ID = ID;
            TempPlant.pos = pos;
            TempPlant.AddAmount(1);
            Dictioary.TileMapData.ElementAt(0).Value[posInt].SetPlant(TempPlant);
            Dictioary.TileMapData.ElementAt(0).Value[posInt].Clone = Clone;
            if (Dictioary.TileMapData.ElementAt(0).Value[posInt].IsWatered()) { TempPlant.mWatered = true; }
            else { TempPlant.mWatered = false; }
            /*******************************************************************/
        }
        else
        {
            Debug.Log(tileMap.GetTile(posInt).name);
            Debug.Log("Plot already has a plant placed there");
        }
    }
}
