using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine;



public class LoadLevel : MonoBehaviour
{
    public Animator Transition;
    public bool NewLevel;
    public Canvas UICanvas;
    public EventSystem EventSystemRef;
    public GameObject Player;
    public InventoryClass InventoryRef;
    public HotBarClass HotBarRef;

    public string LevelName;
    // Start is called before the first frame update
    private void Start()
    {
        Transition = GameObject.FindGameObjectWithTag("Transition").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        DontDestroyOnLoad(Player);
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Canvas"));
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("EventSystem"));
        GameObject Manager = GameObject.FindGameObjectWithTag("InventoryManager");
        InventoryClass Invent = Manager.GetComponent<InventoryClass>();
        HotBarClass HotBar = Manager.GetComponent<HotBarClass>();
        for (int i = 0; i < Invent.ItemList.Length; i++)
        {
            if(Invent.ItemList[i] != null)
                DontDestroyOnLoad(Invent.ItemList[i]);
        }
        for (int i = 0; i < HotBar.ItemList.Length; i++)
        {
            if (HotBar.ItemList[i] != null)
                DontDestroyOnLoad(HotBar.ItemList[i]);
        }
        DontDestroyOnLoad(Manager);
        LoadNextLevel(LevelName);
    }

    public void LoadNextLevel(string LevelName)
    {
        StartCoroutine(eLoadNextLevel(LevelName));
    }

    IEnumerator eLoadNextLevel(string LevelName)
    {
        Transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(LevelName);
    }
}
