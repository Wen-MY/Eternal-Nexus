using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public HealthStaminaSystem HSSystem;
    public int maxStackedItems = 4;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    int selectedSlot = -1;
    Item item;
    Item itemType;
    public bool changedTool = false;
    public bool reloading = false;  //need to do when reloading, the ammos will decrease automatically

    private void Start() {
        itemType = ChangeSelectedSlot(0);
        HSSystem = GameObject.Find("Player").GetComponent<HealthStaminaSystem>();
    }
    private void Update() {
       if (Input.GetKeyDown(KeyCode.Alpha1)) {
        itemType = ChangeSelectedSlot(0);
        changedTool = true;
       }
       else if (Input.GetKeyDown(KeyCode.Alpha2)) {
        itemType = ChangeSelectedSlot(1);
        changedTool = true;
       }
       else if (Input.GetKeyDown(KeyCode.Alpha3)) {
        itemType = ChangeSelectedSlot(2);
        changedTool = true;
       }
       else if (Input.GetKeyDown(KeyCode.Alpha4)) {
        itemType = ChangeSelectedSlot(3);
        changedTool = true;
       }
       else if (Input.GetKeyDown(KeyCode.Alpha5)) {
        itemType = ChangeSelectedSlot(4);
        changedTool = true;
       }
       else if (Input.GetKeyDown(KeyCode.Alpha6)) {
        itemType = ChangeSelectedSlot(5);
        changedTool = true;
       }
       else if (Input.GetKeyDown(KeyCode.Alpha7)) {
        itemType = ChangeSelectedSlot(6);
        changedTool = true;
       }
       else if (Input.GetKeyDown(KeyCode.Alpha8)) {
        itemType = ChangeSelectedSlot(7);
        changedTool = true;
       }
       else if (Input.GetKeyDown(KeyCode.Alpha9)) {
        itemType = ChangeSelectedSlot(8);
        changedTool = true;
       }
       else if (Input.GetKeyDown(KeyCode.Alpha0)) {
        itemType = ChangeSelectedSlot(9);
        changedTool = true;
       }

        if (Input.GetKeyDown(KeyCode.E)) {
            if (itemType.type == ItemType.Bandage) {
                HSSystem.RecoverHealth(20f);
                ConsumeSelectedItem();
            }
            else if (itemType.type == ItemType.Medkit)
            {
                HSSystem.RecoverHealth(100f);
                ConsumeSelectedItem();
            }
            else if (itemType.type == ItemType.Adrenaline)
            {
                HSSystem.IncreaseEnergy();
                ConsumeSelectedItem();
            }
            else if (itemType.type == ItemType.Ammos)
            {
                //Actions
            }
            else if (itemType.type == ItemType.Gems)
            {
                //Actions
            }
            else if (itemType.type == ItemType.NotesWithCode)
            {
                //Actions
            }
            Debug.Log("Consume: " + itemType.type); 

        }
                
        }
            
    public Item ChangeSelectedSlot(int newValue) {
        if (selectedSlot >= 0) {
            inventorySlots[selectedSlot].Deselect();
        }
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        Item itemType = itemInSlot.item;
        return itemType;
    }

     public bool AddItem(Item itemToAdd) { /*
        for (int i=0; i<inventorySlots.Length; i++) { 
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && //it is occupied
                itemInSlot.item == item && 
                itemInSlot.count < maxStackedItems &&   //if the tool gt 4, need to add on another slot
                itemInSlot.item.stackable == true) {
                    itemInSlot.count++;
                    //itemInSlot.RefreshCount();
                    return true;
            }
        } */

        for (int i=0; i<inventorySlots.Length; i++) {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null) { //no occupied, then occupy it
                GameObject newItemObject = Instantiate(inventoryItemPrefab, slot.transform);
                InventoryItem newItem = newItemObject.GetComponent<InventoryItem>();
                newItem.item = itemToAdd;
                newItem.count = 1;
                itemInSlot.count++;
                Debug.Log(inventorySlots.Length); //used in touching lootable, but not working properly
                return true;
            }
        }
        return false; 
    } 

    public Item GetSelectedItem(bool use) { 
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null) {
            Item item = itemInSlot.item;
            if (use == true) {
                itemInSlot.count--;
                if (itemInSlot.count < 0) {
                    Destroy(itemInSlot.gameObject);
                } else {
                    //itemInSlot.RefreshCount();
                }
            }
            return item;
        }
        return null;
    }
    private void UpdateUIWithItemImage(InventoryItem itemAdd) {
        itemAdd.item.image = item.image;
        
    }
    public void ConsumeSelectedItem() {
        /*
        //check if it has ammos when reloading? 
         if (reloading == true) {
            for (int i=0; i<inventorySlots.Length; i++) {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            Item itemType = itemInSlot.item;
            if (itemInSlot != null && //it is occupied
                itemType.type == ItemType.Ammos) {
                    Destroy(itemInSlot.gameObject);
                    //itemInSlot.RefreshCount();
                    reloading = false;
                }
            }
         }
         */
            InventorySlot slot = inventorySlots[selectedSlot];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null) {
                // Remove the InventoryItem from the slot
                Destroy(itemInSlot.gameObject);
                // Other logic after consuming the item
                }
            }
}

