using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

// NCN

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuObject;
    private Animator Transition;
    public bool GameIsPaused = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        PauseMenuObject.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    void PauseGame()
    {
        PauseMenuObject.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
    }

    public void QuitButton()
    {
        StartCoroutine(QuitGame());
    }

    public IEnumerator QuitGame()
    {
        PauseMenuObject.SetActive(false);
        Time.timeScale = 1.0f;
        Transition = GameObject.FindGameObjectWithTag("Transition").GetComponent<Animator>();
        Transition.SetTrigger("Start");

        yield return new WaitForSeconds(1.0f);

        DOLDatabase DOLD = GameObject.FindGameObjectWithTag("LoadManager").GetComponent<DOLDatabase>();
        DOLD.DestoryObjects();

        SceneManager.LoadScene("MainMenu");
    }
}
