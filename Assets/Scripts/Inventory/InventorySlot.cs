using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Types;
using Equipments;

/* Sits on all InventorySlots. */

public class InventorySlot : MonoBehaviour {

	public Image icon;			// Reference to the Icon image
	public Button removeButton;	// Reference to the remove button
	public Item item;  // Current item in the slot
	// public ScriptableObject gameObject = new ScriptableObject.CreateInstance<Equipment>();
	public Equipment equipment;
	public EquipmentManager equipmentManager;
	public Inventory inventory;
	bool isEquiped;

    // Add item to the slot
    // public void Awake()
    // {
	// 	isEquiped = false;
    //     equipment = ItemGenerator.Generate(EquipmentType.defaultEquipment, Rarity.Default);
    // }
	public void Start()
    {
		isEquiped = false;
        equipment = ItemGenerator.Generate(EquipmentType.defaultEquipment, Rarity.Default);
		// gameObject = Equipment.CreateInstance(equipmentManager);
    }
    public void AddItem (Equipment equipment)
	{
		this.equipment = equipment;
		isEquiped = false;
		icon.sprite = equipment.icon;
		icon.enabled = true;
		removeButton.interactable = true;
	}
	public void AddItem (Item newItem)
	{
		item = newItem;

		icon.sprite = item.icon;
		icon.enabled = true;
		removeButton.interactable = true;
	}

	// Clear the slot
	public void ClearSlot ()
	{
		item = null;
		isEquiped = true;
		icon.sprite = null;
		icon.enabled = false;
		removeButton.interactable = false;
	}

	// Called when the remove button is pressed
	public virtual void OnRemoveButton ()
	{
		// equipmentManager.Unequip(equipment);
		inventory.Remove(item);
		ClearSlot();
	}
	// Called when the item is pressed
	public void UseItem ()
	{
		// if (item != null)
		// {
		// 	item.Use();
		// }
		if (equipment != null && isEquiped == false)
		{
			equipmentManager.Equip(equipment);
			removeButton.interactable = false;
			isEquiped = true;
		}
	}

}
