using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public static int enemiesAlive = 0;

    public Wave[] waves;

    public Transform spawnPoint;

    //public Text waveCountdownText;

    public GameController gameController;

    public float timeBetweenWaves = 3f;
    private float _countdown = 2f;

    private int _waveIndex = 0;

    private void Start()
    {
        enemiesAlive = 0;
    }
    private void Update()
    {
        //checking if we killed all enemies in wave 
        if (enemiesAlive > 0) return;

        //checking if we passed all waves 
        if (_waveIndex == waves.Length)
        {
            gameController.WinLevel();
            this.enabled = false;
        }

        if (_countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            _countdown = timeBetweenWaves;
            return;
        }

        _countdown -= Time.deltaTime;
        _countdown = Mathf.Clamp(_countdown, 0, Mathf.Infinity);

        //waveCountdownText.text = string.Format("{0:00.00}", _countdown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[_waveIndex];

        enemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        _waveIndex++;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, transform.rotation);
    }

}
