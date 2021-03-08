using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class WindowManager : MonoBehaviour
{

    public GameObject MainPanel;
    public GameObject OptionsPanel;

    #region Attributes

    #region Player Pref Key Constants

    private const string RESOULTION_PREF_KEY = "Resolution";

    #endregion

    #region Resolution

    [SerializeField]
    private TextMeshProUGUI ResolutionText;
    private Resolution[] Resolutions;
    private int CurrentResolutionIndex = 0;

    #endregion

    #endregion

    #region Resolution Cycling

    // Sets the text to be the current resolution
    private void SetResolutionText(Resolution Resolution)
    {
        ResolutionText.text = Resolution.width + "x" + Resolution.height;
    }

    // Sets the next resolution
    public void SetNextResolution()
    {
        CurrentResolutionIndex = GetNextWrappedIndex(Resolutions, CurrentResolutionIndex);
        SetResolutionText(Resolutions[CurrentResolutionIndex]);
    }
    // Set the last resolution
    public void SetLastResolution()
    {
        CurrentResolutionIndex = GetLastWrappedIndex(Resolutions, CurrentResolutionIndex);
        SetResolutionText(Resolutions[CurrentResolutionIndex]);
    }

    #endregion

    #region Apply Res

    // sets and Applies the resoultion 
    private void SetAndApplyRes(int NewResoultionIndex)
    {
        CurrentResolutionIndex = NewResoultionIndex;
        ApplyCurrentResolution();
    }

    private void ApplyCurrentResolution()
    {
        ApplyRes(Resolutions[CurrentResolutionIndex]);
    }

    private void ApplyRes(Resolution Resolution)
    {
        SetResolutionText(Resolution);
        Screen.SetResolution(Resolution.width, Resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt(RESOULTION_PREF_KEY, CurrentResolutionIndex);
    }

    #endregion

    #region Helpers

    #region Index Wrap Helpers
    /* Ok so these allow the user to wrap around the whole array, so can go from 1 in the array to 16th or something. */
    private int GetNextWrappedIndex<T>(IList<T> Collection, int CurrentIndex)
    {
        if (Collection.Count < 1) return 0;
        return (CurrentIndex + 1) % Collection.Count;
    }

    private int GetLastWrappedIndex<T>(IList<T> Collection, int CurrentIndex)
    {
        if (Collection.Count < 1) return 0;
        if ((CurrentIndex - 1) < 0) return Collection.Count - 1;
        return (CurrentIndex - 1) % Collection.Count;
    }
    /*****************************************************************************/
    #endregion

    #endregion
    // Gets the current res and sets up the text and res for the game
    private void Start()
    {
        Resolutions = Screen.resolutions;
        CurrentResolutionIndex = PlayerPrefs.GetInt(RESOULTION_PREF_KEY, 0);

        SetResolutionText(Resolutions[CurrentResolutionIndex]);
    }

    // Apply changes to the screen
    public void ApplyChanges()
    {
        MainPanel.SetActive(true);
        OptionsPanel.SetActive(false);
        SetAndApplyRes(CurrentResolutionIndex);
    }

    // Sets the game to fullscreen
    public void SetFullScreen(bool IsFullScreen)
    {
        Screen.fullScreen = IsFullScreen;
    }
}
