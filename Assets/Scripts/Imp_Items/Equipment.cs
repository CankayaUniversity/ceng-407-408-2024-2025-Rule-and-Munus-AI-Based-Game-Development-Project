using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* An Item that can be equipped. */

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {



	public EquipmentSlot equipSlot; // Slot to store equipment in
	// public Player player; //Character of the player

	public int armorModifier;		// Increase/decrease in armor
	public int damageModifier;      // Increase/decrease in damage
    public int attack = 0;
    public int defence = 0;
    public int STR = 0;
    public int DEX = 0;
    public int INT = 0;
    public int WIS = 0;
    public int CON = 0;
    public int CHA = 0;
    public SkinnedMeshRenderer mesh;
    public EquipmentManager.MeshBlendShape[] coveredMeshRegions;

	// When pressed in inventory
	public override void Use()
	{
		base.Use();
		EquipmentManager.instance.Equip(this);	// Equip it
		RemoveFromInventory();					// Remove it from inventory
	}

}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet }
