using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine;


public class TransferData : MonoBehaviour
{
    //public struct LoadData
    //{
    //    public GameObject mPlayer;
    //    public InventoryClass mInventoryRef;
    //    public HotBarClass mHotBarRef;
    //}

    public InventoryClass mInventoryRef;
    public HotBarClass mHotBarRef;

   // protected LoadData Data;
    public void ReturnData(ref InventoryClass InventoryRef, ref HotBarClass HotBarRef)
    {

    }

    public void SetData(InventoryClass InventoryRef, HotBarClass HotBarRef)
    {
        mInventoryRef = InventoryRef;
        mHotBarRef = HotBarRef;
    }
}
