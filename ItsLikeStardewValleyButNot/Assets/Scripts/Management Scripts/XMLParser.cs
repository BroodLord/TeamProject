using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Tilemaps;

public class XMLParser : MonoBehaviour
{
    public Dictionary<string, ItemBase> items = new Dictionary<string, ItemBase>();
    public Dictionary<int, NPC> NPCs = new Dictionary<int, NPC>();
    public Dictionary<string, Vector2[]> NPCWaypoints = new Dictionary<string, Vector2[]>();    //this is here for when multiple characters get added so they can filter out what waypoints 

    // Creates and loads the xml document
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
        XmlNodeList constVarList = root.SelectNodes("GordonNPC");
        Vector2[] tempArray = new Vector2[5];
        int NPCCounter = 0;
        int arrayIndex = 0;

        foreach (XmlNode ItemsXML in constVarList)
        {

            if (ItemsXML.Name == "GordonNPC")
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

                tempArray[arrayIndex] = pos;
                arrayIndex++;
            }

        }
        NPCWaypoints.Add("GordonNPC", tempArray);
        arrayIndex = 0;
        constVarList = root.SelectNodes("ShirleyNPC");
        tempArray = new Vector2[3];

        foreach (XmlNode ItemsXML in constVarList)
        {
            if (ItemsXML.Name == "ShirleyNPC")
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

                tempArray[arrayIndex] = pos;
                arrayIndex++;
            }
        }

        NPCWaypoints.Add("ShirleyNPC", tempArray);
        arrayIndex = 0;
        constVarList = root.SelectNodes("CrazyEdNPC");
        tempArray = new Vector2[3];

        foreach (XmlNode ItemsXML in constVarList)
        {
            if (ItemsXML.Name == "CrazyEdNPC")
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

                tempArray[arrayIndex] = pos;
                arrayIndex++;
            }

        }

        NPCWaypoints.Add("CrazyEdNPC", tempArray);
        arrayIndex = 0;
        constVarList = root.SelectNodes("Item");

        foreach (XmlNode ItemsXML in constVarList)
        {
            /*Get the attributes we want for each variable*/
            string name = ItemsXML.Attributes.GetNamedItem("name").Value;
            string itemType = ItemsXML.Attributes.GetNamedItem("type").Value;
            string amount = ItemsXML.Attributes.GetNamedItem("amount").Value;
            string stackable = ItemsXML.Attributes.GetNamedItem("stackable").Value;
            string prefab = ItemsXML.Attributes.GetNamedItem("Prefab").Value;
            string TileName = ItemsXML.Attributes.GetNamedItem("tile-src-image").Value;
            string SoundEffectName = ItemsXML.Attributes.GetNamedItem("sound-effect").Value;
            string customDataString = ItemsXML.Attributes.GetNamedItem("customData").Value;
            string ItemDesc = ItemsXML.Attributes.GetNamedItem("ItemDesc").Value;
            var CustomData = 0;
            bool StackableResult = false;
            float sellPrice;
            ItemBase.ItemTypes Item = ItemBase.ItemTypes.Tool;
            /***************************************************************/
            // Get the sell price
            if (float.TryParse(ItemsXML.Attributes.GetNamedItem("sellPrice").Value, out float result))
            {
                sellPrice = result;
            }
            else
            {
                sellPrice = 0.0f;
            }
            /*Depending on the tool, set up tool with the correct enum*/
            if (itemType == "Tool")
            {
                Item = ItemBase.ItemTypes.Tool;
            }
            else if (itemType == "Wood")
            {
                Item = ItemBase.ItemTypes.Wood;
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
            else if (itemType == "Fish")
            {
                Item = ItemBase.ItemTypes.Fish;
            }

            /*****************************************************/
            // Gets the source image
            string srcImage = ItemsXML.Attributes.GetNamedItem("src-image").Value;

            // Gets the amount of each item
            int CastedAmount = int.Parse(amount);
            int Amount = CastedAmount;

            // Set up if its stackable
            if (stackable == "Yes")
            {
                StackableResult = true;
            }
            else if (stackable == "No")
            {
                StackableResult = false;
            }
            // Loads what tile we want the item to use
            string Path = "XML Loaded Assets/" + TileName;
            TileBase Tile = Resources.Load<TileBase>(Path);
            // Load a given prefab
            Path = "XML Loaded Assets/" + prefab;
            var Prefab = Resources.Load(Path);
            GameObject Pre = (GameObject)Prefab;
            // Load the sound effect we want to use
            Path = "XML Loaded Assets/" + SoundEffectName;
            AudioClip Audio = Resources.Load<AudioClip>(Path);
            ItemBase temp = new ItemBase();
            // Set up the item and add it to the dicitiory for the XML.
            temp.SetUpThisItem(Item, name, Amount, StackableResult, srcImage, Audio, Tile, Pre, sellPrice, Convert.ToInt32(customDataString), ItemDesc);
            items.Add(name, temp);
        }
        constVarList = root.SelectNodes("NPCInfo");
        foreach (XmlNode ItemsXML in constVarList)
        {
            string Name                    = ItemsXML.Attributes.GetNamedItem("name").Value;
            string Protrait                = ItemsXML.Attributes.GetNamedItem("Protrait").Value;
            string IntroDialogueCount      = ItemsXML.Attributes.GetNamedItem("IntroDialogueCount").Value;
            string AboutDialogueCount      = ItemsXML.Attributes.GetNamedItem("AboutDialogueCount").Value;
            string DoingTodayDialogueCount = ItemsXML.Attributes.GetNamedItem("DoingTodayDialogueCount").Value;
            string[] IntroDialogue = new string[int.Parse(IntroDialogueCount)];
            string[] AboutDialogue = new string[int.Parse(AboutDialogueCount)];
            string[] DoingDialogue = new string[int.Parse(DoingTodayDialogueCount)];
            for (int i = 0; i < IntroDialogue.Length; i++)
            {
                IntroDialogue[i] = ItemsXML.Attributes.GetNamedItem("AboutDialogue" + i).Value;
            }
            for (int i = 0; i < AboutDialogue.Length; i++)
            {
                AboutDialogue[i] = ItemsXML.Attributes.GetNamedItem("AboutDialogue" + i).Value;
            }
            for (int i = 0; i < DoingDialogue.Length; i++)
            {
                DoingDialogue[i] = ItemsXML.Attributes.GetNamedItem("DoingDialogue" + i).Value;
            }
            NPC npc = new NPC();
            npc.SetUp(Name, Protrait, IntroDialogue, AboutDialogue, DoingDialogue);
            NPCs.Add(NPCCounter, npc);
            NPCCounter++;
        }



    }

}


