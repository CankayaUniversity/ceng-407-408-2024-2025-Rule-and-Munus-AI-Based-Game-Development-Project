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
    private readonly int maxHealth = 100;
    [SerializeField]
    public int currentHealth = 100;
    // public Slider healthBar; 
    private readonly int maxStamina = 80;
    [SerializeField]
    public int currentStamina = 80;
    // public Slider staminaBar; 
    public bool isDead = false;
    [SerializeField]
    private int attackModifier = 10;
    [SerializeField]
    private int defenceModifier = 5;
    public Dictionary<StatType, Stat> stats = _stats.stats;
    #region Singleton

	public static Attributes instance;

	void Awake()
	{
        instance = this;
        StaminaBar(currentStamina);
        HealthBar(currentHealth);
        isDead = false;
		// for(int i = 0; i < stats.Count - 1 ; ++i)
		// {
		// 	textList[i].GetComponent<TextMeshProUGUI>().text = stats.ElementAt(i).Value.value.ToString();
		// 	Debug.Log($"{textList[i].GetComponent<TextMeshProUGUI>().text}");
		// }
		// Debug.Log($"Stats Bounded");
        // // ShowStats();
	}

	#endregion
    public void SetName(string name)
    {
        this.name = name;
    }
    public void Add(StatType type, StatModifier modifier)
    {
        stats[type].AddModifier(modifier);
    }
    public void Remove(StatType type, StatModifier modifier)
    {
        stats[type].RemoveModifier(modifier);

    }
    public void IncreaseBase(StatType type)
    {
        stats[type].IncreaseBase();
    }
    public bool DecreaseBase(StatType type)
    {
        return stats[type].DecreaseBase();
    }
    public bool IsDead()
    {
        return isDead;
    }
    public void UpdateHealth(int value)
    {
        this.currentHealth = (int)math.clamp(currentHealth + value, 0, maxHealth);
        if(this.currentHealth <= 0)
        {
            isDead = true;
            Debug.Log($"{Name} is dead!");
        }
        HealthBar(this.currentHealth);
        Debug.Log($"{currentHealth} Health Adjusted by {value}.");
    }

    public void UpdateStamina(int value)
    {
        this.currentStamina = (int)math.clamp(currentStamina + value, 0, maxStamina);
        StaminaBar(this.currentStamina);
        Debug.Log($"{currentStamina} Stamina Adjusted by {value}.");
    }

    public void ShowStats()
    {
        Debug.Log($"{Name}");
        for(int i = 0; i < stats.Count; ++i)
        {
        Debug.Log($"{stats.ElementAt(i).Key}: {stats.ElementAt(i).Value.value}");
        }
    }
    public void UpdateAttackModifier(int value)
    {
        this.attackModifier = (int)math.max(0, attackModifier + value);
        Debug.Log($"{attackModifier} Attack Modifier Adjusted by {value}.");
    }
    public void UpdateDefenceModifier(int value)
    {
        this.defenceModifier = (int)math.max(0, defenceModifier + value);
        Debug.Log($"{defenceModifier} Defence Modifier Adjusted by {value}.");
    }
    public void StaminaBar(int value)
    {
        // staminaBar.value = value;
    }
	public void HealthBar(int value)
    {
        // healthBar.value = value;
    }
    public Stat GetStat(StatType type)
    {
        return stats[type];
    } 
}
