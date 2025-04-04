using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;
using Stats;

/* An Item that can be equipped. */

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {
	public EquipmentSlot equipSlot; // Slot to store equipment in
	// public Player player; //Character of the player
	public Rarity rarirty;
	public int armorModifier;		// Increase/decrease in armor
	public int damageModifier;      // Increase/decrease in damage
    public StatModifier attack;
    public StatModifier defence;
	public Dictionary<StatType, StatModifier> statModifiers = _stats.statModifiers;
	public SkinnedMeshRenderer mesh;
    public EquipmentManager.MeshBlendShape[] coveredMeshRegions;

	public Equipment(EquipmentSlot equipmentSlot, Rarity rarity, int armorModifier, int damageModifier)
	{
		this.equipSlot = equipmentSlot;
		this.rarirty = rarity;
		this.armorModifier = armorModifier;
		this.damageModifier = damageModifier;
	}
	public Equipment(EquipmentSlot equipmentSlot, Rarity rarity, int armorModifier, int damageModifier, Dictionary<StatType, StatModifier> modifiers): this(equipmentSlot, rarity, armorModifier, damageModifier)
	{
		AdjustStatModifiers(modifiers);
	}
	public void AdjustStatModifiers(Dictionary<StatType, StatModifier> modifiers)
	{
		if(modifiers.ContainsKey(StatType.STR))
		{
			statModifiers[StatType.STR] = modifiers[StatType.STR];
		}
		if(modifiers.ContainsKey(StatType.DEX))
		{
			statModifiers[StatType.DEX] = modifiers[StatType.DEX];
		}
		if(modifiers.ContainsKey(StatType.INT))
		{
			statModifiers[StatType.INT] = modifiers[StatType.INT];
		}
		if(modifiers.ContainsKey(StatType.WIS))
		{
			statModifiers[StatType.WIS] = modifiers[StatType.WIS];
		}
		if(modifiers.ContainsKey(StatType.CON))
		{
			statModifiers[StatType.CON] = modifiers[StatType.CON];
		}
		if(modifiers.ContainsKey(StatType.CHA))
		{
			statModifiers[StatType.CHA] = modifiers[StatType.CHA];
		}
		if(modifiers.ContainsKey(StatType.LUCK))
		{
			statModifiers[StatType.LUCK] = modifiers[StatType.LUCK];
		}
	}

	// When pressed in inventory
	public override void Use()
	{
		base.Use();
		EquipmentManager.instance.Equip(this);	// Equip it
		
		RemoveFromInventory();					// Remove it from inventory
	}

}


