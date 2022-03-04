using System;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Transform _target;
    private Enemy _targetEnemy;
    private GameObject _selectedEnemy;

    private Vector3 _dir;
    private Quaternion _lookRotation;
    private Vector3 _rotation;

    [Header("General")]
    public float range = 15f;
    public string[] enemyTypes = new string[] { "RedEnemy" };

    [Header("Use bullets (default)")]
    public bool useBullets = true;
    public GameObject bulletPrefab;
    public float fireRate = 2f;
    private float fireCountdown = 0;

    [Header("Use laser")]
    public bool useLaser = false;

    public float initialDamageOverTime = 10.0f;

    public LineRenderer lineRenderer;

    [SerializeField] private float _pointingTime;
    [SerializeField] private float _lastPointingTime;
    [SerializeField] private float _currentDamageOverTime;
    //public ParticleSystem impactEffect;
    //public Light impactLight;

    [Header("Slowing effect")]
    public bool hasSlowingEffect = false;

    public float slowAmount = .5f;

    [Header("Areal effect")]
    public bool hasArealEffect = false;
    private List<Enemy> _enemiesInArea;


    [Header("Range visualization")]

    [SerializeField] private bool hasSphereEffect = false;
    [SerializeField] private Transform sphereEffect;

    [Header("Bullet setup fields")]
    private int bulletDamage;
    private float bulletExplosionRadius;


    [Header("Unity Setup Fields")]

    public bool turnable = true;
    public float turnSpeed = 10f;
    public bool upgradableToLvl2 = false;
    public bool upgradableToLvl3 = false;


    private string enemyTag = "Enemy";
    [SerializeField] private Transform firePoint;

    private DataManager _dataManager;

    private void Awake()
    {
        _dataManager = DataManager.instance;
    }

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.25f);

        _currentDamageOverTime = initialDamageOverTime;
        _enemiesInArea = new List<Enemy>();

        if (hasSphereEffect)
        {
            sphereEffect.gameObject.SetActive(true);
            sphereEffect.localScale *= range;
        }
    }

    private void UpdateTarget()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag(enemyTag);

        List<GameObject> relatedEnemies = new List<GameObject>();

        foreach(GameObject enemy in allEnemies)
        {
            string currentEnemyType = enemy.GetComponent<Enemy>().enemyType;
            if (Array.Exists(enemyTypes, n => n == currentEnemyType))
            {
                relatedEnemies.Add(enemy);
            }
        }


        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        if (hasArealEffect)
        {
            _enemiesInArea.Clear();

            foreach (GameObject enemy in relatedEnemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if(distanceToEnemy <= range)
                {
                    _enemiesInArea.Add(enemy.GetComponent<Enemy>());
                }
            }
            return;
        }

        foreach (GameObject enemy in relatedEnemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy == _selectedEnemy)
        {
            return;
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            _selectedEnemy = nearestEnemy;
            _target = _selectedEnemy.transform;
            _targetEnemy = _target.GetComponent<Enemy>();
            _currentDamageOverTime = initialDamageOverTime;
            _pointingTime = 0f;
            _lastPointingTime = 0f;
        }
        else
        {
            _target = null;
        }
    }

    private void Update()
    {
        if (_target == null && _enemiesInArea.Count == 0)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    //impactEffect.Stop();
                    //impactLight.enabled = false;
                }
            }
            return;
        }

        //target lock on
        if(turnable)
        {
            LockOnTarget();
        }

        if (useLaser)
        {
            Laser();
            DamageOverTimeRise();
        }

        if(useBullets)
        {
            if(fireCountdown <= 0)
            {
                Shoot();
                fireCountdown = fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }

        if(hasArealEffect)
        {
            foreach (Enemy enemy in _enemiesInArea)
            {
                SlowDown(enemy);
            }
        }
    }

    private void LockOnTarget()
    {
        _dir = _target.position - transform.position;
        _lookRotation = Quaternion.LookRotation(_dir);
        _rotation = Quaternion.Lerp(transform.rotation, _lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, _rotation.y, 0f);
    }

    private void SlowDown(Enemy enemy)
    {
        enemy.Slow(slowAmount);
    }
    private void Laser()
    {
        _targetEnemy.TakeDamage(_currentDamageOverTime * Time.deltaTime);

        //below code related to graphics
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            //impactEffect.Play();
            //impactLight.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, _target.position);

        Vector3 dir = transform.position - _target.position;

        //impactEffect.transform.position = _target.position + dir.normalized * 1f;
        //impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    private void DamageOverTimeRise()
    {
        _pointingTime += Time.deltaTime;

        if(_pointingTime - _lastPointingTime >= 1f)
        {
            _lastPointingTime++;
            _currentDamageOverTime += initialDamageOverTime * 0.5f;
        }
    }

    private void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.damage = bulletDamage;
            bullet.explosionRadius = bulletExplosionRadius;
            bullet.SetAim(_target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void InverseTower()
    {

    }

    public void SetNewParameters(TowerBlueprint blueprint)
    {
        foreach (TowerParameters tParams in _dataManager.towerParameters)
        {
            if (tParams.prefab == blueprint.prefab)
            {
                range = tParams.range;
                fireRate = tParams.fireRate;
                initialDamageOverTime = tParams.damageOverTime;
                bulletDamage = tParams.bulletDamage;
                bulletExplosionRadius = tParams.explosionRadius;
            }
        }
    }
}
