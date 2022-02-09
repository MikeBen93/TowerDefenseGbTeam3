using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform _target;
    private int _waypointIndex = 0;

    private Enemy _enemy;

    private void Start()
    {
        _target = Waypoints.waypoints[_waypointIndex];
        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        Vector3 direction = _target.position - transform.position;
        transform.Translate(direction.normalized * _enemy.speed * Time.deltaTime, Space.World);

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
    }

    private void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.enemiesAlive--;
        Destroy(gameObject);
    }
}
