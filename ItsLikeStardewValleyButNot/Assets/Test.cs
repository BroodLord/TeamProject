using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject Player;
    public Canvas UICanvas;
    public GameObject Manager;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(Player);
        DontDestroyOnLoad(UICanvas);
        DontDestroyOnLoad(Manager);
        SceneManager.LoadScene("PlayerFarm");
    }
}
