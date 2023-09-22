using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickUp;
    public float pickupRadius = 2f; // 定义玩家可以拾取物体的半径

    private void OnTriggerEnter(Collider other)
        {
            PickupRandomItem();
        }

    public void PickupRandomItem()
    {
        if (itemsToPickUp.Length == 0)
        {
            Debug.LogWarning("No items to pick up.");
            return;
        }

        // 随机选择一个物品
        int randomIndex = UnityEngine.Random.Range(0, itemsToPickUp.Length);
        Item randomItem = itemsToPickUp[randomIndex];

        // 将选中的物品添加到玩家的库存中
        inventoryManager.AddItem(randomItem);

        // 在这里可以处理物体在地图上的消失
        Destroy(gameObject); // 销毁拾取的物体
    }

}
