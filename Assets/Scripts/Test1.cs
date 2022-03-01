using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test1 : MonoBehaviour
{
    private DataManager dataManger;
    public GameObject prefab;

    private void Start()
    {
        dataManger = DataManager.instance;
    }
    public void LoadTestScene()
    {
        SceneManager.LoadScene("TestScene");
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level01");
    }

    public void SetTowerStats()
    {
        foreach(TowerParameters tParams in  dataManger.towerParameters)
        {
            if(tParams.prefab == prefab)
            {
                Debug.Log("bingo");
                tParams.cost = 1000;
                tParams.range = 30;
            }
        }
    }
 
}
