using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;
using TMPro;
using Types;
using Stats;
using System.Linq;
using UnityEngine.UI;

public class Attributes : MonoBehaviour 
{
    public string Name = "Lorem";
    public string _class = "Fighter";
    public string race = "Ipsum";
    public readonly int maxHealth = 100;
    public int currentHealth = 0;
    public Slider healthBar; 
    public readonly int maxStamina = 80;
    public int currentStamina = 0;
    public Slider staminaBar; 
    public bool isDead = false;
    public Dictionary<StatType, Stat> stats = _stats.stats;
    public int Limit = 100;
    public List<GameObject> textList;


    #region Singleton

	public static Attributes instance;

	void Awake()
	{
        instance = this;
        StaminaBar(currentStamina);
        HealthBar(currentHealth);
        isDead = false;
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
        this.currentHealth = (int)math.clamp(value, 0, maxHealth);
        HealthBar(currentHealth);
        if (this.currentHealth <= 0)
        {
            isDead = true;
            Debug.Log($"{Name} is dead!");
        }
        Debug.Log($"{currentHealth} Health Adjusted {value}.");
    }

    public void UpdateStamina(int value)
    {
        this.currentStamina = (int)math.clamp(value, 0, maxStamina);
        Debug.Log($"{currentStamina} Health Adjusted {value}.");
    }

    public void ShowStats()
    {
        Debug.Log($"{Name}");
        for(int i = 0; i < stats.Count; ++i)
        {
        Debug.Log($"{stats.ElementAt(i).Key}: {stats.ElementAt(i).Value.value}");
        }
    }

    private void StaminaBar(int value)
    {
        staminaBar.value = value;
    }
	private void HealthBar(int value)
    {
        healthBar.value = value;
    }
}
