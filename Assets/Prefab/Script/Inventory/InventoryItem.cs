using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItem : MonoBehaviour
{
    public Item item;
    
    [Header("UI")]
    public Image image;

    [HideInInspector] public int count = 1; //each item starts with 1 quantity

    private void Start() {
        InitialiseItem(item);
    }

    public void InitialiseItem(Item newItem) {
        item = newItem;
        image.sprite = newItem.image;
    }
}
