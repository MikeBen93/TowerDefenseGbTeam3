using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSound : MonoBehaviour
{
    public float copyLives = PlayerStats.Lives;
    public AudioSource ShieldingSound;

    void Update()
    {
        if (copyLives == PlayerStats.Lives) copyLives = PlayerStats.Lives;
        else 
        {
            copyLives = PlayerStats.Lives;
            ShieldingSound.Play();
        }
    }
}
