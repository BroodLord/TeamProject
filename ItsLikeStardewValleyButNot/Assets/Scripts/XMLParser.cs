using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Tilemaps;

public class XMLParser : MonoBehaviour
{
    public Dictionary<string, ItemBase> items = new Dictionary<string, ItemBase>();

    void Start()
    {
        XmlDocument newXml = new XmlDocument();
        newXml.Load("Assets/Scripts/XML Files/Item File.xml");
        parseXML(newXml);
    }

    private void parseXML(XmlDocument data)
    {
        XmlNode root = data.DocumentElement;

        XmlNodeList constVarList = root.SelectNodes("Item");


        foreach (XmlNode ItemsXML in constVarList)
        {
            string name = ItemsXML.Attributes.GetNamedItem("name").Value;
            string itemType = ItemsXML.Attributes.GetNamedItem("type").Value;
            string amount = ItemsXML.Attributes.GetNamedItem("amount").Value;
            string stackable = ItemsXML.Attributes.GetNamedItem("stackable").Value;
            string TileName = ItemsXML.Attributes.GetNamedItem("tile-src-image").Value;
            bool StackableResult = false;
            float sellPrice;
            ItemBase.ItemTypes Item = ItemBase.ItemTypes.Tool;

            if (float.TryParse(ItemsXML.Attributes.GetNamedItem("sellPrice").Value, out float result))
            {
                sellPrice = result;
            }
            else
            {
                sellPrice = 0.0f;
            }

            if (itemType == "Tool")
            {
                Item = ItemBase.ItemTypes.Tool;
            }
            else if (itemType == "Plant")
            {
                Item = ItemBase.ItemTypes.Plant;
            }
            else if (itemType == "Seed")
            {
                Item = ItemBase.ItemTypes.Seed;
            }
            else if (itemType == "Decoration")
            {
                Item = ItemBase.ItemTypes.Decoration;
            }

            string srcImage = ItemsXML.Attributes.GetNamedItem("src-image").Value;
            int CastedAmount = int.Parse(amount);
            int Amount = CastedAmount;

            if (stackable == "Yes")
            {
                StackableResult = true;
            }
            else if (stackable == "No")
            {
                StackableResult = false;
            }
            string Path = "TEMP ASSETS/" + TileName;

            TileBase Tile = Resources.Load<TileBase>(Path);
            //ItemTypes ItemType, string Name, int Amount, bool Stackable, string SrcImage, float SellPrice
            ItemBase temp = new ItemBase();
            temp.SetUpThisItem(Item, name, Amount, StackableResult, srcImage, Tile, sellPrice);
            items.Add(name, temp);

        }
            
    }
    
}


