using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTabHandler : MonoBehaviour
{
    private bool ActiveUi;
    public GameObject UI;
    public GameObject ShowButton;
    public GameObject UnShowButton;

    //// Start is called before the first frame update
    void Start()
    {
        ActiveUi = false;
    }
    
    //// Update is called once per frame
    //void Update()
    //{
    //    
    //}

    public void EnableUi()
    {
        ActiveUi = true;
        UI.SetActive(true);
        ShowButton.SetActive(false);
        UnShowButton.SetActive(true);
    }

    public void DisableUi()
    {
        ActiveUi = false;
        UI.SetActive(false);
        ShowButton.SetActive(true);
        UnShowButton.SetActive(false);
    }
}
