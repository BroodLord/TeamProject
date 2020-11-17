﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IDropHandler
{
    public InventoryAbstractClass cInventory;
    public HotBarClass cHotBar;
    private Transform OringalParent;
    private bool IsDragging;
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {

            if (eventData.button == PointerEventData.InputButton.Left)
            {
                IsDragging = true;
                OringalParent = transform.parent;
                transform.SetParent(transform.parent.parent);
                GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        
    }
    void IDragHandler.OnDrag(PointerEventData eventData)
    {

            if (eventData.button == PointerEventData.InputButton.Left)
            {
                transform.position = Input.mousePosition;
            }
        
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if (transform.parent.parent.name == "InventoryUI")
        {
            ItemBase DropedItem = cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()];
            if (DropedItem != null)
            {
                if (eventData.pointerDrag.transform.parent.name == gameObject.name)
                {
                    return;
                }
                if (cInventory.ItemList[transform.parent.GetSiblingIndex()] == null)
                {
                    cInventory.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cInventory.Markers[transform.parent.GetSiblingIndex()] = true;
                    cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = null;
                    cInventory.cInventory.UpdateUI();
                    //cHotBar.cHotBar.UpdateUI();
                }
                else
                {
                    ItemBase TempItem = cInventory.ItemList[transform.parent.GetSiblingIndex()];
                    cInventory.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = TempItem;
                    cInventory.cInventory.UpdateUI();
                    //cHotBar.cHotBar.UpdateUI();
                }
            }
            else
            {
                if (DropedItem == null) { DropedItem = cHotBar.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()]; }
                if (cInventory.ItemList[transform.parent.GetSiblingIndex()] == null)
                {
                    cInventory.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cInventory.Markers[transform.parent.GetSiblingIndex()] = true;
                    cHotBar.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = null;
                    cInventory.cHotBar.UpdateUI();
                    cHotBar.cInventory.UpdateUI();
                }
            }
        }
        if (transform.parent.parent.name == "HotbarUI")
        {
            ItemBase DropedItem = cHotBar.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()];
            if (DropedItem != null)
            {
                if (eventData.pointerDrag.transform.parent.name == gameObject.name)
                {
                    return;
                }
                if (cHotBar.ItemList[transform.parent.GetSiblingIndex()] == null)
                {
                    cHotBar.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cHotBar.Markers[transform.parent.GetSiblingIndex()] = true;
                    cHotBar.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = null;
                    //cInventory.cInventory.UpdateUI();
                    cHotBar.cHotBar.UpdateUI();
                }
                else
                {
                    ItemBase TempItem = cHotBar.ItemList[transform.parent.GetSiblingIndex()];
                    cHotBar.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cHotBar.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = TempItem;
                    //cInventory.cInventory.UpdateUI();
                    cHotBar.cHotBar.UpdateUI();
                }
            }
            else
            {
                if (DropedItem == null) { DropedItem = cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()]; }
                if (cHotBar.ItemList[transform.parent.GetSiblingIndex()] == null)
                {
                    cHotBar.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cHotBar.Markers[transform.parent.GetSiblingIndex()] = true;
                    cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = null;
                    cHotBar.cInventory.UpdateUI();
                    cInventory.cHotBar.UpdateUI();

                }
            }
        }
    }



    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            IsDragging = false;
            transform.SetParent(OringalParent);
            transform.localPosition = Vector3.zero;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
}
