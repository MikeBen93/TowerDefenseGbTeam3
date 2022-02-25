using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform _target;
    private int _waypointIndex = 0;

    private Vector3 _dir;
    private Quaternion _lookRotation;
    private Vector3 _rotation;

    private Enemy _enemy;

    private void Start()
    {
        _target = Waypoints.waypoints[_waypointIndex];

        _dir = _target.position - transform.position;
        _lookRotation = Quaternion.LookRotation(_dir);

        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        Vector3 direction = _target.position - transform.position;
        _rotation = Quaternion.Lerp(transform.rotation, _lookRotation, Time.deltaTime * 10).eulerAngles;

        transform.Translate(direction.normalized * _enemy.speed * Time.deltaTime, Space.World);
        transform.rotation = Quaternion.Euler(0f, _rotation.y, 0f);

        if (ReachedWaypoint()) GetNextWaypoint();

        _enemy.speed = _enemy.startSpeed;
    }

    /// <summary>
    /// Function to checek if enemy has reached waypoint
    /// </summary>
    /// <returns></returns>
    private bool ReachedWaypoint()
    {
        return Vector3.Distance(_target.position, transform.position) < 0.4f;
    }

    /// <summary>
    /// Function to set new waypoint 
    /// </summary>
    private void GetNextWaypoint()
    {
        if (_waypointIndex >= Waypoints.waypoints.Length - 1)
        {
            EndPath();
            return;
        }

        _waypointIndex++;
        _target = Waypoints.waypoints[_waypointIndex];
        _dir = _target.position - transform.position;
        _lookRotation = Quaternion.LookRotation(_dir);
    }

    private void EndPath()
    {
        PlayerStats.Lives -= _enemy.damage;
        WaveSpawner.enemiesAlive--;
        Destroy(gameObject);
    }
}
