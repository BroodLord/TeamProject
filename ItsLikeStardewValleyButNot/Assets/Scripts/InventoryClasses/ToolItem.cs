using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ToolItem : ItemBase
{
    public void Init(string Name, int Amount)
    {
        mName = Name;
        mAmount = Amount;
        mItemType = ItemTypes.Tool;
    }

    public void Interact()
    {
        Debug.Log("UWU INTERACT UWU");
    }
}
