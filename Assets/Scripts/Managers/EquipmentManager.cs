using UnityEngine;
using System;
using Types;
using System.Collections.Generic;
using Equipments;

/* Keep track of equipment. Has functions for adding and removing items. */

public class EquipmentManager : MonoBehaviour {

	#region Singleton
	public bool flag = false;
    public enum MeshBlendShape {Head, Body, Legs, Feet, Default};
    public Equipment[] defaultEquipment;
	public static EquipmentManager instance;
	public SkinnedMeshRenderer targetMesh;

    SkinnedMeshRenderer[] currentMeshes;

	void Awake ()
	{
		instance = this;
	}

	#endregion

	public List<Equipment> currentEquipment;   // Items we currently have equipped

	// Callback for when an item is equipped/unequipped
	public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
	public OnEquipmentChanged onEquipmentChanged;
	public Inventory inventory;	
	public InventoryUI inventoryUI;
	AttributeManager attributeManager;
	void Start ()
	{
		// inventory = Inventory.instance;		// Get a reference to our inventory
		inventory.ShowItems();
		attributeManager = GetComponent<AttributeManager>();
		// Initialize currentEquipment based on number of equipment slots
		int numSlots = Enum.GetNames(typeof(EquipmentSlot)).Length;
		currentEquipment = new List<Equipment>(numSlots);
		FillDefault();
        currentMeshes = new SkinnedMeshRenderer[numSlots];
		
        EquipDefaults();
	}
	public void FillDefault()
	{
		// Equipment equipment;
		// equipment = ItemGenerator.Generate(EquipmentType.headLeatherArmor, Rarity.Common);
		// Equip(equipment);
		// equipment = ItemGenerator.Generate(EquipmentType.bodyLeatherArmor, Rarity.Common);
		// Equip(equipment);
		// equipment = ItemGenerator.Generate(EquipmentType.legLeatherArmor, Rarity.Common);
		// Equip(equipment);
		// equipment = ItemGenerator.Generate(EquipmentType.feetLeatherArmor, Rarity.Common);
		// Equip(equipment);
		// equipment = ItemGenerator.Generate(EquipmentType.shortSword, Rarity.Common);
		// Equip(equipment);
		// equipment = ItemGenerator.Generate(EquipmentType.bow, Rarity.Common);
		// Equip(equipment);
		currentEquipment.Add(ItemGenerator.Generate(EquipmentType.headLeatherArmor, Rarity.Legendary));
	    currentEquipment.Add(ItemGenerator.Generate(EquipmentType.bodyLeatherArmor, Rarity.Legendary));
		currentEquipment.Add(ItemGenerator.Generate(EquipmentType.legLeatherArmor, Rarity.Legendary));
		currentEquipment.Add(ItemGenerator.Generate(EquipmentType.feetLeatherArmor, Rarity.Legendary));
		currentEquipment.Add(ItemGenerator.Generate(EquipmentType.shortSword, Rarity.Legendary));
		currentEquipment.Add(ItemGenerator.Generate(EquipmentType.bow, Rarity.Legendary));
	}
	// Equip a new item
	public void Equip (Equipment newItem)
	{
		// inventory.ShowItems();
		// Find out what slot the item fits in
		int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = Unequip(slotIndex);

		// An item has been equipped so we trigger the callback
		if (onEquipmentChanged != null)
		{
			onEquipmentChanged.Invoke(newItem, oldItem);
		}

		// Insert the item into the slot
		currentEquipment[slotIndex] = newItem;
		flag = true;
		attributeManager.UpdateStats(newItem, true);
        AttachToMesh(newItem, slotIndex);
		inventory.ShowItems();
		inventoryUI.UpdateUI();
	}

	// Unequip an item with a particular index
	public Equipment Unequip (int slotIndex)
	{
        Equipment oldItem = null;
		// Only do this if an item is there
		if (currentEquipment[slotIndex] != null)
		{
			// Add the item to the inventory
			oldItem = currentEquipment[slotIndex];
			inventory.Add(oldItem);

            SetBlendShapeWeight(oldItem, 0);
            // Destroy the mesh
            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }

			// Remove the item from the equipment array
			currentEquipment[slotIndex] = null;

			// Equipment has been removed so we trigger the callback
			if (onEquipmentChanged != null)
			{
				onEquipmentChanged.Invoke(null, oldItem);
			}
		}
		attributeManager.UpdateStats(oldItem, false);
		inventory.ShowItems();
		inventoryUI.UpdateUI();
        return oldItem;
	}

	// Unequip all items
	public void UnequipAll ()
	{
		for (int i = 0; i < currentEquipment.Count; i++)
		{
			Unequip(i);
		}

        EquipDefaults();
	}

    void AttachToMesh(Equipment item, int slotIndex)
	{

        SkinnedMeshRenderer newMesh = Instantiate(item.mesh) as SkinnedMeshRenderer;
        newMesh.transform.parent = targetMesh.transform.parent;

        newMesh.rootBone = targetMesh.rootBone;
		newMesh.bones = targetMesh.bones;
		
		currentMeshes[slotIndex] = newMesh;


        SetBlendShapeWeight(item, 100);
       
	}

    void SetBlendShapeWeight(Equipment item, int weight)
    {
		foreach (MeshBlendShape blendshape in item.coveredMeshRegions)
		{
			int shapeIndex = (int)blendshape;
            targetMesh.SetBlendShapeWeight(shapeIndex, weight);
		}
    }

    void EquipDefaults()
    {
		foreach (Equipment e in defaultEquipment)
		{
			Equip(e);
		}
    }

	void Update ()
	{
		// Unequip all items if we press U
		if (Input.GetKeyDown(KeyCode.U))
			UnequipAll();
	}

}
