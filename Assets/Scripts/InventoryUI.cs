using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    GameObject uiItemPrefab;

    [Header("References")]
    [SerializeField]
    Inventory inventory;
    [SerializeField]
    Transform uiInventoryParent;

    [Header("State")]
    [SerializeField]
    SerializedDictionary<string, GameObject> inventoryUI = new();


    public void AddUIItem(string inventoryId, Item item)
    {
        var itemUI = Instantiate(uiItemPrefab).GetComponent<ItemUI>();
        itemUI.transform.SetParent(uiInventoryParent);
        inventoryUI.Add(inventoryId, itemUI.gameObject);
        itemUI.Initialize(inventoryId, item, inventory.DropItem);
    }

    public void RemoveUIItem(string inventoryId)
    {
        var itemUI = inventoryUI.GetValueOrDefault(inventoryId);
        inventoryUI.Remove(inventoryId);
        Destroy(itemUI);
    }

}
