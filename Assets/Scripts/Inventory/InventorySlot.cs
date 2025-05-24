using UnityEngine;
using UnityEngine.UI;
using System;

/* Sits on all InventorySlots. */

public class InventorySlot : MonoBehaviour {

	public Image icon;			// Reference to the Icon image
	public Button removeButton;	// Reference to the remove button
	public Button useButton;	// Reference to the remove button
	public Item item;  // Current item in the slot
	public Equipment equipment;
	bool isEquiped;
	public int slotIndex; // NEW - To keep track of this slot's index in the UI
	public void Start()
    {
		isEquiped = false;
    }

    public void Setup(int index, Action<int> onRemove, Action<int> onUse)
    {
        slotIndex = index;
        removeButton.onClick.AddListener(() => onRemove(slotIndex));
        useButton.onClick.AddListener(() => onUse(slotIndex));
    }
    public void AddItem (Equipment equipment)
	{
		if (equipment == null)
		{
			Debug.LogError("Upcoming Equipment to Slot is Null!");
		}
		//Debug.LogError("Slot created!");
		//Debug.Log($"Upcoming slot: {equipment.equipSlot}");
		//Debug.Log($"Upcoming icon: {equipment.icon}");
		this.equipment = equipment;
		isEquiped = false;
		icon.sprite = equipment.icon;
		icon.enabled = true;
		useButton.interactable = true;
		removeButton.interactable = true;
	}
	public void AddItem (Item newItem)
	{
		item = newItem;
		icon.sprite = item.icon;
		icon.enabled = true;
		useButton.interactable = true;
		removeButton.interactable = true;
	}
	public void ClearSlot ()
	{
		//Debug.LogError("Slot cleared!");
		item = null;
		isEquiped = true;
		icon.sprite = null;
		icon.enabled = false;
		useButton.interactable = false;
		removeButton.interactable = false;
	}
	// public virtual void OnRemoveButton ()
	// {
	// 	ClearSlot();
	// }
	public void UseItem ()
	{
		if (equipment != null && isEquiped == false)
		{
			removeButton.interactable = false;
			isEquiped = true;
		}
	}

}
