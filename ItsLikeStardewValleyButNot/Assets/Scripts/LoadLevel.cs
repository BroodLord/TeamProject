using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using TMPro;
using UnityEngine;



public class LoadLevel : MonoBehaviour
{
    public TileBase TilledTiled;
    public Animator Transition;
    public bool NewLevel;
    public Canvas UICanvas;
    public EventSystem EventSystemRef;
    public GameObject Player;
    public InventoryClass InventoryRef;
    public HotBarClass HotBarRef;
    public TileDictionaryClass Dictionary;
    // Start is called before the first frame update

    public void TransferLevel(string LevelName)
    {
        Transition = GameObject.FindGameObjectWithTag("Transition").GetComponent<Animator>();
        Dictionary = GameObject.FindGameObjectWithTag("TileMapManager").GetComponent<TileDictionaryClass>();
        if (LevelName != "PlayerFarm")
        {
            foreach (var V in Dictionary.TileMapData)
            {
                if (V.Value.Clone != null)
                {
                    DontDestroyOnLoad(V.Value.Clone);
                }
            }
        }
        Player = GameObject.FindGameObjectWithTag("Player");
        DontDestroyOnLoad(Player);
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Canvas"));
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("EventSystem"));
        GameObject Manager = GameObject.FindGameObjectWithTag("InventoryManager");
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
            foreach (var V in Dictionary.TileMapData)
            {
                if (V.Value.Clone != null)
                {
                    V.Value.Clone.SetActive(false);
                    DontDestroyOnLoad(V.Value.Clone);
                }
            }
            if (LevelName == "PlayerFarm")
            {
                foreach (var v in Dictionary.TileMapData)
                {
                    v.Value.GetTileMap();
                    v.Value.TileMap.SetTile(v.Key, v.Value.Tile);
                    if (v.Value.Clone != null)
                    {
                        v.Value.Clone.SetActive(true);
                        PlantAbstractClass P = v.Value.GetPlant();
                        P.UpdatePlantSprite();
                        if (P.mGrowth == true)
                        {
                            v.Value.SetWatered(false); P.mGrowth = false;
                            v.Value.TileMap.SetTile(v.Key, TilledTiled);
                        }
                    }
                    /*BUG OCCURS HERE: */
                    else
                    {
                        v.Value.SetWatered(false);
                        PlantAbstractClass P = v.Value.GetPlant();
                        v.Value.TileMap.SetTile(v.Key, TilledTiled);
                    }
                }
            }
        }
    }
}
