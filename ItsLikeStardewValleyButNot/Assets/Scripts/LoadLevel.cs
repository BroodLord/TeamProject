using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine;



public class LoadLevel : MonoBehaviour
{
    public TransferData TransferManager;
    public bool NewLevel;
    public Canvas UICanvas;
    public EventSystem EventSystemRef;
    public GameObject Player;
    public InventoryClass InventoryRef;
    public HotBarClass HotBarRef;

    public string LevelName;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Canvas"));
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("EventSystem"));
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Manager"));
        TransferManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<TransferData>();
        if (LevelName == "PlayerRoom")
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            InventoryRef = Player.GetComponent<InventoryClass>();
            HotBarRef = Player.GetComponent<HotBarClass>();
            TransferManager.SetData(InventoryRef, HotBarRef);
            DontDestroyOnLoad(TransferManager.mInventoryRef);
            DontDestroyOnLoad(TransferManager.mHotBarRef);
            Destroy(Player);
            Application.LoadLevel("PlayerRoom");
            InventoryRef = Player.GetComponent<InventoryClass>();
            HotBarRef = Player.GetComponent<HotBarClass>();
            TransferManager.ReturnData(ref InventoryRef, ref HotBarRef);


        }
        if (LevelName == "PlayerFarm")
        {
            Application.LoadLevel("PlayerFarm");
        }

    }
}
