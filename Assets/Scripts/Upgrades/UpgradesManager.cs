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

    public void DeSelectUpgrade()
    {
        choosenUpgrade = null;
        towersToUpgrade = null;
        towersType = null;

        upgradeDescription.text = "";
    }

    public void ImplementUpgrade()
    {
        if (towersToUpgrade == null) return;

        foreach(GameObject tower in towersToUpgrade)
        {
            TowerParameters paramsToUpdate = SeekTowerParam(tower);
            UpgradeParameters(paramsToUpdate, choosenUpgrade.parametersToUpgrade);
        }

        _dataManager.ChipsAmount -= choosenUpgrade.upgradeCost;
        _dataManager.TotalChipsSpend += choosenUpgrade.upgradeCost;
        _dataManager.SaveTotalChipsSpend();

        DeSelectUpgrade();
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
        int power = 1;

        if (isReverseUpgrade) power = -1;

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

        paramsToUpgrade.cost = (int)(paramsToUpgrade.cost * Mathf.Pow((1 + paramsUpgrades.costChangePrc / 100.0f), power));
        paramsToUpgrade.fireRate = (paramsToUpgrade.fireRate * Mathf.Pow((1 + paramsUpgrades.fireRateChangePrc / 100.0f), power));
        paramsToUpgrade.range = (paramsToUpgrade.range * Mathf.Pow((1 + paramsUpgrades.rangeChangePrc / 100.0f), power));
        paramsToUpgrade.damageOverTime = (paramsToUpgrade.damageOverTime * Mathf.Pow((1 + paramsUpgrades.damageOverTimeChangePrc / 100.0f), power));
        paramsToUpgrade.bulletDamage = (int)(paramsToUpgrade.bulletDamage * Mathf.Pow((1 + paramsUpgrades.bulletDamageChangePrc / 100.0f), power));
        paramsToUpgrade.explosionRadius = (paramsToUpgrade.explosionRadius * Mathf.Pow((1 + paramsUpgrades.explosionRadiusChangePrc / 100.0f), power));

        if (!isReverseUpgrade)
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
