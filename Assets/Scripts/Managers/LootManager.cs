using UnityEngine;
using System;
using Types;
using UnityEngine.TextCore.Text;

public class LootManager : MonoBehaviour
{
	# region Singleton
	public Equipment[] defaultEquipment;
    public Material[] defaultMaterial;
	public static LootManager instance;
    public Stat luck;
	void Awake ()
	{
		instance = this;
        luck = attributes.stats[StatType.LUCK];
    }

	#endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Attributes attributes = Attributes.instance;
    
    void Start()
    {
        
    }

    public void GenerateLoot()
    {
        // luck = attributes.stats[StatType.LUCK];
        Debug.Log($"Luck is: {luck.value}");
        LootGenerator.RandomEquipment(luck);
        LootGenerator.RandomMaterial(luck);
    }
}
