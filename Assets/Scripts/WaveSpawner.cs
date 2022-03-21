using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public static int enemiesAlive = 0;

    public Wave[] waves;

    private string _enemyType;

    public Transform spawnPoint;

    public Text waveNumberText;
    public Image heartCountdownImage;

    public GameController gameController;

    public float timeBetweenWaves = 10f;
    private float _initialCountdown = 5f;
    private float _countdown;

    private int _waveIndex = 0;

    [SerializeField] private Material redCenterMat;
    [SerializeField] private Material blueCenterMat;
    [SerializeField] private Material purpleCenterMat;
    [SerializeField] private Renderer portalCenterRend;

    [SerializeField] private AudioSource redEnemiesEntranceSound;
    [SerializeField] private AudioSource redEnemiesDeathSound;
    [SerializeField] private AudioSource blueEnemiesEntranceSound;
    [SerializeField] private AudioSource blueEnemiesDeathSound;
    [SerializeField] private AudioSource purpleEnemiesEntranceSound;
    [SerializeField] private AudioSource purpleEnemiesDeathSound;

    private AudioSource entranceSoundToPlay;
    private AudioSource deathSoundToPlay;
    private bool _deathSoundPlayed = true;


    private void Start()
    {
        gameController = GetComponent<GameController>();

           enemiesAlive = 0;
        _countdown = _initialCountdown;
        waveNumberText.text = (_waveIndex + 1).ToString() + "/" + waves.Length.ToString();

        _enemyType = waves[0].enemy.GetComponent<Enemy>().enemyType;
        SetPortalCenter(_enemyType);
        SetAudio(_enemyType);
    }
    private void Update()
    {
        
        //checking if we killed all enemies in wave 
        if (enemiesAlive > 0) return;

        if(!_deathSoundPlayed)
        {
            deathSoundToPlay.Play();
            SetAudio(_enemyType);
            _deathSoundPlayed = true;
        }

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
            if (i == 0) entranceSoundToPlay.Play();
            if (i == wave.count - 1) _deathSoundPlayed = false;
            yield return new WaitForSeconds(wave.rate);
        }

        _waveIndex++;

        if (_waveIndex != waves.Length)
        {
            _enemyType = waves[_waveIndex].enemy.GetComponent<Enemy>().enemyType;
            SetPortalCenter(_enemyType);
        }

    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, transform.rotation);
    }

    private void SetPortalCenter(string enemyType)
    {
        if (enemyType == "RedEnemy") portalCenterRend.material = redCenterMat;
        else if (enemyType == "BlueEnemy") portalCenterRend.material = blueCenterMat;
        else if (enemyType == "PurpleEnemy") portalCenterRend.material = purpleCenterMat;
    }

    private void SetAudio(string enemyType)
    {
        if (enemyType == "RedEnemy")
        {
            entranceSoundToPlay = redEnemiesEntranceSound;
            deathSoundToPlay = redEnemiesDeathSound;
        }
        else if (enemyType == "BlueEnemy")
        {
            entranceSoundToPlay = blueEnemiesEntranceSound;
            deathSoundToPlay = blueEnemiesDeathSound;
        }
        else if (enemyType == "PurpleEnemy")
        {
            entranceSoundToPlay = purpleEnemiesEntranceSound;
            deathSoundToPlay = purpleEnemiesDeathSound;
        }
    }

}
