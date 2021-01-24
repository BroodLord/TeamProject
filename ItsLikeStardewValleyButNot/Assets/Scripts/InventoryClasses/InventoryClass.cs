﻿using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class InventoryClass : InventoryAbstractClass
{
    // Start is called before the first frame update
    public bool NewLevel;
    public GameObject[] Plants;
    public UnityEngine.UI.Image[] ImageSlots;
    
    public TextMeshProUGUI[] AmountText;
    public Sprite BackgroundImage;
    public GameObject ImageParent;
    private bool UIEnabled;
    public override void UpdateUI()
    {
        if (UIEnabled != false)
        {
            for (int i = 0; i < ImageSlots.Length; i++)
            {
                if (Markers[i] != false && ImageSlots != null)
                {
                    ImageSlots[i].gameObject.SetActive(true);
                    ImageSlots[i].sprite = ItemList[i].GetSpriteImage();
                    AmountText[i].text = ItemList[i].GetAmount().ToString();
                }
                else
                {
                    if (ImageSlots[i].sprite != null)
                    {
                        ImageSlots[i].gameObject.SetActive(false);
                        ImageSlots[i].sprite = BackgroundImage;
                    }
                    AmountText[i].text = "0";
                    if (Markers[i] == true)
                    {

                        Markers[i] = false;
                    }

                }


            }
        }
    }

    public void DisabledNEnable()
    {
        if(UIEnabled == false)
        {
            UIEnabled = true;
            ImageParent.gameObject.SetActive(true);
            cInventory.UpdateUI();
        }
        else
        {
            UIEnabled = false;
            ImageParent.gameObject.SetActive(false);
     
        }
    }

    void Start()
    {
        UIEnabled = true;
        DisabledNEnable();
        Resize(20);
        Debug.Log(ItemList.Length);
        cInventory = this.GetComponent<InventoryClass>();
        cHotBar = this.GetComponent<HotBarClass>();
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
