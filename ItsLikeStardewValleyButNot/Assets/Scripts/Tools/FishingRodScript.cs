using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class FishingRodScript : ToolScript
{
    [SerializeField] private TileBase fishableTile;
    [SerializeField] private Sprite Bobber;
    private CharacterMovement CM;
    private GameObject BobberHolder;
    bool currentlyFishing;
    [SerializeField]float fishingTimer = 10.0f;
    [SerializeField]float catchTimer;
    GameObject ToolItems;
    List<string> fish;
    XMLParser XML;
    int numOfFish = 0;
    bool audioPlayed;
    enum FishingStates { notFishing, waiting, bobberTrigger, reeling, caught }
    [SerializeField] private int currentState;
    [SerializeField] private int reelDifficulty;
    [SerializeField] private int reelCount;
    public GameObject reelMeter;
    public Slider ReelSlider;

    private void Start()
    {
        CM = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
        fishableTile = GetTile();
        Bobber = Resources.Load<Sprite>("XML Loaded Assets/Bobber");
        ToolItems = GameObject.Find("TemplateTools");
        XML = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<XMLParser>();
        cInventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryClass>();
        fish = new List<string>();
        catchTimer = 2.0f;
        audioPlayed = false;
        currentState = 0;
        reelCount = 0;
        GameObject Parent = GameObject.Find("ReelMeterHolder");
        reelMeter = Parent.gameObject.transform.GetChild(0).gameObject;
        ReelSlider = reelMeter.GetComponent<Slider>();
        //var test = GameObject.FindGameObjectWithTag("ReelBar");

        foreach (var fishType in XML.items)
        {
            if (fishType.Value.GetType() == ItemTypes.Fish)
            {
                fish.Add(fishType.Key);
                numOfFish += 1;
            }
        }
    }
    public override void useTool()
    {

        //get mouse position in world and convert to grid space
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int posInt = grid.LocalToCell(pos);
        tileMap = GameObject.FindGameObjectWithTag("Non-Walkable").GetComponent<Tilemap>();
        if (tileMap.GetTile(posInt) == fishableTile)
        {
            Debug.Log("This tile can be fished in");
            fishingTimer = Random.Range(3.0f, 10.0f);
            catchTimer = 2.0f;
            CM.Moveable = false;
            BobberHolder = new GameObject();
            BobberHolder.AddComponent<SpriteRenderer>();
            BobberHolder.GetComponent<SpriteRenderer>().sortingOrder = 6;
            BobberHolder.GetComponent<SpriteRenderer>().sprite = Instantiate(Bobber, new Vector3(0,0,0), Quaternion.identity);//Sprite.Instantiate(Bobber, pos, Quaternion.identity);
            BobberHolder.transform.position = new Vector3(pos.x,pos.y,0);
            //Instantiate(Bobber, pos, Quaternion.identity);
            currentState = 1;
            ToolUsed = true;

        }
        else
        {
            Debug.Log("This tile cannot be fished in");
        }


    }

    private void Update()
    {
        switch (currentState)
        {
            //not fishing state
            case 0:
                break;
            //waiting state
            case 1:
                fishingTimer -= Time.deltaTime;
                if (fishingTimer < 0)
                {
                    currentState = 2;
                }

                break;
            //bobber triggered state
            case 2:
                if (!audioPlayed)
                {
                    AudioSource Audio = gameObject.GetComponentInParent<AudioSource>();
                    Audio.clip = GetSoundEffect();
                    Audio.Play();
                    audioPlayed = true;
                }
                catchTimer -= Time.deltaTime;

                if (catchTimer < 0)
                {
                    currentlyFishing = false;
                    catchTimer = 2.0f;
                    DestroyImmediate(BobberHolder);
                    CM.Moveable = true;
                    fishingTimer = 10.0f;
                    audioPlayed = false;
                    currentState = 0;
                }
                
                if (Input.GetKeyDown("space"))
                {
                    catchTimer = 5.0f;
                    fishingTimer = 10.0f;
                    reelDifficulty = Random.Range(15, 30);
                    ReelSlider.maxValue = reelDifficulty;
                    ReelSlider.minValue = 0;
                    ReelSlider.value = 0;
                    reelMeter.SetActive(true);
                    audioPlayed = false;
                    currentState = 3;
                }
                break;
            //reeling state
            case 3:
                catchTimer -= Time.deltaTime;
                if (Input.GetKeyDown("space"))
                {
                    ReelSlider.value += 1;
                    reelCount += 1; 
                }

                if (reelCount >= reelDifficulty)
                {
                    int fishPicked = Random.Range(0, numOfFish);
                    string Name = fish[fishPicked];
                    CM.Moveable = true;
                    InventoryAssessment(GetFishItem(Name));
                    reelCount = 0;
                    reelMeter.SetActive(false);
                    DestroyImmediate(BobberHolder);
                    currentState = 4;
                }
                else if (catchTimer < 0)
                {
                    DestroyImmediate(BobberHolder);
                    catchTimer = 2.0f;
                    reelMeter.SetActive(false);
                    CM.Moveable = true;
                    currentState = 0;
                }
                break;
            //caught state
            case 4:
                
                currentState = 0;
                break;
        }
    }

    public void InventoryAssessment(ItemBase Item)
    {
        if (!cInventory.HasItem(Item.GetName()))
        {
            cInventory.AddItem(Item);
        }
        else
        {
            cInventory.AddAmount(Item.GetName(), Item.GetAmount());
        }

    }

    public ItemBase GetFishItem(string Name)
    {
        ToolItems = GameObject.Find("TemplateTools");
        GameObject SubGameObject = new GameObject(Name);
        SubGameObject.transform.parent = ToolItems.transform;
        // Loop through all the XML items
        foreach (var i in XML.items)
        {
            // if we find the plant on the xml then we want to set up the item with the base value of the xml item.
            if (i.Key.Equals(Name))
            {
                ItemBase PlantItem = SubGameObject.gameObject.AddComponent<ItemBase>() as ItemBase;
                PlantItem.SetUpThisItem(i.Value.bItemType, i.Value.bName, i.Value.bAmount, i.Value.bStackable, i.Value.bSrcImage,
                                        i.Value.bSoundEffect, i.Value.bTile, i.Value.bPrefab, i.Value.bSellPrice, i.Value.bCustomData,
                                        i.Value.GetDesc());
                return PlantItem;
            }
        }
        return null;
    }
}
