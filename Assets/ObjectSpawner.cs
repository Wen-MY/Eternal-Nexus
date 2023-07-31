using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public Transform player;
    public float spawnDistance = 30f;
    public float despawnDistance = 50f;

    private ObjectPooler objectPooler;
    private GameObject currentObject;

    private void Start()
    {
        objectPooler = FindObjectOfType<ObjectPooler>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // Spawn object when the player is within the spawn distance
        if (currentObject == null && distanceToPlayer <= spawnDistance)
        {
            currentObject = objectPooler.GetPooledObject();
            if (currentObject != null)
            {
                currentObject.transform.position = transform.position;
                currentObject.SetActive(true);
            }
        }

        // Despawn object when the player is beyond the despawn distance
        if (currentObject != null && distanceToPlayer > despawnDistance)
        {
            currentObject.SetActive(false);
            currentObject = null;
        }
    }
}
