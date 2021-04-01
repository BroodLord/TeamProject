using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : NPCBase
{
    [SerializeField]
    private int NPCID;
    public Sprite Protrait;
    public string Name;
    public string[] IntroText;
    public string[] AboutText;
    public string[] DoingText;

    private void Start()
    {
        XMLParser XML = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<XMLParser>();
        foreach (var npc in XML.NPCs)
        {
            if (NPCID == npc.Key)
            {
                SetUp(npc.Value.Name, npc.Value.Protrait.name, npc.Value.IntroText, npc.Value.AboutText, npc.Value.DoingText);
            }
        }
    }

    public void SetUp(string name, string protrait, string[] introText, string[] aboutText, string[] doingText)
    {
        Name = name;
        // Loads all the sprite sheet into an array of sprites.
        Sprite[] Sprites;
        Sprites = Resources.LoadAll<Sprite>("XML Loaded Assets/" + "CGabrielFaces48x48");
        // Loop through all sprites and if we find the sprite then return it.
        for (int i = 0; i < Sprites.Length; i++)
        {
            if (Sprites[i].name == protrait)
                Protrait = Sprites[i];
        }
        IntroText = introText;
        AboutText = aboutText;
        DoingText = doingText;
    }
    public override TextMeshProUGUI GetIntroText()
    {
        int rand = Random.Range(0, IntroText.Length);
        TextMeshProUGUI text = new TextMeshProUGUI();
        text.text = IntroText[rand];
        return text;
    }
    public override TextMeshProUGUI GetAboutText()
    {
        int rand = Random.Range(0, AboutText.Length);
        TextMeshProUGUI text = new TextMeshProUGUI();
        text.text = AboutText[rand];
        return text;
    }
    public override TextMeshProUGUI GetDoingText()
    {
        int rand = Random.Range(0, DoingText.Length);
        TextMeshProUGUI text = new TextMeshProUGUI();
        text.text = DoingText[rand];
        return text;
    }
}
