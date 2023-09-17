using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerTeleporterManager>().SpawnOrReplaceTeleporter();
            Destroy(gameObject);
        }
    }
}
