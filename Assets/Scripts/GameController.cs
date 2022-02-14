using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static bool GameIsOver;
    public bool ShowTutorial = true;

    public GameObject tutorialUI;
    //public GameObject gameOverUI;
    //public GameObject completeLevelUI;

    [SerializeField] private Tutorial[] tutorials;


    private void Start()
    {
        GameIsOver = false;
    }
    private void Update()
    {

        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }

        //if (Input.GetKeyDown("e")) EndGame();

    }

    private void EndGame()
    {
        GameIsOver = true;
        Debug.Log("GAME IS OVER");
        //gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        GameIsOver = true;
        Debug.Log("GAME IS OVER");
        //completeLevelUI.SetActive(true);
    }

    public void ShowTutorialUI(string sceneName, int nextWaveNumber)
    {
        foreach (Tutorial tutorial in tutorials)
        {
            if (tutorial.sceneName == sceneName && tutorial.nextWaveNumber == nextWaveNumber && !tutorial.showed)
            {
                tutorial.showed = true;
                tutorialUI.SetActive(true);
                tutorialUI.GetComponent<TutorialMenu>().SetTutorialText(tutorial.tutorialText);

                Time.timeScale = 0;
                return;
            }
        }
    }
}
