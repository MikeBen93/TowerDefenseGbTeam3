using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReverseAbility : MonoBehaviour
{
    [SerializeField] private float _reverseTime = 5;
    [SerializeField] private int _reverseCost = 0;
    private bool _abilityIsOn = false;

    private Image revImage;
    private float _currentRevTime;

    private Button reverseButton;
    private AudioSource reverseAudio;

    [SerializeField] private string towerTagToReverse = "PlasmaTower";

    [SerializeField] private string[] revTowerEnemies;
    [SerializeField] private string[] initTowerEnemies;

    [SerializeField] private Material lvl1RevMat;
    [SerializeField] private Material lvl2RevMat;
    [SerializeField] private Material lvl3RevMat;

    [SerializeField] private Material lvl1InitMat;
    [SerializeField] private Material lvl2InitMat;
    [SerializeField] private Material lvl3InitMat;

    private void Start()
    {
        reverseAudio = GetComponent<AudioSource>();
        reverseButton = GetComponent<Button>();
        revImage = GetComponent<Image>();
        _currentRevTime = _reverseTime;
    }

    private void Update()
    {
        CheckAbilityToUse();
        ButtonTimer();
    }

    private void CheckAbilityToUse()
    {
        if (PlayerStats.Crystals >= _reverseCost && !_abilityIsOn) reverseButton.interactable = true;
        else reverseButton.interactable = false;
    }

    public void Reverse()
    {
        PlayerStats.Crystals -= _reverseCost;
        reverseAudio.Play();
        _currentRevTime = 0;

        GameObject[] firstTowers = GameObject.FindGameObjectsWithTag(towerTagToReverse);

        StartCoroutine(StartReverse(firstTowers));
    }

    private IEnumerator StartReverse(GameObject[] towersToReverse)
    {
        _abilityIsOn = true;
        reverseButton.interactable = false;
        ChangeTowers(towersToReverse, true);

        yield return new WaitForSeconds(_reverseTime);

        _abilityIsOn = false;
        ChangeTowers(towersToReverse, false);
        CheckAbilityToUse();
    }

    private void ChangeTowers(GameObject[] towers, bool toNewColor)
    {
        foreach(GameObject towerObj in towers)
        {
            Transform towerTr = towerObj.transform.GetChild(0);
            Tower tower = towerObj.GetComponent<Tower>();
            Material material;
            if (toNewColor)
            {
                material = GetRevMaterial(tower.CurrentLevel);
                tower.enemyTypes = revTowerEnemies;
            } else
            {
                material = GetInitMaterial(tower.CurrentLevel);
                tower.enemyTypes = initTowerEnemies;
            }

            for (int i = 0; i < towerTr.childCount; i++)
            {
                towerTr.GetChild(i).GetComponent<Renderer>().material = material;
            }
        }
    }

    private Material GetRevMaterial(int towerLevel)
    {
        Debug.Log(towerLevel);
        if (towerLevel == 1) return lvl1RevMat;
        else if (towerLevel == 2) return lvl2RevMat;
        else return lvl3RevMat;
    }

    private Material GetInitMaterial(int towerLevel)
    {
        if (towerLevel == 1) return lvl1InitMat;
        else if (towerLevel == 2) return lvl2InitMat;
        else return lvl3InitMat;
    }

    private void ButtonTimer()
    {
        if (_abilityIsOn)
        {
            _currentRevTime += Time.deltaTime;
            revImage.fillAmount = _currentRevTime / _reverseTime;
        }
    }
}
