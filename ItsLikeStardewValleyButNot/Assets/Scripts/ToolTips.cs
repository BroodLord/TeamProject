using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;

public class ToolTips : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public GameObject ToolTip;
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI DescText;
    public InventoryAbstractClass ChosenInventory;

    public bool isOver = false;

    void Start()
    {
        ToolTip.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(transform.parent.parent.name == "SellChestUI")
        {
            ChosenInventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<SellChestClass>();
        }
        else if(transform.parent.parent.name == "InventoryUI")
        {
            ChosenInventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryClass>();
        }
        else if (transform.parent.parent.name == "HotbarUI")
        {
            ChosenInventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<HotBarClass>();
        }
        if (ChosenInventory.ItemList[eventData.pointerEnter.GetComponent<ToolTips>().transform.parent.GetSiblingIndex()] != null)
        {
            isOver = true;
            Vector3 newPos = eventData.pointerEnter.transform.position;
            newPos.y += 70f;
            ToolTip.transform.position = newPos;
            TitleText.text = ChosenInventory.ItemList[eventData.pointerEnter.GetComponent<ToolTips>().transform.parent.GetSiblingIndex()].GetName();
            DescText.text = ChosenInventory.ItemList[eventData.pointerEnter.GetComponent<ToolTips>().transform.parent.GetSiblingIndex()].GetDesc();
            ToolTip.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exit");
        isOver = false;
        ToolTip.SetActive(false);
    }
}
