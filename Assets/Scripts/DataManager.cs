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
    private bool _towerParamsIsLoaded = false;
    private int amountOfLoadedChips = 0;

    public int chipsTraining;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            if(!_chipsIsLoaded)
            {
                LoadChips();
            }

            if(!_towerParamsIsLoaded)
            {
                CheckCurrentTowerParametersInPrefs();
            }
        } 
        else if (instance != this)
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
        SetTotalChipsSpend();
        ResetTotalChipsAmount();

        _chipsIsLoaded = true;
    }

    public void ResetTotalChipsAmount()
    {
        amountOfLoadedChips = 0;

        for (int i = 1; i <= 7; i++)
        {
            amountOfLoadedChips += PlayerPrefs.GetInt("chips_recieveid_on_Level0" + i, 0);
        }

        ChipsAmount = amountOfLoadedChips - TotalChipsSpend;
    }

    public void SaveTotalChipsSpend()
    {
        PlayerPrefs.SetInt("total_chips_spend", TotalChipsSpend);
    }

    public void SetTotalChipsSpend()
    {
        TotalChipsSpend = PlayerPrefs.GetInt("total_chips_spend");
    }

    public void SaveAllTowerParametersToPrefs()
    {
        foreach(TowerParameters towerParam in towerParameters)
        {
            SaveTowerParametersToPrefs(towerParam);
        }
    }

    private void CheckCurrentTowerParametersInPrefs()
    {
        foreach (TowerParameters towerParam in towerParameters)
        {
            string towerName = towerParam.prefab.name;
            
            if(PlayerPrefs.GetString(towerName + "_" + "name", " ") == " ")
            {
                SaveTowerParametersToPrefs(towerParam);
            } else
            {
                GetTowerParametersFromPrefs(towerParam);
            }
        }

        _towerParamsIsLoaded = true;
    }

    public void SaveTowerParametersToPrefs(TowerParameters param)
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


    public void GetTowerParametersFromPrefs(TowerParameters param)
    {
        string towerName = param.prefab.name;

        PlayerPrefs.GetString(towerName + "_" + "name");

        param.cost = PlayerPrefs.GetInt(towerName + "_" + "cost");
        param.fireRate = PlayerPrefs.GetFloat(towerName + "_" + "fireRate");
        param.range = PlayerPrefs.GetFloat(towerName + "_" + "range");
        param.damageOverTime = PlayerPrefs.GetFloat(towerName + "_" + "damageOverTime");
        param.bulletDamage = PlayerPrefs.GetInt(towerName + "_" + "bulletDamage");
        param.explosionRadius = PlayerPrefs.GetFloat(towerName + "_" + "explosionRadius");

        param.upgradableToLvl2 = Convert.ToBoolean(PlayerPrefs.GetInt(towerName + "_" + "upgradableToLvl2"));
        param.upgradableToLvl3 = Convert.ToBoolean(PlayerPrefs.GetInt(towerName + "_" + "upgradableToLvl3"));

        for (int i = 0; i < param.towerUprgradesBought.Length; i++)
        {
            param.towerUprgradesBought[i] = Convert.ToBoolean(PlayerPrefs.GetInt(towerName + "_" + "towerUprgradesBought" + "[" + i + "]"));
        }

        for (int i = 0; i < param.enemyTypes.Length; i++)
        {
            param.enemyTypes[i] = PlayerPrefs.GetString(towerName + "_" + "enemyTypes" + "[" + i + "]");
        }
    }

}
