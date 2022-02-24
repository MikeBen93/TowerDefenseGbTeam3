using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public static int enemiesAlive = 0;

    public Wave[] waves;

    public Transform spawnPoint;

    public Text waveNumberText;
    public Image heartCountdownImage;

    public GameController gameController;

    public float timeBetweenWaves = 10f;
    private float _initialCountdown = 2f;
    private float _countdown;

    private int _waveIndex = 0;

    private void Start()
    {
        enemiesAlive = 0;
        _countdown = _initialCountdown;
    }
    private void Update()
    {
        
        //checking if we killed all enemies in wave 
        if (enemiesAlive > 0) return;

        if (gameController.ShowTutorial && _countdown <= _initialCountdown - 1.5f)
        {
            gameController.ShowTutorialUI(SceneManager.GetActiveScene().name, _waveIndex);
        }

        //checking if we passed all waves 
        if (_waveIndex == waves.Length)
        {
            gameController.WinLevel();
            this.enabled = false;
        }

        if (_countdown <= 0f)
        {
            waveNumberText.text = (_waveIndex + 1).ToString() + "/" + waves.Length.ToString();
            StartCoroutine(SpawnWave());
            _countdown = timeBetweenWaves;
            return;
        }

        _countdown -= Time.deltaTime;
        _countdown = Mathf.Clamp(_countdown, 0, Mathf.Infinity);

        heartCountdownImage.fillAmount = 1 - _countdown / timeBetweenWaves;

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
            yield return new WaitForSeconds(wave.rate);
        }

        _waveIndex++;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, transform.rotation);
    }

}
