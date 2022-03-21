using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroReminder : MonoBehaviour
{
    void Start()
    {
        if (PlayerStats.Chips < 2) gameObject.SetActive(false);
        else gameObject.SetActive(true);
    }
}
