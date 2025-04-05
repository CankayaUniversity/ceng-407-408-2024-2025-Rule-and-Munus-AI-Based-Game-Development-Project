using System.Collections.Generic;
using Types;
using UnityEngine;
using UnityEngine.Rendering;

public class AttributeManager : MonoBehaviour {

	#region Singleton

	public static AttributeManager instance;

	void Awake ()
	{
		instance = this;
	}

	#endregion
	new GameObject gameObject;
	Attributes attributes;
	void Start ()
	{
		gameObject  = GameObject.Find("Player");
		attributes = gameObject.GetComponent<Attributes>();
	}
	// void Update()
	// {
	// 	if (equipmentManager.flag)
	// 	{
	// 		// Stat.CalculateValue();
	// 	}
	// }

	public void UpdateStats(Equipment equipment, bool isEquiped)
	{
		Dictionary<StatType, StatModifier> modifiers = equipment.statModifiers;
		if(isEquiped)
		{
			if(modifiers.ContainsKey(StatType.STR))
			{
				attributes.Add(StatType.STR, modifiers[StatType.STR]);
			}
			if(modifiers.ContainsKey(StatType.DEX))
			{
				attributes.Add(StatType.DEX, modifiers[StatType.DEX]);
			}
			if(modifiers.ContainsKey(StatType.INT))
			{
				attributes.Add(StatType.INT, modifiers[StatType.INT]);
			}
			if(modifiers.ContainsKey(StatType.WIS))
			{
				attributes.Add(StatType.WIS, modifiers[StatType.WIS]);
			}
			if(modifiers.ContainsKey(StatType.CON))
			{
				attributes.Add(StatType.CON, modifiers[StatType.CON]);
			}
			if(modifiers.ContainsKey(StatType.CHA))
			{
				attributes.Add(StatType.CHA, modifiers[StatType.CHA]);
			}
			if(modifiers.ContainsKey(StatType.LUCK))
			{
				attributes.Add(StatType.LUCK, modifiers[StatType.LUCK]);
			}
		}
	}

}
