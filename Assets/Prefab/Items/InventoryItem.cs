using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItem : MonoBehaviour
{
    public TextMeshProUGUI countText;
    public Item item;
    [HideInInspector] public int count = 1; //each item starts with 1 quantity
    [Header("UI")]
    public Image image;
/*
    private void Start() {
        InitialiseItem(item);
    }
    
    public void InitialiseItem(Item newItem) {
        item = newItem;
        image.sprite = newItem.image;
        RefreshCount();
    }
    public void RefreshCount() {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }*/
}
