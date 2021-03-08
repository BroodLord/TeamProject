using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// So each image has an itemdrag component on them, so when we do transform.position we are getting the one we have selected

public class ItemDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IDropHandler
{
    private InventoryAbstractClass cInventory;
    private HotBarClass cHotBar;
    private SellChestClass cChest;
    private Transform OringalParent;
    private bool IsDragging;

    // When we click on a slot we want to attach that slot to the mouse
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            IsDragging = true;
            // Wanna keep the oringal parent so it can be put back there if not swapped
            OringalParent = transform.parent;
            transform.SetParent(transform.parent.parent);
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        // If we are dragging an item then we want to set its position to the mouse pos.
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            transform.position = Input.mousePosition;
        }

    }

    // So this function is made up of different cases of if its a different inventory class so one for hotbar, inventory and sellchest. only gonna comment one of them as
    // you can check what happens in the one part when looking for references 

    // transform.parent.GetSiblingIndex(): This line will get the index we are swapping too
    // eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex(): This line will get the item we are dragging

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        cInventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryAbstractClass>();
        cHotBar = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<HotBarClass>();
        cChest = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<SellChestClass>();
        
        // This checks what the parent name of the slot we are transfer the item to
        if (transform.parent.parent.name == "SellChestUI")
        {
            ItemBase DropedItem = new ItemBase();
            // This line will check that the item we are dragging is part of the inventory ui
            if (eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.parent.name == "InventoryUI")
            {
                // If both of the markers are false then we are swapping nothing so just return false;
                if (cChest.Markers[transform.parent.GetSiblingIndex()] == false && cInventory.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] == false)
                {
                    return;
                }
                // This will set the dropped item to be the item we are dragging
                DropedItem = cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()];
                // If both markers are true then we want to preform a swap
                if (cChest.Markers[transform.parent.GetSiblingIndex()] && cInventory.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()])
                {
                    // Save a temp item which is the slot we are swapping with
                    ItemBase TempItem = cChest.ItemList[transform.parent.GetSiblingIndex()];
                    // Make the slot where we swap at to be the dropped item
                    cChest.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cChest.Markers[transform.parent.GetSiblingIndex()] = true;
                    // Set the slot that we got the item from to be the temp item
                    cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = TempItem;
                    // Update the UIs
                    cChest.UpdateUI();
                    cInventory.UpdateUI();
                }
                // if we are doing an empty swap then we just want to check if there is an item in the chest to be spawned with
                else if (cChest.Markers[transform.parent.GetSiblingIndex()])
                {
                    // Save the item that we want to swap
                    ItemBase TempItem = cChest.ItemList[transform.parent.GetSiblingIndex()];
                    // Set the slot in the chest to null and reset the markers
                    cChest.ItemList[transform.parent.GetSiblingIndex()] = null;
                    cChest.Markers[transform.parent.GetSiblingIndex()] = false;
                    // Set up the item to be in the swapped empty slot
                    cInventory.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = true;
                    cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = TempItem;
                    // Update the UIs
                    cChest.UpdateUI();
                    cInventory.UpdateUI();
                }
                // this is the oppersite of the above if statement, swaps the dragged item with the empty slot in the chest
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
            // this else statement is if we are swapping a dragged item with a slot in the current inventory so SellChest to SellChest
            else
            {
                //if the dragged item is dropped in the place wehere we dragged the item from then return
                if (eventData.pointerDrag.transform.parent.name == gameObject.name)
                {
                    return;
                }
                // if we are dragging an empty item to an empty slot then return false
                if (cChest.Markers[transform.parent.GetSiblingIndex()] == false && cChest.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] == false)
                {
                    return;
                }
                Debug.Log("Index: " + eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex());
                // Assigned the droped item to be the dragged item
                DropedItem = cChest.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()];
                // if the marker for where we are dropping it is false
                if (cChest.Markers[transform.parent.GetSiblingIndex()] == false)
                {
                    // Assigned the droped item to the empty slot and set the dropped item slot to empty
                    cChest.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
                    cChest.Markers[transform.parent.GetSiblingIndex()] = true;
                    cChest.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = false;
                    cChest.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = null;
                    cChest.UpdateUI();
                }
                // if we are swapping an empty with a non-empty slot
                else if (cChest.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] == false)
                {
                    // Save the item and set that slot to empty
                    ItemBase TempItem = cChest.ItemList[transform.parent.GetSiblingIndex()];
                    cChest.ItemList[transform.parent.GetSiblingIndex()] = null;
                    cChest.Markers[transform.parent.GetSiblingIndex()] = false;
                    // place the saved item on the empty slot we are swapping with
                    cChest.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = TempItem;
                    cChest.Markers[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = true;
                    cChest.UpdateUI();
                }
                // If we are actually swapping two item that are not empty have something on them
                else
                {
                    // Save the swapped item, and swap the items around
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
                else if (cInventory.Markers[transform.parent.GetSiblingIndex()])
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
            if (eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.parent.name == "InventoryUI")
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
                else if (cHotBar.Markers[transform.parent.GetSiblingIndex()])
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


    // When the user stops dragging the item this will set it to stop following the mouse.
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
