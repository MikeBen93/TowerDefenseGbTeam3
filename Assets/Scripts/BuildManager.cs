using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More the one BuildManager in scene");
            return;
        }
        instance = this;
    }

    private TowerBlueprint towerToBuild;
    private Node selectedNode;

    public NodeUI nodeUI;

    //property to check if we have chosen towe to build
    public bool CanBuild { get { return towerToBuild != null; } }
    //property to check if we have enough money to build that tower
    public bool HasMoney { get { return PlayerStats.Money >= towerToBuild.cost; } }

    public void SelectTowerToBuild(TowerBlueprint cupid)
    {
        towerToBuild = cupid;
        string result = selectedNode.TryToBuildTower();
        DeselectNode();
    }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        towerToBuild = null;

        nodeUI.ActivateNodeUI(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.DeactivateNodeUI();
    }

    public TowerBlueprint GetTowerToBuild()
    {
        return towerToBuild;
    }
}
