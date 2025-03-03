using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class Inventory : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    InventoryUI ui;
    [SerializeField]
    AudioSource audioSource;

    [Header("Audio Clips")]
    [SerializeField]
    AudioClip buyItemAudio;
    [SerializeField]
    AudioClip sellItemAudio;
    [SerializeField]
    AudioClip dropItemAudio;

    [Header("State")]
    [SerializeField]
    SerializedDictionary<string, Item> inventory = new();


    void AddItem(Item item)
    {
        var inventoryId = Guid.NewGuid().ToString();
        inventory.Add(inventoryId, item);
        ui.AddUIItem(inventoryId, item);
    }

    public void DropItem(string inventoryId)
    {
        var item = inventory.GetValueOrDefault(inventoryId);
        inventory.Remove(inventoryId);
        ui.RemoveUIItem(inventoryId);
        audioSource.PlayOneShot(dropItemAudio);
    }

}
