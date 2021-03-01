using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IDropHandler
{
    private InventoryAbstractClass cInventory;
    private HotBarClass cHotBar;
    private SellChestClass cChest;
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
        cChest = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<SellChestClass>();
        if (transform.parent.parent.name == "SellChestUI")
        {
            ItemBase DropedItem = new ItemBase();
            if (eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.parent.name == "InventoryUI")
            {
                if (cChest.Markers[transform.parent.GetSiblingIndex()] == false && cInventory.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] == false)
                {
                    return;
                }

                DropedItem = cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()];
                if (cChest.Markers[transform.parent.GetSiblingIndex()] && cInventory.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()])
                {
                    ItemBase TempItem = cChest.ItemList[transform.parent.GetSiblingIndex()];
                    cChest.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cChest.Markers[transform.parent.GetSiblingIndex()] = true;
                    cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = TempItem;
                    cChest.UpdateUI();
                    cInventory.UpdateUI();
                }
                else if (cChest.Markers[transform.parent.GetSiblingIndex()])
                {
                    ItemBase TempItem = cChest.ItemList[transform.parent.GetSiblingIndex()];
                    cChest.ItemList[transform.parent.GetSiblingIndex()] = null;
                    cChest.Markers[transform.parent.GetSiblingIndex()] = false;
                    cInventory.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = true;
                    cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = TempItem;
                    cChest.UpdateUI();
                    cInventory.UpdateUI();
                }
                else
                {
                    ItemBase TempItem = cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()];
                    cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = null;
                    cInventory.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = false;
                    cChest.Markers[transform.parent.GetSiblingIndex()] = true;
                    cChest.ItemList[transform.parent.GetSiblingIndex()] = TempItem;
                    cChest.UpdateUI();
                    cInventory.UpdateUI();
                }
            }
            else
            {
                if (eventData.pointerDrag.transform.parent.name == gameObject.name)
                {
                    return;
                }
                if (cChest.Markers[transform.parent.GetSiblingIndex()] == false && cChest.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] == false)
                {
                    return;
                }
                Debug.Log("Index: " + eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex());
                DropedItem = cChest.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()];
                if (cChest.Markers[transform.parent.GetSiblingIndex()] == false)
                {
                    cChest.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cChest.Markers[transform.parent.GetSiblingIndex()] = true;
                    cChest.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = false;
                    cChest.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = null;
                    cChest.UpdateUI();
                }
                else if (cChest.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] == false)
                {
                    ItemBase TempItem = cChest.ItemList[transform.parent.GetSiblingIndex()];
                    cChest.ItemList[transform.parent.GetSiblingIndex()] = null;
                    cChest.Markers[transform.parent.GetSiblingIndex()] = false;
                    cChest.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = TempItem;
                    cChest.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = true;
                    cChest.UpdateUI();
                }
                else
                {
                    ItemBase TempItem = cChest.ItemList[transform.parent.GetSiblingIndex()];
                    cChest.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cChest.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = TempItem;
                    cChest.UpdateUI();
                }
            }
        }
        else if (transform.parent.parent.name == "InventoryUI")
        {
            ItemBase DropedItem = new ItemBase();
            if (eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.parent.name == "HotbarUI")
            {
                if (cInventory.Markers[transform.parent.GetSiblingIndex()] == false && cHotBar.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] == false)
                {
                    return;
                }

                DropedItem = cHotBar.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()];
                if (cInventory.Markers[transform.parent.GetSiblingIndex()] && cHotBar.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()])
                {
                    ItemBase TempItem = cInventory.ItemList[transform.parent.GetSiblingIndex()];
                    cInventory.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cInventory.Markers[transform.parent.GetSiblingIndex()] = true;
                    cHotBar.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = TempItem;
                    cInventory.UpdateUI();
                    cHotBar.UpdateUI();
                }
                else if(cInventory.Markers[transform.parent.GetSiblingIndex()])
                {
                    ItemBase TempItem = cInventory.ItemList[transform.parent.GetSiblingIndex()];
                    cInventory.ItemList[transform.parent.GetSiblingIndex()] = null;
                    cInventory.Markers[transform.parent.GetSiblingIndex()] = false;
                    cHotBar.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = true;
                    cHotBar.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = TempItem;
                    cInventory.UpdateUI();
                    cHotBar.UpdateUI();
                }
                else
                {
                    ItemBase TempItem = cHotBar.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()];
                    cHotBar.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = null;
                    cHotBar.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = false;
                    cInventory.Markers[transform.parent.GetSiblingIndex()] = true;
                    cInventory.ItemList[transform.parent.GetSiblingIndex()] = TempItem;
                    cInventory.UpdateUI();
                    cHotBar.UpdateUI();
                }
            }
            else if (eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.parent.name == "SellChestUI")
            {
                if (cInventory.Markers[transform.parent.GetSiblingIndex()] == false && cChest.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] == false)
                {
                    return;
                }

                DropedItem = cChest.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()];
                if (cInventory.Markers[transform.parent.GetSiblingIndex()] && cChest.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()])
                {
                    ItemBase TempItem = cInventory.ItemList[transform.parent.GetSiblingIndex()];
                    cInventory.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cInventory.Markers[transform.parent.GetSiblingIndex()] = true;
                    cChest.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = TempItem;
                    cInventory.UpdateUI();
                    cChest.UpdateUI();
                }
                else if (cInventory.Markers[transform.parent.GetSiblingIndex()])
                {
                    ItemBase TempItem = cInventory.ItemList[transform.parent.GetSiblingIndex()];
                    cInventory.ItemList[transform.parent.GetSiblingIndex()] = null;
                    cInventory.Markers[transform.parent.GetSiblingIndex()] = false;
                    cChest.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = true;
                    cChest.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = TempItem;
                    cInventory.UpdateUI();
                    cChest.UpdateUI();
                }
                else
                {
                    ItemBase TempItem = cChest.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()];
                    cChest.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = null;
                    cChest.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = false;
                    cInventory.Markers[transform.parent.GetSiblingIndex()] = true;
                    cInventory.ItemList[transform.parent.GetSiblingIndex()] = TempItem;
                    cInventory.UpdateUI();
                    cChest.UpdateUI();
                }
            }
            else
            {
                if (eventData.pointerDrag.transform.parent.name == gameObject.name)
                {
                    return;
                }
                if (cInventory.Markers[transform.parent.GetSiblingIndex()] == false && cInventory.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] == false)
                {
                    return;
                }
                Debug.Log("Index: " + eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex());
                DropedItem = cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()];
                if (cInventory.Markers[transform.parent.GetSiblingIndex()] == false)
                {
                    cInventory.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cInventory.Markers[transform.parent.GetSiblingIndex()] = true;
                    cInventory.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = false;
                    cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = null;
                    cInventory.cInventory.UpdateUI();
                }
                else if (cInventory.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] == false)
                {
                    ItemBase TempItem = cInventory.ItemList[transform.parent.GetSiblingIndex()];
                    cInventory.ItemList[transform.parent.GetSiblingIndex()] = null;
                    cInventory.Markers[transform.parent.GetSiblingIndex()] = false;
                    cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = TempItem;
                    cInventory.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = true;
                    cInventory.cInventory.UpdateUI();
                }
                else
                {
                    ItemBase TempItem = cInventory.ItemList[transform.parent.GetSiblingIndex()];
                    cInventory.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = TempItem;
                    cInventory.cInventory.UpdateUI();
                }
            }
        }
        else if (transform.parent.parent.name == "HotbarUI")
        {
            ItemBase DropedItem = new ItemBase();
            if(eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.parent.name == "InventoryUI")
            {
                if (cHotBar.Markers[transform.parent.GetSiblingIndex()] == false && cInventory.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] == false)
                {
                    return;
                }
                DropedItem = cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()];
                if (cHotBar.Markers[transform.parent.GetSiblingIndex()] && cInventory.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()])
                {

                    ItemBase TempItem = cHotBar.ItemList[transform.parent.GetSiblingIndex()];
                    cHotBar.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cHotBar.Markers[transform.parent.GetSiblingIndex()] = true;
                    cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = TempItem;
                    cHotBar.UpdateUI();
                    cInventory.UpdateUI();

                }
                else if(cHotBar.Markers[transform.parent.GetSiblingIndex()])
                {
                    ItemBase TempItem = cHotBar.ItemList[transform.parent.GetSiblingIndex()];
                    cHotBar.ItemList[transform.parent.GetSiblingIndex()] = null;
                    cHotBar.Markers[transform.parent.GetSiblingIndex()] = false;
                    cInventory.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = true;
                    cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = TempItem;
                    cInventory.UpdateUI();
                    cHotBar.UpdateUI();
                }
                else
                {
                    ItemBase TempItem = cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()];
                    cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = null;
                    cInventory.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = false;
                    cHotBar.Markers[transform.parent.GetSiblingIndex()] = true;
                    cHotBar.ItemList[transform.parent.GetSiblingIndex()] = TempItem;
                    cInventory.UpdateUI();
                    cHotBar.UpdateUI();
                }
            }
            else
            {
                if (eventData.pointerDrag.transform.parent.name == gameObject.name)
                {
                    return;
                }
                if (cHotBar.Markers[transform.parent.GetSiblingIndex()] == false && cHotBar.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] == false)
                {
                    return;
                }
                Debug.Log("Index: " + eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex());
                DropedItem = cHotBar.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()];
                if (cHotBar.Markers[transform.parent.GetSiblingIndex()] == false)
                {
                    cHotBar.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cHotBar.Markers[transform.parent.GetSiblingIndex()] = true;
                    cHotBar.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = false;
                    cHotBar.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = null;
                    cHotBar.cHotBar.UpdateUI();
                }
                else if (cHotBar.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] == false)
                {
                    ItemBase TempItem = cHotBar.ItemList[transform.parent.GetSiblingIndex()];
                    cHotBar.ItemList[transform.parent.GetSiblingIndex()] = null;
                    cHotBar.Markers[transform.parent.GetSiblingIndex()] = false;
                    cHotBar.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = TempItem;
                    cHotBar.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = true;
                    cHotBar.cHotBar.UpdateUI();
                }
                else
                {
                    ItemBase TempItem = cHotBar.ItemList[transform.parent.GetSiblingIndex()];
                    cHotBar.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cHotBar.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = TempItem;
                    cHotBar.cHotBar.UpdateUI();
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
