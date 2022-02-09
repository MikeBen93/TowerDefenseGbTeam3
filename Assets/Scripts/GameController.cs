using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static bool GameIsOver;

    //public GameObject gameOverUI;
    //public GameObject completeLevelUI;


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
}
