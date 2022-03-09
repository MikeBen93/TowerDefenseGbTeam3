using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChipsUI : MonoBehaviour
{

    private DataManager _dataManager;
    [SerializeField] private Text chipsAmount;

    private void Start()
    {
        _dataManager = DataManager.instance;

        chipsAmount.text = _dataManager.ChipsAmount.ToString();
    }

    private void Update()
    {
        chipsAmount.text = _dataManager.ChipsAmount.ToString();
    }
}
