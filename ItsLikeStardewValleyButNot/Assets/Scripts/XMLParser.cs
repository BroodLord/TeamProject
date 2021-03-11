using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Tilemaps;

public class XMLParser : MonoBehaviour
{
    public Dictionary<string, ItemBase> items = new Dictionary<string, ItemBase>();
    public Dictionary<string, Vector2[]> NPCWaypoints = new Dictionary<string, Vector2[]>();    //this is here for when multiple characters get added so they can filter out what waypoints 
    public List<Vector2> townWaypoints;
    void Start()
    {
        XmlDocument newXml = new XmlDocument();
        // string Test = Application.streamingAssetsPath;
        newXml.Load(Application.dataPath + "\\StreamingAssets\\XML Files\\Item File.xml");
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
            string prefab = ItemsXML.Attributes.GetNamedItem("Prefab").Value;
            string TileName = ItemsXML.Attributes.GetNamedItem("tile-src-image").Value;
            string SoundEffectName = ItemsXML.Attributes.GetNamedItem("sound-effect").Value;
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
            else if (itemType == "Ore")
            {
                Item = ItemBase.ItemTypes.Ore;
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
            Path = "TEMP ASSETS/" + prefab;
            var Prefab = Resources.Load(Path);
            GameObject Pre = (GameObject)Prefab;
            Path = "TEMP ASSETS/" + SoundEffectName;
            AudioClip Audio = Resources.Load<AudioClip>(Path);
            ItemBase temp = new ItemBase();
            temp.SetUpThisItem(Item, name, Amount, StackableResult, srcImage, Audio, Tile, Pre, sellPrice);
            items.Add(name, temp);
        }

        constVarList = root.SelectNodes("Waypoint");

        foreach (XmlNode ItemsXML in constVarList)
        {
            string name = ItemsXML.Attributes.GetNamedItem("name").Value;
            Vector2 pos;
            if (float.TryParse(ItemsXML.Attributes.GetNamedItem("x").Value, out float result))
            {
                pos.x = result;
            }
            else
            {
                pos.x = 0.0f;
            }

            if (float.TryParse(ItemsXML.Attributes.GetNamedItem("y").Value, out float result2))
            {
                pos.y = result2;
            }
            else
            {
                pos.y = 0.0f;
            }

            townWaypoints.Add(pos);
        }
    }
}