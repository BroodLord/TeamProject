using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class FarmingTest : MonoBehaviour
{     
    //You need references to to the Grid and the Tilemap     
    Tilemap tm;     
    Grid gd;     
    public TileBase diffrentTile;      
    void Start()     
    {         
        //This is probably not the best way to get references to         
        //these objects but it works for this example         
        gd = FindObjectOfType<Grid>();         
        tm = FindObjectOfType<Tilemap>();     
    }      
    void Update()     
    {          
        if (Input.GetMouseButtonDown(0))         
        {             
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);             
            Vector3Int posInt = gd.LocalToCell(pos);              
            //Shows the cell reference for the grid             
            Debug.Log(posInt);              
            // Shows the name of the tile at the specified coordinates             
            Debug.Log(tm.GetTile(posInt).name);              
            tm.SetTile(posInt, diffrentTile);         
        }     
    } 
}

