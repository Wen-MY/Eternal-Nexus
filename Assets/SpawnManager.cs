using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject itemPrefab; 
    public Transform[] spawnPoints; 
    public InventoryManager inventoryManager;
    private void Start()
    { SpawnItems();
    }

     void SpawnItems()
    {
        ShuffleArray(spawnPoints);

        // Spawn three items at random spawn points.
        for (int i = 0; i < 10; i++)
        {
            Instantiate(itemPrefab, spawnPoints[i].position, Quaternion.identity);
        }
    }

    void ShuffleArray(Transform[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Transform temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}
