using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color redTowerColor;
    public Color blueTowerColor;
    public Color purpleTowerColor;
    public Vector3 positionOffset; // offset of the tower relative to node position

    [HideInInspector]
    public GameObject tower;
    [HideInInspector]
    public TowerBlueprint towerBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Color defaultColor;
    private Renderer rend;
    private BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        defaultColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    public void ChangeNodeColor(string enemyColor)
    {
        switch(enemyColor)
        {
            case "RedEnemy":
                rend.material.color = redTowerColor;
                break;
            case "BlueEnemy":
                rend.material.color = blueTowerColor;
                break;
            case "PurpleEnemy":
                rend.material.color = purpleTowerColor;
                break;
            default:
                rend.material.color = defaultColor;
                break;
        }
    }

    public string TryToBuildTower()
    {
        if (!buildManager.CanBuild)
        {
            return "Tower is not choosen";
        }

        if(!buildManager.HasMoney)
        {
            return "Not enough money";
        }
        TowerBlueprint towerToBuild = buildManager.GetTowerToBuild();
        BuildTower(towerToBuild);
        ChangeNodeColor(towerToBuild.prefab.GetComponent<Tower>().enemyTag);

        return "Tower built";
    }

    private void BuildTower(TowerBlueprint blueprint)
    {
        //if (PlayerStats.Money < blueprint.cost)
        //{
        //    Debug.Log("NOT ENOUGH MONEY TO BUILD");
        //    return;
        //}

        PlayerStats.Money -= blueprint.cost;

        GameObject createdTower = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        tower = createdTower;

        towerBlueprint = blueprint;

        //GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        //estroy(effect, 4f);
    }
}
