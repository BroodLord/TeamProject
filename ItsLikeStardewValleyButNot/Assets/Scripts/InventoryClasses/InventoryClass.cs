using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.PackageManager;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class InventoryClass : InventoryAbstractClass
{
    // Start is called before the first frame update
    public UnityEngine.UI.Image[] ImageSlots;
    private GameObject Parent;
    private bool UIEnabled;
    public void UpdateUI()
    {
        if (UIEnabled != false)
        {
            TextMeshProUGUI AmountText = new TextMeshProUGUI();
            for (int i = 0; i < ImageSlots.Length; i++)
            {
                if (ItemList[i] != null)
                {
                    ImageSlots[i].sprite = ItemList[i].GetSpriteImage(); ;
                    AmountText = ImageSlots[i].gameObject.GetComponentInChildren<TextMeshProUGUI>();
                    AmountText.text = ItemList[i].GetAmount().ToString();
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

    void Start()
    {
        Parent = ImageSlots[0].transform.parent.gameObject;
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
