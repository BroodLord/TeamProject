using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;


public class XMLParser : MonoBehaviour
{
    Dictionary<string, ItemBase> items = new Dictionary<string, ItemBase>();

    private void Start()
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
            ItemBase temp = new ItemBase();

            temp.SetName(ItemsXML.Attributes.GetNamedItem("name").Value);
            string itemType = ItemsXML.Attributes.GetNamedItem("type").Value;
            string stackable = ItemsXML.Attributes.GetNamedItem("stackable").Value;
            float sellPrice;

            if (float.TryParse(ItemsXML.Attributes.GetNamedItem("sellPrice").Value, out float result))
            {
                sellPrice = result;
            }
            else
            {
                sellPrice = 0.0f;
            }
            
            if ( itemType == "Tool")
            {
                temp.SetType(ItemBase.ItemTypes.Tool);
            }
            else if ( itemType == "Seed")
            {
                temp.SetType(ItemBase.ItemTypes.Seed);
            }
            else if ( itemType == "Decoration")
            {
                temp.SetType(ItemBase.ItemTypes.Decoration);
            }

            temp.SetImage(ItemsXML.Attributes.GetNamedItem("src-image").Value);

            if ( stackable == "Yes")
            {
                temp.SetStackable(true);
            }
            else if (stackable == "No")
            {
                temp.SetStackable(false);
            }

            temp.SetName(ItemsXML.Attributes.GetNamedItem("name").Value);

            temp.SetSellPrice(sellPrice);

            items.Add(temp.GetName(), temp);

        }
            
    }
    
}


