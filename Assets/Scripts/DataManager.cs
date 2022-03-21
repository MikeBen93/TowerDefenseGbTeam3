using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    private int _currentChipsAmount;
    private bool _chipsIsLoaded = false;
    private int amountOfLoadedChips = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            if(!_chipsIsLoaded)
            {
                LoadChips();
            }
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

    private void LoadChips()
    {
        for(int i = 0; i < 7; i ++)
        {
            amountOfLoadedChips += PlayerPrefs.GetInt("chips_recieveid_on_Level0" + i, 0);
        }

        ChipsAmount = amountOfLoadedChips;
        _chipsIsLoaded = true;
    }
}
