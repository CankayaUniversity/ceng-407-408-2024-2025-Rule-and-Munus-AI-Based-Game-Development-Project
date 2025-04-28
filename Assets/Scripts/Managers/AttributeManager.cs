using System.Collections.Generic;
using System.Linq;
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
		var statList = attributes.stats.Keys;
		StatType stat;
		if(isEquiped)
		{
			for(int i = 0; i < modifiers.Count; ++i)
			{
				stat = statList.ElementAt(i);
				if(modifiers.ContainsKey(stat))
				{
					attributes.Add(stat, modifiers[stat]);
				}
			}
		}
		else
		{
			for(int i = 0; i < modifiers.Count; ++i)
			{
				stat = statList.ElementAt(i);
				if(modifiers.ContainsKey(stat))
				{
					attributes.Remove(stat, modifiers[stat]);
				}
			}
		}
	}

}
