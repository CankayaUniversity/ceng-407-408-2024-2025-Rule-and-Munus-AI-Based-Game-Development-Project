using UnityEngine;
using Unity.Mathematics;

public class Attributes : MonoBehaviour 
{
    public string Name = "Lorem";
    public string _class = "Fighter";
    public string race = "Ipsum";
    public int MaxHealth = 100;
    public int CurrentHealth = 0;
    public int MaxStamina = 100;
    public int CurrentStamina = 0;
    // public Dictionary<string, int> stats = new Dictionary<string, int> ();
    public Stat attack = new Stat(1);
    public Stat defence = new Stat(1);
    public Stat STR = new Stat(1);
    public Stat DEX = new Stat(1);
    public Stat INT = new Stat(1);
    public Stat WIS = new Stat(1);
    public Stat CON = new Stat(1);
    public Stat CHA = new Stat(1);
    public Stat LUCK = new Stat(1);
    public int Limit = 100;

    #region Singleton

	public static Attributes instance;

	void Awake()
	{
		instance = this;
	}

	#endregion

    // public void Add(string name, int value)
    // {
	// 	stats.Add(name, value);
    //     Debug.Log($"{name} Stat Added {value}!");
    // }
    public void Add(string name, StatModifier modifier)
    {
		switch(name)
        {
            case "STR":
                STR.AddModifier(modifier);
                Debug.Log($"{name} Stat Added {modifier.value}!");
                break;
            case "DEX":
                DEX.AddModifier(modifier);
                Debug.Log($"{name} Stat Added {modifier.value}!");
                break;
            case "INT":
                INT.AddModifier(modifier);
                Debug.Log($"{name} Stat Added {modifier.value}!");
                break;
            case "WIS":
                WIS.AddModifier(modifier);
                Debug.Log($"{name} Stat Added {modifier.value}!");
                break;
            case "CON":
                CON.AddModifier(modifier);
                Debug.Log($"{name} Stat Added {modifier.value}!");
                break;
            case "CHA":
                CHA.AddModifier(modifier);
                Debug.Log($"{name} Stat Added {modifier.value}!");
                break;
            case "LUCK":
                LUCK.AddModifier(modifier);
                Debug.Log($"{name} Stat Added {modifier.value}!");
                break;
            default:
                Debug.Log($"Unrecognized stat modifier!");
                Debug.Log($"Name {name} Value: {modifier.value}!");
                break;
        }
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
        Debug.Log($"{Name} - STR: {STR.value}, DEX: {DEX.value}, CON: {CON.value}, INT: {INT.value}, WIS: {WIS.value}, CHA: {CHA.value}");
    }

	
}
