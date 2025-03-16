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
	public Dictionary<string, Material> materials = new Dictionary<string, Material>();
	public List<Material> mat = new List<Material>();
	public int MaxMaterialAmount = 100;
    public int MaxMaterialTypes = 4;

	// Add a new item. If there is enough room we
	// return true. Else we return false.
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

	public bool Add(string name, int amount)
    {
        if (!materials.ContainsKey(name))
        {
            Debug.Log("Invalid material type!");
            return false;
        }
		if (materials[name].Count + amount < MaxMaterialAmount)
		{
			materials[name].AddCount(amount);
			mat.Add(new Material(name, amount));
		}
		// If old amount + gathered amount exceed the max amount and updated amount does not exceed max amount.
		else if (materials[name].Count + amount > MaxMaterialAmount && materials[name].Count + amount - MaxMaterialAmount < MaxMaterialAmount)
		{
			materials[name].AddCount(materials[name].Count + amount - MaxMaterialAmount);
			mat.Add(new Material(name, amount));
		}
		else
		{
			Debug.Log("Exceed the max amount of: " + name);
			return false;
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

}
