using System;
using System.Threading;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public TileDictionaryClass Dictioary;
    public MoneyClass GoldManager;
    public GameObject SeedParentShopSlot;
    public GameObject Player;
    public GameObject ToolParentShopSlot;
    public GameObject ShopSlot;
    public GameObject GoldSpritePrefab;
    public StaminaScript Stam;
    public Animator Transition;
    /*UI Components for Text on the Canvus*/
    public TextMeshProUGUI[] TimeUIText;
    public TextMeshProUGUI[] DateUIText;
    public Image Lighting;
    private SellChestClass cChest;
    public TimeFormats ClockTimeFormat = TimeFormats.Hour_24;
    public DateFormats ClockDateFormat = DateFormats.DD_MM_YYYY;
    public float SecondsPerMin;
    public TextMeshProUGUI GoldText;
    public GameObject GoldSpriteHolder;
    public PauseMenu PauseMenuScript;
    public int PassoutTimer;
    public bool[] WeeklyReset;
    public Sprite[] GoldTextSprites;
    private List<KeyValuePair<int, GameObject>> SpriteDictionary;

    public Vector3 PlayerPos;
    public String SceneName;

    private bool AtMidDay;

    /*Strings which will be assigned to the UI*/
    private string TimeText;
    private string DateText;

    /*Boolen for the 12 hour clock for AM/PM*/
    private bool AM;

    public int Hour, Min, Day, Month, Year, WeekCounter;
    public int MaxHour = 24, MaxMin = 60, MaxDay = 30, MaxMouth = 12; // Setting the max an hour, min and months in a day *FOR NOW WE WILL KEEP IT 30 DAYS, CAN CHANGE LATER WITH SEASONS*

    public float TimeTimer = 0.0f; // Will count amount of time has past.

    /*Different formats for Time and Date*/
    public enum TimeFormats { Hour_24, Hour_12 }
    public enum DateFormats { MM_DD_YYYY, DD_MM_YYYY }

    /*Starting values for the clock *THIS CAN BE CHANGED LATER* */
    private void Start()
    {
        AtMidDay = false;
        PassoutTimer = 0;
        WeeklyReset = new bool[4];
        WeeklyReset[0] = true;
        WeeklyReset[1] = true;
        WeeklyReset[2] = true;
        WeeklyReset[3] = true;
        Hour = 8;
        WeekCounter = 1;
        Min = 0;
        Day = 1;
        Month = 5;
        Year = 1969;
        SpriteDictionary = new List<KeyValuePair<int, GameObject>>();
        foreach (Transform child in transform)
        {
            if (child.tag == "Lighting")
            {
                Lighting = child.GetComponent<Image>();
                break;
            }
        }
        if (Hour < 12) { AM = true; }
        cChest = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<SellChestClass>();
        SetTextStrings();
        AssignShopItems(DefaultItemBase.ItemTypes.Seed);
        AssignShopItems(DefaultItemBase.ItemTypes.Tool);
    }
    // Used when the player goes to bed
    public void NightUpdate()
    {
        /*Used to reset the global lightning*/
        AtMidDay = false;
        PassoutTimer = 0;
        var tempColor = Lighting.color;
        tempColor.a = 0.1176471f;
        Lighting.color = tempColor;
        /************************************/
        /* This used to counter if it has been a week, used to reset the rocks in the mines currently */
        WeekCounter++;
        if (WeekCounter == 7)
        {
            for (int i = 0; i < 3; i++)
            {
                WeeklyReset[i] = true;
            }
            WeekCounter = 0;
        }
        /************************************/
        TimeTimer = SecondsPerMin;
        Min = -1;
        Hour = 8;
        ++Day;
        /* This is used to update the players gold */
        for (int i = 0; i < cChest.ItemList.Length; i++)
        {
            /*If the current index marker is true then we have something to sell*/
            if (cChest.Markers[i])
            {
                // An item will have an amount attached so we also want to sell these too
                float Temp = cChest.ItemList[i].GetSellPrice() * cChest.ItemList[i].GetAmount();
                GoldManager.AddAmount(Temp);
                // Reset the current chest slot as it should be empty
                cChest.ItemList[i] = null;



            }
        }
        // Reset the markers as they should all be false
        cChest.ResetMarkers();
        // Update the text
        //string Output = GoldManager.GetMoney().ToString();
        //GoldText.text = Output;

        AssignGoldSprites();

        /************************************/

        // THIS FUNCTION WILL BE USED TO UPDATED EVERYTHING WE WANT TO CHANGE WHEN THE PLAYER FALLS ALSEEP!
        foreach (var v in Dictioary.TileMapData.ElementAt(0).Value)
        {
            if (v.Value.HasPlant())
            {
                PlantAbstractClass P = v.Value.GetPlant();
                P.UpdatePlant(5);
            }
            else
            {
                v.Value.SetWatered(false);
            }
        }
    }

    int FindNumber(char Gold)
    {
        for (int i = 0; i < 10; i++)
        {
            int GoldNum = (int)char.GetNumericValue(Gold);
            if (i == GoldNum)
            {
                return i;
            }
        }
        return 0;
    }

    void AssginSprites(string Gold)
    {
        int[] array = new int[Gold.Length];
        for (int i = 0; i < Gold.Length; i++)
        {
            array[i] = FindNumber(Gold[i]);
        }
        for (int i = 0; i < array.Length; i++)
        {
            GameObject Sprite = Instantiate(GoldSpritePrefab) as GameObject;
            Sprite.transform.parent = GoldSpriteHolder.transform;
            Sprite.transform.position = GoldSpriteHolder.transform.position;
            Sprite.transform.Find("Sprite").GetComponent<Image>().sprite = GoldTextSprites[array[i]];
            KeyValuePair<int, GameObject> KVP = new KeyValuePair<int, GameObject>(array[i], Sprite);
            SpriteDictionary.Add(KVP);
        }
    }

    void ChangeSprites(string Gold)
    {
        int[] array = new int[Gold.Length];
        for (int i = 0; i < Gold.Length; i++)
        {
            array[i] = FindNumber(Gold[i]);
        }
        for (int i = 0; i < array.Length; i++)
        {
            if (array.Length > SpriteDictionary.Count)
            {
                if (i < SpriteDictionary.Count)
                {
                    if (SpriteDictionary[i].Key != array[i])
                    {
                        KeyValuePair<int, GameObject> KVP = new KeyValuePair<int, GameObject>(array[i], SpriteDictionary[i].Value);
                        SpriteDictionary[i].Value.transform.Find("Sprite").GetComponent<Image>().sprite = GoldTextSprites[array[i]];
                        SpriteDictionary[i] = KVP;
                    }
                }
                else
                {
                    GameObject Sprite = Instantiate(GoldSpritePrefab) as GameObject;
                    Sprite.transform.parent = GoldSpriteHolder.transform;
                    Sprite.transform.position = GoldSpriteHolder.transform.position;
                    Sprite.transform.Find("Sprite").GetComponent<Image>().sprite = GoldTextSprites[array[i]];
                    KeyValuePair<int, GameObject> KVP = new KeyValuePair<int, GameObject>(array[i], Sprite);
                    SpriteDictionary.Add(KVP);
                }
            }
            else if (array.Length < SpriteDictionary.Count)
            {
                int Diff = SpriteDictionary.Count - array.Length;
                for(int j = 0; j < Diff; j++)
                {
                    Destroy(SpriteDictionary[j].Value);
                    SpriteDictionary.RemoveAt(j);
                }
            }
        }
    }

    void AssignGoldSprites()
    {
        // Assigns the sprite if there isn't any
        string Gold = GoldManager.GetMoney().ToString();
        if (SpriteDictionary.Count == 0)
        {
            AssginSprites(Gold);
        }
        else if (Gold.Length > SpriteDictionary.Count)
        {
            ChangeSprites(Gold);
        }
        else if (Gold.Length < SpriteDictionary.Count)
        {
            ChangeSprites(Gold);
        }
        // Will check if the amount has changed
        else if (Gold.Length == SpriteDictionary.Count)
        {
            int Counter = 0;
            for (int i = 0; i < Gold.Length; i++)
            {
                int Number = FindNumber(Gold[Counter]);
                if (SpriteDictionary[i].Key != Number)
                {
                    KeyValuePair<int, GameObject> KVP = new KeyValuePair<int, GameObject>(Number, SpriteDictionary[i].Value);
                    SpriteDictionary[i].Value.gameObject.transform.Find("Sprite").GetComponent<Image>().sprite = GoldTextSprites[Number];
                    SpriteDictionary[i] = KVP;
                }
                Counter++;
            }
        }
    }

    public int getHour()
    {
        return Hour;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuScript.GameIsPaused)
        {

            //string Output = GoldManager.GetMoney().ToString();
            //GoldText.text = Output;
            AssignGoldSprites();

            /*This Update function is simple, if a enough time has passed then increase the Min, If that reach the max then increase the hour etc.*/
            if (TimeTimer >= SecondsPerMin)
            {
                /*******************************/
                /* Change the light so it represents a full realistic lighting, 8am 70% light, 12 100%, 12pm - 2am decrease the light till 30% light */
                // 30% 0.1176471, 100% 0.3921569

                // GROUND BREAKING LIGHTING MATHS THAT THE WORLD NEEDS TO KNOW ABOUT, ONLY WORKS X NUMBER OF TIMES
                // Lighting = ( 39 % ((CurrentLighting% / Time(Hours)) / 60)) 0.0004875f;
                var tempColor = Lighting.color;
                if (!AtMidDay)
                {
                    tempColor.a -= 0.0004875f;
                    if (PassoutTimer == 4)
                    {
                        AtMidDay = true;
                    }
                }
                else
                {
                    tempColor.a += 0.0004875f;
                }

                Lighting.color = tempColor;
                /*********************************/
                //Lighting.color.a = 1f;
                ++Min;
                if (Min >= MaxMin)
                {
                    Min = 0;
                    ++PassoutTimer;
                    if (PassoutTimer == 18)
                    {
                        PassoutTimer = 0;
                        PlayerPassOut();
                    }
                    ++Hour;
                    if (Hour >= MaxHour)
                    {
                        Hour = 0;
                        ++Day;
                        if (Day >= MaxDay)
                        {
                            Day = 1;
                            ++Month;
                            if (Month >= MaxMouth)
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
    }

    void PlayerPassOut()
    {
        // Start the play transition
        Stam.SetStamina(75);
        NightUpdate();
        LoadLevel Load = GameObject.FindGameObjectWithTag("LoadManager").GetComponent<LoadLevel>();
        Load.TransferLevel("PlayerRoom", new Vector3(-1, 9, 0));
    }

    // RIP AZIR
    void AssignShopItems(DefaultItemBase.ItemTypes Type)
    {
        // Finds the refs
        XMLParser XML = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<XMLParser>();
        ItemTypeFinder TypeFinder = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemTypeFinder>();
        // Loop through all the XML
        for (int i = 0; i < XML.items.Count; i++)
        {
            // if its a seed
            if (XML.items.ElementAt(i).Value.GetType() == Type)
            {
                // Generate a random number to see if that seed will be in the shop
                int RandomChance = UnityEngine.Random.Range(0, 100);
                if (Type == DefaultItemBase.ItemTypes.Tool || RandomChance > 50)
                {
                    /*Wanna get the gameobject we will put the component on*/
                    GameObject childObject = Instantiate(ShopSlot) as GameObject;
                    if (Type == DefaultItemBase.ItemTypes.Seed)
                    {
                        childObject.transform.parent = SeedParentShopSlot.transform;
                    }
                    else
                    {
                        childObject.transform.parent = ToolParentShopSlot.transform;
                    }

                    /******************************************/
                    /* Get the image so we can set the sprite to appear there */
                    Image Image = childObject.transform.Find("ImageSlot").transform.Find("ItemImage").GetComponent<Image>();
                    Image.sprite = XML.items.ElementAt(i).Value.GetSpriteImage();
                    /******************************************/
                    // Sets the title
                    TextMeshProUGUI TitleText = childObject.transform.Find("Title").GetComponent<TextMeshProUGUI>();
                    TitleText.text = XML.items.ElementAt(i).Value.GetName();
                    // Sets the price
                    TextMeshProUGUI PriceText = childObject.transform.Find("Price").GetComponent<TextMeshProUGUI>();
                    PriceText.text = "Price: " + XML.items.ElementAt(i).Value.GetSellPrice().ToString();
                    // Set the desctext
                    TextMeshProUGUI DescText = childObject.transform.Find("Description").GetComponent<TextMeshProUGUI>();
                    DescText.text = XML.items.ElementAt(i).Value.GetDesc();
                    // Find what item we are wanting to use and set it up
                    TypeFinder.TyepFinder(i, childObject).SetUpThisItem(XML.items.ElementAt(i).Value.bItemType, XML.items.ElementAt(i).Value.bName, XML.items.ElementAt(i).Value.bAmount,
                                                                        XML.items.ElementAt(i).Value.bStackable, XML.items.ElementAt(i).Value.bSrcImage, XML.items.ElementAt(i).Value.bSoundEffect,
                                                                        XML.items.ElementAt(i).Value.bTile, XML.items.ElementAt(i).Value.bPrefab, XML.items.ElementAt(i).Value.bSellPrice, XML.items.ElementAt(i).Value.bCustomData,
                                                                        XML.items.ElementAt(i).Value.GetDesc());
                }
            }
        }
    }

    void SetTextStrings()
    {
        /*This function contains switch statements for time and date enums, each format is different and requires it own enum*/
        switch (ClockTimeFormat)
        {
            case TimeFormats.Hour_24:
                {
                    if (Hour <= 9) { TimeText = "0" + Hour + ":"; }

                    else { TimeText = Hour + ":"; }

                    if (Min <= 9) { TimeText += "0" + Min; }

                    else { TimeText += Min; }
                    break;
                }
            case TimeFormats.Hour_12:
                {
                    int ConvertedHour;

                    if (Hour >= 13) { ConvertedHour = Hour - 12; AM = false; }

                    else if (Hour == 0) { ConvertedHour = 12; }

                    else { ConvertedHour = Hour; }

                    TimeText = ConvertedHour + ":";

                    if (Min <= 9) { TimeText += "0" + Min; }

                    else { TimeText += Min; }

                    if (AM) { TimeText += " AM"; }
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
        for (int i = 0; i < TimeUIText.Length; i++) { TimeUIText[i].text = TimeText; }
        for (int i = 0; i < DateUIText.Length; i++) { DateUIText[i].text = DateText; }
    }
}
