using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class HotBarClass : InventoryAbstractClass
{
    // Start is called before the first frame update
    public UnityEngine.UI.Image[] ImageSlots;
    public void UpdateUI()
    {
        TextMeshProUGUI AmountText = new TextMeshProUGUI();
        for (int i = 0; i < ImageSlots.Length; i++)
        {
            ImageSlots[i].sprite = ItemList[i].GetSpriteImage(); ;
            AmountText = ImageSlots[i].gameObject.GetComponentInChildren<TextMeshProUGUI>();
            AmountText.text = ItemList[i].GetAmount().ToString();


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
