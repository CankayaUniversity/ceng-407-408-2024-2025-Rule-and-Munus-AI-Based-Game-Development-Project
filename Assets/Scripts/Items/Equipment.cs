using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Types;
using Stats;
using UnityEngine.XR;

/* An Item that can be equipped. */

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {
	public Types.EquipmentSlot equipSlot; // Slot to store equipment in
	// public Player player; //Character of the player
	public Rarity rarirty;
	public int armorModifier;		// Increase/decrease in armor
	public int damageModifier;      // Increase/decrease in damage
    public StatModifier attack;
    public StatModifier defence;
	public Dictionary<StatType, StatModifier> statModifiers;
	public SkinnedMeshRenderer mesh;
    public EquipmentManager.MeshBlendShape[] coveredMeshRegions;
	public Equipment(EquipmentSlot equipmentSlot, Rarity rarity, int armorModifier, int damageModifier, SkinnedMeshRenderer mesh, Sprite icon)
	{
		this.InitEquipment(equipmentSlot, rarity, armorModifier, damageModifier, mesh, icon);
	}
	public Equipment(EquipmentSlot equipmentSlot, Rarity rarity, int armorModifier, int damageModifier, SkinnedMeshRenderer mesh, Sprite icon, Dictionary<StatType, StatModifier> modifiers): this(equipmentSlot, rarity, armorModifier, damageModifier, mesh, icon)
	{
		AdjustStatModifiers(modifiers);
	}
	public void InitEquipment(EquipmentSlot equipmentSlot, Rarity rarity, int armorModifier, int damageModifier, SkinnedMeshRenderer mesh, Sprite icon)
	{
		this.InitItem();
		this.SetIcon(icon);
		this.mesh = mesh;
		statModifiers = _stats.statModifiers;
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
		// for(int i = 0; i < statModifiers.Count; ++i)
        // {
		// 	Debug.Log("Current Modifiers");
        // 	Debug.Log($"{statModifiers.ElementAt(i).Key}: {statModifiers.ElementAt(i).Value.value}");
        // }
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
		// Debug.Log("Current Modifiers");
		// for(int i = 0; i < statModifiers.Count; ++i)
        // {
        // 	Debug.Log($"{statModifiers.ElementAt(i).Key}: {statModifiers.ElementAt(i).Value.value}");
        // }

	}

	// When pressed in inventory
	public override void Use()
	{
		base.Use();
		EquipmentManager.instance.Equip(this);	// Equip it
		
		RemoveFromInventory();					// Remove it from inventory
	}

}


