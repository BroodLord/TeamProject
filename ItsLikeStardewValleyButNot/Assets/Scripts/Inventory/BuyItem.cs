using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuyItem : MonoBehaviour
{
    /*RIP AZIR*/
    public void BuyShopItem()
    {
        XMLParser XML = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<XMLParser>();
        MoneyClass PlayerMoney = GameObject.FindGameObjectWithTag("Player").GetComponent<MoneyClass>();
        InventoryClass cInventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryClass>();
        HotBarClass cHotbar = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<HotBarClass>();
        SellChestClass cSellChest = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<SellChestClass>();
        ItemBase Item = new ItemBase();
        ItemBase NewItem = gameObject.GetComponentInParent<ItemBase>();
        ItemTypeFinder TypeFinder = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemTypeFinder>();
        // Get the object the component will be on and give it a name
        GameObject SubGameObject = new GameObject(NewItem.bName);
        SubGameObject.transform.parent = cInventory.ToolItems.transform;

        // If the player has enough gold
        if (PlayerMoney.GetMoney() >= NewItem.GetSellPrice())
        {
            // If it has the item and is stackable
            if (cInventory.HasItem(NewItem.GetName()) && NewItem.GetStackable())
            {
                cInventory.AddAmount(NewItem.GetName(), NewItem.GetAmount());
                PlayerMoney.SetMoney(PlayerMoney.GetMoney() - NewItem.GetSellPrice());
            }
            else if(!cInventory.HasItem(NewItem.GetName()) && !cHotbar.HasItem(NewItem.GetName()) && !cSellChest.HasItem(NewItem.GetName()))
            {
                // Find the type of the item and set it up, after add it to the dictionary.
                Item = TypeFinder.ItemTyepFinder(NewItem, SubGameObject);
                Item.SetUpThisItem(NewItem.bItemType, NewItem.bName, NewItem.bAmount,
                                   NewItem.bStackable, NewItem.bSrcImage, NewItem.bSoundEffect,
                                   NewItem.bTile, NewItem.bPrefab, NewItem.bSellPrice, NewItem.bCustomData,
                                   NewItem.GetDesc());
                cInventory.AddItem(Item);
                PlayerMoney.SetMoney(PlayerMoney.GetMoney() - Item.GetSellPrice());
            }
        }
        cInventory.UpdateUI();
    }
}
