using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public TowerBlueprint laserTower;
    public TowerBlueprint plasmaTower;
    public TowerBlueprint electroTower;
    public TowerBlueprint magnitTower;

    public Button buyLaserTowerButton;
    public Button buyPlasmaTowerButton;
    public Button buyElectroTowerButton;
    public Button buyMagnitTowerButton;

    public Text laserTowerCostText;
    public Text plasmaTowerCostText;
    public Text electroTowerCostText;
    public Text magnitTowerCostText;

    private BuildManager _buildManager;
    private DataManager _dataManager;

    private void Start()
    {
        _buildManager = BuildManager.instance;
        _dataManager = DataManager.instance;

        SetTowerCosts(laserTower);
        SetTowerCosts(plasmaTower);
        SetTowerCosts(electroTower);
        SetTowerCosts(magnitTower);
    }

    private void Update()
    {
        laserTowerCostText.text = laserTower.cost.ToString();
        plasmaTowerCostText.text = plasmaTower.cost.ToString();
        electroTowerCostText.text = electroTower.cost.ToString();
        magnitTowerCostText.text = magnitTower.cost.ToString();

        buyLaserTowerButton.interactable = PlayerStats.Money >= laserTower.cost;
        buyPlasmaTowerButton.interactable = PlayerStats.Money >= plasmaTower.cost;
        buyElectroTowerButton.interactable = PlayerStats.Money >= electroTower.cost;
        buyMagnitTowerButton.interactable = PlayerStats.Money >= magnitTower.cost;
    }

    public void SelectLaserTower()
    {
        _buildManager.SelectTowerToBuild(laserTower);
    }

    public void SelectPlasmaTower()
    {
        _buildManager.SelectTowerToBuild(plasmaTower);
    }

    public void SelectElectroTower()
    {
        _buildManager.SelectTowerToBuild(electroTower);
    }

    public void SelectMagnitTower()
    {
        _buildManager.SelectTowerToBuild(magnitTower);
    }

    private void SetTowerCosts(TowerBlueprint towerBl)
    {
        GameObject lvl1Pref = towerBl.prefab;
        GameObject lvl2Pref = towerBl.lvl2Prefab;
        GameObject lvl3Pref = towerBl.lvl3Prefab;
        foreach (TowerParameters towParams in _dataManager.towerParameters)
        {
            if (lvl1Pref == towParams.prefab)
            {
                towerBl.cost = towParams.cost;
            } else if (lvl2Pref == towParams.prefab)
            {
                towerBl.lvl2Cost = towParams.cost;
            }
            else if (lvl3Pref == towParams.prefab)
            {
                towerBl.lvl3Cost = towParams.cost;
            }
        }
    }
}
