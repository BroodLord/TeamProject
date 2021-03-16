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

    public void UseStamina(int Amount)
    {
        CurrentStam -= Amount;
        StamBar.value = CurrentStam;
    }

}

