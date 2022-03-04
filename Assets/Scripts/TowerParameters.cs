using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class TowerParameters
{
    public GameObject prefab;

    public int cost;
    public float fireRate;
    public float range;
    public float damageOverTime;
    public int bulletDamage;
    public float explosionRadius;

    public bool upgradableToLvl2 = false;
    public bool upgradableToLvl3 = false;

    public bool firstUprgradeBought = false;
    public bool secondtUprgradeBought = false;
    public bool thirdUprgradeBought = false;
    public bool fourthUprgradeBought = false;

    public string[] enemyTypes;
}
