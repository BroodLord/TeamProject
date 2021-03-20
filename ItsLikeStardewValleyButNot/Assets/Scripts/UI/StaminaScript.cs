using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// NCN

public class StaminaScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider StamBar;
    private int MaxStam = 100;
    private int CurrentStam;
    public Clock cClock;

    void Start()
    {
        CurrentStam = MaxStam;
        StamBar.maxValue = MaxStam;
        StamBar.value = MaxStam;
    }

    public int GetStamina()
    {
        return CurrentStam;
    }

    public void Reset()
    {
        CurrentStam = MaxStam;
        StamBar.value = CurrentStam;
    }

    public void SetStamina(int Amount)
    {
        CurrentStam = Amount;
        StamBar.value = CurrentStam;
    }

    public void UseStamina(int Amount)
    {
        CurrentStam -= Amount;
        StamBar.value = CurrentStam;
        if (CurrentStam <= 0)
        {
            // Start the play transition
                SetStamina(75);
                cClock.NightUpdate();
                LoadLevel Load = GameObject.FindGameObjectWithTag("LoadManager").GetComponent<LoadLevel>();
                Load.TransferLevel("PlayerRoom", new Vector3(-1, 9, 0));
        }
    }

}

