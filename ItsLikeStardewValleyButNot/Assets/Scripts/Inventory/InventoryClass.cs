using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine;

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
        Resize(24);
        Debug.Log(ItemList.Length);
        cInventory = this.GetComponent<InventoryClass>();
        cHotBar = this.GetComponent<HotBarClass>();
        //ItemTypeFinder TypeFinder = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemTypeFinder>();
        //for (int i = 0; i < XML.items.Count; i++)
        //{
        //    if (XML.items.ElementAt(i).Value.bItemType == DefaultItemBase.ItemTypes.Seed)
        //    {
        //        ItemBase BasicItem = new ItemBase();
        //        // Get the object the component will be on and give it a name
        //        GameObject SubGameObject = new GameObject(XML.items.ElementAt(i).Value.bName);
        //        SubGameObject.transform.parent = ToolItems.transform;
        //        // Find the type of the item and set it up, after add it to the dictionary.
        //        BasicItem = TypeFinder.TyepFinder(i, SubGameObject);
        //        BasicItem.SetUpThisItem(XML.items.ElementAt(i).Value.bItemType, XML.items.ElementAt(i).Value.bName, XML.items.ElementAt(i).Value.bAmount,
        //                                XML.items.ElementAt(i).Value.bStackable, XML.items.ElementAt(i).Value.bSrcImage, XML.items.ElementAt(i).Value.bSoundEffect,
        //                                XML.items.ElementAt(i).Value.bTile, XML.items.ElementAt(i).Value.bPrefab, XML.items.ElementAt(i).Value.bSellPrice);
        //        AddItem(BasicItem);
        //    }
        //}
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
