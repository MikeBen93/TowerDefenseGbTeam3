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

    public static int Rounds;

    private void Start()
    {
        Money = startMoney;
        Lives = startLives;
        initialLives = startLives;

        Rounds = 0;
    }

    public static float GetRemainPercatnage()
    {
        return Lives / initialLives * 100f;
    }
}
