using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;
using Types;
using Stats;
using System.Linq;

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

    #region Singleton

	public static Attributes instance;

	void Awake()
	{
        Debug.Log("Attributes created");
        // ShowStats();
		instance = this;
	}

	#endregion

    // public void Add(string name, int value)
    // {
	// 	stats.Add(name, value);
    //     Debug.Log($"{name} Stat Added {value}!");
    // }
    public void Add(StatType type, StatModifier modifier)
    {
        stats[type].AddModifier(modifier);
    }
    public void Remove(StatType type, StatModifier modifier)
    {
        stats[type].RemoveModifier(modifier);
    }
    // public bool UpdateStats(string stat, int value)
    // {
    //     if (stats.ContainsKey(stat))
    //     {
    //         Debug.Log("Unrecognized Stat Type!" + stat);
    //         return false;
    //     }
    //     stats[stat] = math.clamp(value, 0, Limit);
    //     Debug.Log($"{stat} Stat Adjusted {value}!");
    //     return true;
    // }

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
        Debug.Log($"{stats.ElementAt(i).Key}: {stats.ElementAt(i).Value.baseValue}");
        }
    }

	
}
