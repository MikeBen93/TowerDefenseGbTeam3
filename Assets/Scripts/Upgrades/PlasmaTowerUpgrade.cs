using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlasmaTowerUpgrade : MonoBehaviour
{
    private DataManager _dataManager;

    [SerializeField] private GameObject towerPrefabLVL1;
    [SerializeField] private GameObject towerPrefabLVL2;
    [SerializeField] private GameObject towerPrefabLVL3;

    private TowerParameters tParamLVL1;
    private TowerParameters tParamLVL2;
    private TowerParameters tParamLVL3;

    [SerializeField] private Button firstUpgradeButton;
    [SerializeField] private Button secondUpgradeButton;
    [SerializeField] private Button thirdUpgradeButton;
    [SerializeField] private Button fourthUpgradeButton;

    private bool firstUprgradeBought = false;
    private bool secondUprgradeBought = false;
    private bool thirdUprgradeBought = false;
    private bool fourthUprgradeBought = false;

    [SerializeField] private int firstUpgradeCost;
    [SerializeField] private int secondUpgradeCost;
    [SerializeField] private int thirdUpgradeCost;
    [SerializeField] private int fourthUpgradeCost;

    [Header("Second Upgrade")]
    [SerializeField] private float costDecreasePrc;
    [Header("Third Upgrade")]
    [SerializeField] private float fireRateDecreasePrc;

    void Start()
    {
        _dataManager = DataManager.instance;

        SetData();
        CheckButtonInteractiablitiy();  
    }

    private void SetData()
    {
        tParamLVL1 = SeekTowerParam(towerPrefabLVL1);
        tParamLVL2 = SeekTowerParam(towerPrefabLVL2);
        tParamLVL3 = SeekTowerParam(towerPrefabLVL3);

        firstUprgradeBought = tParamLVL1.firstUprgradeBought;
        secondUprgradeBought = tParamLVL1.secondtUprgradeBought;
        thirdUprgradeBought = tParamLVL1.thirdUprgradeBought;
        fourthUprgradeBought = tParamLVL1.fourthUprgradeBought;
    }

    public void CheckButtonInteractiablitiy()
    {
        if (_dataManager.ChipsAmount >= firstUpgradeCost && !firstUprgradeBought)
        {
            firstUpgradeButton.interactable = true;
        }
        else
        {
            firstUpgradeButton.interactable = false;
        }

        if (_dataManager.ChipsAmount >= secondUpgradeCost && firstUprgradeBought && !secondUprgradeBought)
        {
            secondUpgradeButton.interactable = true;
        }
        else
        {
            secondUpgradeButton.interactable = false;
        }

        if (_dataManager.ChipsAmount >= thirdUpgradeCost && secondUprgradeBought && !thirdUprgradeBought)
        {
            thirdUpgradeButton.interactable = true;
        }
        else
        {
            thirdUpgradeButton.interactable = false;
        }

        if (_dataManager.ChipsAmount >= fourthUpgradeCost && thirdUprgradeBought && !fourthUprgradeBought)
        {
            fourthUpgradeButton.interactable = true;
        }
        else
        {
            fourthUpgradeButton.interactable = false;
        }
    }

    private TowerParameters SeekTowerParam(GameObject prefabToSeek)
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

    public void BuyFirstUpgrade()
    {

        if (tParamLVL1 != null && tParamLVL2 != null && tParamLVL3 != null)
        {
            tParamLVL1.upgradableToLvl2 = tParamLVL2.upgradableToLvl2 = tParamLVL3.upgradableToLvl2 = true;
            tParamLVL1.firstUprgradeBought = tParamLVL2.firstUprgradeBought = tParamLVL3.firstUprgradeBought = true;

            firstUprgradeBought = true;
        }
        else Debug.LogError("Tower doesn't set to TowerParameters in DataManager");

        _dataManager.ChipsAmount = _dataManager.ChipsAmount - firstUpgradeCost;
    }

    public void BuySecondUpgrade()
    {
        if (tParamLVL1 != null && tParamLVL2 != null && tParamLVL3 != null)
        {
            tParamLVL1.cost = (int)(tParamLVL1.cost * (1 - costDecreasePrc / 100.0f));
            tParamLVL2.cost = (int)(tParamLVL2.cost * (1 - costDecreasePrc / 100.0f)); 
            tParamLVL3.cost = (int)(tParamLVL3.cost * (1 - costDecreasePrc / 100.0f));

            tParamLVL1.secondtUprgradeBought = tParamLVL2.secondtUprgradeBought = tParamLVL3.secondtUprgradeBought = true;

            secondUprgradeBought = true;
        }
        else Debug.LogError("Tower doesn't set to TowerParameters in DataManager");

        _dataManager.ChipsAmount = _dataManager.ChipsAmount - secondUpgradeCost;
    }

    public void BuyThirdUpgrade()
    {
        if (tParamLVL1 != null && tParamLVL2 != null && tParamLVL3 != null)
        {
            tParamLVL1.fireRate = tParamLVL1.fireRate * (1 - fireRateDecreasePrc / 100.0f);
            tParamLVL2.fireRate = tParamLVL2.fireRate * (1 - fireRateDecreasePrc / 100.0f);
            tParamLVL3.fireRate = tParamLVL3.fireRate * (1 - fireRateDecreasePrc / 100.0f);

            tParamLVL1.thirdUprgradeBought = tParamLVL2.thirdUprgradeBought = tParamLVL3.thirdUprgradeBought = true;

            thirdUprgradeBought = true;
        }
        else Debug.LogError("Tower doesn't set to TowerParameters in DataManager");

        _dataManager.ChipsAmount = _dataManager.ChipsAmount - thirdUpgradeCost;
    }

    public void BuyFourthUpgrade()
    {
        if (tParamLVL1 != null && tParamLVL2 != null && tParamLVL3 != null)
        {
            tParamLVL1.upgradableToLvl3 = tParamLVL2.upgradableToLvl3 = tParamLVL3.upgradableToLvl3 = true;
            tParamLVL1.fourthUprgradeBought = tParamLVL2.fourthUprgradeBought = tParamLVL3.fourthUprgradeBought = true;

            fourthUprgradeBought = true;
        }
        else Debug.LogError("Tower doesn't set to TowerParameters in DataManager");

        _dataManager.ChipsAmount = _dataManager.ChipsAmount - fourthUpgradeCost;
    }
}
