using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayOptions : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject OptionsPanel;

    public void DisplayOptionsPanel()
    {
        MainPanel.SetActive(false);
        OptionsPanel.SetActive(true);
    }
}
