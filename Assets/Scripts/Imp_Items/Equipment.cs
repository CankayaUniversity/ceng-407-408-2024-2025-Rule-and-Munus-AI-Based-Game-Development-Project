using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

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
    public StatModifier STR;
    public StatModifier DEX;
    public StatModifier INT;
    public StatModifier WIS;
    public StatModifier CON;
    public StatModifier CHA;
	public StatModifier LUCK;
    public SkinnedMeshRenderer mesh;
    public EquipmentManager.MeshBlendShape[] coveredMeshRegions;

	public Equipment(EquipmentSlot equipmentSlot, Rarity rarity, int armorModifier, int damageModifier)
	{
		this.equipSlot = equipmentSlot;
		this.rarirty = rarity;
		this.armorModifier = armorModifier;
		this.damageModifier = damageModifier;
		STR = new StatModifier(0, StatModType.Flat);
		DEX = new StatModifier(0, StatModType.Flat);
		INT = new StatModifier(0, StatModType.Flat);
		WIS = new StatModifier(0, StatModType.Flat);
		CON = new StatModifier(0, StatModType.Flat);
		CHA = new StatModifier(0, StatModType.Flat);
	}
	public Equipment(EquipmentSlot equipmentSlot, Rarity rarity, int armorModifier, int damageModifier, Dictionary<string, StatModifier> modifiers): this(equipmentSlot, rarity, armorModifier, damageModifier)
	{
		if(modifiers.ContainsKey("STR"))
		{
			STR = modifiers["STR"];
		}
		if(modifiers.ContainsKey("DEX"))
		{
			DEX = modifiers["DEX"];
		}
		if(modifiers.ContainsKey("STR"))
		{
			INT = modifiers["INT"];
		}
		if(modifiers.ContainsKey("WIS"))
		{
			WIS = modifiers["WIS"];
		}
		if(modifiers.ContainsKey("CON"))
		{
			CON = modifiers["CON"];
		}
		if(modifiers.ContainsKey("CHA"))
		{
			CHA = modifiers["CHA"];
		}
		if(modifiers.ContainsKey("LUCK"))
		{
			LUCK = modifiers["LUCK"];
		}
	}
	public void AdjustStatModifiers(Dictionary<StatType, StatModifier> modifiers)
	{
		if(modifiers.ContainsKey(StatType.STR))
		{
			STR = modifiers[StatType.STR];
		}
		if(modifiers.ContainsKey(StatType.DEX))
		{
			DEX = modifiers[StatType.DEX];
		}
		if(modifiers.ContainsKey(StatType.INT))
		{
			INT = modifiers[StatType.INT];
		}
		if(modifiers.ContainsKey(StatType.WIS))
		{
			WIS = modifiers[StatType.WIS];
		}
		if(modifiers.ContainsKey(StatType.CON))
		{
			CON = modifiers[StatType.CON];
		}
		if(modifiers.ContainsKey(StatType.CHA))
		{
			CHA = modifiers[StatType.CHA];
		}
		if(modifiers.ContainsKey(StatType.LUCK))
		{
			LUCK = modifiers[StatType.LUCK];
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


