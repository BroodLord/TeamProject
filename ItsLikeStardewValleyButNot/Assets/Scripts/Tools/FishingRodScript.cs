using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class FishingRodScript : ToolScript
{
    [SerializeField] private Object fishableTile;
    bool currentlyFishing;
    [SerializeField]float fishingTimer = 10.0f;
    [SerializeField]float catchTimer;
    GameObject ToolItems;
    List<string> fish;
    XMLParser XML;
    int numOfFish = 0;
    bool audioPlayed;

    private void Start()
    {
        fishableTile = Resources.Load("Assets/Sprites/Overworld Sprites/Overworld_276.asset");
        ToolItems = GameObject.Find("TemplateTools");
        XML = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<XMLParser>();
        cInventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryClass>();
        fish = new List<string>();
        catchTimer = 2.0f;
        audioPlayed = false;
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

        if (tileMap.GetTile(posInt) == fishableTile)
        {
            Debug.Log("This tile can be fished in");
            fishingTimer = Random.Range(3.0f, 10.0f);
            currentlyFishing = true;
            ToolUsed = true;

        }
        else
        {
            Debug.Log("This tile cannot be fished in");
        }


    }

    private void Update()
    {
        if (currentlyFishing)
        {
            fishingTimer -= Time.deltaTime;
            if (fishingTimer < 0)
            {
                if (!audioPlayed)
                {
                    AudioSource Audio = gameObject.GetComponentInParent<AudioSource>();
                    Audio.clip = GetSoundEffect();
                    Audio.Play();
                    audioPlayed = true;
                }
                /** CAUTION, EARRAPE! UNCOMMENT AT YOUR OWN RISK **/
                //Audio.PlayOneShot(Audio.clip);

                catchTimer -= Time.deltaTime;
                if (catchTimer < 0)
                {
                    currentlyFishing = false;
                    catchTimer = 2.0f;
                    fishingTimer = 10.0f;
                    audioPlayed = false;
                }

                if (Input.GetMouseButtonDown(1))
                {
                    int fishPicked = Random.Range(0, numOfFish);
                    string Name = fish[fishPicked];

                    InventoryAssessment(GetFishItem(Name));

                    currentlyFishing = false;
                    catchTimer = 2.0f;
                    fishingTimer = 10.0f;
                }
            }

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
                                        i.Value.bSoundEffect, i.Value.bTile, i.Value.bPrefab, i.Value.bSellPrice, i.Value.bCustomData);
                return PlantItem;
            }
        }
        return null;
    }
}
