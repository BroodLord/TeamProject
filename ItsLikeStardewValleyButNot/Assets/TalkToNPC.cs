using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class TalkToNPC : MonoBehaviour
{
    public GameObject[] NPCs;
    public GameObject DialogueUI;
    public GameObject ConversationUI;
    private NPC ChosenNPC;
    public bool InMenu;
    // Start is called before the first frame update

    private void Start()
    {
        InMenu = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!InMenu)
        {
            if (Input.GetMouseButtonDown(1))
            {
                NPCs = GameObject.FindGameObjectsWithTag("NPC");
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2Int posInt = new Vector2Int((int)pos.x, (int)pos.y);
                foreach (GameObject NPCGB in NPCs)
                {
                    Vector2Int NPCPosInt = new Vector2Int((int)NPCGB.transform.position.x, (int)NPCGB.transform.position.y);
                    if (NPCPosInt == posInt && Vector2.Distance(this.transform.position, posInt) < 1)
                    {
                        Debug.Log("TALKING TO NPC");
                        ChosenNPC = NPCGB.GetComponent<NPC>();
                        ShowDialogueMenu();
                    }
                    //else if(NPCPosInt.x == posInt.x + 1 && NPCPosInt.y == posInt.y || NPCPosInt.x == posInt.x - 1 && NPCPosInt.y == posInt.y ||
                    //        NPCPosInt.x == posInt.x && NPCPosInt.y == posInt.y + 1 || NPCPosInt.x == posInt.x && NPCPosInt.y == posInt.y - 1)
                    //{
                    //    Debug.Log("TALKING TO NPC2");
                    //}
                }
            }
        }
    }

    void ShowDialogueMenu()
    {
        Time.timeScale = 0.0f;
        DialogueUI.SetActive(true);
        Image picture = DialogueUI.transform.Find("Background").Find("Portait").GetComponent<Image>();
        picture.sprite = ChosenNPC.Protrait;
        DialogueUI.transform.Find("TextBackground").Find("TalkingText").GetComponent<TextMeshProUGUI>().text = ChosenNPC.GetIntroText().text;
    }

    public void ShowAboutDialogue()
    {
        ConversationUI.SetActive(true);
        Image picture = ConversationUI.transform.Find("Background").Find("Portait").GetComponent<Image>();
        picture.sprite = ChosenNPC.Protrait;
        ConversationUI.transform.Find("TextBackground").Find("TalkingText").GetComponent<TextMeshProUGUI>().text = ChosenNPC.GetAboutText().text;
        DialogueUI.SetActive(false);
    }

    public void ReturnToDialogueMenu()
    {
        ConversationUI.SetActive(false);
        DialogueUI.SetActive(true);
    }

    public void ShowWDYTDialogue()
    {
        ConversationUI.SetActive(true);
        Image picture = ConversationUI.transform.Find("Background").Find("Portait").GetComponent<Image>();
        picture.sprite = ChosenNPC.Protrait;
        ConversationUI.transform.Find("TextBackground").Find("TalkingText").GetComponent<TextMeshProUGUI>().text = ChosenNPC.GetAboutText().text;
        DialogueUI.SetActive(false);
    }

    public void DontShowDialogueMenu()
    {
        Time.timeScale = 1.0f;
        DialogueUI.SetActive(false);
    }
}
