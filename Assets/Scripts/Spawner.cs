using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    private Player player;
    public float timeBetweenSpawns = 2.0f;
    private float nextSpawnTime;
    private float gameStartTime;

    public GameObject[] enemyPrefabs;
    public float[] enemyProbabilities; // should be the same length as enemyPrefabs
    public Transform[] spawnPoints;

    private List<GameObject> enemies = new List<GameObject>();

    private void Start()
    {
        DestroyEnemies();
        gameStartTime = Time.time;
        nextSpawnTime = gameStartTime + timeBetweenSpawns;
    }

    private void Update()
    {
        SpawnEnemies();
        IncreaseSpawnRate();
    }

    private void DestroyEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        enemies.Clear();
    }

    private void SpawnEnemies()
    {
        if (Time.time >= nextSpawnTime)
        {
            float totalProbability = 0f;
            foreach (float enemyProbability in enemyProbabilities)
            {
                totalProbability += enemyProbability;
            }

            float randomNum = Random.Range(0f, totalProbability);

            float cumulativeProbability = 0f;
            for (int i = 0; i < enemyPrefabs.Length; i++)
            {
                cumulativeProbability += enemyProbabilities[i];
                if (randomNum <= cumulativeProbability)
                {
                    GameObject newEnemy = Instantiate(enemyPrefabs[i], spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
                    enemies.Add(newEnemy);
                    break;
                }
            }

            nextSpawnTime += timeBetweenSpawns;
        }
    }

    private void IncreaseSpawnRate()
    {
        float elapsedTime = Time.time - gameStartTime;
        if (elapsedTime >= 10f)
        {
            if (Time.timeSinceLevelLoad < 250f)
            {
              timeBetweenSpawns -= 0.01f; // 0.01f
            }
            else
            {
                timeBetweenSpawns -= 0.20f;
            }

            timeBetweenSpawns = Mathf.Max(timeBetweenSpawns, 1.0f); // 1.2f
            nextSpawnTime = Time.time + timeBetweenSpawns;
            gameStartTime = Time.time;
        }
    }
}
