using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public TowerBlueprint laserTower;
    public TowerBlueprint plasmaTower;

    public Button buyLaserTowerButton;
    public Button buyPlasmaTowerButton;

    public Text laserTowerCostText;
    public Text plasmaTowerCostText;

    private BuildManager _buildManager;

    private void Start()
    {
        _buildManager = BuildManager.instance;
    }

    private void Update()
    {
        laserTowerCostText.text = laserTower.cost.ToString() + "$";
        plasmaTowerCostText.text = plasmaTower.cost.ToString() + "$";

        buyLaserTowerButton.interactable = PlayerStats.Money >= laserTower.cost;
        buyPlasmaTowerButton.interactable = PlayerStats.Money >= plasmaTower.cost;
    }

    public void SelectLaserTower()
    {
        _buildManager.SelectTowerToBuild(laserTower);
    }

    public void SelectPlasmaTower()
    {
        _buildManager.SelectTowerToBuild(plasmaTower);
    }
}
