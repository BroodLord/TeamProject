using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.PackageManager;
using UnityEngine;

public class InventoryClass : InventoryAbstractClass
{
    // Start is called before the first frame update
    ItemBase mItem;

    void Start()
    {
        ToolItem Item = new ToolItem();
        Item.Init("UWUW MAN", 69420);
        mItem = Item;
        //ItemList.Add(mItem);
    }

    // Update is called once per frame
    void Update()
    {
        if(mItem.GetType() == ToolItem.ItemTypes.Tool)
        {
            ((ToolItem)mItem).Interact();
        }
    }
}
