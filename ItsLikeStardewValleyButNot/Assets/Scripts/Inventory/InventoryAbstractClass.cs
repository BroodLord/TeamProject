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
    public InventoryAbstractClass()
    {
        MaxCapacity = 32;
        ItemList = new ItemBase[MaxCapacity];
        Markers = new bool[MaxCapacity];
    }

    public abstract void UpdateUI();

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

    public bool AddItem(ItemBase Item)
    {
        for (int i = 0; i < ItemList.Length; i++)
        {
            if(Markers[i] == false)
            {
                ItemList[i] = Item;
                Markers[i] = true;
                UpdateUI();
                return true;
            }
        }
       return false;
    }
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
