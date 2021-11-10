using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] private WaveConfig waveConfig;

    private Level _level;
    
    private bool _destroyOnLastWaypoint = true;
    private List<Transform> _waypoints;
    private float _enemyMovementSpeed;
    private Vector3 _nextWaypointPosition;
    private int _waypointIndex;

    private void Start()
    {
        _level = FindObjectOfType<Level>();
        
        _waypoints = waveConfig.GetWaypoints();
        
        _waypointIndex = 0;
        _nextWaypointPosition = _waypoints[_waypointIndex].position;
    }

    private void Update()
    {
        VisitEachWaypoint(_destroyOnLastWaypoint);
    }
    
    public void SetWaveConfig(WaveConfig wave) { waveConfig = wave; }
    
    public void SetMovementSpeed(WaveConfig wave) { _enemyMovementSpeed = wave.GetMovementSpeed(); }
    
    public void SetDestroyOnLastWaypoint(WaveConfig wave) { _destroyOnLastWaypoint = wave.GetDestroyOnLastWaypoint(); }
    
    private void VisitEachWaypoint(bool needToDestroy)
    {
        if (EnemyNotInWaypoint())
        {
            MoveToWaypoint();
        }
        else if (EnemyNotInLastWaypoint())
        {
            GetNextWaypoint();
        }
        else if (needToDestroy)
        {
            Destroy(gameObject);
            _level.RemoveEnemyFromLevel();
        }
    }
    
    private bool EnemyNotInLastWaypoint() { return _waypointIndex < _waypoints.Count - 1; }
    
    private bool EnemyNotInWaypoint() { return transform.position != _nextWaypointPosition; }
    
    private void GetNextWaypoint()
    {
        _waypointIndex++;
        _nextWaypointPosition = _waypoints[_waypointIndex].position;
    }
    
    private void MoveToWaypoint()
    {
        // In MoveTowards we need frame rate independent movement speed
        var speed = _enemyMovementSpeed * Time.deltaTime;

        var currentPosition = transform.position;

        transform.position = Vector2.MoveTowards(currentPosition, _nextWaypointPosition, speed);
    }
}
