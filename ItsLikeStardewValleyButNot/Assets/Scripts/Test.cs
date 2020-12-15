using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject Player;
    public Canvas UICanvas;
    public GameObject Manager;
    public GameObject TileMapManager;
    public GameObject LoadManager;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(Player);
        DontDestroyOnLoad(UICanvas);
        DontDestroyOnLoad(Manager);
        DontDestroyOnLoad(TileMapManager);
        DontDestroyOnLoad(LoadManager);
        SceneManager.LoadScene("PlayerFarm");
    }
}
