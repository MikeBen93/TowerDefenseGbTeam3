using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackOnScenes : MonoBehaviour
{
    public void LoadLevelSelector()
    {
        SceneManager.LoadScene("LevelSelecter");
    }
}
