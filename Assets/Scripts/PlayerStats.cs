using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 400;

    public static float Lives;
    public float startLives = 20;
    public static float initialLives;

    public static int Crystals;
    public static int Chips;

    public static int Rounds;

    private DataManager _dataManager;
 

    private void Start()
    {
        _dataManager = DataManager.instance;

        Money = startMoney;
        Lives = startLives;
        initialLives = startLives;

        Chips = _dataManager.ChipsAmount;

        Rounds = 0;
    }

    public static float GetRemainPercatnage()
    {
        return Lives / initialLives * 100f;
    }
}
