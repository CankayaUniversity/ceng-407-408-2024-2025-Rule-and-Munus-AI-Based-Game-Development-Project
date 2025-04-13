using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;
using TMPro;
using Types;
using Stats;
using System.Linq;
using System;

public class Attributes : MonoBehaviour 
{
    public string Name = "Lorem";
    public string _class = "Fighter";
    public string race = "Ipsum";
    public int MaxHealth = 100;
    public int CurrentHealth = 0;
    public int MaxStamina = 100;
    public int CurrentStamina = 0;
    public Dictionary<StatType, Stat> stats = _stats.stats;
    public int Limit = 100;
    public List<GameObject> textList;


    #region Singleton

	public static Attributes instance;

	void Awake()
	{
        instance = this;
        textList = new List<GameObject>(){
			GameObject.Find("STR_Value"), 
			GameObject.Find("DEX_Value"),
			GameObject.Find("INT_Value"),
			GameObject.Find("WIS_Value"),
            GameObject.Find("CON_Value"),
            GameObject.Find("CHA_Value"),
            GameObject.Find("LUCK_Value"),
		};
		for(int i = 0; i < stats.Count - 1 ; ++i)
		{
			textList[i].GetComponent<TextMeshProUGUI>().text = stats.ElementAt(i).Value.value.ToString();
			Debug.Log($"{textList[i].GetComponent<TextMeshProUGUI>().text}");
		}
		Debug.Log($"Stats Bounded");
        // ShowStats();
	}

	#endregion
    public void Add(StatType type, StatModifier modifier)
    {
        stats[type].AddModifier(modifier);
        UpdateText();
        // ShowStats();
    }
    public void Remove(StatType type, StatModifier modifier)
    {
        stats[type].RemoveModifier(modifier);
        UpdateText();
        // ShowStats();
    }
    public void IncreaseBase(string type)
    {
        stats[Converter(type)].IncreaseBase();
        UpdateText();
        // ShowStats();
    }
    public void DecreaseBase(string type)
    {
        stats[Converter(type)].DecreaseBase();
        UpdateText();
        // ShowStats();
    }
    protected StatType Converter(string type)
    {
        switch(type)
        {
            case "STR":
                return StatType.STR;
            case "DEX":
                return StatType.DEX;
            case "INT":
                return StatType.INT;
            case "WIS":
                return StatType.WIS;
            case "CON":
                return StatType.CON;
            case "CHA":
                return StatType.CHA;
            case "LUCK":
                return StatType.LUCK;
            default:
                Debug.Log("Unrecognized Stat Type");
                return StatType.Default;
        }
    } 
    public void UpdateText()
    {
        for(int i = 0; i < textList.Count; ++i)
		{
			textList[i].GetComponent<TextMeshProUGUI>().text = stats.ElementAt(i).Value.value.ToString();
			Debug.Log($"{textList[i].GetComponent<TextMeshProUGUI>().text}");
		}
    }

    public void UpdateHealth(int value)
    {
        this.CurrentHealth = (int)math.clamp(value, 0, MaxHealth);
        Debug.Log($"{CurrentHealth} Health Adjusted {value}.");
    }

    public void UpdateStamina(int value)
    {
        this.CurrentStamina = (int)math.clamp(value, 0, MaxStamina);
        Debug.Log($"{CurrentStamina} Health Adjusted {value}.");
    }

    public void ShowStats()
    {
        Debug.Log($"{Name}");
        for(int i = 0; i < stats.Count; ++i)
        {
        Debug.Log($"{stats.ElementAt(i).Key}: {stats.ElementAt(i).Value.value}");
        }
    }

	
}
