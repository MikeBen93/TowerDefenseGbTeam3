using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpecificUpgrade
{
    public TowerParametersUpgrade parametersToUpgrade;
    public int upgradeCost;
    public string upgradeDescription;

    public bool upgradeBought = false;
}
