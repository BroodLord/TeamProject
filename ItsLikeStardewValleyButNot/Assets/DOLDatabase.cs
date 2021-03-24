using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOLDatabase : MonoBehaviour
{
    [SerializeField]
    List<GameObject> DOLList;
    // Start is called before the first frame update
    void Start()
    {
        DOLList = new List<GameObject>();
    }

    public void Add(GameObject Gameobj)
    {
        bool OnList = false;
        foreach (var instance in DOLList)
        {
            if (instance.name == Gameobj.name)
            {
                OnList = true;
            }

        }
        if (!OnList)
        {
            DOLList.Add(Gameobj);
        }
    }

    public void Remove(GameObject Gameobj)
    {
        foreach (var instance in DOLList)
        {
            if (instance.name == Gameobj.name)
            {
                DOLList.Remove(instance);
                return;
            }
        }
    }

    public void DestoryObjects()
    {
        GameObject Manager = GameObject.FindGameObjectWithTag("InventoryManager");
        InventoryClass Invent = Manager.GetComponent<InventoryClass>();
        HotBarClass HotBar = Manager.GetComponent<HotBarClass>();
        TileDictionaryClass Dictionary = GameObject.FindGameObjectWithTag("TileMapManager").GetComponent<TileDictionaryClass>();

        // Loops through all the databases and makes the clones are don't destory on load
        foreach (var BaseV in Dictionary.TileMapData)
        {
            foreach (var ChildV in BaseV.Value)
            {
                Destroy(ChildV.Value.Clone);
            }
        }

        for (int i = 0; i < Invent.ItemList.Length; i++)
        {
            Destroy(Invent.ItemList[i]);
        }
        for (int i = 0; i < HotBar.ItemList.Length; i++)
        {
            Destroy(HotBar.ItemList[i]);
        }

        for (int i = 0; i < DOLList.Count; i++)
        {
            Destroy(DOLList[i]);
        }
        DOLList.Clear();
    }
}
