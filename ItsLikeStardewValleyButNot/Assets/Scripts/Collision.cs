using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public InventoryClass cInventory;
    // Start is called before the first frame update
    void Start()
    {
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
            Item.SetName("Test Item");
            Item.SetSpriteImage(ItemSprite);
            cInventory.AddItem(Item);
            Debug.Log("COLLISION!");
        }
        Destroy(this.gameObject);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {

    }
}
