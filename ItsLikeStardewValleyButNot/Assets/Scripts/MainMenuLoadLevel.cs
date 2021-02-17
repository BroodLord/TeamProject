using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuLoadLevel : MonoBehaviour
{
    public string Name;
    public void LoadLevel()
    {
        SceneManager.LoadScene(Name);
    }
}
