using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject pathPrefab;
    [SerializeField] private float timeBetweenSpawns = 2f;
    [SerializeField] private int numberOfEnemies = 5;
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private bool destroyOnLastWaypoint;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        
        foreach (var waypoint in pathPrefab.transform)
        {
            waveWaypoints.Add(waypoint as Transform);
        }

        return waveWaypoints;
    }
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    public int GetNumberOfEnemies() { return numberOfEnemies; }
    public float GetMovementSpeed() { return movementSpeed; }
    public bool GetDestroyOnLastWaypoint() { return destroyOnLastWaypoint; }
}
