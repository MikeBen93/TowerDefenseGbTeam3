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

    public string[] enemyTypes;
}
