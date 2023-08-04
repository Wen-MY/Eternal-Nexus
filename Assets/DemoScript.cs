using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{

    public InventoryManager inventoryManager;
    public Item[] itemsToPickUp;

    public void PickupItem(int id) {
        bool result = inventoryManager.AddItem(itemsToPickUp[id]);
        if (result == true) {
            Debug.Log("Item received");
        }
        else {
            Debug.Log("No received item");
        }
    }
    public void GetSelectedItem() {
        Item receivedItem = inventoryManager.GetSelectedItem(false);
        if (receivedItem != null) {
            Debug.Log("Received");
        }
        else {
            Debug.Log("No received");
        }
    }
    public void UseSelectedItem() {
        Item receivedItem = inventoryManager.GetSelectedItem(true);
        if (receivedItem != null) {
            Debug.Log("Use item " + receivedItem);
        }
        else 
        {
            Debug.Log("No using item");
        }
    } 
}
