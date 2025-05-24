using UnityEngine;
using System;
using Types;
using System.Collections.Generic;
using Equipments;
using Unity.AppUI.UI;
public class EquipmentManager : MonoBehaviour {

	#region Singleton
	public bool flag = false;
    public enum MeshBlendShape {Head, Body, Legs, Feet, Default};
    public Equipment[] defaultEquipment;
	public static EquipmentManager instance;
	public SkinnedMeshRenderer targetMesh;
	public List<Equipment> currentEquipment;   // Items we currently have equipped
	public Inventory inventory;	
	public InventoryUI inventoryUI;
	public AttributeManager attributeManager;
	public List<Button> removeButtons;
	public List<Button> useButtons;
    SkinnedMeshRenderer[] currentMeshes;
	void Awake ()
	{
		instance = this;
		// Initialize currentEquipment based on number of equipment slots
		int numSlots = Enum.GetNames(typeof(EquipmentSlot)).Length;
		currentEquipment = new List<Equipment>(numSlots);
		FillDefault();
		foreach (Equipment equipment in currentEquipment)
		{
			Debug.Log($"I have: {equipment.equipSlot} with {equipment.rarirty}");
		}
		Equip(ItemGenerator.Generate(EquipmentType.headLeatherArmor, Rarity.Legendary));
		foreach (Equipment equipment in currentEquipment)
		{
			Debug.Log($"I have: {equipment.rarirty}, {equipment.equipSlot}");
		}
        currentMeshes = new SkinnedMeshRenderer[numSlots];
		// EquipDefaults();
	}
	#endregion
	void Start ()
	{
		attributeManager = GetComponent<AttributeManager>();
		//inventoryUI.SetupSlots(RemoveEquipmentAt, UseEquipmentAt);
	}
	public void RemoveEquipmentAt(int index)
	{
		if (index < inventory.equipments.Count && inventory.equipments[index] != null)
		{
			inventory.Remove(index);
			inventoryUI.UpdateUI(inventory.equipments);
    	}
	}

	public void UseEquipmentAt(int index)
	{
    	if (index < inventory.equipments.Count && inventory.equipments[index] != null)
    	{
    	    Equip(inventory.equipments[index]);
    	}
	}
	public void FillDefault()
	{
		currentEquipment.Add(ItemGenerator.Generate(EquipmentType.headLeatherArmor, Rarity.Common));
	    currentEquipment.Add(ItemGenerator.Generate(EquipmentType.bodyLeatherArmor, Rarity.Common));
		currentEquipment.Add(ItemGenerator.Generate(EquipmentType.legLeatherArmor, Rarity.Common));
		currentEquipment.Add(ItemGenerator.Generate(EquipmentType.feetLeatherArmor, Rarity.Common));
		currentEquipment.Add(ItemGenerator.Generate(EquipmentType.shortSword, Rarity.Common));
		currentEquipment.Add(ItemGenerator.Generate(EquipmentType.bow, Rarity.Common));
	}
	public void Equip (Equipment newItem)
	{
		if(newItem != null)
		{
			int slotIndex = (int)newItem.equipSlot-1;
			if (currentEquipment[slotIndex] != null)
			{
				inventory.Remove(newItem);
				Unequip(slotIndex);
				currentEquipment[slotIndex] = newItem;
				attributeManager.UpdateStats(newItem, true);
				inventoryUI.UpdateUI(inventory.equipments);
			}
		}
		else
		{
			Debug.Log("newItem is null!");
		}
	}
	public void Unequip (int slotIndex)
	{
		if (currentEquipment[slotIndex] != null && inventory.equipments.Count<20)
		{
			// Add the item to the inventory
			Equipment oldItem = currentEquipment[slotIndex];
			inventory.Add(oldItem);
			currentEquipment[slotIndex] = null;
			attributeManager.UpdateStats(oldItem, false);
			inventoryUI.UpdateUI(inventory.equipments);
		}
		else
		{
			Debug.Log("Either Inventory is full or specified slot is empty!");
		}
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
	public void Add(Equipment equipment)
	{
		inventory.Add(equipment);
		inventoryUI.UpdateUI(inventory.equipments);
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

    }

	void Update ()
	{
		// Unequip all items if we press U
		if (Input.GetKeyDown(KeyCode.U))
			UnequipAll();
	}

}
