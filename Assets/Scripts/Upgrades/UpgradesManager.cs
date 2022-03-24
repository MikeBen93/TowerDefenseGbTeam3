using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager instance;

    private SpecificUpgrade choosenUpgrade;
    private GameObject[] towersToUpgrade;
    private string towersType;
    private DataManager _dataManager;
    private int upgradeNumberInOrder;

    [SerializeField] private Text upgradeDescription;

    [SerializeField] private TowersUpgrade[] allUpgrades;

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
        towersToUpgrade = towers;
        towersType = type;

        upgradeDescription.text = choosenUpgrade.upgradeDescription;
    }

    public void ImplementUpgrade()
    {
        foreach(GameObject tower in towersToUpgrade)
        {
            TowerParameters paramsToUpdate = SeekTowerParam(tower);
            UpgradeParameters(paramsToUpdate, choosenUpgrade.parametersToUpgrade);
        }

        _dataManager.ChipsAmount -= choosenUpgrade.upgradeCost;
        _dataManager.TotalChipsSpend += choosenUpgrade.upgradeCost;
        _dataManager.SaveTotalChipsSpend();
    }
    private void ReverseUpgrade()
    {
        foreach (GameObject tower in towersToUpgrade)
        {
            TowerParameters paramsToRev = SeekTowerParam(tower);
            UpgradeParameters(paramsToRev, choosenUpgrade.parametersToUpgrade, true);
        }

        _dataManager.ChipsAmount += choosenUpgrade.upgradeCost;
        
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

    private void UpgradeParameters(TowerParameters paramsToUpgrade, TowerParametersUpgrade paramsUpgrades, bool isReverseUpgrade = false)
    {
        int multiplier = 1;

        if (isReverseUpgrade) multiplier = -1;

        if (paramsUpgrades.upgradableToLvl2 && !isReverseUpgrade)
        {
            paramsToUpgrade.upgradableToLvl2 = true;
        } 
        else if (paramsUpgrades.upgradableToLvl2 && isReverseUpgrade)
        {
            paramsToUpgrade.upgradableToLvl2 = false;
        }

        if (paramsUpgrades.upgradableToLvl3 && !isReverseUpgrade)
        {
            paramsToUpgrade.upgradableToLvl3 = true;
        }
        else if (paramsUpgrades.upgradableToLvl3 && isReverseUpgrade)
        {
            paramsToUpgrade.upgradableToLvl3 = false;
        }

        paramsToUpgrade.cost = (int)(paramsToUpgrade.cost * (1 + multiplier * paramsUpgrades.costChangePrc / 100.0f));
        paramsToUpgrade.fireRate = (paramsToUpgrade.fireRate * (1 + multiplier * paramsUpgrades.fireRateChangePrc / 100.0f));
        paramsToUpgrade.range = (paramsToUpgrade.range * (1 + multiplier * paramsUpgrades.rangeChangePrc / 100.0f));
        paramsToUpgrade.damageOverTime = (paramsToUpgrade.damageOverTime * (1 + multiplier * paramsUpgrades.damageOverTimeChangePrc / 100.0f));
        paramsToUpgrade.bulletDamage = (int)(paramsToUpgrade.bulletDamage * (1 + multiplier * paramsUpgrades.bulletDamageChangePrc / 100.0f));
        paramsToUpgrade.explosionRadius = (paramsToUpgrade.explosionRadius * (1 + multiplier * paramsUpgrades.explosionRadiusChangePrc / 100.0f));

        if(!isReverseUpgrade)
        {
            paramsToUpgrade.towerUprgradesBought[upgradeNumberInOrder] = choosenUpgrade.upgradeBought = true;
        }
        else
        {
            paramsToUpgrade.towerUprgradesBought[upgradeNumberInOrder] = choosenUpgrade.upgradeBought = false;
        }

        _dataManager.SaveTowerParametersToPrefs(paramsToUpgrade);
    }

    public void ResetUpgrades()
    {
        foreach(TowersUpgrade speceificTowerTypeReverse in allUpgrades)
        {
            GameObject[] towersGOToReverse = speceificTowerTypeReverse.towersVariant;

            for(int i = 0; i < speceificTowerTypeReverse.upgrades.Length;  i++)
            {
                if (speceificTowerTypeReverse.upgrades[i].upgradeBought)
                {
                    upgradeNumberInOrder = i;
                    choosenUpgrade = speceificTowerTypeReverse.upgrades[i];
                    towersToUpgrade = towersGOToReverse;
                    towersType = speceificTowerTypeReverse.towersTypeName;

                    ReverseUpgrade();
                }
            }
        }

        _dataManager.TotalChipsSpend = 0;
        _dataManager.SaveTotalChipsSpend();
        _dataManager.SaveAllTowerParametersToPrefs();
    }

}
