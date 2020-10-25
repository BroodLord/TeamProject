using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryAbstractClass : MonoBehaviour
{
    public ItemBase[] ItemList;
    public const int MaxCapacity = 5;
    InventoryAbstractClass()
    {
        ItemList = new ItemBase[MaxCapacity];
    }
    public ItemBase GetItem(string ID) 
    {
        for(int i = 0; i < MaxCapacity; i++)
        {
            if(ID == ItemList[i].GetName())
            {
                return ItemList[i];
            }
        }
        return null;
    }

    public bool HasItem(string ID)
    {
        for (int i = 0; i < MaxCapacity; i++)
        {
            if (ID == ItemList[i].GetName())
            {
                return true;
            }
        }
        return false;
    }
    public bool RemoveItem(string ID)
    {
        if (MaxCapacity > 0)
        {
            for (int i = 0; i < MaxCapacity; i++)
            {
                if (ID == ItemList[i].GetName())
                {
                    ItemList.Remove(ItemList[i]);
                    return true;
                }
            }
        }
        return false;
    }
    public bool AddItem(ItemBase Item)
    {
        if (ItemList.Count < MaxCapacity)
        {
            ItemList.Add(Item);
        }
        return false;
    }
    public int CheckItemAmount(string ID)
    {
        for (int i = 0; i < MaxCapacity; i++)
        {
            if (ID == ItemList[i].GetName())
            {
                return ItemList[i].GetAmount();
            }
        }
        return 0;
    }
}
