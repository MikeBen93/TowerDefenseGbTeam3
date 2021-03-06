using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public Button upgradeButton;
    public Text upgradeText;
    public Text sellCostText;

    [SerializeField] private GameObject _nodeShopUI;
    [SerializeField] private GameObject _nodeTowerUI;

    private Node _choosenNode;
    private CameraSeeker _cameraSeeker;

    private void Start()
    {
        _cameraSeeker = CameraSeeker.instance;
    }
    private void Update()
    {
        if(!_nodeTowerUI.activeSelf)
        {
            return;
        }

        if(_choosenNode.TowerIsUpgradable())
        {
            upgradeButton.interactable = true;
            return;
        }

        upgradeButton.interactable = false;
    }
    private void ActivateShopUI()
    {
        _nodeShopUI.SetActive(true);
        _nodeTowerUI.SetActive(false);
    }

    private void ActivateTowerUI()
    {
        _nodeTowerUI.SetActive(true);
        _nodeShopUI.SetActive(false);

        sellCostText.text = $"{_choosenNode.towerBlueprint.GetSellAmount(_choosenNode.tower.CurrentLevel)}";
        if(_choosenNode.towerNextlevel == 0)
        {
            upgradeText.text = "FULLY UPGRADED";
        }
        else
        {
            upgradeText.text = $"{_choosenNode.upgradeCostToNextLevel}";
        }

    }

    public void DeactivateNodeUI()
    {
        _nodeShopUI.SetActive(false);
        _nodeTowerUI.SetActive(false);
    }

    public void ActivateNodeUI(Node node)
    {
        _choosenNode = node;

        transform.position = node.transform.position + (_cameraSeeker.GetCameraPosition - node.transform.position).normalized * 15;
        transform.rotation = Quaternion.LookRotation(Vector3.forward + Vector3.down);
        //transform.LookAt(_cameraSeeker.GetCameraPosition);

        if (node.tower == null)
        {
            ActivateShopUI();
        }
        else
        {
            ActivateTowerUI();
        }
    }

    public void Sell()
    {
        _choosenNode.SellTower();
        BuildManager.instance.DeselectNode();
    }

    public void Upgrade()
    {
        _choosenNode.UpgradeTower();
        BuildManager.instance.DeselectNode();
    }
}
