using System.Collections;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;



public class LoadLevel : MonoBehaviour
{
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
    // Start is called before the first frame update

    public void TransferLevel(string LevelName, Vector3 startLoc)
    {
        Transition = GameObject.FindGameObjectWithTag("Transition").GetComponent<Animator>();
        Dictionary = GameObject.FindGameObjectWithTag("TileMapManager").GetComponent<TileDictionaryClass>();
        DontDestroyOnLoad(Dictionary);
        if (LevelName != "PlayerFarm")
        {
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
        Player = GameObject.FindGameObjectWithTag("Player");
        DontDestroyOnLoad(Player);
        UICanvas = GameObject.FindGameObjectWithTag("Canvas");
        DontDestroyOnLoad(UICanvas);
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("EventSystem"));
        GameObject Manager = GameObject.FindGameObjectWithTag("InventoryManager");
        //UICanvas.SetActive(false);
        StartLocation = new Vector3(startLoc.x, startLoc.y, startLoc.z);
        InventoryClass Invent = Manager.GetComponent<InventoryClass>();
        HotBarClass HotBar = Manager.GetComponent<HotBarClass>();
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
        DontDestroyOnLoad(Manager);
        LoadNextLevel(LevelName);
    }


    public void LoadNextLevel(string LevelName)
    {
        StartCoroutine(eLoadNextLevel(LevelName));
    }

    IEnumerator eLoadNextLevel(string LevelName)
    {
        Transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(LevelName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        if (asyncLoad.isDone)
        {
            //UICanvas = GameObject.FindGameObjectWithTag("Canvas");
            //UICanvas.SetActive(true);
            foreach (var BaseV in Dictionary.TileMapData)
            {


                foreach (var ChildV in Dictionary.TileMapData.ElementAt(0).Value)
                {
                    ChildV.Value.Clone.SetActive(false);
                    DontDestroyOnLoad(ChildV.Value.Clone);
                }
            }
            Player.transform.position = StartLocation;
            Debug.Log(StartLocation);
            if (LevelName == "PlayerFarm")
            {
                foreach (var Childv in Dictionary.TileMapData.ElementAt(0).Value)
                {
                    Childv.Value.GetTileMap();
                    Childv.Value.TileMap.SetTile(Childv.Key, Childv.Value.Tile);
                    if (Childv.Value.Clone != null)
                    {
                        Childv.Value.Clone.SetActive(true);
                        PlantAbstractClass P = Childv.Value.GetPlant();
                        P.UpdatePlantSprite();
                        if (P.mGrowth == true)
                        {
                            Childv.Value.SetWatered(false);
                            P.mGrowth = false;
                            Childv.Value.TileMap.SetTile(Childv.Key, TilledTile);
                        }
                    }
                    /*BUG OCCURS HERE: */
                    else
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
