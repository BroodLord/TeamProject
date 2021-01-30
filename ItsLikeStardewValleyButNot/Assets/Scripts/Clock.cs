using System;
using System.Threading;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public TileDictionaryClass Dictioary;
    /*UI Components for Text on the Canvus*/
    public TextMeshProUGUI[] TimeUIText;
    public TextMeshProUGUI[] DateUIText;
    private SellChestClass cChest;
    public TimeFormats ClockTimeFormat = TimeFormats.Hour_24;
    public DateFormats ClockDateFormat = DateFormats.DD_MM_YYYY;
    public float SecondsPerMin = 1;
    public TextMeshProUGUI[] GoldText;
    

    /*Strings which will be assigned to the UI*/
    private string TimeText;
    private string DateText;

    /*Boolen for the 12 hour clock for AM/PM*/
    private bool AM;

    int Hour, Min, Day, Month, Year;
    int MaxHour = 24, MaxMin = 60, MaxDay = 30, MaxMouth = 12; // Setting the max an hour, min and months in a day *FOR NOW WE WILL KEEP IT 30 DAYS, CAN CHANGE LATER WITH SEASONS*

    float TimeTimer = 0.0f; // Will count amount of time has past.

    /*Different formats for Time and Date*/
    public enum TimeFormats { Hour_24, Hour_12 }
    public enum DateFormats { MM_DD_YYYY, DD_MM_YYYY}

    /*Starting values for the clock *THIS CAN BE CHANGED LATER* */
    private void Start()
    {
        Hour = 8;
        Min = 0;
        Day = 1;
        Month = 5;
        Year = 2020;
        if(Hour < 12) { AM = true; }
        cChest = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<SellChestClass>();
        SetTextStrings();
    }
    public void NightUpdate()
    {
        TimeTimer = SecondsPerMin;
        Min = -1;
        Hour = 8;
        ++Day;
        float TempGold = 0;
        for(int i = 0; i < cChest.ItemList.Length; i++)
        {
            if(cChest.Markers[i])
            {
                TempGold += cChest.ItemList[i].GetSellPrice() * cChest.ItemList[i].GetAmount();
                cChest.ItemList[i] = null;
                
            }
        }
        string Output = "Gold: " + TempGold.ToString();
        for (int i = 0; i < GoldText.Length; i++)
        {
            GoldText[i].text = Output;
        }

        // THIS FUNCTION WILL BE USED TO UPDATED EVERYTHING WE WANT TO CHANGE WHEN THE PLAYER FALLS ALSEEP!
        foreach (var v in Dictioary.TileMapData)
        {
            if(v.Value.HasPlant())
            {
                PlantAbstractClass P = v.Value.GetPlant();
                P.UpdatePlant(3);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*This Update function is simple, if a enough time has passed then increase the Min, If that reach the max then increase the hour etc.*/
        if(TimeTimer >= SecondsPerMin)
        {
            ++Min;
            if (Min >= MaxMin)
            {
                Min = 0;
                ++Hour;
                if(Hour >= MaxHour)
                {
                    Hour = 0;
                    ++Day;
                    if(Day >= MaxDay)
                    {
                        Day = 1;
                        ++Month;
                        if(Month >= MaxMouth)
                        {
                            Month = 1;
                            ++Year;
                        }
                    }
                }
            }
            SetTextStrings(); // Function call for setting the strings.
            TimeTimer = 0;
        }
        else
        {
            TimeTimer += Time.deltaTime;
        }
    }

    void SetTextStrings()
    {
        /*This function contains switch statements for time and date enums, each format is different and requires it own enum*/
        switch (ClockTimeFormat)
        {
            case TimeFormats.Hour_24:
                {
                    if(Hour <= 9) { TimeText = "0" + Hour + ":"; }

                    else { TimeText = Hour + ":"; }

                    if(Min <= 9) { TimeText += "0" + Min; }

                    else { TimeText += Min; }
                    break;
                }
            case TimeFormats.Hour_12:
                {
                    int ConvertedHour;

                    if(Hour >= 13) { ConvertedHour = Hour - 12; }

                    else if(Hour == 0) { ConvertedHour = 12; }

                    else { ConvertedHour = Hour; }

                    TimeText = ConvertedHour + ":";

                    if (Min <= 9) { TimeText += "0" + Min; }

                    else { TimeText += Min; }

                    if(AM) { TimeText += " AM"; }
                    else { TimeText += " PM"; }
                    break;
                }
        }
        switch (ClockDateFormat)
        {
            case DateFormats.DD_MM_YYYY:
                {
                    DateText = Day + "/" + Month + "/" + Year;
                    break;
                }
            case DateFormats.MM_DD_YYYY:
                {
                    DateText = Month + "/" + Day + "/" + Year;
                    break;
                }
        }

        /*These two for loops will set all the time and dates in the array to the current time*/
        for(int i = 0; i < TimeUIText.Length; i++)  { TimeUIText[i].text = TimeText; }
        for (int i = 0; i < DateUIText.Length; i++) { DateUIText[i].text = DateText; }
    }
}
