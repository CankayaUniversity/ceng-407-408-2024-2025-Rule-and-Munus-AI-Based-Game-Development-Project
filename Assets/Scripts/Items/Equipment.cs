using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Types;
using Stats;
using Icons;

/* An Item that can be equipped. */

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {
	public EquipmentSlot equipSlot; // Slot to store equipment in
	// public Player player; //Character of the player
	public Rarity rarirty;
	public int armorModifier;		// Increase/decrease in armor
	public int damageModifier;      // Increase/decrease in damage
    // public StatModifier attack;
    // public StatModifier defence;
	public DamageType damageType;
	public Dictionary<StatType, StatModifier> statModifiers;
	public SkinnedMeshRenderer mesh;
    public EquipmentManager.MeshBlendShape[] coveredMeshRegions;
	// public Equipment()
	// {
	// 	this.InitEquipment(EquipmentSlot.Default, Rarity.Default, DamageType.Default, 0, 0, icons.slotMesh[EquipmentSlot.Default], icons.slotSprite[EquipmentSlot.Default]);
	// }
	public Equipment(EquipmentSlot equipmentSlot, Rarity rarity, DamageType damageType, int armorModifier, int damageModifier, SkinnedMeshRenderer mesh, Sprite icon)
	{
		this.InitEquipment(equipmentSlot, rarity, damageType, armorModifier, damageModifier, mesh, icon);
	}
	public Equipment(EquipmentSlot equipmentSlot, Rarity rarity, DamageType damageType, int armorModifier, int damageModifier, SkinnedMeshRenderer mesh, Sprite icon, Dictionary<StatType, StatModifier> modifiers): this(equipmentSlot, rarity, damageType, armorModifier, damageModifier, mesh, icon)
	{
		AdjustStatModifiers(modifiers);
	}
	public void InitEquipment(EquipmentSlot equipmentSlot, Rarity rarity, DamageType damageType, int armorModifier, int damageModifier, SkinnedMeshRenderer mesh, Sprite icon)
	{
		this.InitItem();
		this.SetIcon(icon);
		this.mesh = mesh;
		statModifiers = _stats.statModifiers;
		this.damageType = damageType;
		this.equipSlot = equipmentSlot;
		this.rarirty = rarity;
		this.armorModifier = armorModifier;
		this.damageModifier = damageModifier;
	}
	public void AdjustStatModifiers(Dictionary<StatType, StatModifier> modifiers)
	{
		Debug.Log($"Type of Equipment: {equipSlot}");
		for(int i = 0; i < modifiers.Count; ++i)
        {
			Debug.Log("Upcoming Modifiers");
        	Debug.Log($"{modifiers.ElementAt(i).Key}: {modifiers.ElementAt(i).Value.value}");
        }

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
	public void setDamageModifier(int damageModifier){
		this.damageModifier = damageModifier;
	}
	public void setArmorModifier(int armorModifier){
		this.armorModifier = armorModifier;
	}

	// When pressed in inventory
	public override void Use()
	{
		base.Use();
		EquipmentManager.instance.Equip(this);	// Equip it
		
		RemoveFromInventory();					// Remove it from inventory
	}

}


