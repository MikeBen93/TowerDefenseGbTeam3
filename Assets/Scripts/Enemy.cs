using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;
    public float startHealth = 100;
    public float health;
    public int worth = 50; //amount of money gained from enemy killing
    //public GameObject deathEffect;
    [Header("Unity Stuff")]
    //public Image healthBar;
    private bool isDead = false;

    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;

        //healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        PlayerStats.Money += worth;

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
