using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float waveSpawnDelay = 1f;
    [SerializeField] private List<WaveConfig> waveConfigs;
    [SerializeField] private bool looping = true;

    private IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        foreach (var currentWaveConfig in waveConfigs)
        {
            StartCoroutine(SpawnAllEnemiesInWave(currentWaveConfig));
            yield return new WaitForSeconds(waveSpawnDelay);
        }
    }
    
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        var numberOfEnemies = waveConfig.GetNumberOfEnemies();

        for (var i = 0; i < numberOfEnemies; i++)
        {
            SpawnEnemy(waveConfig);
            
            var enemySpawnDelay = waveConfig.GetTimeBetweenSpawns();
            yield return new WaitForSeconds(enemySpawnDelay);
        }
    }
    
    private void SpawnEnemy(WaveConfig waveConfig)
    {
        var enemyPrefab = waveConfig.GetEnemyPrefab();
        var waypoints = waveConfig.GetWaypoints();
        var spawnPosition = waypoints[0].position;

        var enemy = Instantiate(enemyPrefab, spawnPosition, enemyPrefab.transform.rotation);
        AttachWaveConfigToEnemy(waveConfig, enemy);
    }
    
    private void AttachWaveConfigToEnemy(WaveConfig waveConfig, GameObject enemy)
    {
        var enemyScript = enemy.GetComponent<EnemyPathing>();
        
        enemyScript.SetWaveConfig(waveConfig);
        enemyScript.SetMovementSpeed(waveConfig);
        enemyScript.SetDestroyOnLastWaypoint(waveConfig);
    }
}
