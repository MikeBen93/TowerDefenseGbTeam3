using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerBlueprint
{
    public GameObject prefab;
    public int cost;

    public GameObject lvl2Prefab;
    public int lvl2Cost;

    public GameObject lvl3Prefab;
    public int lvl3Cost;

    public int GetSellAmount()
    {
        return cost * 3 / 5;
    }
}
