using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateMoneyUI : MonoBehaviour
{
    public MoneyClass GoldManager;
    public SellChestClass cChest;
    public TextMeshProUGUI Text;
    public void UpdateText()
    {
        float TotalGold = 0;
        for (int i = 0; i < cChest.ItemList.Length; i++)
        {
            if (cChest.Markers[i])
            {
                TotalGold += cChest.ItemList[i].GetSellPrice() * cChest.ItemList[i].GetAmount();
            }
        }
        for(int i = 0; i <= TotalGold; i++)
        {
            Text.text = i.ToString();
        }
    }
}
