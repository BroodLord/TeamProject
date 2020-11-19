//using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public InventoryClass cInventory;
    public Clock cClock;
    // Start is called before the first frame update
    void Start()
    {
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
            ItemBase Item = collision.GetComponent<ItemBase>();
            Sprite ItemSprite = collision.GetComponent<SpriteRenderer>().sprite;
            if (cInventory.HasItem(Item.name) && Item.GetStackable() == true)
            {
                Item.AddAmount(1);
                cInventory.UpdateUI();
            }
            else
            {
                Item.SetName(Item.name);
                Item.SetAmount(1);
                Item.SetSpriteImage(ItemSprite);
                cInventory.AddItem(Item, Item.GetAmount());
                cInventory.UpdateUI();
            }
            Debug.Log("COLLISION!");
            //Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Bed")
        {
            cClock.NightUpdate();
            Debug.Log("COLLISION!");
        }
    }
}
