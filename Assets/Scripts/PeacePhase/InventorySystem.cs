using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    // Envanter listesi (her þeyin iskeleti)
    public List<string> items = new List<string>();

    // Eþya ekleme
    public void AddItem(string itemName)
    {
        items.Add(itemName);
        Debug.Log($"Item added: {itemName}");
    }

    // Eþy çýkarma
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

    // herhangi bi eþya var mý kontrol edilecek
    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }

    // inventory bastýrýyor
    public void PrintInventory()
    {
        Debug.Log("Inventory contents:");
        foreach (string item in items)
        {
            Debug.Log($"- {item}");
        }
    }
}
