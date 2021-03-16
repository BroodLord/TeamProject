using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

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
    private bool[] Resets;
    private int[] Slots;
    public void SaveToXML()
    {
        Slots = new int[3];
        Slots[0] = Inventory.MaxCapacity;
        Slots[1] = HotBar.MaxCapacity;
        Slots[2] = Chest.MaxCapacity;
        PlayerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Vector3Int Date = new Vector3Int(cClock.Day, cClock.Month, cClock.Year);
        Vector3 Pos = new Vector3(PlayerPos.position.x, PlayerPos.position.y, PlayerPos.position.z);
        Resets = cClock.WeeklyReset;
        SaveData Data = new SaveData(Date, Pos, Resets, Slots, Money.GetMoney(), Stamania.GetStamina(), Inventory.ItemList,
                                    HotBar.ItemList, Chest.ItemList, Database.TileMapData);
        WriteXML(Data);
    }

    private void WriteXML(SaveData Data)
    {
        XmlDocument SavedDataXML = new XmlDocument();
        XmlElement root = SavedDataXML.CreateElement("SaveData");
        root.SetAttribute("SaveData", "Data");

        #region CreateXML PlayerData

        XmlElement PlayerData = SavedDataXML.CreateElement("PlayerData");

        XmlElement MoneyElement = SavedDataXML.CreateElement("MoneyNumber");
        MoneyElement.SetAttribute("Money",Data.PlayerGold.ToString());
        PlayerData.AppendChild(MoneyElement);

        XmlElement EnergyElement = SavedDataXML.CreateElement("PlayerEnergy");
        EnergyElement.SetAttribute("Energy",Data.PlayerEnergy.ToString());
        PlayerData.AppendChild(EnergyElement);

        root.AppendChild(PlayerData);
        #endregion

        #region CreateXML WorldData

        XmlElement WorldData = SavedDataXML.CreateElement("WorldData");

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
            XmlElement ElementData, NameElement, TypeElement, ImageElement, TileImageElement, SoundEffectElement, PrefabElement, AmountElement, StackableElement, PriceElement;
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

                    NameElement.SetAttribute("Name", Data.Inventory[i].Name);
                    TypeElement.SetAttribute("Type", Data.Inventory[i].Type);
                    ImageElement.SetAttribute("Image", Data.Inventory[i].Src_Image);
                    TileImageElement.SetAttribute("Tile", Data.Inventory[i].Tile_Src_Image);
                    SoundEffectElement.SetAttribute("SoundEffect", Data.Inventory[i].Sound_Effect);
                    PrefabElement.SetAttribute("Prefab", Data.Inventory[i].Prefab);
                    AmountElement.SetAttribute("Amount", Data.Inventory[i].Amount.ToString());
                    StackableElement.SetAttribute("Stackable", Data.Inventory[i].Stackable.ToString());
                    PriceElement.SetAttribute("Price", Data.Inventory[i].SellPrice.ToString());

                    ElementData.AppendChild(NameElement);
                    ElementData.AppendChild(TypeElement);
                    ElementData.AppendChild(ImageElement);
                    ElementData.AppendChild(TileImageElement);
                    ElementData.AppendChild(SoundEffectElement);
                    ElementData.AppendChild(PrefabElement);
                    ElementData.AppendChild(AmountElement);
                    ElementData.AppendChild(StackableElement);
                    ElementData.AppendChild(PriceElement);

                    InventoryData.AppendChild(ElementData);
                }
            }

            root.AppendChild(InventoryData);
        }



        #endregion

        #region CreateXML HotbarData

        {
            XmlElement ElementData, NameElement, TypeElement, ImageElement, TileImageElement, SoundEffectElement, PrefabElement, AmountElement, StackableElement, PriceElement;
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

                    NameElement.SetAttribute("Name", Data.HotBar[i].Name);
                    TypeElement.SetAttribute("Type", Data.HotBar[i].Type);
                    ImageElement.SetAttribute("Image", Data.HotBar[i].Src_Image);
                    TileImageElement.SetAttribute("Tile", Data.HotBar[i].Tile_Src_Image);
                    SoundEffectElement.SetAttribute("SoundEffect", Data.HotBar[i].Sound_Effect);
                    PrefabElement.SetAttribute("Prefab", Data.HotBar[i].Prefab);
                    AmountElement.SetAttribute("Amount", Data.HotBar[i].Amount.ToString());
                    StackableElement.SetAttribute("Stackable", Data.HotBar[i].Stackable.ToString());
                    PriceElement.SetAttribute("Price", Data.HotBar[i].SellPrice.ToString());

                    ElementData.AppendChild(NameElement);
                    ElementData.AppendChild(TypeElement);
                    ElementData.AppendChild(ImageElement);
                    ElementData.AppendChild(TileImageElement);
                    ElementData.AppendChild(SoundEffectElement);
                    ElementData.AppendChild(PrefabElement);
                    ElementData.AppendChild(AmountElement);
                    ElementData.AppendChild(StackableElement);
                    ElementData.AppendChild(PriceElement);

                    HotbarData.AppendChild(ElementData);
                }
            }

            root.AppendChild(HotbarData);
        }
        #endregion

        #region CreateXML ChestData

        {
            XmlElement ElementData, NameElement, TypeElement, ImageElement, TileImageElement, SoundEffectElement, PrefabElement, AmountElement, StackableElement, PriceElement;
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

                    NameElement.SetAttribute("Name", Data.Chest[i].Name);
                    TypeElement.SetAttribute("Type", Data.Chest[i].Type);
                    ImageElement.SetAttribute("Image", Data.Chest[i].Src_Image);
                    TileImageElement.SetAttribute("Tile", Data.Chest[i].Tile_Src_Image);
                    SoundEffectElement.SetAttribute("SoundEffect", Data.Chest[i].Sound_Effect);
                    PrefabElement.SetAttribute("Prefab", Data.Chest[i].Prefab);
                    AmountElement.SetAttribute("Amount", Data.Chest[i].Amount.ToString());
                    StackableElement.SetAttribute("Stackable", Data.Chest[i].Stackable.ToString());
                    PriceElement.SetAttribute("Price", Data.Chest[i].SellPrice.ToString());

                    ElementData.AppendChild(NameElement);
                    ElementData.AppendChild(TypeElement);
                    ElementData.AppendChild(ImageElement);
                    ElementData.AppendChild(TileImageElement);
                    ElementData.AppendChild(SoundEffectElement);
                    ElementData.AppendChild(PrefabElement);
                    ElementData.AppendChild(AmountElement);
                    ElementData.AppendChild(StackableElement);
                    ElementData.AppendChild(PriceElement);

                    ChestData.AppendChild(ElementData);
                }
            }

            root.AppendChild(ChestData);
        }
        #endregion

        #region CreateXML FarmData

        {
            XmlElement CropData, TypeElement, AmountElement, CurrentDaysElement, GrowthTimElement, SpritesElement, IdElement, PosElement, HarvesElement, GrowthElement, XmlNamElement, WateredElement;
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

                    HarvesElement.SetAttribute("Harvestable", Data.Farm[i].Harvestable.ToString());
                    GrowthElement.SetAttribute("HasGrown", Data.Farm[i].Growth.ToString());
                    XmlNamElement.SetAttribute("XMLName", Data.Farm[i].XMLName);
                    WateredElement.SetAttribute("Watered", Data.Farm[i].Watered.ToString());

                    FarmData.AppendChild(TypeElement);
                    FarmData.AppendChild(AmountElement);
                    FarmData.AppendChild(CurrentDaysElement);
                    FarmData.AppendChild(GrowthTimElement);
                    FarmData.AppendChild(SpritesElement);
                    FarmData.AppendChild(IdElement);
                    FarmData.AppendChild(PosElement);
                    FarmData.AppendChild(HarvesElement);
                    FarmData.AppendChild(GrowthElement);
                    FarmData.AppendChild(XmlNamElement);
                    FarmData.AppendChild(WateredElement);

                    FarmData.AppendChild(CropData);
                }
            }

            root.AppendChild(FarmData);
        }

        #endregion

        #region CreateXML MinesData

        #region CreateXML Mines0Data

        {
            XmlElement RockData, TypeElement, OreElement, IdElement, PosElement, XmlNameElement, AmountElement;
            XmlElement MinesData = SavedDataXML.CreateElement("Mines0Data");

            for (int i = 0; i < Data.Mines.Length; i++)
            {
                RockData = SavedDataXML.CreateElement("RockData");
                TypeElement = SavedDataXML.CreateElement("TypElement");
                OreElement = SavedDataXML.CreateElement("OreElement");
                IdElement = SavedDataXML.CreateElement("IdElement");
                PosElement = SavedDataXML.CreateElement("PosElement");
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

                XmlNameElement.SetAttribute("XMLName", Data.Mines[i].XMLName);
                AmountElement.SetAttribute("Amount", Data.Mines[i].Amount.ToString());


                RockData.AppendChild(TypeElement);
                RockData.AppendChild(OreElement);
                RockData.AppendChild(IdElement);
                RockData.AppendChild(PosElement);
                RockData.AppendChild(XmlNameElement);
                RockData.AppendChild(AmountElement);

                MinesData.AppendChild(RockData);
            }

            root.AppendChild(MinesData);
        }

        #endregion

        #region CreateXML Mines1Data

        {
            XmlElement RockData, TypeElement, OreElement, IdElement, PosElement, XmlNameElement, AmountElement;
            XmlElement MinesData = SavedDataXML.CreateElement("Mines1Data");

            for (int i = 0; i < Data.Mines1.Length; i++)
            {
                RockData = SavedDataXML.CreateElement("RockData");
                TypeElement = SavedDataXML.CreateElement("TypElement");
                OreElement = SavedDataXML.CreateElement("OreElement");
                IdElement = SavedDataXML.CreateElement("IdElement");
                PosElement = SavedDataXML.CreateElement("PosElement");
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

                XmlNameElement.SetAttribute("XMLName", Data.Mines1[i].XMLName);
                AmountElement.SetAttribute("Amount", Data.Mines1[i].Amount.ToString());


                RockData.AppendChild(TypeElement);
                RockData.AppendChild(OreElement);
                RockData.AppendChild(IdElement);
                RockData.AppendChild(PosElement);
                RockData.AppendChild(XmlNameElement);
                RockData.AppendChild(AmountElement);

                MinesData.AppendChild(RockData);
            }

            root.AppendChild(MinesData);
        }

        #endregion

        #region CreateXML Mines2Data

        {
            XmlElement RockData, TypeElement, OreElement, IdElement, PosElement, XmlNameElement, AmountElement;
            XmlElement MinesData = SavedDataXML.CreateElement("Mines2Data");

            for (int i = 0; i < Data.Mines2.Length; i++)
            {
                RockData = SavedDataXML.CreateElement("RockData");
                TypeElement = SavedDataXML.CreateElement("TypElement");
                OreElement = SavedDataXML.CreateElement("OreElement");
                IdElement = SavedDataXML.CreateElement("IdElement");
                PosElement = SavedDataXML.CreateElement("PosElement");
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

                XmlNameElement.SetAttribute("XMLName", Data.Mines2[i].XMLName);
                AmountElement.SetAttribute("Amount", Data.Mines2[i].Amount.ToString());


                RockData.AppendChild(TypeElement);
                RockData.AppendChild(OreElement);
                RockData.AppendChild(IdElement);
                RockData.AppendChild(PosElement);
                RockData.AppendChild(XmlNameElement);
                RockData.AppendChild(AmountElement);

                MinesData.AppendChild(RockData);
            }

            root.AppendChild(MinesData);
        }

        #endregion

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

    }
}
