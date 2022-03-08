using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public string enemyType;
    public float startSpeed = 10f;
    public float speed;
    public float startHealth = 100;
    public float health;
    //public float Health = 100;
    public float damage = 1;
    public int worth = 50; //amount of money gained from enemy killing
    //public GameObject deathEffect;
    [Header("Unity Stuff")]
    public Image healthBar;
    private bool isDead = false;

    [SerializeField] private AudioSource startAudio;
    [SerializeField] private AudioSource deathAudio;

    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
        startAudio.Play();
    }
    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        deathAudio.Play();

        PlayerStats.Money += worth;
        PlayerStats.Crystals++;

        //GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);

        //Destroy(effect, 5f);

        WaveSpawner.enemiesAlive--;

        Destroy(gameObject);
    }

    public void Slow(float ratio)
    {
        speed = startSpeed * (1 - ratio);
    }
}
