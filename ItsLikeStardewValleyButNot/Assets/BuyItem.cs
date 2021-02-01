using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuyItem : MonoBehaviour
{
    public void BuyShopItem()
    {
        XMLParser XML = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<XMLParser>();
        MoneyClass PlayerMoney = GameObject.FindGameObjectWithTag("Player").GetComponent<MoneyClass>();
        InventoryClass cInventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryClass>();
        ItemBase Item = new ItemBase();
        ItemBase NewItem = gameObject.GetComponentInParent<ItemBase>();

        GameObject SubGameObject = new GameObject(NewItem.bName);
        SubGameObject.transform.parent = cInventory.ToolItems.transform;

        if (PlayerMoney.GetMoney() >= NewItem.GetSellPrice())
        {
            if (cInventory.HasItem(NewItem.GetName()) && NewItem.GetStackable())
            {
                cInventory.AddAmount(NewItem.GetName(), NewItem.GetAmount());
                PlayerMoney.SetMoney(PlayerMoney.GetMoney() - NewItem.GetSellPrice());
            }
            else
            {
                if (NewItem.bName == "Hoe") { Item = SubGameObject.AddComponent<HoeScript>() as HoeScript; }
                else if (NewItem.bName == "Water Bucket") { Item = SubGameObject.gameObject.AddComponent<WateringCanScript>() as WateringCanScript; }
                else if (NewItem.bName == "Scythe") { Item = SubGameObject.gameObject.AddComponent<ScytheTool>() as ScytheTool; }
                else if (NewItem.bItemType == DefaultItemBase.ItemTypes.Seed) { Item = SubGameObject.gameObject.AddComponent<PlantSeed>() as PlantSeed; }
                else { Item = SubGameObject.gameObject.AddComponent<ItemBase>() as ItemBase; }
                Item.SetUpThisItem(NewItem.bItemType, NewItem.bName, NewItem.bAmount,
                                   NewItem.bStackable, NewItem.bSrcImage, NewItem.bSoundEffect,
                                   NewItem.bTile, NewItem.bPrefab, NewItem.bSellPrice);
                cInventory.AddItem(Item);
                PlayerMoney.SetMoney(PlayerMoney.GetMoney() - Item.GetSellPrice());
            }
        }
        cInventory.UpdateUI();
    }
}
