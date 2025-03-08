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
    public string description = "Null";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public string tag = "Item";
    public int attack = 0;
    public int defence = 0;
    public int STR = 0;
    public int DEX = 0;
    public int INT = 0;
    public int WIS = 0;
    public int CON = 0;
    public int CHA = 0;


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
