using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

// RIP AZIR

public class InitMainCharacter : MonoBehaviour
{
    public GameObject Player;
    public Canvas UICanvas;
    public GameObject Manager;
    public GameObject TileMapManager;
    public GameObject LoadManager;
    public GameObject ItemManager;
    public XMLParser XML;
    public string TargetScene;

    // Start is called before the first frame update
    void Start()
    {
        DOLDatabase DOLD = GameObject.FindGameObjectWithTag("LoadManager").GetComponent<DOLDatabase>();
        DontDestroyOnLoad(Player);
        DOLD.Add(Player);
        DontDestroyOnLoad(UICanvas);
        DOLD.Add(UICanvas.gameObject);
        DontDestroyOnLoad(Manager);
        DOLD.Add(Manager);
        DontDestroyOnLoad(TileMapManager);
        DOLD.Add(TileMapManager);
        DontDestroyOnLoad(LoadManager);
        DOLD.Add(LoadManager);
        DontDestroyOnLoad(ItemManager);
        DOLD.Add(ItemManager);
        DontDestroyOnLoad(XML);
        DOLD.Add(XML.gameObject);

        /*TEST REMOVE AFTER*/
        //Player.transform.position = new Vector3(9, 12, 0);

        Clock cClock = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Clock>();
        LoadLevel Load = GameObject.FindGameObjectWithTag("LoadManager").GetComponent<LoadLevel>();

        if (SceneManager.GetActiveScene().name != "LoadSaveScene")
        {
            Load.TransferLevel("PlayerFarm", new Vector3(0,0,0));
        }
        else
        {
            
            Load.TransferLevel(cClock.SceneName, cClock.PlayerPos);
        }
    }
}
