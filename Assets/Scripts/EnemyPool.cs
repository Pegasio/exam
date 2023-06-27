using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public int poolSize = 10;

    private List<GameObject>[] pool;

    private void Start()
    {
        pool = new List<GameObject>[enemyPrefabs.Length];

        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            pool[i] = new List<GameObject>();

            for (int j = 0; j < poolSize; j++)
            {
                GameObject enemy = Instantiate(enemyPrefabs[i], Vector3.zero, Quaternion.identity);
                enemy.SetActive(false);
                pool[i].Add(enemy);
            }
        }
    }

    public GameObject GetEnemy(int index)
    {
        for (int i = 0; i < pool[index].Count; i++)
        {
            if (!pool[index][i].activeInHierarchy)
            {
                return pool[index][i];
            }
        }

        // If all objects are active, create a new one and add it to the pool
        GameObject newEnemy = Instantiate(enemyPrefabs[index], Vector3.zero, Quaternion.identity);
        newEnemy.SetActive(false);
        pool[index].Add(newEnemy);

        return newEnemy;
    }

    public void ReturnToPool(GameObject enemy)
    {
        enemy.SetActive(false);
    }
}
