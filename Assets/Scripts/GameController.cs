using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool GameIsOver;
    public static bool GameIsPaused;
    public bool ShowTutorial = true;

    //public TutorialContentController tutorialController;
    public GameObject gameOverUI;
    public GameObject completeLevelUI;

    [SerializeField] private DialogueMenu dialogueMenu;
    [SerializeField] private Tutorial[] tutorials;

    private DataManager _dataManager;
    public int chipsRecievedOnLevel = 0;
    [SerializeField] private float _healthRatioRelatedToChips;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void Start()
    {
        _dataManager = DataManager.instance;
        GameIsOver = false;
    }
    private void Update()
    {

        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void DebugEndGame()
    {
        PlayerStats.Lives = 0;
    }

    public void DebugWinLevel()
    {
        WinLevel(mode:"debug");
    }

    public void WinLevel(string mode = "normal")
    {
        Time.timeScale = 0f;
        GameIsOver = true;
        
        if (PlayerStats.Lives == PlayerStats.initialLives) chipsRecievedOnLevel = 3;
        else if ((float)PlayerStats.Lives/PlayerStats.initialLives >= _healthRatioRelatedToChips) chipsRecievedOnLevel = 2;
        else chipsRecievedOnLevel = 1;
        
        //if (mode == "debug") chipsRecievedOnLevel = 3;

        int previouslyRecievedChips = PlayerPrefs.GetInt("chips_recieveid_on_" + SceneManager.GetActiveScene().name, 0);
        

        if (chipsRecievedOnLevel > previouslyRecievedChips)
        {
            PlayerPrefs.SetInt("chips_recieveid_on_" + SceneManager.GetActiveScene().name, chipsRecievedOnLevel);
        }
        else
        {
            PlayerPrefs.SetInt("chips_recieveid_on_" + SceneManager.GetActiveScene().name, previouslyRecievedChips);
        }

        _dataManager.ResetTotalChipsAmount();

        if (dialogueMenu != null && dialogueMenu.showDialogueMenu)
        {
            dialogueMenu.ActivateDialogue();

        } else
        {
            ShowWinWindow();
        }
    }

    public void ShowWinWindow()
    {
        completeLevelUI.SetActive(true);
    }

    public void ShowTutorialUI(string sceneName, int nextWaveNumber)
    {
        foreach (Tutorial tutorial in tutorials)
        {
            if (tutorial.sceneName == sceneName && tutorial.nextWaveNumber == nextWaveNumber && !tutorial.showed)
            {
                tutorial.showed = true;
                tutorial.tutorialContentController.gameObject.SetActive(true);
                //tutorialUI.GetComponent<TutorialMenu>().SetTutorialText(tutorial.tutorialText);

                Time.timeScale = 0;
                GameIsPaused = true;
                return;
            }
        }
    }
}
