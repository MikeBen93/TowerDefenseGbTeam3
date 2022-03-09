using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerBlueprint
{
    [SerializeField] float sellingRate = 3 / 5;

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
            return (int)(cost * sellingRate);
        }
        else if (currentLevel == 2)
        {
            return (int)((cost + lvl2Cost) * sellingRate);
        }
        else
        {
            return (int)((cost + lvl2Cost + lvl3Cost) * sellingRate);
        }
    }
}
