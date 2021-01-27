using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IDropHandler
{
    private InventoryAbstractClass cInventory;
    private HotBarClass cHotBar;
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
        cInventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryAbstractClass>();
        cHotBar = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<HotBarClass>();
        if (transform.parent.parent.name == "InventoryUI")
        {

            ItemBase DropedItem = new ItemBase();
            if (eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.parent.name != "HotbarUI"
                && eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex() < cInventory.ItemList.Length)
            {
                Debug.Log("Index: " + eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex());
                DropedItem = cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()];
                //if (cInventory.Markers[transform.parent.GetSiblingIndex()] == true)
                //{
                //    DropedItem = null;
                //}
            }
            else
            {
                DropedItem = null;
            }
            if (DropedItem != null)
            {
                if (eventData.pointerDrag.transform.parent.name == gameObject.name)
                {
                    return;
                }
                if (cInventory.Markers[transform.parent.GetSiblingIndex()] == false)
                {
                    cInventory.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cInventory.Markers[transform.parent.GetSiblingIndex()] = true;
                    cInventory.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = false;
                    cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = null;
                    cInventory.cInventory.UpdateUI();
                    cHotBar.cHotBar.UpdateUI();
                }
                else
                {
                    ItemBase TempItem = cInventory.ItemList[transform.parent.GetSiblingIndex()];
                    cInventory.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = TempItem;
                    cInventory.cInventory.UpdateUI();
                    cHotBar.cHotBar.UpdateUI();
                }
            }
            else
            {
                if (DropedItem == null) { DropedItem = cHotBar.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()]; }
                if (cInventory.Markers[transform.parent.GetSiblingIndex()])
                {
                    ItemBase TempItem = cInventory.ItemList[transform.parent.GetSiblingIndex()];
                    cInventory.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cInventory.Markers[transform.parent.GetSiblingIndex()] = true;
                    //cHotBar.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = false;
                    cHotBar.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = TempItem;
                    cInventory.cHotBar.UpdateUI();
                    cHotBar.cInventory.UpdateUI();
                }
                else
                {
                    cInventory.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cInventory.Markers[transform.parent.GetSiblingIndex()] = true;
                    cHotBar.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = false;
                    cHotBar.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = null;
                    cInventory.cHotBar.UpdateUI();
                    cHotBar.cInventory.UpdateUI();
                }
            }
        }
        if (transform.parent.parent.name == "HotbarUI")
        {
            ItemBase DropedItem = new ItemBase();
            if (eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.parent.name != "InventoryUI" 
                && eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex() < cHotBar.ItemList.Length)
            {
                
                Debug.Log("Index: " + eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex());
                DropedItem = cHotBar.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()];
                //if(cHotBar.Markers[transform.parent.GetSiblingIndex()] == true)
                //{
                //    DropedItem = null;
                //}
            }
            else
            {
                DropedItem = null;
            }
            if (DropedItem != null)
            {
                if (eventData.pointerDrag.transform.parent.name == gameObject.name)
                {
                    return;
                }
                if (cHotBar.Markers[transform.parent.GetSiblingIndex()] == false)
                {
                    cHotBar.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cHotBar.Markers[transform.parent.GetSiblingIndex()] = true;
                    cHotBar.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = false;
                    cHotBar.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = null;
                    cInventory.cInventory.UpdateUI();
                    cHotBar.cHotBar.UpdateUI();
                }
                else
                {
                    ItemBase TempItem = cHotBar.ItemList[transform.parent.GetSiblingIndex()];
                    cHotBar.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cHotBar.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = TempItem;
                    cInventory.cInventory.UpdateUI();
                    cHotBar.cHotBar.UpdateUI();
                }
            }
            else
            {
                if (DropedItem == null) { DropedItem = cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()]; }
                if (cHotBar.Markers[transform.parent.GetSiblingIndex()])
                {
                    ItemBase TempItem = cHotBar.ItemList[transform.parent.GetSiblingIndex()];
                    cHotBar.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cHotBar.Markers[transform.parent.GetSiblingIndex()] = true;
                    //cInventory.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = false;
                    cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = TempItem;
                    cHotBar.cInventory.UpdateUI();
                    cInventory.cHotBar.UpdateUI();

                }
                else
                {
                    cHotBar.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cHotBar.Markers[transform.parent.GetSiblingIndex()] = true;
                    cInventory.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = false;
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
