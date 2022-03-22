using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowersUpgrade : MonoBehaviour
{
    public string towersTypeName;

    [SerializeField] private GameObject[] towersVariant;
    [SerializeField] private SpecificUpgrade[] upgrades;

    [SerializeField] private Button[] upgradeButtons;

    private UpgradesManager _upgradesManager;
    private DataManager _dataManager;

    private void Awake()
    {
        _dataManager = DataManager.instance;
        _upgradesManager = UpgradesManager.instance;
    }

    private void Start()
    {
        GetDataFromDataManager();
    }

    private void Update()
    {
        CheckButtonInteractbility();
    }

    public void GetDataFromDataManager()
    {
        foreach(GameObject tower in towersVariant)
        {
            TowerParameters parameters = _upgradesManager.SeekTowerParam(tower);

            if(parameters == null || parameters.towerUprgradesBought.Length == 0)
            {
                Debug.LogError($"Can't find TowerParameters for following tower {tower.name}");
                return;
            }

            if(parameters.towerUprgradesBought.Length == 0)
            {
                Debug.LogError($"Empty array towerUprgradesBought in TowerParameters for following tower {tower.name}");
                return;
            }

            if (parameters.towerUprgradesBought.Length != upgrades.Length)
            {
                Debug.LogError($"Array lemgth of towerUprgradesBought in TowerParameters isnn't equal to upgrades.Length for following tower {tower.name}");
                return;
            }

            for (int i = 0; i < upgrades.Length; i++)
            {
                upgrades[i].upgradeBought = parameters.towerUprgradesBought[i];
            }
        }
    }

    public void SelectUpgrade(int number)
    {
        if(number > upgrades.Length + 1)
        {
            Debug.LogError("Incorrect upgrade numver");
            return;
        }

        _upgradesManager.SelectUpgrade(number, upgrades[number], towersVariant, towersTypeName);
    }

    public void CheckButtonInteractbility()
    {
        for (int i = 0; i < upgrades.Length; i++)
        {
            if (i == 0 && upgrades[i].upgradeCost >= _dataManager.ChipsAmount && !upgrades[i].upgradeBought)
            {
                upgradeButtons[i].interactable = true;
            } 
            else if(i > 0 && upgrades[i].upgradeCost >= _dataManager.ChipsAmount && !upgrades[i].upgradeBought && upgrades[i-1].upgradeBought)
            {
                upgradeButtons[i].interactable = true;
            }
            else
            {
                upgradeButtons[i].interactable = false;
            }
        }
        
    }
}
