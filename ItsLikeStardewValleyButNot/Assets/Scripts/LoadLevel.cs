using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine;



public class LoadLevel : MonoBehaviour
{
    public TransferData TransferManager;
    public bool NewLevel;
    public Canvas UICanvas;
    public EventSystem EventSystemRef;
    public GameObject Player;
    public InventoryClass InventoryRef;
    public HotBarClass HotBarRef;

    public string LevelName;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Canvas"));
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("EventSystem"));
        GameObject Manager = GameObject.FindGameObjectWithTag("InventoryManager");
        InventoryClass Invent = Manager.GetComponent<InventoryClass>();
        HotBarClass HotBar = Manager.GetComponent<HotBarClass>();
        for (int i = 0; i < Invent.ItemList.Length; i++)
        {
            if(Invent.ItemList[i] != null)
                DontDestroyOnLoad(Invent.ItemList[i]);
        }
        for (int i = 0; i < HotBar.ItemList.Length; i++)
        {
            if (HotBar.ItemList[i] != null)
                DontDestroyOnLoad(HotBar.ItemList[i]);
        }

        DontDestroyOnLoad(Manager);
        if (LevelName == "PlayerRoom")
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            DontDestroyOnLoad(Player);
            Application.LoadLevel("PlayerRoom");


        }
        if (LevelName == "PlayerFarm")
        {
            Application.LoadLevel("PlayerFarm");
        }

    }
}
