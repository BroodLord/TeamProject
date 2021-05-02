using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMissionManagement : MonoBehaviour
{
    public TextMeshProUGUI MainMissionText;
    public TextMeshProUGUI ScreenDiologe;
    public GameObject MissionBoardUI;
    public bool PaidTaxes;
    public int TaxAmount;
    MoneyClass MoneyManager;
    // Start is called before the first frame update
    void Start()
    {
        MoneyManager = this.GetComponent<MoneyClass>();
        TaxAmount = 25000;
        PaidTaxes = false;
    }

    public void UpdateFromXML(bool B)
    {
        PaidTaxes = B;
        if(PaidTaxes)
        {
            MainMissionText.text = "Main Mission Complete";
        }
        else
        {
            MainMissionText.text = "Main Objective: Pay Taxes(£25000)";
        }
    }

    public void PayTaxes()
    {
        if (MoneyManager.GetMoney() < TaxAmount)
        {
            ScreenDiologe.text = "This isn't enough, the amount needed is " + TaxAmount + " so get earning kid";
        }
        else
        {
            PaidTaxes = true;
            MissionBoardUI.SetActive(false);
            float newMoney = (MoneyManager.GetMoney() - TaxAmount);
            MoneyManager.SetMoney(newMoney);
            MainMissionText.text = "Main Mission Complete";
        }
    }
}
