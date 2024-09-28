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
    public GameObject enemyPrefab;
    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves = 2f;
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
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        enemy.GetComponent<EnemySpawnable>().OnDeath += OnEnemyDeath;
    }

    void OnEnemyDeath(object sender, EventArgs e)
    {
        _enemiesRemainingAlive--;
    }
}
