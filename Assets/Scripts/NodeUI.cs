using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public Text sellCostText;

    [SerializeField] private GameObject _nodeShopUI;
    [SerializeField] private GameObject _nodeTowerUI;

    private Node _choosenNode;

    private void ActivateShopUI()
    {
        _nodeShopUI.SetActive(true);
        _nodeTowerUI.SetActive(false);
    }

    private void ActivateTowerUI()
    {
        _nodeTowerUI.SetActive(true);
        _nodeShopUI.SetActive(false);

        sellCostText.text = $"SELL: {_choosenNode.towerBlueprint.GetSellAmount()}$";
        
    }

    public void DeactivateNodeUI()
    {
        _nodeShopUI.SetActive(false);
        _nodeTowerUI.SetActive(false);
    }

    public void ActivateNodeUI(Node node)
    {
        _choosenNode = node;

        transform.position = node.transform.position;

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
}