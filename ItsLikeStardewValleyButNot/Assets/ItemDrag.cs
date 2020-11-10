using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IDropHandler
{
    public InventoryClass cInventory;
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
        ItemBase DropedItem = cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()];
        if (eventData.pointerDrag.transform.parent.name == gameObject.name)
        {
            return;
        }
        if (cInventory.ItemList[transform.parent.GetSiblingIndex()] == null)
        {
            cInventory.ItemList[transform.parent.GetSiblingIndex()] = DropedItem;
            cInventory.Markers[transform.parent.GetSiblingIndex()] = true;
            cInventory.ItemList[eventData.pointerDrag.GetComponent<ItemDrag>().transform.parent.GetSiblingIndex()] = null;
            cInventory.UpdateUI();
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
