//using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public PauseMenu PauseMenuScript;
    public GameObject SleepUI;
    XMLParser ItemManager;
    public InventoryClass cInventory;
    public SellChestClass cChest;
    public GameObject ShopUI;
    public Clock cClock;
    // Start is called before the first frame update
    // Get all the refenece for the variables
    void Start()
    {
        ItemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<XMLParser>();
        cInventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryClass>();
        cChest = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<SellChestClass>();
        cClock = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Clock>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*If we collide with an item then we want to add it to the Inventory*/
        if (collision.gameObject.tag == "Item")
        {
            string ItemName = collision.gameObject.name;
            ItemBase Item = new ItemBase();
            ItemManager.items.TryGetValue(ItemName, out Item);
            Sprite ItemSprite = collision.GetComponent<SpriteRenderer>().sprite;
            if (cInventory.HasItem(Item.GetName()) && Item.GetStackable() == true)
            {
                Item.AddAmount(1);
                cInventory.UpdateUI();
            }
            else
            {
                //Item.SetAmount(1);
                //Item.SetSpriteImage(ItemSprite);
                cInventory.AddItem(Item);
                cInventory.UpdateUI();
            }
            Debug.Log("COLLISION!");
        }
        // if we collide with a bed then show the sleep menu and pause the game
        if (collision.gameObject.tag == "Bed")
        {
            SleepUI.SetActive(true);
            PauseMenuScript.GameIsPaused = true;
            //cClock.NightUpdate();
            //Debug.Log("COLLISION!");
        }
        // if we collide with the sell chest then enable it
        if (collision.gameObject.tag == "SellChest")
        {
            cChest.DisabledNEnable();
            if (!cInventory.ImageParent.gameObject.activeInHierarchy)
            {
                cInventory.DisabledNEnable();
            }
        }
        // if we collide with the shop then enable the shop
        if (collision.gameObject.tag == "Shop")
        {
            ShopUI.SetActive(true);
        }
    }
    /* if we leave any of these collisions then disable them. */
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SellChest")
        {
            cChest.DisabledNEnable();
            if (cInventory.ImageParent.gameObject.activeInHierarchy)
            {
                cInventory.DisabledNEnable();
            }
        }
        if (collision.gameObject.tag == "Shop")
        {
            ShopUI.SetActive(false);
        }
    }
}
