using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{

    public string LevelName;
    private Canvas UICanvas;
    private GameObject Player;
    private InventoryClass InventoryRef;
    private HotBarClass HotBarRef;
    private EventSystem EventSystemRef;

    private void FindItems()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        InventoryRef = Player.gameObject.GetComponent<InventoryClass>();
        HotBarRef = Player.gameObject.GetComponent<HotBarClass>();
        GameObject GO = GameObject.FindGameObjectWithTag("Canvas");
        UICanvas = GO.transform.GetComponent<Canvas>();
        GO = GameObject.FindGameObjectWithTag("EventSystem");
        EventSystemRef = GO.transform.GetComponent<EventSystem>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindItems();
        // Load the level named "PlayerRoom".
        DontDestroyOnLoad(Player);
        DontDestroyOnLoad(UICanvas);
        DontDestroyOnLoad(InventoryRef);
        DontDestroyOnLoad(HotBarRef);
        DontDestroyOnLoad(EventSystemRef);
        for (int i = 0; i < InventoryRef.ItemList.Length; i++)
        {
            if (InventoryRef.ItemList[i] != null)
            {
                DontDestroyOnLoad(InventoryRef.ItemList[i]);
            }
        }
        if (LevelName == "PlayerRoom")
        {
            Application.LoadLevel("PlayerRoom");
        }
        if (LevelName == "PlayerFarm")
        {
            Application.LoadLevel("PlayerFarm");
        }

    }
}
