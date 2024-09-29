using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

[Serializable]
public struct Wave
{
    public int enemyCount;
    public float spawnInterval;
}

public class Spawner : MonoBehaviour
{
    public EnemySpawnable enemyPrefab;
    public Wave[] waves;
    public float timeBetweenWaves = 2f;
    public float spawnRadius;
    
    private int _currentWaveIndex;
    private int _enemiesRemainingToSpawn;
    private int _enemiesRemainingAlive;
    private float _nextSpawnTime;
     
    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (_currentWaveIndex < waves.Length)
        {
            yield return StartCoroutine(SpawnWave(waves[_currentWaveIndex]));
            _currentWaveIndex++;
            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        _enemiesRemainingToSpawn = wave.enemyCount;
        _enemiesRemainingAlive = wave.enemyCount;

        while (_enemiesRemainingToSpawn > 0)
        {
            if (Time.time >= _nextSpawnTime)
            {
                SpawnEnemy();
                _enemiesRemainingToSpawn--;
                _nextSpawnTime = Time.time + wave.spawnInterval;
            }
            yield return null;
        }

        while (_enemiesRemainingAlive > 0)
        {
            yield return null;
        }
    }

    void SpawnEnemy()
    {
        float angle = Random.Range(0, 360);
        var spawnPoint = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.right;
        var enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.AngleAxis(angle, Vector3.down));
        enemy.OnDeath += OnEnemyDeath;
    }

    void OnEnemyDeath()
    {
        _enemiesRemainingAlive--;
    }
}
