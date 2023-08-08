using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool reloading = false;

    private void Start() {
        itemType = ChangeSelectedSlot(0);
    }
    private void Update() {
       /*if (Input.inputString != null) {
        bool isNumber = int.TryParse(Input.inputString, out int number);
        if (isNumber && number > 0 && number < 8) {
            ChangeSelectedSlot(number-1);
        }
       } */
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
            }
            else if (itemType.type == ItemType.Medkit)
            {
                HSSystem.RecoverHealth(100f);
            }
            else if (itemType.type == ItemType.Adrenaline)
            {
                HSSystem.IncreaseEnergy();
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
            ConsumeSelectedItem();

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

    /* public bool AddItem(Item item) {
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
        }

        for (int i=0; i<inventorySlots.Length; i++) {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null) { //no occupied, then occupy it
                SpawnNewItem(item,slot);
                return true;
            }
        }
        return false;
    } */

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

