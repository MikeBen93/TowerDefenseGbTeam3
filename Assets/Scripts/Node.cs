using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector3 positionOffset; // offset of the tower relative to node position

    [HideInInspector]
    public GameObject tower;
    [HideInInspector]
    public TowerBlueprint towerBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Color defaultColor;
    private Renderer rend;
    private BuildManager _buildManager;
    


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

        if(tower != null )
        {
            return "Tower is already build";
        }

        if(!_buildManager.HasMoney)
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

        GameObject createdTower = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        tower = createdTower;

        tower.GetComponent<Tower>().SetNewParameters(blueprint);

        towerBlueprint = blueprint;
    }

    public void SellTower()
    {
        PlayerStats.Money += towerBlueprint.GetSellAmount();
        rend.material.color = defaultColor;
        Destroy(tower);
        towerBlueprint = null;
    }

    public string TryToUpgradeTower()
    {
        if (tower != null)
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

    private void UpgradeTower(TowerBlueprint blueprint)
    {
        PlayerStats.Money -= blueprint.cost;

        GameObject createdTower = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        tower = createdTower;

        tower.GetComponent<Tower>().SetNewParameters(blueprint);

        towerBlueprint = blueprint;
    }
}
