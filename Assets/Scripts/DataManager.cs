using System;
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

    public int TotalChipsSpend { get; set; }

    private void LoadChips()
    {
        for(int i = 0; i < 7; i ++)
        {
            amountOfLoadedChips += PlayerPrefs.GetInt("chips_recieveid_on_Level0" + i, 0);
        }

        ChipsAmount = amountOfLoadedChips;
        _chipsIsLoaded = true;
    }

    public void SaveTotalChipsSpend()
    {
        PlayerPrefs.SetInt("total_chips_spend", TotalChipsSpend);
    }

    public void SetTotalChipsSpend()
    {
        TotalChipsSpend = PlayerPrefs.GetInt("total_chips_spend");
    }

    public void SaveTowerParamsToPrefs()
    {
        foreach(TowerParameters towerParam in towerParameters)
        {
            ConvertTowerParametersToPrefs(towerParam);
        }
    }

    private void ConvertTowerParametersToPrefs(TowerParameters param)
    {
        string towerName = param.prefab.name;

        PlayerPrefs.SetString(towerName + "_" + "name", param.prefab.name);

        PlayerPrefs.SetInt(towerName + "_" + "cost", param.cost);
        PlayerPrefs.SetFloat(towerName + "_" + "fireRate", param.fireRate);
        PlayerPrefs.SetFloat(towerName + "_" + "range", param.range);
        PlayerPrefs.SetFloat(towerName + "_" + "damageOverTime", param.damageOverTime);
        PlayerPrefs.SetInt(towerName + "_" + "bulletDamage", param.bulletDamage);
        PlayerPrefs.SetFloat(towerName + "_" + "explosionRadius", param.explosionRadius);

        PlayerPrefs.SetInt(towerName + "_" + "upgradableToLvl2", Convert.ToInt32(param.upgradableToLvl2));
        PlayerPrefs.SetInt(towerName + "_" + "upgradableToLvl3", Convert.ToInt32(param.upgradableToLvl3));

        for(int i = 0; i < param.towerUprgradesBought.Length; i++)
        {
            PlayerPrefs.SetInt(towerName + "_" + "towerUprgradesBought" + "[" + i + "]", Convert.ToInt32(param.towerUprgradesBought[i]));
        }

        for (int i = 0; i < param.enemyTypes.Length; i++)
        {
            PlayerPrefs.SetString(towerName + "_" + "enemyTypes" + "[" + i + "]",param.enemyTypes[i]);
        }
    }
}
