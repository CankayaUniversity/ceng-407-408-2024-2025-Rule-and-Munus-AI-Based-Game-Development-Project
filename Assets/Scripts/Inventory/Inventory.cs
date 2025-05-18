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
		ShowItems();
	}

	#endregion

	public int space = 20;	// Amount of slots in inventory

	// Current list of items in inventory
	public List<Item> items = new List<Item>();
	public List<Equipment> equipments = new List<Equipment>();
	public void ShowItems()
	{
		Debug.Log("Stored Items");
		for(int i = 0; i < equipments.Count; ++i)
        {
        	Debug.Log($"Slot of item: {equipments[i].equipSlot}, Type of item: {equipments[i].equipSlot.ToString()}");
        }
	}
	public bool Add (Equipment item)
	{
		if (!item.isDefaultItem)
		{
			if (equipments.Count >= space)
			{
				Debug.Log("Not enough room.");
				return false;
			}

			equipments.Add(item);
		}

		return true;
	}
	public bool Add (Item item)
	{
		if (!item.isDefaultItem)
		{
			if (items.Count >= space)
			{
				Debug.Log("Not enough room.");
				return false;
			}

			items.Add(item);
		}

		return true;
	}
	// Remove an item
	public void Remove (Item item)
	{
		items.Remove(item);
	}
	public void Remove(int index)
	{
		equipments.RemoveAt(index);
	}
	public void Remove(Equipment equipment)
	{
		equipments.Remove(equipment);
	}
}
