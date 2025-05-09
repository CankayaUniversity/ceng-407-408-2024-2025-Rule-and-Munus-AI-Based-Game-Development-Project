using UnityEngine;
using UnityEngine.Rendering;
using System;
using System.Collections.Generic;

/* The base item class. All items should derive from this. */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject 
{

	//new public string name = "New Item";	// Name of the item
	//public Sprite icon = null;				// Item icon
	//public bool isDefaultItem = false;      // Is the item default wear?

    public string id = "Null";
    new public string name = "New Item";
    public string description = "Empty Description";
    public Sprite icon;
    public bool isDefaultItem = false;
    public string tag = "Default Item";
	public Item()
	{
		this.InitItem();
	}
	public void InitItem()
	{
		this.id = "Null";
		this.name = "New Item";
		this.description = "Null";
		this.icon = null;
		this.isDefaultItem = false;
		this.tag = "Item";
	}
	public Item(string id, string name, string description, Sprite icon, bool isDefault, string tag)
	{
		this.InitItem(id, name, description, icon, isDefault, tag);
	}
	public void SetIcon(Sprite icon)
	{
		this.icon = icon;
	}

	public void InitItem(string id, string name, string description, Sprite icon, bool isDefault, string tag)
	{
		this.id = id;
		this.name = name;
		this.description = description;
		this.icon = icon;
		this.isDefaultItem = isDefault;
		this.tag = tag;
	}

    // Called when the item is pressed in the inventory
    public virtual void Use ()
	{
		// Use the item
		// Something might happen
        
		Debug.Log("Using " + name);
	}

	public void RemoveFromInventory ()
	{
		Inventory.instance.Remove(this);
	}
	
}
