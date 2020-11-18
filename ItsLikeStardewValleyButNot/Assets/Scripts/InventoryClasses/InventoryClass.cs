using System.Collections;
using System.Collections.Generic;
using System.Drawing;
//using UnityEditor.PackageManager;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class InventoryClass : InventoryAbstractClass
{
    // Start is called before the first frame update
    public LoadLevel Data; 
    public UnityEngine.UI.Image[] ImageSlots;
    public TextMeshProUGUI[] AmountText;
    public Sprite BackgroundImage;
    private GameObject Parent;
    private bool UIEnabled;
    public void UpdateUI()
    {
        if (UIEnabled != false)
        {
            for (int i = 0; i < ImageSlots.Length; i++)
            {
                if (ItemList[i] != null)
                {
                    
                    ImageSlots[i].sprite = ItemList[i].GetSpriteImage();
                    AmountText[i].text = ItemList[i].GetAmount().ToString();
                }
                else
                {
                    ImageSlots[i].sprite = BackgroundImage;
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
            Parent.gameObject.SetActive(true);
            cInventory.UpdateUI();
        }
        else
        {
            UIEnabled = false;
            Parent.gameObject.SetActive(false);
        }
    }

    void Awake()
    {
        Parent = ImageSlots[0].transform.parent.parent.gameObject;
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
