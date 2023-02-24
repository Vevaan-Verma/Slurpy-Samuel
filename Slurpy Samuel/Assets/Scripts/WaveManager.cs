using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    [Header("Enemies")]
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private int[] waveEnemiesAmount;

    [Header("Waves")]
    private int currentWave;

    [Header("Spawners")]
    [SerializeField] private float spawnInterval;
    private EnemySpawner[] enemySpawners;

    private void Start() {

        enemySpawners = FindObjectsOfType<EnemySpawner>();

        currentWave = -1;
        StartNextWave();

    }

    public IEnumerator SpawnWave() {

        System.Random random = new System.Random();

        for (int i = 0; i < waveEnemiesAmount[currentWave]; i++) {

            enemySpawners[random.Next(0, enemySpawners.Length)].SpawnEnemy(enemies[random.Next(0, enemies.Length)]);
            yield return new WaitForSeconds(spawnInterval);
            
        }
    }

    public int GetCurrentWave() {

        return currentWave;

    }

    public void StartNextWave() {

        currentWave++;
        StartCoroutine(SpawnWave());

    }

    public void CheckWaveEnd() {

        foreach (Enemy enemy in FindObjectsOfType<Enemy>()) {

            if (!enemy.isDead) {

                return;

            }
        }

        StartNextWave();

    }
}
