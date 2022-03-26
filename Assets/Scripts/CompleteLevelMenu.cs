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

    [SerializeField] private GameObject[] chipsObjectsToShow;
    private DataManager _dataManager;

    private void OnEnable()
    {
        _dataManager = DataManager.instance;

        if (levelToUnlock > PlayerPrefs.GetInt("levelReached", 0))
        {
            PlayerPrefs.SetInt("levelReached", levelToUnlock);
        }

        int recievedChipsOnLevel = PlayerPrefs.GetInt("chips_recieveid_on_" + SceneManager.GetActiveScene().name, 0);

        for (int i = 0; i < chipsObjectsToShow.Length; i++)
        {
            if (i < recievedChipsOnLevel)
            {
                chipsObjectsToShow[i].SetActive(true);
            }
            else
            {
                chipsObjectsToShow[i].SetActive(false);
            }
        }
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