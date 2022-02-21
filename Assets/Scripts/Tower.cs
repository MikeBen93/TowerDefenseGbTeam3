using System.Collections;
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

    [Header("Use bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 2f;
    private float fireCountdown = 0;

    [Header("Use laser")]
    public bool useLaser = false;

    public float initialDamageOverTime = 10.0f;
    public float slowAmount = .5f;

    public LineRenderer lineRenderer;

    [SerializeField] private float _pointingTime;
    [SerializeField] private float _lastPointingTime;
    [SerializeField] private float _currentDamageOverTime;
    //public ParticleSystem impactEffect;
    //public Light impactLight;

    [Header("Unity Setup Fields")]

    public string enemyTag = "RedEnemy";
    public float turnSpeed = 10f;

    public Transform firePoint;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.25f);

        _currentDamageOverTime = initialDamageOverTime;
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
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
        if (_target == null)
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
        LockOnTarget();

        if (useLaser)
        {
            Laser();
            DamageOverTimeRise();
        } else
        {
            if(fireCountdown <= 0)
            {
                Shoot();
                fireCountdown = 1 / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    private void LockOnTarget()
    {
        _dir = _target.position - transform.position;
        _lookRotation = Quaternion.LookRotation(_dir);
        _rotation = Quaternion.Lerp(transform.rotation, _lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, _rotation.y, 0f);
    }

    private void Laser()
    {
        _targetEnemy.TakeDamage(_currentDamageOverTime * Time.deltaTime);
        _targetEnemy.Slow(slowAmount);

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

        if (bullet != null) bullet.SetAim(_target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
