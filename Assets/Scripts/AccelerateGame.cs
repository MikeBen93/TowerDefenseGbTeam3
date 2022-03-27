using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AccelerateGame : MonoBehaviour
{
    private bool isHeldDown = false;
    [SerializeField] private int accelerationSpeed = 3;

    private void Update()
    {
        if (GameController.GameIsOver || GameController.GameIsPaused)
        {
            return;
        }

        if ((!GameController.GameIsOver || !GameController.GameIsPaused) && isHeldDown)
        {
            Debug.Log("Start aceleration");
            Time.timeScale = accelerationSpeed;
            return;
        }

        if (!isHeldDown)
        {
            Time.timeScale = 1;
            return;
        }
    }

    public void onPress()
    {
        isHeldDown = true;
        Debug.Log(isHeldDown);
    }

    public void onRelease()
    {
        isHeldDown = false;
        Debug.Log(isHeldDown);
    }
}
