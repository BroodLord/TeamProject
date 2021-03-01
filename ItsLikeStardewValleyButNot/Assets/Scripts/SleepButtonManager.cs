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

    public void SleepButton()
    {
        SleepUI.SetActive(false);
        ReportUI.SetActive(true);
        MoneyUpdate.UpdateText();
    }

    public void ResumeDay()
    {
       PauseMenuScript.GameIsPaused = false;
       SleepUI.SetActive(false);
    }

    public void NextDayButton()
    {
        cClock.NightUpdate();
        ReportUI.SetActive(false);
        PauseMenuScript.GameIsPaused = false;
    }
}
