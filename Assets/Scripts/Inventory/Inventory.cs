using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	#region Singleton

	public static Inventory instance;

	void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning("More than one instance of Inventory found!");
			return;
		}
		instance = this;
	}

	#endregion

	// Callback which is triggered when
	// an item gets added/removed.
	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

	public int space = 20;	// Amount of slots in inventory

	// Current list of items in inventory
	public List<Item> items = new List<Item>();
	public List<Equipment> equipments = new List<Equipment>();
	// Add a new item. If there is enough room we
	// return true. Else we return false.
	public void ShowItems()
	{
		Debug.Log("Stored Items");
		for(int i = 0; i < equipments.Count; ++i)
        {
        	Debug.Log($"Name of item: {equipments[i].name.ToString()}, Type of item: {equipments[i].equipSlot.ToString()}");
        }
	}
	public bool Add (Equipment item)
	{
		// Don't do anything if it's a default item
		if (!item.isDefaultItem)
		{
			// Check if out of space
			if (equipments.Count >= space)
			{
				Debug.Log("Not enough room.");
				return false;
			}

			equipments.Add(item);	// Add item to list

			// Trigger callback
			if (onItemChangedCallback != null)
				onItemChangedCallback.Invoke();
		}

		return true;
	}
	public bool Add (Item item)
	{
		// Don't do anything if it's a default item
		if (!item.isDefaultItem)
		{
			// Check if out of space
			if (items.Count >= space)
			{
				Debug.Log("Not enough room.");
				return false;
			}

			items.Add(item);	// Add item to list

			// Trigger callback
			if (onItemChangedCallback != null)
				onItemChangedCallback.Invoke();
		}

		return true;
	}
	// Remove an item
	public void Remove (Item item)
	{
		items.Remove(item);		// Remove item from list

		// Trigger callback
		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}
	public void Remove (Equipment equipment)
	{
		items.Remove(equipment);

		// Trigger callback
		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}
}
