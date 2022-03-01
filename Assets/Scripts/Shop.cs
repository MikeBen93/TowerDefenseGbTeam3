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

        laserTower.cost = GetTowerCostFromData(laserTower);
        plasmaTower.cost = GetTowerCostFromData(plasmaTower);
        electroTower.cost = GetTowerCostFromData(electroTower);
        magnitTower.cost = GetTowerCostFromData(magnitTower);
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

    private int GetTowerCostFromData(TowerBlueprint blueprint)
    {
        foreach(TowerParameters towParams in _dataManager.towerParameters)
        {
            if(blueprint.prefab == towParams.prefab)
            {
                return towParams.cost;
            }
        }

        return 0;
    }
}
