using UnityEngine;

/* This object updates the inventory UI. */

public class InventoryUI : MonoBehaviour {

	public Transform itemsParent;	// The parent object of all the items
	public GameObject inventoryUI;	// The entire UI
	public Inventory inventory;	// Our current inventory
	InventorySlot[] slots;	// List of all the slots
	void Start () {
		inventory.onItemChangedCallback += UpdateUI;	// Subscribe to the onItemChanged callback

		// Populate our slots array
		slots = itemsParent.GetComponentsInChildren<InventorySlot>();
	}
	
	void Update () {
		// Check to see if we should open/close the inventory
		if (Input.GetKeyDown(KeyCode.E))
		{
			inventoryUI.SetActive(!inventoryUI.activeSelf);
		}
	}

	// Update the inventory UI by:
	//		- Adding items
	//		- Clearing empty slots
	// This is called using a delegate on the Inventory.
	public void UpdateUI ()
	{
		// Loop through all the slots
		for (int i = 0; i < slots.Length; i++)
		{
			if (i < inventory.equipments.Count)	// If there is an item to add
			{
				slots[i].AddItem(inventory.equipments[i]);	// Add it
				Debug.Log($"Inventory has: {inventory.equipments[i].equipSlot.ToString()}");
			} else
			{
				// Otherwise clear the slot
				slots[i].ClearSlot();
			}
		}
	}
}
