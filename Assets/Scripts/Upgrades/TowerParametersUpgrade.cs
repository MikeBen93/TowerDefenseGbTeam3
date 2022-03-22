using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class TowerParametersUpgrade
{
    public int costChangePrc = 0;
    public float fireRateChangePrc = 0;
    public float rangeChangePrc = 0;
    public float damageOverTimeChangePrc = 0;
    public int bulletDamageChangePrc = 0;
    public float explosionRadiusChangePrc = 0;

    public bool upgradableToLvl2 = false;
    public bool upgradableToLvl3 = false;
}
