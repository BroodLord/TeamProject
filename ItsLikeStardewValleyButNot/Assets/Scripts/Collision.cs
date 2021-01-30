//using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Collision : MonoBehaviour
{
    XMLParser ItemManager;
    public InventoryClass cInventory;
    public SellChestClass cChest;
    public Clock cClock;
    // Start is called before the first frame update
    void Start()
    {
        ItemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<XMLParser>();
        cInventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryClass>();
        cChest = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<SellChestClass>();
        cClock = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Clock>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            string ItemName = collision.gameObject.name;
            ItemBase Item = new ItemBase();
            ItemManager.items.TryGetValue(ItemName, out Item);
            //ItemBase Item = collision.GetComponent<ItemBase>();
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
        if (collision.gameObject.tag == "Bed")
        {
            cClock.NightUpdate();
            Debug.Log("COLLISION!");
        }
        if (collision.gameObject.tag == "SellChest")
        {
            cChest.DisabledNEnable();
            if (!cInventory.ImageParent.gameObject.activeInHierarchy)
            {
                cInventory.DisabledNEnable();
            }
        }
    }
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
    }
}
