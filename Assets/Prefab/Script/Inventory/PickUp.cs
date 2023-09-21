using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickUp;
    public float pickupRadius = 2f; // 定义玩家可以拾取物体的半径


    private void OnTriggerEnter(Collider other)
    {
        // 检测进入碰撞触发区域的物体是否是玩家
        if (other.CompareTag("Player"))
        {
             PickupItem(0);
        }
    }

    public void PickupItem(int id)
    {
        inventoryManager.AddItem(itemsToPickUp[id]);
        // 在这里可以处理物体在地图上的消失
        Destroy(gameObject); // 销毁拾取的物体
    }
}
