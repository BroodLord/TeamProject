using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DataXMLParser : MonoBehaviour
{
    public InventoryClass Inventory;
    public HotBarClass HotBar;
    public SellChestClass Chest;
    public Clock cClock;
    public MoneyClass Money;
    public StaminaScript Stamania;
    public TileDictionaryClass Database;
    private Transform PlayerPos;
    public GameObject ToolItems;
    private bool[] Resets;
    private int[] Slots;

    public struct RockData
    {
        public string ItemType;
        public string OreName;
        public float[] ID;
        public float[] Pos;
        public string Tile;
        public string XMLName;
        public float Amount;

        public void SetUp()
        {
            ID = new float[3];
            Pos = new float[3];
        }
    }
    public struct TreeData
    {
        public string ItemType;
        public string OreName;
        public float[] ID;
        public float[] Pos;
        public string Tile;
        public string XMLName;
        public float Amount;

        public void SetUp()
        {
            ID = new float[3];
            Pos = new float[3];
        }
    }
    public struct PlantData
    {
        public string ItemType;
        public int Amount;
        public int CurrentDays;
        public int GrowthTime;
        public string[] GrowthSprite;
        public float[] ID;
        public float[] Pos;
        public string Tile;
        public bool Harvestable;
        public bool Growth;
        public string XMLName;
        public bool Watered;

        public void SetUp()
        {
            GrowthSprite = new string[3];
            ID = new float[3];
            Pos = new float[3];
        }
    }

    public struct ItemData
    {
        public string Name;
        public string Type;
        public string Src_Image;
        public string Tile_Src_Image;
        public string Sound_Effect;
        public string Prefab;
        public int Amount;
        public bool Stackable;
        public float SellPrice;
        public string Desc;
    }

    // PURELY FOR TESTING REASONS
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "LoadSaveScene")
        {
            LoadXML();
        }
    }

    public void SaveToXML()
    {
        Slots = new int[3];
        Slots[0] = Inventory.MaxCapacity;
        Slots[1] = HotBar.MaxCapacity;
        Slots[2] = Chest.MaxCapacity;
        PlayerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Vector3Int Date = new Vector3Int(cClock.Day, cClock.Month, cClock.Year);
        Vector3Int Time = new Vector3Int(cClock.Hour, cClock.Min, (int)cClock.TimeTimer);
        float[] lighting = new float[4];
        lighting[0] = cClock.Lighting.color.r; lighting[1] = cClock.Lighting.color.g; lighting[2] = cClock.Lighting.color.b; lighting[3] = cClock.Lighting.color.a;
        Vector3 Pos = new Vector3(PlayerPos.position.x, PlayerPos.position.y, PlayerPos.position.z);
        Resets = cClock.WeeklyReset;
        SaveData Data = new SaveData(Date, Pos, Time, lighting, Resets, Slots, Money.GetMoney(), Stamania.GetStamina(), Inventory.ItemList,
                                    HotBar.ItemList, Chest.ItemList, Database.TileMapData);
        WriteXML(Data);
    }

    public ItemBase.ItemTypes FindType(string Type)
    {
        if (Type == "Tool")
        {
            return ItemBase.ItemTypes.Tool;
        }
        else if (Type == "Wood")
        {
            return ItemBase.ItemTypes.Wood;
        }
        else if (Type == "Plant")
        {
            return ItemBase.ItemTypes.Plant;
        }
        else if (Type == "Seed")
        {
            return ItemBase.ItemTypes.Seed;
        }
        else if (Type == "Ore")
        {
            return ItemBase.ItemTypes.Ore;
        }
        else if (Type == "Decoration")
        {
            return ItemBase.ItemTypes.Decoration;
        }
        else if (Type == "Fish")
        {
            return ItemBase.ItemTypes.Fish;
        }
        return ItemBase.ItemTypes.Decoration;
    }

    private void WriteXML(SaveData Data)
    {
        XmlDocument SavedDataXML = new XmlDocument();
        XmlElement root = SavedDataXML.CreateElement("SaveData");
        root.SetAttribute("SaveData", "Data");

        #region CreateXML PlayerData

        XmlElement PlayerData = SavedDataXML.CreateElement("PlayerData");

        XmlElement PaidTaxesElement = SavedDataXML.CreateElement("PaidTaxes");
        PaidTaxesElement.SetAttribute("PaidTaxes", GameObject.FindGameObjectWithTag("Player").GetComponent<MainMissionManagement>().PaidTaxes.ToString());
        PlayerData.AppendChild(PaidTaxesElement);

        XmlElement MoneyElement = SavedDataXML.CreateElement("MoneyNumber");
        MoneyElement.SetAttribute("Money", Data.PlayerGold.ToString());
        PlayerData.AppendChild(MoneyElement);

        XmlElement EnergyElement = SavedDataXML.CreateElement("PlayerEnergy");
        EnergyElement.SetAttribute("Energy", Data.PlayerEnergy.ToString());
        PlayerData.AppendChild(EnergyElement);

        XmlElement CurrentScene = SavedDataXML.CreateElement("CurrentScene");
        CurrentScene.SetAttribute("Scene", SceneManager.GetActiveScene().name);
        PlayerData.AppendChild(CurrentScene);

        Transform transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        XmlElement PlayerPos = SavedDataXML.CreateElement("PlayerPos");
        PlayerPos.SetAttribute("x", transform.position.x.ToString());
        PlayerPos.SetAttribute("y", transform.position.y.ToString());
        PlayerPos.SetAttribute("z", transform.position.z.ToString());
        PlayerData.AppendChild(PlayerPos);

        root.AppendChild(PlayerData);
        #endregion

        #region CreateXML WorldData

        XmlElement WorldData = SavedDataXML.CreateElement("WorldData");

        XmlElement Date = SavedDataXML.CreateElement("Date");
        Date.SetAttribute("Day", Data.Date[0].ToString());
        Date.SetAttribute("Month", Data.Date[1].ToString());
        Date.SetAttribute("Year", Data.Date[2].ToString());
        WorldData.AppendChild(Date);

        XmlElement Time = SavedDataXML.CreateElement("Time");
        Time.SetAttribute("Hour", Data.Time[0].ToString());
        Time.SetAttribute("Min", Data.Time[1].ToString());
        Time.SetAttribute("Seconds", Data.Time[2].ToString());
        WorldData.AppendChild(Time);

        XmlElement Lighting = SavedDataXML.CreateElement("Lighting");
        Lighting.SetAttribute("Lighting.r", Data.Lighting[0].ToString());
        Lighting.SetAttribute("Lighting.g", Data.Lighting[1].ToString());
        Lighting.SetAttribute("Lighting.b", Data.Lighting[2].ToString());
        Lighting.SetAttribute("Lighting.a", Data.Lighting[3].ToString());
        WorldData.AppendChild(Lighting);

        XmlElement WeeklyReset = SavedDataXML.CreateElement("WeeklyReset");
        WeeklyReset.SetAttribute("Resets" + 0, Data.WeeklyResets[0].ToString());
        WeeklyReset.SetAttribute("Resets" + 1, Data.WeeklyResets[1].ToString());
        WeeklyReset.SetAttribute("Resets" + 2, Data.WeeklyResets[2].ToString());
        WeeklyReset.SetAttribute("Resets" + 3, Data.WeeklyResets[3].ToString());
        WorldData.AppendChild(WeeklyReset);

        XmlElement slots = SavedDataXML.CreateElement("Slots");
        slots.SetAttribute("Slots" + 0, Data.InventorySlotAmount[0].ToString());
        slots.SetAttribute("Slots" + 1, Data.InventorySlotAmount[1].ToString());
        slots.SetAttribute("Slots" + 2, Data.InventorySlotAmount[2].ToString());
        WorldData.AppendChild(slots);

        root.AppendChild(WorldData);
        #endregion

        #region CreateXML InventoryData

        {
            XmlElement ElementData, NameElement, TypeElement, ImageElement, TileImageElement, SoundEffectElement, PrefabElement, AmountElement, StackableElement, PriceElement, DescElement;
            XmlElement InventoryData = SavedDataXML.CreateElement("InventoryData");
            for (int i = 0; i < Data.InventorySlotAmount[0]; i++)
            {
                if (Data.Inventory[i].Name != null)
                {
                    ElementData = SavedDataXML.CreateElement("ElementData");
                    NameElement = SavedDataXML.CreateElement("NameElement");
                    TypeElement = SavedDataXML.CreateElement("TypeElement");
                    ImageElement = SavedDataXML.CreateElement("ImageElement");
                    TileImageElement = SavedDataXML.CreateElement("TileImageElement");
                    SoundEffectElement = SavedDataXML.CreateElement("SoundEffectElement");
                    PrefabElement = SavedDataXML.CreateElement("PrefabElement");
                    AmountElement = SavedDataXML.CreateElement("AmountElement");
                    StackableElement = SavedDataXML.CreateElement("StackableElement");
                    PriceElement = SavedDataXML.CreateElement("PriceElement");
                    DescElement = SavedDataXML.CreateElement("DescElement");


                    NameElement.SetAttribute("Name", Data.Inventory[i].Name);
                    TypeElement.SetAttribute("Type", Data.Inventory[i].Type);
                    ImageElement.SetAttribute("Image", Data.Inventory[i].Src_Image);
                    TileImageElement.SetAttribute("Tile", Data.Inventory[i].Tile_Src_Image);
                    SoundEffectElement.SetAttribute("SoundEffect", Data.Inventory[i].Sound_Effect);
                    PrefabElement.SetAttribute("Prefab", Data.Inventory[i].Prefab);
                    AmountElement.SetAttribute("Amount", Data.Inventory[i].Amount.ToString());
                    StackableElement.SetAttribute("Stackable", Data.Inventory[i].Stackable.ToString());
                    PriceElement.SetAttribute("Price", Data.Inventory[i].SellPrice.ToString());
                    DescElement.SetAttribute("Desc", Data.Inventory[i].Desc);

                    ElementData.AppendChild(NameElement);
                    ElementData.AppendChild(TypeElement);
                    ElementData.AppendChild(ImageElement);
                    ElementData.AppendChild(TileImageElement);
                    ElementData.AppendChild(SoundEffectElement);
                    ElementData.AppendChild(PrefabElement);
                    ElementData.AppendChild(AmountElement);
                    ElementData.AppendChild(StackableElement);
                    ElementData.AppendChild(PriceElement);
                    ElementData.AppendChild(DescElement);

                    InventoryData.AppendChild(ElementData);
                }
            }

            root.AppendChild(InventoryData);
        }



        #endregion

        #region CreateXML HotbarData

        {
            XmlElement ElementData, NameElement, TypeElement, ImageElement, TileImageElement, SoundEffectElement, PrefabElement, AmountElement, StackableElement, PriceElement, DescElement;
            XmlElement HotbarData = SavedDataXML.CreateElement("HotbarData");
            for (int i = 0; i < Data.InventorySlotAmount[1]; i++)
            {
                if (Data.HotBar[i].Name != null)
                {
                    ElementData = SavedDataXML.CreateElement("ElementData");
                    NameElement = SavedDataXML.CreateElement("NameElement");
                    TypeElement = SavedDataXML.CreateElement("TypeElement");
                    ImageElement = SavedDataXML.CreateElement("ImageElement");
                    TileImageElement = SavedDataXML.CreateElement("TileImageElement");
                    SoundEffectElement = SavedDataXML.CreateElement("SoundEffectElement");
                    PrefabElement = SavedDataXML.CreateElement("PrefabElement");
                    AmountElement = SavedDataXML.CreateElement("AmountElement");
                    StackableElement = SavedDataXML.CreateElement("StackableElement");
                    PriceElement = SavedDataXML.CreateElement("PriceElement");
                    DescElement = SavedDataXML.CreateElement("DescElement");

                    NameElement.SetAttribute("Name", Data.HotBar[i].Name);
                    TypeElement.SetAttribute("Type", Data.HotBar[i].Type);
                    ImageElement.SetAttribute("Image", Data.HotBar[i].Src_Image);
                    TileImageElement.SetAttribute("Tile", Data.HotBar[i].Tile_Src_Image);
                    SoundEffectElement.SetAttribute("SoundEffect", Data.HotBar[i].Sound_Effect);
                    PrefabElement.SetAttribute("Prefab", Data.HotBar[i].Prefab);
                    AmountElement.SetAttribute("Amount", Data.HotBar[i].Amount.ToString());
                    StackableElement.SetAttribute("Stackable", Data.HotBar[i].Stackable.ToString());
                    PriceElement.SetAttribute("Price", Data.HotBar[i].SellPrice.ToString());
                    DescElement.SetAttribute("Desc", Data.HotBar[i].Desc);

                    ElementData.AppendChild(NameElement);
                    ElementData.AppendChild(TypeElement);
                    ElementData.AppendChild(ImageElement);
                    ElementData.AppendChild(TileImageElement);
                    ElementData.AppendChild(SoundEffectElement);
                    ElementData.AppendChild(PrefabElement);
                    ElementData.AppendChild(AmountElement);
                    ElementData.AppendChild(StackableElement);
                    ElementData.AppendChild(PriceElement);
                    ElementData.AppendChild(DescElement);

                    HotbarData.AppendChild(ElementData);
                }
            }

            root.AppendChild(HotbarData);
        }
        #endregion

        #region CreateXML ChestData

        {
            XmlElement ElementData, NameElement, TypeElement, ImageElement, TileImageElement, SoundEffectElement, PrefabElement, AmountElement, StackableElement, PriceElement, DescElement;
            XmlElement ChestData = SavedDataXML.CreateElement("ChestData");
            for (int i = 0; i < Data.InventorySlotAmount[2]; i++)
            {
                if (Data.Chest[i].Name != null)
                {
                    ElementData = SavedDataXML.CreateElement("ElementData");
                    NameElement = SavedDataXML.CreateElement("NameElement");
                    TypeElement = SavedDataXML.CreateElement("TypeElement");
                    ImageElement = SavedDataXML.CreateElement("ImageElement");
                    TileImageElement = SavedDataXML.CreateElement("TileImageElement");
                    SoundEffectElement = SavedDataXML.CreateElement("SoundEffectElement");
                    PrefabElement = SavedDataXML.CreateElement("PrefabElement");
                    AmountElement = SavedDataXML.CreateElement("AmountElement");
                    StackableElement = SavedDataXML.CreateElement("StackableElement");
                    PriceElement = SavedDataXML.CreateElement("PriceElement");
                    DescElement = SavedDataXML.CreateElement("DescElement");

                    NameElement.SetAttribute("Name", Data.Chest[i].Name);
                    TypeElement.SetAttribute("Type", Data.Chest[i].Type);
                    ImageElement.SetAttribute("Image", Data.Chest[i].Src_Image);
                    TileImageElement.SetAttribute("Tile", Data.Chest[i].Tile_Src_Image);
                    SoundEffectElement.SetAttribute("SoundEffect", Data.Chest[i].Sound_Effect);
                    PrefabElement.SetAttribute("Prefab", Data.Chest[i].Prefab);
                    AmountElement.SetAttribute("Amount", Data.Chest[i].Amount.ToString());
                    StackableElement.SetAttribute("Stackable", Data.Chest[i].Stackable.ToString());
                    PriceElement.SetAttribute("Price", Data.Chest[i].SellPrice.ToString());
                    DescElement.SetAttribute("Desc", Data.Chest[i].Desc);

                    ElementData.AppendChild(NameElement);
                    ElementData.AppendChild(TypeElement);
                    ElementData.AppendChild(ImageElement);
                    ElementData.AppendChild(TileImageElement);
                    ElementData.AppendChild(SoundEffectElement);
                    ElementData.AppendChild(PrefabElement);
                    ElementData.AppendChild(AmountElement);
                    ElementData.AppendChild(StackableElement);
                    ElementData.AppendChild(PriceElement);
                    ElementData.AppendChild(DescElement);

                    ChestData.AppendChild(ElementData);
                }
            }

            root.AppendChild(ChestData);
        }
        #endregion

        #region CreateXML FarmData

        {
            XmlElement CropData, TypeElement, AmountElement, TileElement, CurrentDaysElement, GrowthTimElement, SpritesElement, IdElement, PosElement, HarvesElement, GrowthElement, XmlNamElement, WateredElement;
            XmlElement FarmData = SavedDataXML.CreateElement("FarmData");

            for (int i = 0; i < Data.Farm.Length; i++)
            {
                if (Data.Farm[i].ItemType != null)
                {
                    CropData = SavedDataXML.CreateElement("CropData");
                    TypeElement = SavedDataXML.CreateElement("TypeElement");
                    AmountElement = SavedDataXML.CreateElement("AmountElement");
                    CurrentDaysElement = SavedDataXML.CreateElement("CurrentDaysElement");
                    GrowthTimElement = SavedDataXML.CreateElement("GrowthTimElement");
                    SpritesElement = SavedDataXML.CreateElement("SpritesElement");
                    IdElement = SavedDataXML.CreateElement("IdElement");
                    PosElement = SavedDataXML.CreateElement("PosElement");
                    TileElement = SavedDataXML.CreateElement("TileElement");
                    HarvesElement = SavedDataXML.CreateElement("HarvesElement");
                    GrowthElement = SavedDataXML.CreateElement("GrowthElement");
                    XmlNamElement = SavedDataXML.CreateElement("XmlNamElement");
                    WateredElement = SavedDataXML.CreateElement("WateredElement");

                    TypeElement.SetAttribute("Type", Data.Farm[i].ItemType);
                    AmountElement.SetAttribute("Amount", Data.Farm[i].Amount.ToString());
                    CurrentDaysElement.SetAttribute("CurrentDays", Data.Farm[i].CurrentDays.ToString());
                    GrowthTimElement.SetAttribute("GrowthTime", Data.Farm[i].GrowthTime.ToString());
                    for (int j = 0; j < 3; j++)
                    {
                        SpritesElement.SetAttribute("Sprite" + j, Data.Farm[i].GrowthSprite[j]);
                    }
                    string[] XYZ = { "X", "Y", "Z" };
                    for (int j = 0; j < 3; j++)
                    {
                        IdElement.SetAttribute(XYZ[j].ToString(), Data.Farm[i].ID[j].ToString());
                    }


                    for (int j = 0; j < 3; j++)
                    {
                        PosElement.SetAttribute(XYZ[j].ToString(), Data.Farm[i].Pos[j].ToString());
                    }
                    TileElement.SetAttribute("Tile", Data.Farm[i].Tile);
                    HarvesElement.SetAttribute("Harvestable", Data.Farm[i].Harvestable.ToString());
                    GrowthElement.SetAttribute("HasGrown", Data.Farm[i].Growth.ToString());
                    XmlNamElement.SetAttribute("XMLName", Data.Farm[i].XMLName);
                    WateredElement.SetAttribute("Watered", Data.Farm[i].Watered.ToString());

                    CropData.AppendChild(TypeElement);
                    CropData.AppendChild(AmountElement);
                    CropData.AppendChild(CurrentDaysElement);
                    CropData.AppendChild(GrowthTimElement);
                    CropData.AppendChild(SpritesElement);
                    CropData.AppendChild(IdElement);
                    CropData.AppendChild(PosElement);
                    CropData.AppendChild(TileElement);
                    CropData.AppendChild(HarvesElement);
                    CropData.AppendChild(GrowthElement);
                    CropData.AppendChild(XmlNamElement);
                    CropData.AppendChild(WateredElement);

                    FarmData.AppendChild(CropData);
                }
            }

            root.AppendChild(FarmData);
        }

        #endregion

        #region CreateXML MinesData

        #region CreateXML Mines0Data

        {
            XmlElement RockData, TypeElement, OreElement, IdElement, PosElement, TileElement, XmlNameElement, AmountElement;
            XmlElement MinesData = SavedDataXML.CreateElement("Mines0Data");

            for (int i = 0; i < Data.Mines.Length; i++)
            {
                RockData = SavedDataXML.CreateElement("RockData");
                TypeElement = SavedDataXML.CreateElement("TypElement");
                OreElement = SavedDataXML.CreateElement("OreElement");
                IdElement = SavedDataXML.CreateElement("IdElement");
                PosElement = SavedDataXML.CreateElement("PosElement");
                TileElement = SavedDataXML.CreateElement("TileElement");
                XmlNameElement = SavedDataXML.CreateElement("XmlElement");
                AmountElement = SavedDataXML.CreateElement("AmountElement");

                TypeElement.SetAttribute("Type", Data.Mines[i].ItemType);
                OreElement.SetAttribute("Ore", Data.Mines[i].OreName);

                string[] XYZ = { "X", "Y", "Z" };
                for (int j = 0; j < 3; j++)
                {
                    IdElement.SetAttribute(XYZ[j].ToString(), Data.Mines[i].ID[j].ToString());
                }


                for (int j = 0; j < 3; j++)
                {
                    PosElement.SetAttribute(XYZ[j].ToString(), Data.Mines[i].Pos[j].ToString());
                }

                TileElement.SetAttribute("Tile", Data.Mines[i].Tile);

                XmlNameElement.SetAttribute("XMLName", Data.Mines[i].XMLName);
                AmountElement.SetAttribute("Amount", Data.Mines[i].Amount.ToString());


                RockData.AppendChild(TypeElement);
                RockData.AppendChild(OreElement);
                RockData.AppendChild(IdElement);
                RockData.AppendChild(PosElement);
                RockData.AppendChild(TileElement);
                RockData.AppendChild(XmlNameElement);
                RockData.AppendChild(AmountElement);

                MinesData.AppendChild(RockData);
            }

            root.AppendChild(MinesData);
        }

        #endregion

        #region CreateXML Mines1Data

        {
            XmlElement RockData, TypeElement, OreElement, IdElement, PosElement, TileElement, XmlNameElement, AmountElement;
            XmlElement MinesData = SavedDataXML.CreateElement("Mines1Data");

            for (int i = 0; i < Data.Mines1.Length; i++)
            {
                RockData = SavedDataXML.CreateElement("RockData");
                TypeElement = SavedDataXML.CreateElement("TypElement");
                OreElement = SavedDataXML.CreateElement("OreElement");
                IdElement = SavedDataXML.CreateElement("IdElement");
                PosElement = SavedDataXML.CreateElement("PosElement");
                TileElement = SavedDataXML.CreateElement("TileElement");
                XmlNameElement = SavedDataXML.CreateElement("XmlElement");
                AmountElement = SavedDataXML.CreateElement("AmountElement");

                TypeElement.SetAttribute("Type", Data.Mines1[i].ItemType);
                OreElement.SetAttribute("Ore", Data.Mines1[i].OreName);

                string[] XYZ = { "X", "Y", "Z" };
                for (int j = 0; j < 3; j++)
                {
                    IdElement.SetAttribute(XYZ[j].ToString(), Data.Mines1[i].ID[j].ToString());
                }


                for (int j = 0; j < 3; j++)
                {
                    PosElement.SetAttribute(XYZ[j].ToString(), Data.Mines1[i].Pos[j].ToString());
                }

                TileElement.SetAttribute("Tile", Data.Mines1[i].Tile);

                XmlNameElement.SetAttribute("XMLName", Data.Mines1[i].XMLName);
                AmountElement.SetAttribute("Amount", Data.Mines1[i].Amount.ToString());


                RockData.AppendChild(TypeElement);
                RockData.AppendChild(OreElement);
                RockData.AppendChild(IdElement);
                RockData.AppendChild(PosElement);
                RockData.AppendChild(TileElement);
                RockData.AppendChild(XmlNameElement);
                RockData.AppendChild(AmountElement);

                MinesData.AppendChild(RockData);
            }

            root.AppendChild(MinesData);
        }

        #endregion

        #region CreateXML Mines2Data

        {
            XmlElement RockData, TypeElement, OreElement, IdElement, PosElement, TileElement, XmlNameElement, AmountElement;
            XmlElement MinesData = SavedDataXML.CreateElement("Mines2Data");

            for (int i = 0; i < Data.Mines2.Length; i++)
            {
                RockData = SavedDataXML.CreateElement("RockData");
                TypeElement = SavedDataXML.CreateElement("TypElement");
                OreElement = SavedDataXML.CreateElement("OreElement");
                IdElement = SavedDataXML.CreateElement("IdElement");
                PosElement = SavedDataXML.CreateElement("PosElement");
                TileElement = SavedDataXML.CreateElement("TileElement");
                XmlNameElement = SavedDataXML.CreateElement("XmlElement");
                AmountElement = SavedDataXML.CreateElement("AmountElement");

                TypeElement.SetAttribute("Type", Data.Mines2[i].ItemType);
                OreElement.SetAttribute("Ore", Data.Mines2[i].OreName);

                string[] XYZ = { "X", "Y", "Z" };
                for (int j = 0; j < 3; j++)
                {
                    IdElement.SetAttribute(XYZ[j].ToString(), Data.Mines2[i].ID[j].ToString());
                }


                for (int j = 0; j < 3; j++)
                {
                    PosElement.SetAttribute(XYZ[j].ToString(), Data.Mines2[i].Pos[j].ToString());
                }

                TileElement.SetAttribute("Tile", Data.Mines2[i].Tile);

                XmlNameElement.SetAttribute("XMLName", Data.Mines2[i].XMLName);
                AmountElement.SetAttribute("Amount", Data.Mines2[i].Amount.ToString());


                RockData.AppendChild(TypeElement);
                RockData.AppendChild(OreElement);
                RockData.AppendChild(IdElement);
                RockData.AppendChild(PosElement);
                RockData.AppendChild(TileElement);
                RockData.AppendChild(XmlNameElement);
                RockData.AppendChild(AmountElement);

                MinesData.AppendChild(RockData);
            }

            root.AppendChild(MinesData);
        }

        #endregion

        #endregion

        #region CreateXML ForestData

        {
            XmlElement TreeData, TypeElement, TreeElement, IdElement, PosElement, TileElement, XmlNameElement, AmountElement;
            XmlElement MinesData = SavedDataXML.CreateElement("ForestData");

            for (int i = 0; i < Data.Forest.Length; i++)
            {
                TreeData = SavedDataXML.CreateElement("TreeData");
                TypeElement = SavedDataXML.CreateElement("TypElement");
                TreeElement = SavedDataXML.CreateElement("TreeElement");
                IdElement = SavedDataXML.CreateElement("IdElement");
                PosElement = SavedDataXML.CreateElement("PosElement");
                TileElement = SavedDataXML.CreateElement("TileElement");
                XmlNameElement = SavedDataXML.CreateElement("XmlElement");
                AmountElement = SavedDataXML.CreateElement("AmountElement");

                TypeElement.SetAttribute("Type", Data.Forest[i].ItemType);
                TreeElement.SetAttribute("Tree", Data.Forest[i].TreeName);

                string[] XYZ = { "X", "Y", "Z" };
                for (int j = 0; j < 3; j++)
                {
                    IdElement.SetAttribute(XYZ[j].ToString(), Data.Forest[i].ID[j].ToString());
                }


                for (int j = 0; j < 3; j++)
                {
                    PosElement.SetAttribute(XYZ[j].ToString(), Data.Forest[i].Pos[j].ToString());
                }

                TileElement.SetAttribute("Tile", Data.Forest[i].Tile);

                XmlNameElement.SetAttribute("XMLName", Data.Forest[i].XMLName);
                AmountElement.SetAttribute("Amount", Data.Forest[i].Amount.ToString());


                TreeData.AppendChild(TypeElement);
                TreeData.AppendChild(TreeElement);
                TreeData.AppendChild(IdElement);
                TreeData.AppendChild(PosElement);
                TreeData.AppendChild(TileElement);
                TreeData.AppendChild(XmlNameElement);
                TreeData.AppendChild(AmountElement);

                MinesData.AppendChild(TreeData);
            }

            root.AppendChild(MinesData);
        }

        #endregion

        SavedDataXML.AppendChild(root);
        string Path = (Application.dataPath + "/StreamingAssets/XML Files/") + "DataXML.txt";
        SavedDataXML.Save(Path);
        if (File.Exists(Application.dataPath + "/StreamingAssets/XML Files/" + "DataXML.txt"))
        {
            Debug.Log(("XML FILE SAVED"));
        }
    }

    public void LoadXML()
    {
        XmlDocument XMLDoc = new XmlDocument();
        XMLDoc.Load(Application.dataPath + "\\StreamingAssets\\XML Files\\DataXML.txt");

        //XmlNode node = XMLDoc.DocumentElement.SelectSingleNode("SaveData");

        foreach (XmlNode currentnode in XMLDoc.DocumentElement.ChildNodes)
        {
            /*Little side note, wanna redo these two but don't have time before the demo day*/
            if (currentnode.Name == "PlayerData")
            {
                
                XmlNodeList lst = currentnode.ChildNodes;
                bool Taxesbool = Convert.ToBoolean(lst[0].Attributes.GetNamedItem("PaidTaxes").Value);
                GameObject.FindGameObjectWithTag("Player").GetComponent<MainMissionManagement>().UpdateFromXML(Taxesbool);
                string MoneyText = lst[1].Attributes.GetNamedItem("Money").Value;
                Money.SetMoney(float.Parse(MoneyText));
                string EnergyText = lst[2].Attributes.GetNamedItem("Energy").Value;
                Stamania.SetStamina(int.Parse(EnergyText));
                cClock.SceneName = lst[3].Attributes.GetNamedItem("Scene").Value;
                cClock.PlayerPos.x = float.Parse(lst[4].Attributes.GetNamedItem("x").Value);
                cClock.PlayerPos.y = float.Parse(lst[4].Attributes.GetNamedItem("y").Value);
                cClock.PlayerPos.z = float.Parse(lst[4].Attributes.GetNamedItem("z").Value);
            }
            else if (currentnode.Name == "WorldData")
            {
                XmlNodeList lst = currentnode.ChildNodes;
                string[] Resets = new string[4];
                string[] Slots = new string[3];
                float[] lighting = new float[4];
                int[] Date = new int[3];
                int[] Time = new int[3];
                Date[0] = int.Parse(lst[0].Attributes.GetNamedItem("Day").Value);
                Date[1] = int.Parse(lst[0].Attributes.GetNamedItem("Month").Value);
                Date[2] = int.Parse(lst[0].Attributes.GetNamedItem("Year").Value);
                Time[0] = int.Parse(lst[1].Attributes.GetNamedItem("Hour").Value);
                Time[1] = int.Parse(lst[1].Attributes.GetNamedItem("Min").Value);
                Time[2] = int.Parse(lst[1].Attributes.GetNamedItem("Seconds").Value);
                lighting[0] = float.Parse(lst[2].Attributes.GetNamedItem("Lighting.r").Value);
                lighting[1] = float.Parse(lst[2].Attributes.GetNamedItem("Lighting.g").Value);
                lighting[2] = float.Parse(lst[2].Attributes.GetNamedItem("Lighting.b").Value);
                lighting[3] = float.Parse(lst[2].Attributes.GetNamedItem("Lighting.a").Value);
                Resets[0] = lst[3].Attributes.GetNamedItem("Resets0").Value;
                Resets[1] = lst[3].Attributes.GetNamedItem("Resets1").Value;
                Resets[2] = lst[3].Attributes.GetNamedItem("Resets2").Value;
                Resets[3] = lst[3].Attributes.GetNamedItem("Resets3").Value;
                Slots[0] = lst[4].Attributes.GetNamedItem("Slots0").Value;
                Slots[1] = lst[4].Attributes.GetNamedItem("Slots1").Value;
                Slots[2] = lst[4].Attributes.GetNamedItem("Slots2").Value;
                Color color = new Color(lighting[0], lighting[1], lighting[2], lighting[3]);
                cClock.Day = Date[0]; cClock.Month = Date[1]; cClock.Year = Date[2];
                cClock.Hour = Time[0]; cClock.Min = Time[1]; cClock.TimeTimer = Time[2];
                cClock.Lighting.color = color;
                for (int i = 0; i < 4; i++)
                {
                    cClock.WeeklyReset[i] = bool.Parse(Resets[i]);
                }
                Inventory.Resize(Convert.ToInt32(Slots[0]));
                HotBar.Resize(Convert.ToInt32(Slots[1]));
                Chest.Resize(Convert.ToInt32(Slots[2]));
            }
            /************************************************************************/
            else if (currentnode.Name == "InventoryData")
            {
                ItemTypeFinder TypeFinder = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemTypeFinder>();
                ItemData Item;
                XmlNodeList lst = currentnode.ChildNodes;
                foreach (XmlNode Node in lst)
                {
                    XmlNodeList lst0 = Node.ChildNodes;
                    Item.Name = lst0[0].Attributes.GetNamedItem("Name").Value;
                    Item.Type = lst0[1].Attributes.GetNamedItem("Type").Value;
                    Item.Src_Image = lst0[2].Attributes.GetNamedItem("Image").Value;
                    Item.Tile_Src_Image = lst0[3].Attributes.GetNamedItem("Tile").Value;
                    Item.Sound_Effect = lst0[4].Attributes.GetNamedItem("SoundEffect").Value;
                    Item.Prefab = lst0[5].Attributes.GetNamedItem("Prefab").Value;
                    Item.Amount = Convert.ToInt32(lst0[6].Attributes.GetNamedItem("Amount").Value);
                    Item.Stackable = Convert.ToBoolean(lst0[7].Attributes.GetNamedItem("Stackable").Value);
                    Item.SellPrice = float.Parse(lst0[8].Attributes.GetNamedItem("Price").Value);
                    Item.Desc = lst0[9].Attributes.GetNamedItem("Desc").Value;
                    ItemBase BasicItem = new ItemBase();
                    // Get the object the component will be on and give it a name
                    GameObject SubGameObject = new GameObject(Item.Name);
                    SubGameObject.transform.parent = ToolItems.transform;
                    // Find the type of the item and set it up, after add it to the dictionary.

                    // Loads what tile we want the item to use
                    string Path = "XML Loaded Assets/" + Item.Tile_Src_Image;
                    TileBase Tile = Resources.Load<TileBase>(Path);
                    // Load a given prefab
                    Path = "XML Loaded Assets/" + Item.Prefab;
                    var Prefab = Resources.Load(Path);
                    GameObject Pre = (GameObject)Prefab;
                    // Load the sound effect we want to use
                    Path = "XML Loaded Assets/" + Item.Sound_Effect;
                    AudioClip Audio = Resources.Load<AudioClip>(Path);

                    BasicItem = TypeFinder.ItemByTyepFinder(Item.Name, Item.Type, SubGameObject);
                    BasicItem.SetUpThisItem(FindType(Item.Type), Item.Name, Item.Amount, Item.Stackable, Item.Src_Image, Audio,
                        Tile, Pre, Item.SellPrice, 0, Item.Desc);
                    Inventory.AddItem(BasicItem);
                }
            }
            else if (currentnode.Name == "HotbarData")
            {
                ItemTypeFinder TypeFinder = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemTypeFinder>();
                ItemData Item;
                XmlNodeList lst = currentnode.ChildNodes;
                foreach (XmlNode Node in lst)
                {
                    XmlNodeList lst0 = Node.ChildNodes;
                    Item.Name = lst0[0].Attributes.GetNamedItem("Name").Value;
                    Item.Type = lst0[1].Attributes.GetNamedItem("Type").Value;
                    Item.Src_Image = lst0[2].Attributes.GetNamedItem("Image").Value;
                    Item.Tile_Src_Image = lst0[3].Attributes.GetNamedItem("Tile").Value;
                    Item.Sound_Effect = lst0[4].Attributes.GetNamedItem("SoundEffect").Value;
                    Item.Prefab = lst0[5].Attributes.GetNamedItem("Prefab").Value;
                    Item.Amount = Convert.ToInt32(lst0[6].Attributes.GetNamedItem("Amount").Value);
                    Item.Stackable = Convert.ToBoolean(lst0[7].Attributes.GetNamedItem("Stackable").Value);
                    Item.SellPrice = float.Parse(lst0[8].Attributes.GetNamedItem("Price").Value);
                    Item.Desc = lst0[9].Attributes.GetNamedItem("Desc").Value;
                    ItemBase BasicItem = new ItemBase();
                    // Get the object the component will be on and give it a name
                    GameObject SubGameObject = new GameObject(Item.Name);
                    SubGameObject.transform.parent = ToolItems.transform;
                    // Find the type of the item and set it up, after add it to the dictionary.

                    // Loads what tile we want the item to use
                    string Path = "XML Loaded Assets/" + Item.Tile_Src_Image;
                    TileBase Tile = Resources.Load<TileBase>(Path);
                    // Load a given prefab
                    Path = "XML Loaded Assets/" + Item.Prefab;
                    var Prefab = Resources.Load(Path);
                    GameObject Pre = (GameObject)Prefab;
                    // Load the sound effect we want to use
                    Path = "XML Loaded Assets/" + Item.Sound_Effect;
                    AudioClip Audio = Resources.Load<AudioClip>(Path);

                    BasicItem = TypeFinder.ItemByTyepFinder(Item.Name, Item.Type, SubGameObject);
                    BasicItem.SetUpThisItem(FindType(Item.Type), Item.Name, Item.Amount, Item.Stackable, Item.Src_Image, Audio,
                        Tile, Pre, Item.SellPrice, 0, Item.Desc);
                    HotBar.AddItem(BasicItem);
                }
            }
            else if (currentnode.Name == "ChestData")
            {
                ItemTypeFinder TypeFinder = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemTypeFinder>();
                ItemData Item;
                XmlNodeList lst = currentnode.ChildNodes;
                foreach (XmlNode Node in lst)
                {
                    XmlNodeList lst0 = Node.ChildNodes;
                    Item.Name = lst0[0].Attributes.GetNamedItem("Name").Value;
                    Item.Type = lst0[1].Attributes.GetNamedItem("Type").Value;
                    Item.Src_Image = lst0[2].Attributes.GetNamedItem("Image").Value;
                    Item.Tile_Src_Image = lst0[3].Attributes.GetNamedItem("Tile").Value;
                    Item.Sound_Effect = lst0[4].Attributes.GetNamedItem("SoundEffect").Value;
                    Item.Prefab = lst0[5].Attributes.GetNamedItem("Prefab").Value;
                    Item.Amount = Convert.ToInt32(lst0[6].Attributes.GetNamedItem("Amount").Value);
                    Item.Stackable = Convert.ToBoolean(lst0[7].Attributes.GetNamedItem("Stackable").Value);
                    Item.SellPrice = float.Parse(lst0[8].Attributes.GetNamedItem("Price").Value);
                    Item.Desc = lst0[9].Attributes.GetNamedItem("Desc").Value;
                    ItemBase BasicItem = new ItemBase();
                    // Get the object the component will be on and give it a name
                    GameObject SubGameObject = new GameObject(Item.Name);
                    SubGameObject.transform.parent = ToolItems.transform;
                    // Find the type of the item and set it up, after add it to the dictionary.

                    // Loads what tile we want the item to use
                    string Path = "XML Loaded Assets/" + Item.Tile_Src_Image;
                    TileBase Tile = Resources.Load<TileBase>(Path);
                    // Load a given prefab
                    Path = "XML Loaded Assets/" + Item.Prefab;
                    var Prefab = Resources.Load(Path);
                    GameObject Pre = (GameObject)Prefab;
                    // Load the sound effect we want to use
                    Path = "XML Loaded Assets/" + Item.Sound_Effect;
                    AudioClip Audio = Resources.Load<AudioClip>(Path);

                    BasicItem = TypeFinder.ItemByTyepFinder(Item.Name, Item.Type, SubGameObject);
                    BasicItem.SetUpThisItem(FindType(Item.Type), Item.Name, Item.Amount, Item.Stackable, Item.Src_Image, Audio,
                        Tile, Pre, Item.SellPrice, 0, Item.Desc);
                    Chest.AddItem(BasicItem);
                }
            }
            else if (currentnode.Name == "FarmData")
            {
                ItemTypeFinder TypeFinder = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemTypeFinder>();
                DOLDatabase DOLD = GameObject.FindGameObjectWithTag("LoadManager").GetComponent<DOLDatabase>();
                XMLParser XML = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<XMLParser>();
                PlantData Item = new PlantData();
                Item.SetUp();
                XmlNodeList lst = currentnode.ChildNodes;
                foreach (XmlNode Node in lst)
                {
                    XmlNodeList lst0 = Node.ChildNodes;
                    Item.ItemType = lst0[0].Attributes.GetNamedItem("Type").Value;
                    Item.Amount = Convert.ToInt32(lst0[1].Attributes.GetNamedItem("Amount").Value);
                    Item.CurrentDays = Convert.ToInt32(lst0[2].Attributes.GetNamedItem("CurrentDays").Value);
                    Item.GrowthTime = Convert.ToInt32(lst0[3].Attributes.GetNamedItem("GrowthTime").Value);
                    Item.GrowthSprite[0] = lst0[4].Attributes.GetNamedItem("Sprite0").Value;
                    Item.GrowthSprite[1] = lst0[4].Attributes.GetNamedItem("Sprite1").Value;
                    Item.GrowthSprite[2] = lst0[4].Attributes.GetNamedItem("Sprite2").Value;
                    Item.ID[0] = float.Parse(lst0[5].Attributes.GetNamedItem("X").Value);
                    Item.ID[1] = float.Parse(lst0[5].Attributes.GetNamedItem("Y").Value);
                    Item.ID[2] = float.Parse(lst0[5].Attributes.GetNamedItem("Z").Value);
                    Item.Pos[0] = float.Parse(lst0[6].Attributes.GetNamedItem("X").Value);
                    Item.Pos[1] = float.Parse(lst0[6].Attributes.GetNamedItem("Y").Value);
                    Item.Pos[2] = float.Parse(lst0[6].Attributes.GetNamedItem("Z").Value);
                    Item.Tile  = lst0[7].Attributes.GetNamedItem("Tile").Value;
                    Item.Harvestable = Convert.ToBoolean(lst0[8].Attributes.GetNamedItem("Harvestable").Value);
                    Item.Growth = Convert.ToBoolean(lst0[9].Attributes.GetNamedItem("HasGrown").Value);
                    Item.XMLName = lst0[10].Attributes.GetNamedItem("XMLName").Value;
                    Item.Watered = Convert.ToBoolean(lst0[11].Attributes.GetNamedItem("Watered").Value);

                    GameObject Clone;
                    int Index = TypeFinder.FindItemIndex(Item.XMLName + " Seeds");
                    Clone = Instantiate(XML.items.ElementAt(Index).Value.GetPrefab(), new Vector3(Item.Pos[0] + 0.5f, Item.Pos[1] + 0.5f, Item.Pos[2]), Quaternion.identity);
                    DontDestroyOnLoad(Clone);
                    DOLD.Add(Clone);
                    ///*****************************/
                    ///* Set up the plant for the clone on that spot in the database */
                    PlantAbstractClass TempPlant = Clone.GetComponent<PlantAbstractClass>();
                    TempPlant.ID = new Vector3Int(Convert.ToInt32(Item.ID[0]), Convert.ToInt32(Item.ID[1]), Convert.ToInt32(Item.ID[2]));
                    TempPlant.pos = new Vector3Int(Convert.ToInt32(Item.Pos[0]), Convert.ToInt32(Item.Pos[1]), Convert.ToInt32(Item.Pos[2]));
                    TempPlant.mHarvestable = Item.Harvestable;
                    TempPlant.mGrowth = Item.Growth;
                    TempPlant.mCurrentDays = Item.CurrentDays;
                    TempPlant.mGrowthTime = Item.GrowthTime;
                    TempPlant.mItemType = FindType(Item.ItemType);
                    TempPlant.SetAmount(Item.Amount);
                    TileDataClass Data = new TileDataClass();
                    Database.TileMapData.ElementAt(0).Value.Add(TempPlant.ID, Data);
                    Database.TileMapData.ElementAt(0).Value[TempPlant.ID].SetPlant(TempPlant);
                    string Path = "XML Loaded Assets/" + Item.Tile;
                    TileBase Tile = Resources.Load<TileBase>(Path);
                    Database.TileMapData.ElementAt(0).Value[TempPlant.ID].Tile = Tile;
                    Database.TileMapData.ElementAt(0).Value[TempPlant.ID].Clone = Clone;
                    Database.TileMapData.ElementAt(0).Value[TempPlant.ID].SetWatered(Item.Watered);
                }
            }
            else if (currentnode.Name == "Mines0Data")
            {
                ItemTypeFinder TypeFinder = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemTypeFinder>();
                XMLParser XML = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<XMLParser>();
                RockData Data = new RockData();
                Data.SetUp();
                XmlNodeList lst = currentnode.ChildNodes;
                foreach (XmlNode Node in lst)
                {
                    XmlNodeList lst0 = Node.ChildNodes;

                    Data.ItemType = lst0[0].Attributes.GetNamedItem("Type").Value;
                    Data.OreName = lst0[1].Attributes.GetNamedItem("Ore").Value;
                    Data.ID[0] = float.Parse(lst0[2].Attributes.GetNamedItem("X").Value);
                    Data.ID[0] = float.Parse(lst0[2].Attributes.GetNamedItem("Y").Value);
                    Data.ID[0] = float.Parse(lst0[2].Attributes.GetNamedItem("Z").Value);
                    Data.Pos[0] = float.Parse(lst0[3].Attributes.GetNamedItem("X").Value);
                    Data.Pos[1] = float.Parse(lst0[3].Attributes.GetNamedItem("Y").Value);
                    Data.Pos[2] = float.Parse(lst0[3].Attributes.GetNamedItem("Z").Value);
                    Data.Tile = lst0[4].Attributes.GetNamedItem("Tile").Value;
                    Data.XMLName = lst0[5].Attributes.GetNamedItem("XMLName").Value;
                    Data.Amount = float.Parse(lst0[6].Attributes.GetNamedItem("Amount").Value);


                    TileDataClass Temp = new TileDataClass();
                    Vector3Int posInt = new Vector3Int(Convert.ToInt32(Data.Pos[0]), Convert.ToInt32(Data.Pos[1]), Convert.ToInt32(Data.Pos[2]));
                    // Added the new slot
                    Database.TileMapData.ElementAt(1).Value.Add(posInt, Temp);
                    Ore Item = new Ore();
                    Item = this.gameObject.AddComponent<Ore>() as Ore;
                    // Sets up and ore item
                    int Index = TypeFinder.FindItemIndex(Data.XMLName);
                    Item.SetUpThisItem(FindType(Data.ItemType), Data.XMLName, (int)Data.Amount, XML.items.ElementAt(Index).Value.bStackable, 
                                       XML.items.ElementAt(Index).Value.bSrcImage, XML.items.ElementAt(Index).Value.bSoundEffect,
                                       XML.items.ElementAt(Index).Value.bTile, XML.items.ElementAt(Index).Value.bPrefab, 
                                       XML.items.ElementAt(Index).Value.bSellPrice, XML.items.ElementAt(Index).Value.bCustomData, XML.items.ElementAt(Index).Value.bDesc);

                    // Loads what tile we want the item to use
                    string Path = "XML Loaded Assets/" + Data.Tile;
                    TileBase Tile = Resources.Load<TileBase>(Path);

                    Database.TileMapData.ElementAt(1).Value[posInt].Tile = Tile;
                    Database.TileMapData.ElementAt(1).Value[posInt].SetOre(Item);
                }
            }
            else if (currentnode.Name == "Mines1Data")
            {
                ItemTypeFinder TypeFinder = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemTypeFinder>();
                XMLParser XML = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<XMLParser>();
                RockData Data = new RockData();
                Data.SetUp();
                XmlNodeList lst = currentnode.ChildNodes;
                foreach (XmlNode Node in lst)
                {
                    XmlNodeList lst0 = Node.ChildNodes;

                    Data.ItemType = lst0[0].Attributes.GetNamedItem("Type").Value;
                    Data.OreName = lst0[1].Attributes.GetNamedItem("Ore").Value;
                    Data.ID[0] = float.Parse(lst0[2].Attributes.GetNamedItem("X").Value);
                    Data.ID[0] = float.Parse(lst0[2].Attributes.GetNamedItem("Y").Value);
                    Data.ID[0] = float.Parse(lst0[2].Attributes.GetNamedItem("Z").Value);
                    Data.Pos[0] = float.Parse(lst0[3].Attributes.GetNamedItem("X").Value);
                    Data.Pos[1] = float.Parse(lst0[3].Attributes.GetNamedItem("Y").Value);
                    Data.Pos[2] = float.Parse(lst0[3].Attributes.GetNamedItem("Z").Value);
                    Data.Tile = lst0[4].Attributes.GetNamedItem("Tile").Value;
                    Data.XMLName = lst0[5].Attributes.GetNamedItem("XMLName").Value;
                    Data.Amount = float.Parse(lst0[6].Attributes.GetNamedItem("Amount").Value);


                    TileDataClass Temp = new TileDataClass();
                    Vector3Int posInt = new Vector3Int(Convert.ToInt32(Data.Pos[0]), Convert.ToInt32(Data.Pos[1]), Convert.ToInt32(Data.Pos[2]));
                    // Added the new slot
                    Database.TileMapData.ElementAt(2).Value.Add(posInt, Temp);
                    Ore Item = new Ore();
                    Item = this.gameObject.AddComponent<Ore>() as Ore;
                    // Sets up and ore item
                    int Index = TypeFinder.FindItemIndex(Data.XMLName);
                    Item.SetUpThisItem(FindType(Data.ItemType), Data.XMLName, (int)Data.Amount, XML.items.ElementAt(Index).Value.bStackable,
                                       XML.items.ElementAt(Index).Value.bSrcImage, XML.items.ElementAt(Index).Value.bSoundEffect,
                                       XML.items.ElementAt(Index).Value.bTile, XML.items.ElementAt(Index).Value.bPrefab,
                                       XML.items.ElementAt(Index).Value.bSellPrice, XML.items.ElementAt(Index).Value.bCustomData, XML.items.ElementAt(Index).Value.bDesc);
                    // Loads what tile we want the item to use
                    string Path = "XML Loaded Assets/" + Data.Tile;
                    TileBase Tile = Resources.Load<TileBase>(Path);
                    Database.TileMapData.ElementAt(2).Value[posInt].Tile = Tile;
                    Database.TileMapData.ElementAt(2).Value[posInt].SetOre(Item);
                }
            }
            else if (currentnode.Name == "Mines2Data")
            {
                ItemTypeFinder TypeFinder = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemTypeFinder>();
                XMLParser XML = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<XMLParser>();
                RockData Data = new RockData();
                Data.SetUp();
                XmlNodeList lst = currentnode.ChildNodes;
                foreach (XmlNode Node in lst)
                {
                    XmlNodeList lst0 = Node.ChildNodes;

                    Data.ItemType = lst0[0].Attributes.GetNamedItem("Type").Value;
                    Data.OreName = lst0[1].Attributes.GetNamedItem("Ore").Value;
                    Data.ID[0] = float.Parse(lst0[2].Attributes.GetNamedItem("X").Value);
                    Data.ID[0] = float.Parse(lst0[2].Attributes.GetNamedItem("Y").Value);
                    Data.ID[0] = float.Parse(lst0[2].Attributes.GetNamedItem("Z").Value);
                    Data.Pos[0] = float.Parse(lst0[3].Attributes.GetNamedItem("X").Value);
                    Data.Pos[1] = float.Parse(lst0[3].Attributes.GetNamedItem("Y").Value);
                    Data.Pos[2] = float.Parse(lst0[3].Attributes.GetNamedItem("Z").Value);
                    Data.Tile = lst0[4].Attributes.GetNamedItem("Tile").Value;
                    Data.XMLName = lst0[5].Attributes.GetNamedItem("XMLName").Value;
                    Data.Amount = float.Parse(lst0[6].Attributes.GetNamedItem("Amount").Value);


                    TileDataClass Temp = new TileDataClass();
                    Vector3Int posInt = new Vector3Int(Convert.ToInt32(Data.Pos[0]), Convert.ToInt32(Data.Pos[1]), Convert.ToInt32(Data.Pos[2]));
                    // Added the new slot
                    Database.TileMapData.ElementAt(3).Value.Add(posInt, Temp);
                    Ore Item = new Ore();
                    Item = this.gameObject.AddComponent<Ore>() as Ore;
                    // Sets up and ore item
                    int Index = TypeFinder.FindItemIndex(Data.XMLName);
                    Item.SetUpThisItem(FindType(Data.ItemType), Data.XMLName, (int)Data.Amount, XML.items.ElementAt(Index).Value.bStackable,
                                       XML.items.ElementAt(Index).Value.bSrcImage, XML.items.ElementAt(Index).Value.bSoundEffect,
                                       XML.items.ElementAt(Index).Value.bTile, XML.items.ElementAt(Index).Value.bPrefab,
                                       XML.items.ElementAt(Index).Value.bSellPrice, XML.items.ElementAt(Index).Value.bCustomData, XML.items.ElementAt(Index).Value.bDesc);
                    // Loads what tile we want the item to use
                    string Path = "XML Loaded Assets/" + Data.Tile;
                    TileBase Tile = Resources.Load<TileBase>(Path);

                    Database.TileMapData.ElementAt(3).Value[posInt].Tile = Tile;
                    Database.TileMapData.ElementAt(3).Value[posInt].SetOre(Item);
                }
            }
            else if (currentnode.Name == "ForestData")
            {
                ItemTypeFinder TypeFinder = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemTypeFinder>();
                XMLParser XML = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<XMLParser>();
                TreeData Data = new TreeData();
                Data.SetUp();
                XmlNodeList lst = currentnode.ChildNodes;
                foreach (XmlNode Node in lst)
                {
                    XmlNodeList lst0 = Node.ChildNodes;

                    Data.ItemType = lst0[0].Attributes.GetNamedItem("Type").Value;
                    Data.OreName = lst0[1].Attributes.GetNamedItem("Tree").Value;
                    Data.ID[0] = float.Parse(lst0[2].Attributes.GetNamedItem("X").Value);
                    Data.ID[0] = float.Parse(lst0[2].Attributes.GetNamedItem("Y").Value);
                    Data.ID[0] = float.Parse(lst0[2].Attributes.GetNamedItem("Z").Value);
                    Data.Pos[0] = float.Parse(lst0[3].Attributes.GetNamedItem("X").Value);
                    Data.Pos[1] = float.Parse(lst0[3].Attributes.GetNamedItem("Y").Value);
                    Data.Pos[2] = float.Parse(lst0[3].Attributes.GetNamedItem("Z").Value);
                    Data.Tile = lst0[4].Attributes.GetNamedItem("Tile").Value;
                    Data.XMLName = lst0[5].Attributes.GetNamedItem("XMLName").Value;
                    Data.Amount = float.Parse(lst0[6].Attributes.GetNamedItem("Amount").Value);


                    TileDataClass Temp = new TileDataClass();
                    Vector3Int posInt = new Vector3Int(Convert.ToInt32(Data.Pos[0]), Convert.ToInt32(Data.Pos[1]), Convert.ToInt32(Data.Pos[2]));
                    // Added the new slot
                    Database.TileMapData.ElementAt(4).Value.Add(posInt, Temp);
                    ItemBase Item = new ItemBase();
                    Item = this.gameObject.AddComponent<ItemBase>() as ItemBase;
                    // Sets up and ore item
                    int Index = TypeFinder.FindItemIndex(Data.XMLName);
                    Item.SetUpThisItem(FindType(Data.ItemType), Data.XMLName, (int)Data.Amount, XML.items.ElementAt(Index).Value.bStackable,
                                       XML.items.ElementAt(Index).Value.bSrcImage, XML.items.ElementAt(Index).Value.bSoundEffect,
                                       XML.items.ElementAt(Index).Value.bTile, XML.items.ElementAt(Index).Value.bPrefab,
                                       XML.items.ElementAt(Index).Value.bSellPrice, XML.items.ElementAt(Index).Value.bCustomData, XML.items.ElementAt(Index).Value.bDesc);
                    // Loads what tile we want the item to use
                    string Path = "XML Loaded Assets/" + Data.Tile;
                    TileBase Tile = Resources.Load<TileBase>(Path);

                    Database.TileMapData.ElementAt(4).Value[posInt].Tile = Tile;
                    Database.TileMapData.ElementAt(4).Value[posInt].SetItem(Item);
                }
            }
        }
    }
}
