using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector3 positionOffset; // offset of the tower relative to node position

    //[HideInInspector]
    public GameObject towerObj;
    //[HideInInspector]
    public Tower tower;
    //[HideInInspector]
    public TowerBlueprint towerBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Color defaultColor;
    private Renderer rend;
    private BuildManager _buildManager;
    //[HideInInspector]
    public int towerNextlevel;
    //[HideInInspector]
    public int upgradeCostToNextLevel;



    private void Start()
    {
        rend = GetComponent<Renderer>();
        defaultColor = rend.material.color;
        _buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    public string TryToBuildTower()
    {
        if (!_buildManager.CanBuild)
        {
            return "Tower is not choosen";
        }

        if (towerObj != null)
        {
            return "Tower is already build";
        }

        if (!_buildManager.HasMoney)
        {
            return "Not enough money";
        }
        TowerBlueprint towerToBuild = _buildManager.GetTowerToBuild();
        BuildTower(towerToBuild);
        //ChangeNodeColor(towerToBuild.prefab.GetComponent<Tower>().enemyTag);

        return "Tower built";
    }

    private void BuildTower(TowerBlueprint blueprint)
    {
        PlayerStats.Money -= blueprint.cost;

        towerObj = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);

        tower = towerObj.GetComponent<Tower>();

        tower.SetNewParameters(blueprint, 1);
        towerNextlevel = 2;
        upgradeCostToNextLevel = blueprint.lvl2Cost;

        towerBlueprint = blueprint;
    }

    public void SellTower()
    {
        PlayerStats.Money += towerBlueprint.GetSellAmount(tower.CurrentLevel);
        rend.material.color = defaultColor;
        Destroy(towerObj);
        towerBlueprint = null;
    }

    public bool TowerIsUpgradable()
    {
        Tower towerToCheck = tower.GetComponent<Tower>();

        if (!towerToCheck.IsUpgradable())
        {
            return false;
        }

        if (towerNextlevel == 2 && PlayerStats.Money >= towerBlueprint.lvl2Cost)
        {
            return true;
        } 
        else if (towerNextlevel == 3 && PlayerStats.Money >= towerBlueprint.lvl3Cost)
        {
            return true;
        }

        return false;
    }

    public void UpgradeTower()
    {
        Destroy(towerObj);
        if (towerNextlevel == 2)
        {
            PlayerStats.Money -= towerBlueprint.lvl2Cost;
            towerObj = Instantiate(towerBlueprint.lvl2Prefab, GetBuildPosition(), Quaternion.identity);
            tower = towerObj.GetComponent<Tower>();
            tower.SetNewParameters(towerBlueprint, 2);
            towerNextlevel = 3;
            upgradeCostToNextLevel = towerBlueprint.lvl2Cost;
            return;
        } else if (towerNextlevel == 3)
        {
            PlayerStats.Money -= towerBlueprint.lvl3Cost;
            towerObj = Instantiate(towerBlueprint.lvl3Prefab, GetBuildPosition(), Quaternion.identity);
            tower = towerObj.GetComponent<Tower>();
            tower.SetNewParameters(towerBlueprint, 3);
            towerNextlevel = 0;
            return;
        }
    }
}
