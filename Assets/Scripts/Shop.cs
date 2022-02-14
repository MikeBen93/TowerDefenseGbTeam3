using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public TowerBlueprint redLaserCupid;
    public TowerBlueprint blueLaserCupid;

    public Button redLaserCupidButton;
    public Button blueLaserCupidButton;

    public Text redLaserCupidCostText;
    public Text blueLaserCupidCostText;

    private BuildManager _buildManager;

    private void Start()
    {
        _buildManager = BuildManager.instance;
    }

    private void Update()
    {
        redLaserCupidCostText.text = redLaserCupid.cost.ToString() + "$";
        blueLaserCupidCostText.text = blueLaserCupid.cost.ToString() + "$";

        redLaserCupidButton.interactable = PlayerStats.Money >= redLaserCupid.cost;
        blueLaserCupidButton.interactable = PlayerStats.Money >= blueLaserCupid.cost;
    }

    public void SelectRedLaserCupid()
    {
        _buildManager.SelectTowerToBuild(redLaserCupid);
    }

    public void SelectBlueLaserCupid()
    {
        _buildManager.SelectTowerToBuild(blueLaserCupid);
    }
}
