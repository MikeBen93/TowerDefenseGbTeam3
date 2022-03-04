using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReverseAbility : MonoBehaviour
{
    private float _reverseTime = 5;

    [SerializeField] private string firstTowerTag = "PlasmaTower";
    [SerializeField] private Material firstTowInitMat;
    [SerializeField] private Material firstTowRevMat;
    [SerializeField] private string[] firstTowerEnemies;

    [SerializeField] private string secondTowerTag = "LaserTower";
    [SerializeField] private Material secondTowInitMat;
    [SerializeField] private Material secondTowRevMat;
    [SerializeField] private string[] secondTowerEnemies;

    [SerializeField] private Button reverseButton;

    public void Reverse()
    {
        GameObject[] firstTowers = GameObject.FindGameObjectsWithTag(firstTowerTag);
        GameObject[] secondTowers = GameObject.FindGameObjectsWithTag(secondTowerTag);



        StartCoroutine(StartReverse(firstTowers, secondTowers));
    }

    private IEnumerator StartReverse(GameObject[] redTowers, GameObject[] blueTowers)
    {
        reverseButton.interactable = false;
        ChangeTowers(redTowers, firstTowRevMat, secondTowerEnemies);
        ChangeTowers(blueTowers, secondTowRevMat, firstTowerEnemies);

        yield return new WaitForSeconds(_reverseTime);

        ChangeTowers(redTowers, firstTowInitMat, firstTowerEnemies);
        ChangeTowers(blueTowers, secondTowInitMat, secondTowerEnemies);
        reverseButton.interactable = true;
    }

    private void ChangeTowers(GameObject[] towers, Material material, string[] enemyTypes)
    {
        foreach(GameObject towerObj in towers)
        {
            Transform towerTr = towerObj.transform.GetChild(0);

            for (int i = 0; i < towerTr.childCount; i++)
            {
                towerTr.GetChild(i).GetComponent<Renderer>().material = material;
            }

            Tower tower = towerObj.GetComponent<Tower>();

            tower.enemyTypes = enemyTypes;
        }
    }
}
