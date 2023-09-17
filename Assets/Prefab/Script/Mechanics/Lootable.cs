using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lootable : MonoBehaviour
{
    private bool isLooted = false;
    public List<Item> possibleItems; // List of possible items to loot

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object collided with another object/player
        if (!isLooted && other.CompareTag("Player"))
        {
            // Deactivate the object when the player touches it
            gameObject.SetActive(false);
            isLooted = true; // Mark the object as looted
        }
    }
}
