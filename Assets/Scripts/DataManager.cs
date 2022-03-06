using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    private int _currentChipsAmount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public TowerParameters[] towerParameters;
    public int ChipsAmount { 
        get { return _currentChipsAmount; } 
        set { _currentChipsAmount = value; } 
    }
}
