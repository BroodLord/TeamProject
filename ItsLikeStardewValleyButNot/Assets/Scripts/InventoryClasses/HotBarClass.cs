using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class HotBarClass : InventoryAbstractClass
{
    // Start is called before the first frame update
    public UnityEngine.UI.Image[] ImageSlots;
    public TextMeshProUGUI[] AmountText;
    public Sprite BackgroundImage;
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
    // Start is called before the first frame update
    void Start()
    {
        Resize(10);
        Debug.Log(ItemList.Length);
        cInventory = this.GetComponent<InventoryClass>();
        cHotBar = this.GetComponent<HotBarClass>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
