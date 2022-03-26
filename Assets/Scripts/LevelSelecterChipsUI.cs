using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelecterChipsUI : MonoBehaviour
{
    [SerializeField] private GameObject[] chipsObjectsToShow;
    private DataManager _dataManager;

    private void Start()
    {
        _dataManager = DataManager.instance;

        char related_level = gameObject.name[gameObject.name.Length - 1];

        int chips_on_related_level = PlayerPrefs.GetInt("chips_recieveid_on_Level0" + related_level, 0);

        for(int i = 0; i < chipsObjectsToShow.Length; i++)
        {
            if(i < chips_on_related_level)
            {
                chipsObjectsToShow[i].SetActive(true);
            }
            else
            {
                chipsObjectsToShow[i].SetActive(false);
            }
        }
    }
}
