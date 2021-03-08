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

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(Player);
        DontDestroyOnLoad(UICanvas);
        DontDestroyOnLoad(Manager);
        DontDestroyOnLoad(TileMapManager);
        DontDestroyOnLoad(LoadManager);
        DontDestroyOnLoad(ItemManager);
        DontDestroyOnLoad(XML);
        /*TEST REMOVE AFTER*/
        //Player.transform.position = new Vector3(9, 12, 0);
        SceneManager.LoadScene("PlayerFarm");
    }
}
