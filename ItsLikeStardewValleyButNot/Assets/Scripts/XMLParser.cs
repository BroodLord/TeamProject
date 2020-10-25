using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

struct itemVar
{
    
}

public class XMLParser : MonoBehaviour
{
    Dictionary<string, itemVar> items;

    private void Start()
    {
        XmlDocument newXml = new XmlDocument();
        newXml.Load("Assets/Scripts/XML Files/Item File.xml");
    }

    private void parseXML(XmlDocument data)
    {
        XmlNode root = data.DocumentElement;
        XmlNodeList constVarList = data.SelectNodes("Items");
        foreach (XmlNode ItemsXML in constVarList)
        {
            
        }
            
    }
    
}


