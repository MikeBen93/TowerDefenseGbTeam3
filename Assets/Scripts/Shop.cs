using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    //public TowerBlueprint standardCupid;
    //public TowerBlueprint rocketCupid;
    public TowerBlueprint redLaserCupid;
    public TowerBlueprint blueLaserCupid;

    //public Button standardCupidButton;
    //public Button rocketCupidButton;
    public Button redLaserCupidButton;
    public Button blueLaserCupidButton;

    //public Text standardCupidCostText;
    //public Text rocketCupidCostText;
    //public Text laserCupidCostText;

    private BuildManager _buildManager;

    private void Start()
    {
        _buildManager = BuildManager.instance;
    }

    private void Update()
    {
        //standardCupidButton.enabled = PlayerStats.Money >= standardCupid.cost;
        //rocketCupidButton.enabled = PlayerStats.Money >= rocketCupid.cost;
        redLaserCupidButton.interactable = PlayerStats.Money >= redLaserCupid.cost;
        blueLaserCupidButton.interactable = PlayerStats.Money >= blueLaserCupid.cost;
    }

    //public void SelectStandardCupid()
    //{
    //    _buildManager.SelectTowerToBuild(standardCupid);
    //}

    //public void SelectRocketCupid()
    //{
    //    _buildManager.SelectTowerToBuild(rocketCupid);
    //}

    public void SelectRedLaserCupid()
    {
        _buildManager.SelectTowerToBuild(redLaserCupid);
    }

    public void SelectBlueLaserCupid()
    {
        _buildManager.SelectTowerToBuild(blueLaserCupid);
    }
}
