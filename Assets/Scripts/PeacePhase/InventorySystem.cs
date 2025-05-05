using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    // Envanter listesi (her �eyin iskeleti)
    public List<string> items = new List<string>();

    // E�ya ekleme
    public void AddItem(string itemName)
    {
        items.Add(itemName);
        Debug.Log($"Item added: {itemName}");
    }

    // E�y ��karma
    public void RemoveItem(string itemName)
    {
        if (items.Contains(itemName))
        {
            items.Remove(itemName);
            Debug.Log($"Item removed: {itemName}");
        }
        else
        {
            Debug.LogWarning($"Item not found: {itemName}");
        }
    }

    // herhangi bi e�ya var m� kontrol edilecek
    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }

    // inventory bast�r�yor
    public void PrintInventory()
    {
        Debug.Log("Inventory contents:");
        foreach (string item in items)
        {
            Debug.Log($"- {item}");
        }
    }
}
