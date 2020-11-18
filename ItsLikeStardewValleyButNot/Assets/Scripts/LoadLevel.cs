using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine;

public struct LoadData
{
    public GameObject Player;
    public InventoryClass InventoryRef;
    public HotBarClass HotBarRef;
    public Canvas UICanvas;
    public EventSystem EventSystemRef;

    public UnityEngine.UI.Image[] InventoryImageSlots;
    public TextMeshProUGUI[] InventoryAmountText;
    public ItemBase[] InventoryItemList;
    public bool[] InventoryMarkers;

    public UnityEngine.UI.Image[] HotBarImageSlots;
    public TextMeshProUGUI[] HotBarAmountText;
    public ItemBase[] HotBarItemList;
    public bool[] HotBarMarkers;
}



public class LoadLevel : MonoBehaviour
{
    public bool NewLevel;

    public string LevelName;
    LoadData LoadInfo = new LoadData();

    private void FindItems()
    {
        LoadInfo.Player = GameObject.FindGameObjectWithTag("Player");
        LoadInfo.InventoryRef = LoadInfo.Player.gameObject.GetComponent<InventoryClass>();
        LoadInfo.HotBarRef = LoadInfo.Player.gameObject.GetComponent<HotBarClass>();
        GameObject GO = GameObject.FindGameObjectWithTag("Canvas");
        LoadInfo.UICanvas = GO.transform.GetComponent<Canvas>();
        GO = GameObject.FindGameObjectWithTag("EventSystem");
        LoadInfo.EventSystemRef = GO.transform.GetComponent<EventSystem>();
        LoadInfo.InventoryImageSlots = LoadInfo.InventoryRef.ImageSlots;
        LoadInfo.InventoryItemList = LoadInfo.InventoryRef.ItemList;
        LoadInfo.InventoryAmountText = LoadInfo.InventoryRef.AmountText;
        LoadInfo.InventoryMarkers = LoadInfo.InventoryRef.Markers;
        LoadInfo.EventSystemRef = GO.transform.GetComponent<EventSystem>();
        LoadInfo.HotBarImageSlots = LoadInfo.HotBarRef.ImageSlots;
        LoadInfo.HotBarItemList = LoadInfo.HotBarRef.ItemList;
        LoadInfo.HotBarAmountText = LoadInfo.HotBarRef.AmountText;
        LoadInfo.HotBarMarkers = LoadInfo.HotBarRef.Markers;
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindItems();
        DontDestroyOnLoad(LoadInfo.UICanvas);
        DontDestroyOnLoad(LoadInfo.EventSystemRef);
        Destroy(LoadInfo.Player.gameObject);

        if (LevelName == "PlayerRoom")
        {
            Application.LoadLevel("PlayerRoom");
            FindItems();
            NewLevel = true;
            //InventoryClass Inventory = LoadInfo.Player.gameObject.GetComponent<InventoryClass>();
            //HotBarClass HotBar = LoadInfo.Player.gameObject.GetComponent<HotBarClass>();
            //Inventory.ItemList = LoadInfo.InventoryRef.ItemList;
            //Inventory.Markers = LoadInfo.InventoryRef.Markers;
            //Inventory.AmountText = LoadInfo.InventoryRef.AmountText;
            //Inventory.ImageSlots = LoadInfo.InventoryRef.ImageSlots;
            //HotBar.ItemList = LoadInfo.HotBarRef.ItemList;
            //HotBar.Markers = LoadInfo.HotBarRef.Markers;
            //HotBar.AmountText = LoadInfo.HotBarRef.AmountText;
            //HotBar.ImageSlots = LoadInfo.HotBarRef.ImageSlots;

        }
        if (LevelName == "PlayerFarm")
        {
            Application.LoadLevel("PlayerFarm");
        }

    }
}
