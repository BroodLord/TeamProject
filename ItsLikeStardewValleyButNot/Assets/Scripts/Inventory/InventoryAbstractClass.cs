using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class InventoryAbstractClass : MonoBehaviour
{
    public XMLParser XML;
    public GameObject PlayerTools;
    public ItemBase[] ItemList;
    public int MaxCapacity;
    public GameObject ToolItems;
    public bool[] Markers;
    public InventoryClass cInventory;
    public HotBarClass cHotBar;
    // Base set up for the inventory
    public InventoryAbstractClass()
    {
        MaxCapacity = 32;
        ItemList = new ItemBase[MaxCapacity];
        Markers = new bool[MaxCapacity];
    }

    public abstract void UpdateUI();

    // This will loop through the items and return the one the ID
    public ItemBase GetItem(string ID) 
    {
        for(int i = 0; i < ItemList.Length; i++)
        {
            if(ID == ItemList[i].GetName())
            {
                return ItemList[i];
            }
        }
        return null;
    }

    // Finds a free space in the inventory
    public int FindFreeSpaceIndex()
    {
        for (int i = 0; i < Markers.Length; i++)
        {
            if (!Markers[i])
            {
                return i;
            }
        }
        return -1;
    }

    // Resets all the markers
    public bool ResetMarkers()
    {
        for (int i = 0; i < Markers.Length; i++)
        {
            if(ItemList[i] != null)
            {
                Markers[i] = true;
            }
            Markers[i] = false;
        }
        return true;
    }
    // This will resize the inventory
    public void Resize(int Size)
    {
        MaxCapacity = Size;
        ItemBase[] TempList = new ItemBase[Size];
        bool[] TempMarkers = new bool[Size];
        for(int i = 0; i < TempList.Length; i++)
        {
            TempList[i] = ItemList[i];
            TempMarkers[i] = false;
        }
        ItemList = TempList;
        Markers = TempMarkers;
        ResetMarkers();
    }
    // Check if the item is in the inventory 
    public bool HasItem(string ItemName)
    {
        for (int i = 0; i < ItemList.Length; i++)
        {
            if (Markers[i] && ItemName == ItemList[i].GetName())
            {
                return true;
            }
        }
        return false;
    }

    //private void RemoveToolFromPlayer(int Index)
    //{
    //    ItemBase Item = ItemList[Index];
    //    PlayerTools.GetComponent(Item);
    //}

    // this will loop through the items and if we find the correct one then destory the item and reset that slot in the inventory.
   public bool RemoveItem(string Name)
   {

        for (int i = 0; i < ItemList.Length; i++)
        {
            if (Markers[i] == true)
            {
                if (Name == ItemList[i].name)
                {
                    //RemoveToolFromPlayer(i);
                    Destroy(ItemList[i]);
                    ItemList[i] = null;
                    Markers[i] = false;
                    UpdateUI();
                    return true;
                }
            }
        }

       return false;
   }
    // This will remove a set amount from a given item
    public bool RemoveAmount(string Name, int Amount)
    {
        for (int i = 0; i < ItemList.Length; i++)
        {
            if (Name == ItemList[i].name)
            {
                int temp = ItemList[i].GetAmount();
                temp -= Amount;
                ItemList[i].SetAmount(temp);
                UpdateUI();
                return true;
            }
        }
        return false;
    }
    // Adds an item in an empty space in the inventory
    public bool AddItem(ItemBase Item)
    {
        if (ItemList.Length <= MaxCapacity)
        {
            for (int i = 0; i < ItemList.Length; i++)
            {
                if (Markers[i] == false)
                {
                    ItemList[i] = Item;
                    Markers[i] = true;
                    UpdateUI();
                    return true;
                }
            }
        }
       return false;
    }
    // Adds a set amount to a given item in the inventory
    public bool AddAmount(string Name, int Amount)
    {

        for (int i = 0; i < ItemList.Length; i++)
        {
            if (ItemList[i] != null && Name == ItemList[i].GetName())
            {
                int temp = ItemList[i].GetAmount();
                temp += Amount;
                ItemList[i].SetAmount(temp);
                UpdateUI();
                return true;
            }
        }
        return false;
    }
    // Check an items amount, not currently used but good to check what amount we have for selling and stuff.
    public int CheckItemAmount(string ID)
    {

        for (int i = 0; i < ItemList.Length; i++)
        {
            if (ID == ItemList[i].GetName())
            {
                return ItemList[i].GetAmount();
            }
        }
        return 0;
    }
}
