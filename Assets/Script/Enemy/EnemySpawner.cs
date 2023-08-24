using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float playerDetectionRange = 10f;
    public float spawnRange = 5f;
    public int maxEnemyCount = 4;
    public GameObject enemyPrefab;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkSpawnable();
    }
    private void checkSpawnable()
    {
        if (Vector3.Distance(transform.position, player.position) <= playerDetectionRange)
        {
            // Count existing enemies within spawn range
            int currentEnemyCount = CountEnemiesInRange();

            // Spawn a new enemy if conditions are met
            if (currentEnemyCount < maxEnemyCount)
            {
                SpawnEnemy();
            }
        }
    }
    private int CountEnemiesInRange()
    {
        int count = 0;
        Collider[] colliders = Physics.OverlapSphere(transform.position, spawnRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                count++;
            }
        }
        return count;
    }
    private void SpawnEnemy()
    {
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRange;
        spawnPosition.y = 0.5f; // Ensure enemies spawn at ground level or desired height

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}   
