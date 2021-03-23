using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryClass : InventoryAbstractClass
{
    // Start is called before the first frame update
    public bool NewLevel;
    public UnityEngine.UI.Image[] ImageSlots;
    
    public TextMeshProUGUI[] AmountText;
    public Sprite BackgroundImage;
    public GameObject ImageParent;
    public bool UIEnabled;

    // Same as the Hotbar class so look there for ref.
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
    // This is used to disable and enable the Inventory UI.
    public void DisabledNEnable()
    {
        if(UIEnabled == false)
        {
            UIEnabled = true;
            // All the UI is attached to an image so we just disable it.
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
        if (SceneManager.GetActiveScene().name != "LoadSaveScene")
        {
            Resize(24);
        }
        Debug.Log(ItemList.Length);
        cInventory = this.GetComponent<InventoryClass>();
        cHotBar = this.GetComponent<HotBarClass>();
        UpdateUI();
        /***************************************/
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
