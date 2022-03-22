using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroReminder : MonoBehaviour
{

    private DataManager _dataManager;
    void Start()
    {
        _dataManager = DataManager.instance;

        if (_dataManager.ChipsAmount < 2) gameObject.SetActive(false);
        else gameObject.SetActive(true);
    }
}
