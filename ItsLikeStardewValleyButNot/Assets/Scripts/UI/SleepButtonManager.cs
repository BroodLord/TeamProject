using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepButtonManager : MonoBehaviour
{
    public Clock cClock;
    public UpdateMoneyUI MoneyUpdate;
    public GameObject ReportUI;
    public PauseMenu PauseMenuScript;
    public GameObject SleepUI;

    // Function used to show a UI that asks the player if they would like to sleep
    public void SleepButton()
    {
        SleepUI.SetActive(false);
        ReportUI.SetActive(true);
        MoneyUpdate.UpdateText();
    }
    // If they player declines then we want to resume the current day
    public void ResumeDay()
    {
       PauseMenuScript.GameIsPaused = false;
       SleepUI.SetActive(false);
    }
    // Loads the next day and does any updates we need to do in the night
    public void NextDayButton()
    {
        cClock.NightUpdate();
        ReportUI.SetActive(false);
        PauseMenuScript.GameIsPaused = false;
    }
}
