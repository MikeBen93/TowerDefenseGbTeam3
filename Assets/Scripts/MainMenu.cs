using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "Level01";
    //public SceneFader sceneFader;
    public void Play()
    {
        SceneManager.LoadScene(levelToLoad);
        //sceneFader.FadeTo(levelToLoad);
    }

    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}