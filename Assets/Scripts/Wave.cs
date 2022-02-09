using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject enemy; //TODO: change to array to spawn several types of enemies in one wave
    public int count;//amount of enemies in wave
    //Also can use dicitionary to define how many enemies of each type must be in wave
    public float rate;//time between each spawn of enemy in wave
}