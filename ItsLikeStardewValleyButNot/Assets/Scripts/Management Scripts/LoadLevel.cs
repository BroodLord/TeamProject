using System.Collections;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;



public class LoadLevel : MonoBehaviour
{
    public bool Loading;
    public TileBase TilledTile;
    public Animator Transition;
    public bool NewLevel;
    public GameObject UICanvas;
    public EventSystem EventSystemRef;
    public GameObject Player;
    public InventoryClass InventoryRef;
    public HotBarClass HotBarRef;
    public TileDictionaryClass Dictionary;
    public Vector3 StartLocation;

    public void TransferLevel(string LevelName, Vector3 startLoc)
    {
        Loading = true;
        // Get all the refernece that we need
        //if (SceneManager.GetActiveScene().name != "LoadSaveScene")
        //{
            Transition = GameObject.FindGameObjectWithTag("Transition").GetComponent<Animator>();
            DontDestroyOnLoad(GameObject.FindGameObjectWithTag("EventSystem"));
        //}
        Dictionary = GameObject.FindGameObjectWithTag("TileMapManager").GetComponent<TileDictionaryClass>();
        Player = GameObject.FindGameObjectWithTag("Player");
        UICanvas = GameObject.FindGameObjectWithTag("Canvas");
        GameObject Manager = GameObject.FindGameObjectWithTag("InventoryManager");
        InventoryClass Invent = Manager.GetComponent<InventoryClass>();
        HotBarClass HotBar = Manager.GetComponent<HotBarClass>();
        // Dont destory on load the Dictionary because we need it
        DontDestroyOnLoad(Dictionary);
        if (LevelName != "Farmland")
        {
            // Loops through all the databases and makes the clones are don't destory on load
            foreach (var BaseV in Dictionary.TileMapData)
            {


                foreach (var ChildV in BaseV.Value)
                {
                    if (ChildV.Value.Clone != null)
                    {
                        DontDestroyOnLoad(ChildV.Value.Clone);
                    }
                }
            }
        }
        // Set up the new start location
        StartLocation = new Vector3(startLoc.x, startLoc.y, startLoc.z);
        // Loop through the hot bar and inventory and don't destory on load everything
        for (int i = 0; i < Invent.ItemList.Length; i++)
        {
            if (Invent.ItemList[i] != null)
                DontDestroyOnLoad(Invent.ItemList[i]);
        }
        for (int i = 0; i < HotBar.ItemList.Length; i++)
        {
            if (HotBar.ItemList[i] != null)
                DontDestroyOnLoad(HotBar.ItemList[i]);
        }
        // load level with a selected level
        LoadNextLevel(LevelName);
    }


    public void LoadNextLevel(string LevelName)
    {
        StartCoroutine(eLoadNextLevel(LevelName));
    }

    IEnumerator eLoadNextLevel(string LevelName)
    {
        Transition.SetTrigger("Start");
        // Wait 1 second so the animation can play
        yield return new WaitForSeconds(1);

        // Tell the Async operation to load the level
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(LevelName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        if (asyncLoad.isDone)
        {
            Loading = false;
            //UICanvas = GameObject.FindGameObjectWithTag("Canvas");
            //UICanvas.SetActive(true);
            // if we aren't on the farm then set all the clone to be not active 
            foreach (var BaseV in Dictionary.TileMapData)
            {


                foreach (var ChildV in Dictionary.TileMapData.ElementAt(0).Value)
                {
                    if (ChildV.Value.Clone != null)
                    {
                        ChildV.Value.Clone.SetActive(false);
                        DontDestroyOnLoad(ChildV.Value.Clone);
                    }
                }
            }
            // Set up the player at the location
            Player.transform.position = StartLocation;
            Debug.Log(StartLocation);
            if (LevelName == "Farmland")
            {
                // If we are on the player farm then we want to loop through all the clones and set them all to active
                foreach (var Childv in Dictionary.TileMapData.ElementAt(0).Value)
                {
                    // we also want to set the tile to be tilled
                    Childv.Value.GetTileMap();
                    Childv.Value.TileMap.SetTile(Childv.Key, Childv.Value.Tile);
                    if (Childv.Value.Clone != null)
                    {
                        Childv.Value.Clone.SetActive(true);
                        PlantAbstractClass P = Childv.Value.GetPlant();
                        P.UpdatePlantSprite();
                        // if we have been to sleep then we want to reset the watered tile
                        if (P.mGrowth == true)
                        {
                            Childv.Value.SetWatered(false);
                            P.mGrowth = false;
                            Childv.Value.TileMap.SetTile(Childv.Key, TilledTile);
                        }
                    }
                    else if(!Childv.Value.IsWatered())
                    {
                        Childv.Value.SetWatered(false);
                        PlantAbstractClass P = Childv.Value.GetPlant();
                        Childv.Value.TileMap.SetTile(Childv.Key, TilledTile);
                    }
                }
            }
        }
    }
}
