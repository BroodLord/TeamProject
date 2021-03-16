using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
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

    // Function used to update the UI
    public override void UpdateUI()
    {
        // Loop through all the slots in the UI for the hotbar
        for (int i = 0; i < ImageSlots.Length; i++)
        {
            // Check the marker and check that the image slot is assigned.
            if (Markers[i] != false && ImageSlots[i] != null)
            {
                /*Set the image to active, get the sprite and set the image and set the amount text*/
                ImageSlots[i].gameObject.SetActive(true);
                ImageSlots[i].sprite = ItemList[i].GetSpriteImage();
                AmountText[i].text = ItemList[i].GetAmount().ToString();
            }
            else
            {
                // We want to make sure that we set active to false and set the background image
                if (ImageSlots[i] != null && ImageSlots[i].sprite != null)
                {
                    ImageSlots[i].gameObject.SetActive(false);
                    ImageSlots[i].sprite = BackgroundImage;
                }
                // Set the amount to 0
                AmountText[i].text = "0";
                if (Markers[i] == true)
                {
                    // if for what ever reason the marker is true when nothing is there then set it to false;
                    Markers[i] = false;
                }

            }


        }
    }

    // Gets the camera
    public void GetCamera()
    {
        GameObject Go = GameObject.FindGameObjectWithTag("MainCamera");
        camera = Go.GetComponent<Camera>();
    }

    // Gets the farming Componenets for the grid and tilemap
    private void FindFarmComponenets()
    {
        tool.grid = GameObject.FindGameObjectWithTag("Floor").GetComponentInParent<Grid>();
        tool.tileMap = GameObject.FindGameObjectWithTag("Floor").GetComponent<Tilemap>();
    }

    // Start is called before the first frame update
    //private void Awake()
    //{
    //    GetCamera();
    //}

    void Start()
    {
        // RIP AZIR

        XML = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<XMLParser>();
        GetCamera();
        tool = new ItemBase();
        GetCamera();
        CoolDown = 0.0f;
        Resize(10);
        //for (int i = 0; i < 4; i++) {ItemBase BasicItem = gameObject.AddComponent<ItemBase>() as ItemBase; AddItem(BasicItem); }
        Debug.Log(ItemList.Length);
        cInventory = this.GetComponent<InventoryClass>();
        cHotBar = this.GetComponent<HotBarClass>();
        ItemTypeFinder TypeFinder = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemTypeFinder>();
        for (int i = 0; i < XML.items.Count; i++)
        {
            if (XML.items.ElementAt(i).Value.bName == "Hoe" || XML.items.ElementAt(i).Value.bName == "Water Bucket" ||
                XML.items.ElementAt(i).Value.bName == "Scythe" || XML.items.ElementAt(i).Value.bName == "Pickaxe" || 
                XML.items.ElementAt(i).Value.bName == "Axe" || XML.items.ElementAt(i).Value.bName == "Fishing Rod")
            {
                ItemBase BasicItem = new ItemBase();
                // Get the object the component will be on and give it a name
                GameObject SubGameObject = new GameObject(XML.items.ElementAt(i).Value.bName);
                SubGameObject.transform.parent = ToolItems.transform;
                // Find the type of the item and set it up, after add it to the dictionary.
                BasicItem = TypeFinder.TyepFinder(i, SubGameObject);
                BasicItem.SetUpThisItem(XML.items.ElementAt(i).Value.bItemType, XML.items.ElementAt(i).Value.bName, XML.items.ElementAt(i).Value.bAmount,
                    XML.items.ElementAt(i).Value.bStackable, XML.items.ElementAt(i).Value.bSrcImage, XML.items.ElementAt(i).Value.bSoundEffect,
                    XML.items.ElementAt(i).Value.bTile, XML.items.ElementAt(i).Value.bPrefab, XML.items.ElementAt(i).Value.bSellPrice);
                AddItem(BasicItem);
            }
        }
        UpdateUI();
        //
    }

    // Update is called once per frame
    void Update()
    {
  
        GetCamera(); // Don't know if we need to do this every frame but hey hoo will look later #RIP AZIR
        // Reduce the cooldown
        if (CoolDown > 0.0f)
        {
            CoolDown -= Time.deltaTime;
        }
        // if action is locked then reduce the timer till 0 and reset
        if (actionLocked)
        {

            actionLockedTimer -= Time.deltaTime;
            if (actionLockedTimer < 0)
            {
                actionLocked = false;
                actionLockedTimer = actionLock;
            }
        }
        /*This is used to assign tools when one of the keys are pressed*/
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
        /******************************************/
        /* If we can act */
        if (!actionLocked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                /*If we don't have an active tool then tell the player*/
                if (tool == null)
                {
                    Debug.Log("PLEASE SELECT A VALID TOOL");
                }
                /*Make sure that we actually have an amount of the used tool*/
                else if(tool.GetAmount() > 0)
                {
                    // Get the camera if null
                    if (camera == null) { GetCamera(); }
                    // Get the mouse pos
                    mouseWorldPoint = camera.ScreenToWorldPoint(Input.mousePosition);
                    // Get the player
                    GameObject Player = GameObject.FindGameObjectWithTag("Player");
                    Debug.Log(Vector2.Distance(Player.transform.position, mouseWorldPoint));
                    /*If we are in range, the lock our actions and find the farm compoents and then use the tool. */
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
