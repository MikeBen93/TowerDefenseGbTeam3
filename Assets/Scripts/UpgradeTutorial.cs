using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTutorial : MonoBehaviour
{
    private DataManager _dataManager;
    public GameObject trainingOnUpgrades;

    void Start()
    {
        _dataManager = DataManager.instance;

        if (_dataManager.chipsTraining == 2)
        {
            trainingOnUpgrades.SetActive(true);
            _dataManager.chipsTraining = 3;
        }
    }

}
