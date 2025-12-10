using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    public float spawnDelay = 1f;
    public int limit = 5;

    private int currentCount = 0;
    private bool canSpawn = false;
    private float timer = 0f;

    // Keep track of enemies alive

    private List<GameObject> activeEnemies = new List<GameObject>();

    void Update()
    {
        if (!canSpawn) return;

        if (currentCount >= limit && activeEnemies.Count == 0)
        {
            canSpawn = false;
            return;
        }

        if (currentCount < limit)
        {
            timer += Time.deltaTime;

            if (timer >= spawnDelay)
            {
                SpawnEnemy();
                timer = 0f;
            }
        }
    }

    public void SpawnEnemy()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);

        activeEnemies.Add(enemy);

        EnemyDeathNotifier notifer = enemy.AddComponent<EnemyDeathNotifier>();
        notifer.spawner = this;

        currentCount++;
    }

    public void EnemyNotifer(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }

    public void ActiveSpawner()
    {
        canSpawn = true;
    }

    public void ResetSpawner()
    {
        currentCount = 0;
        activeEnemies.Clear();
        canSpawn = false;
    }

}
