﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Tilemaps;
using UnityEngine;

public class HotBarClass : InventoryAbstractClass
{
    // Start is called before the first frame update
    public UnityEngine.UI.Image[] ImageSlots;
    public TextMeshProUGUI[] AmountText;
    public Sprite BackgroundImage;
    public ItemBase[] InitItems;

    private Vector3 mouseWorldPoint;
    public Camera camera;
    private float CoolDown;
    [SerializeField] private float interactRange;
    private static float actionLock = 0.4f;         //how long after executing an action the character is locked in place for 
    private bool actionLocked;
    private float actionLockedTimer = actionLock;
    public ItemBase tool;                         //tool that is currently being used 
    private bool UIEnabled;
    public void UpdateUI()
    {
        for (int i = 0; i < ImageSlots.Length; i++)
        {
            if (ItemList[i] != null)
            {

                ImageSlots[i].sprite = ItemList[i].GetSpriteImage();
                AmountText[i].text = ItemList[i].GetAmount().ToString();
            }
            else
            {
                if (ImageSlots[i].sprite != null)
                {
                    ImageSlots[i].sprite = BackgroundImage;
                }
                AmountText[i].text = "0";
                if (Markers[i] == true)
                {

                    Markers[i] = false;
                }

            }


        }
    }

    public void GetCamera()
    {
        GameObject Go = GameObject.FindGameObjectWithTag("MainCamera");
        camera = Go.GetComponent<Camera>();
    }

    private void FindFarmComponenets()
    {
        tool.grid = GameObject.FindGameObjectWithTag("Floor").GetComponentInParent<Grid>();
        tool.tileMap = GameObject.FindGameObjectWithTag("Floor").GetComponent<Tilemap>();
    }

    // Start is called before the first frame update
    private void Awake()
    {
        GetCamera();
    }

    void Start()
    {
        tool = new ItemBase();
        GetCamera();
        CoolDown = 0.0f;
        Resize(10);
        Debug.Log(ItemList.Length);
        cInventory = this.GetComponent<InventoryClass>();
        cHotBar = this.GetComponent<HotBarClass>();
        for (int i = 0; i < InitItems.Length; i++)
        {
            AddItem(InitItems[i], 1);
        }
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        GetCamera();
        if (CoolDown > 0.0f)
        {
            CoolDown -= Time.deltaTime;
        }
        if (actionLocked)
        {

            actionLockedTimer -= Time.deltaTime;
            if (actionLockedTimer < 0)
            {
                actionLocked = false;
                actionLockedTimer = actionLock;
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            if (Markers[0] != false)
            {
                Debug.Log("SLOT: 0 SELECTED");
                tool = ItemList[0].GetComponent<ItemBase>();
                FindFarmComponenets();
            }
            else
            {
                tool = new ItemBase();
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            if (Markers[1] != false)
            {
                Debug.Log("SLOT: 1 SELECTED");
                tool = ItemList[1].GetComponent<ItemBase>();
                FindFarmComponenets();
            }
            else
            {
                tool = new ItemBase();
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            if (Markers[2] != false)
            {
                Debug.Log("SLOT: 2 SELECTED");
                tool = ItemList[2].GetComponent<ItemBase>();
                FindFarmComponenets();
            }
            else
            {
                tool = new ItemBase();
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            if (Markers[3] != false)
            {
                Debug.Log("SLOT: 3 SELECTED");
                tool = ItemList[3].GetComponent<ItemBase>();
                FindFarmComponenets();
            }
            else
            {
                tool = new ItemBase();
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            if (Markers[4] != false)
            {
                Debug.Log("SLOT: 4 SELECTED");
                tool = ItemList[4].GetComponent<ItemBase>();
                FindFarmComponenets();
            }
            else
            {
                tool = new ItemBase();
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            if (Markers[5] != false)
            {
                Debug.Log("SLOT: 5 SELECTED");
                tool = ItemList[5].GetComponent<ItemBase>();
                FindFarmComponenets();
            }
            else
            {
                tool = new ItemBase();
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha7))
        {
            if (Markers[6] != false)
            {
                Debug.Log("SLOT: 6 SELECTED");
                tool = ItemList[6].GetComponent<ItemBase>();
                FindFarmComponenets();
            }
            else
            {
                tool = new ItemBase();
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha8))
        {
            if (Markers[7] != false)
            {
                Debug.Log("SLOT: 7 SELECTED");
                tool = ItemList[7].GetComponent<ItemBase>();
                FindFarmComponenets();
            }
            else
            {
                tool = new ItemBase();
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha9))
        {
            if (Markers[8] != false)
            {
                Debug.Log("SLOT: 8 SELECTED");
                tool = ItemList[8].GetComponent<ItemBase>();
                FindFarmComponenets();
            }
            else
            {
                tool = new ItemBase();
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha0))
        {
            if (Markers[9] != false)
            {
                Debug.Log("SLOT: 9 SELECTED");
                tool = ItemList[9].GetComponent<ItemBase>();
                FindFarmComponenets();
            }
            else
            {
                tool = new ItemBase();
            }
        }

        if (!actionLocked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (tool == null)
                {
                    Debug.Log("PLEASE SELECT A VALID TOOL");
                }
                else
                {
                    if (camera == null) { GetCamera(); }
                    mouseWorldPoint = camera.ScreenToWorldPoint(Input.mousePosition);
                    GameObject Player = GameObject.FindGameObjectWithTag("Player");
                    Debug.Log(Vector2.Distance(Player.transform.position, mouseWorldPoint));
                    if (Vector2.Distance(Player.transform.position, mouseWorldPoint) < interactRange)
                    {
                        actionLocked = true;
                        FindFarmComponenets();
                        tool.useTool();
                    }
                    else
                    {
                        Debug.Log("OUT OF RANGE OF TOOL");
                    }
                }
            }
        }
    }
}
