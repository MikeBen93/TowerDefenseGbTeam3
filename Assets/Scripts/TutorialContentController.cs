using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialContentController : MonoBehaviour
{
    private GameObject[] tutorialPanels;
    private GameObject tutorialPanelToShow;
    private GameObject nextTutorialPanelToShow;

    private int _currentTutorialPanelNumber = 0;
    private int _tutorialPanelsAmount;
    private bool _tutorialShowed = false;

    private void Awake()
    {
        _tutorialPanelsAmount = transform.childCount;

        if (_tutorialPanelsAmount == 0) return;
        
        tutorialPanels = new GameObject[_tutorialPanelsAmount];

        for (int i = 0; i < _tutorialPanelsAmount; i++)
        {
            tutorialPanels[i] = transform.GetChild(i).gameObject;
        }
    }

    private void OnEnable()
    {
        Time.timeScale = 0;

        if (_tutorialPanelsAmount == 0) return;

        tutorialPanels[0].SetActive(true);

        for (int i = 1; i < _tutorialPanelsAmount; i++)
        {
            tutorialPanels[i].SetActive(false);
        }

    }


    public void ContinueTutorial()
    {
        tutorialPanels[_currentTutorialPanelNumber].SetActive(false);

        if (_currentTutorialPanelNumber < _tutorialPanelsAmount-1)
        {
            _currentTutorialPanelNumber++;

            tutorialPanels[_currentTutorialPanelNumber].SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            _tutorialShowed = true;
        }

    }
}
