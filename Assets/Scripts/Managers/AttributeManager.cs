using System.Collections.Generic;
using System.Linq;
using Types;
using UnityEngine;
using TMPro;
public class AttributeManager : MonoBehaviour {

	#region Singleton
	public GameManager gameManager;
	public static AttributeManager instance;
	public Attributes attributes;
	public int attributtePoints;
	private int maxPoint = 5;
	public List<GameObject> textList; //On the inspector, assign objects that have textmeshprogui component, where the attributes will be displayed.

	void Awake ()
	{
		instance = this;
		attributtePoints = maxPoint;
		UpdateTexts();
	}

	#endregion
	void Start ()
	{

	}
	public void SetAttributePoints(int points)
	{
		attributtePoints = points;
	}
	public void UpdateStats(Equipment equipment, bool isEquiped)
	{
		if (isEquiped)
		{
			foreach (StatType type in equipment.statModifiers.Keys)
			{
				if (attributes.stats.ContainsKey(type))
				{
					attributes.Add(type, equipment.statModifiers[type]);	
				}
			}
		}
		else
		{
			foreach (StatType type in equipment.statModifiers.Keys)
			{
				if (attributes.stats.ContainsKey(type))
				{
					attributes.Remove(type, equipment.statModifiers[type]);	
				}
			}
		}
		//ShowStats();
		UpdateTexts();
	}
	public void ShowStats()
	{
		attributes.ShowStats();
	}
	public void UpdateTexts()
	{
		int index = 0;
		//In order to: STR -> DEX -> CON -> INT -> WIS -> CHA -> LUCK -> DEFAULT
		if (textList.Count > 0)
		{
			foreach (StatType type in attributes.stats.Keys)
			{
				if ((int)type != (int)StatType.Default)
					//Debug.LogError($"Attribute: {(int)type} Value: {attributes.stats[type].value}");
					textList[index++].GetComponent<TextMeshProUGUI>().text = attributes.stats[type].value.ToString();
			}
		}
	}
	public void SetName(string name)
    {
        attributes.SetName(name);
    }
    public void AddStatModifier(StatType type, StatModifier modifier)
    {
        attributes.Add(type, modifier);
		UpdateTexts();
    }
    public void RemoveStatModifier(StatType type, StatModifier modifier)
    {
        attributes.Remove(type, modifier);
		UpdateTexts();
    }
    public void IncreaseBaseStat(int index)
    {
		StatType type = (StatType)index;
        if(attributtePoints > 0){
			attributes.IncreaseBase(type);
			attributtePoints--;
			UpdateTexts();
		}
		else
		{
			gameManager.DeactivateIncreaseButtons();
		}
    }
    public void DecreaseBaseStat(int index)
    {
		StatType type = (StatType)index;
        if(attributtePoints < maxPoint && attributes.DecreaseBase(type)){
			attributtePoints++;
			UpdateTexts();
		}
		else
		{
			gameManager.DeactivateDecreaseButtons();
		}
    }
    public bool UpdateHealth(int value)
    {
        attributes.UpdateHealth(value);
        return attributes.IsDead();
    }
    public void UpdateStamina(int value)
    {
        attributes.UpdateStamina(value);
    }
    public void UpdateAttack(int value)
    {
        attributes.UpdateAttackModifier(value);
    }
    public void UpdateDefence(int value)
    {
        attributes.UpdateDefenceModifier(value);
    }

}
