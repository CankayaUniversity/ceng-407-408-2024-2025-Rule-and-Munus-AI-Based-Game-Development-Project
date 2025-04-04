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
	void Awake ()
	{
		instance = this;
	}

	#endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Attributes attributes = Attributes.instance;
    public Stat luck;
    void Start()
    {

    }

    public void GenerateLoot()
    {
        luck = attributes.stats[StatType.LUCK];
        LootGenerator.RandomEquipment(luck);
        LootGenerator.RandomMaterial(luck);
    }
}
