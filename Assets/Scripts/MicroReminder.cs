using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroReminder : MonoBehaviour
{

    private DataManager _dataManager;
    public GameObject trainingOnSelector;
    void Start()
    {
        _dataManager = DataManager.instance;

        if (_dataManager.ChipsAmount < 1) gameObject.SetActive(false);
        else gameObject.SetActive(true);

        SelectorTraining();
    }

    public void SelectorTraining()
    {
        if (_dataManager.chipsTraining == 1)
        {
            trainingOnSelector.SetActive(true);
            _dataManager.chipsTraining = 2;
        }
    }
}
