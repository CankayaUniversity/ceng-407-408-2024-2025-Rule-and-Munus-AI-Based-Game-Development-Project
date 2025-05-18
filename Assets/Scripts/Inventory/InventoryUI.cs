using UnityEngine;
using System;

/* This object updates the inventory UI. */

public class InventoryUI : MonoBehaviour {

	public Transform itemsParent;	// The parent object of all the items
	public GameObject inventoryUI;	// The entire UI
	public Inventory inventory;	// Our current inventory
	public InventorySlot[] slots;	// List of all the slots
	void Awake () {
		slots = itemsParent.GetComponentsInChildren<InventorySlot>();
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.E))
		{
			inventoryUI.SetActive(!inventoryUI.activeSelf);
		}
	}
	public void SetupSlots(Action<int> onRemove, Action<int> onUse)
	{
    	for (int i = 0; i < slots.Length; i++)
	    {
        	slots[i].Setup(i, onRemove, onUse);
    	}
	}
	public void UpdateUI ()
	{
		// if (slots == null)
		// {
		// 	return;
		// }
		//Debug.LogError($"Slot Length: {slots.Length}");
		//Debug.LogError($"Equipments Count: {inventory.equipments.Count}");
		for (int i = 0; i < slots.Length; i++)
		{
			if (i < inventory.equipments.Count)
			{
				slots[i].AddItem(inventory.equipments[i]);  // Add it
			}
			else
			{
				slots[i].ClearSlot();
			}
		}
	}
	public InventorySlot[] GetSlots()
	{
		return slots;
	}
}
