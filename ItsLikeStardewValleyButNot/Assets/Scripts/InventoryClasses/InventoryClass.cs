using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.PackageManager;
using UnityEngine.UI;
using UnityEngine;

public class InventoryClass : InventoryAbstractClass
{
    // Start is called before the first frame update
    public UnityEngine.UI.Image[] ImageSlots;
    public void UpdateUI()
    {
        for (int i = 0; i < ImageSlots.Length; i++)
        {
            ImageSlots[i].sprite = ItemList[i].GetSpriteImage();
        }
    }

    void Start()
    {
        Resize(20);
        Debug.Log(ItemList.Length);
        cInventory = this.GetComponent<InventoryClass>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(mItem.GetType() == ToolItem.ItemTypes.Tool)
        //{
        //    ((ToolItem)mItem).Interact();
        //}
    }
}
