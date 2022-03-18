using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CompleteLevelMenu : MonoBehaviour
{
    //public SceneFader sceneFader;
    public string mainMeneSceneName = "MainMenu";

    public string nextLevel = "MainMenu";
    public int levelToUnlock = 2;

    private void OnEnable()
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
    }

    public void Continue()
    {
        //sceneFader.FadeTo(nextLevel);
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextLevel);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMeneSceneName);
        //sceneFader.FadeTo(mainMeneSceneName);
    }

    public void LoadUpgrades()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Upgrades");
    }
}