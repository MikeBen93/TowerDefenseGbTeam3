using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager instance;

    private SpecificUpgrade choosenUpgrade;
    private GameObject[] towersToUgrade;
    private string towersType;
    private DataManager _dataManager;
    private int upgradeNumberInOrder;

    [SerializeField] private Text upgradeDescription;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one UpgradesManager in scene");
            return;
        }
        instance = this;

        _dataManager = DataManager.instance;
    }

    public void SelectUpgrade(int upgradeNumber,SpecificUpgrade upgrade, GameObject[] towers, string type)
    {
        upgradeNumberInOrder = upgradeNumber;
        choosenUpgrade = upgrade;
        towersToUgrade = towers;
        towersType = type;

        upgradeDescription.text = choosenUpgrade.upgradeDescription;
    }

    public void ImplementUpgrade()
    {
        foreach(GameObject tower in towersToUgrade)
        {
            TowerParameters paramsToUpdate = SeekTowerParam(tower);
            UpgradeParameters(paramsToUpdate, choosenUpgrade.parametersToUpgrade);
        }
    }

    public TowerParameters SeekTowerParam(GameObject prefabToSeek)
    {
        foreach (TowerParameters towerParam in _dataManager.towerParameters)
        {
            if (towerParam.prefab == prefabToSeek)
            {
                return towerParam;
            }
        }

        return null;
    }

    private void UpgradeParameters(TowerParameters paramsToUpgrade, TowerParametersUpgrade paramsUpgrades)
    {
        if(paramsUpgrades.upgradableToLvl2)
        {
            paramsToUpgrade.upgradableToLvl2 = true;
        }

        if (paramsUpgrades.upgradableToLvl3)
        {
            paramsToUpgrade.upgradableToLvl3 = true;
        }

        paramsToUpgrade.cost = (int)(paramsToUpgrade.cost * (1 + paramsUpgrades.costChangePrc / 100.0f));
        paramsToUpgrade.fireRate = (paramsToUpgrade.fireRate * (1 + paramsUpgrades.fireRateChangePrc / 100.0f));
        paramsToUpgrade.range = (paramsToUpgrade.range * (1 + paramsUpgrades.rangeChangePrc / 100.0f));
        paramsToUpgrade.damageOverTime = (paramsToUpgrade.damageOverTime * (1 + paramsUpgrades.damageOverTimeChangePrc / 100.0f));
        paramsToUpgrade.bulletDamage = (int)(paramsToUpgrade.bulletDamage * (1 + paramsUpgrades.bulletDamageChangePrc / 100.0f));
        paramsToUpgrade.explosionRadius = (paramsToUpgrade.explosionRadius * (1 + paramsUpgrades.explosionRadiusChangePrc / 100.0f));

        paramsToUpgrade.towerUprgradesBought[upgradeNumberInOrder] = choosenUpgrade.upgradeBought = true;
        
    }

}
