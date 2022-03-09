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

    public int GetSellAmount(int currentLevel)
    {
        if(currentLevel == 1)
        {
            return (cost * 3 / 5);
        }
        else if (currentLevel == 2)
        {
            return ((cost + lvl2Cost) * 3 / 5);
        }
        else
        {
            return ((cost + lvl2Cost + lvl3Cost) * 3 / 5);
        }
    }
}
