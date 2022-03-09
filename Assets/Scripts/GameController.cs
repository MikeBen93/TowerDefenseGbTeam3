using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool GameIsOver;
    public bool ShowTutorial = true;

    public GameObject tutorialUI;
    public GameObject gameOverUI;
    public GameObject completeLevelUI;

    [SerializeField] private DialogueMenu dialogueMenu;
    [SerializeField] private Tutorial[] tutorials;

    private DataManager _dataManager;
    public int chipsRecievedOnLevel;
    [SerializeField] private float _healthRatioRelatedToChips;


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
    }

    public void WinLevel()
    {
        GameIsOver = true;
        if (PlayerStats.Lives == PlayerStats.initialLives) chipsRecievedOnLevel = 3;
        else if (PlayerStats.Lives/ PlayerStats.initialLives >= _healthRatioRelatedToChips) chipsRecievedOnLevel = 2;
        else chipsRecievedOnLevel = 1;

        PlayerPrefs.SetInt("chips_recieveid_on_" + SceneManager.GetActiveScene().name, chipsRecievedOnLevel);

        _dataManager.ChipsAmount = _dataManager.ChipsAmount + chipsRecievedOnLevel;

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
                tutorialUI.SetActive(true);
                tutorialUI.GetComponent<TutorialMenu>().SetTutorialText(tutorial.tutorialText);

                Time.timeScale = 0;
                return;
            }
        }
    }
}
