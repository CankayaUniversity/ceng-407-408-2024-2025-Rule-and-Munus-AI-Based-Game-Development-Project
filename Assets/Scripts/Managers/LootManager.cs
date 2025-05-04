using UnityEngine;
using System;
using Types;
using UnityEngine.TextCore.Text;

public class LootManager : MonoBehaviour
{
	# region Singleton
	public Equipment[] defaultEquipment;
    public Material[] defaultMaterial;
    public new GameObject gameObject;
	public static LootManager instance;
    public Stat luck;
    void Awake ()
	{
		instance = this;
    }

	#endregion
    void Start()
    {
        luck = gameObject.GetComponent<Attributes>().stats[StatType.LUCK];
    }

    public void GenerateLoot()
    {
        Debug.Log($"Luck is --------->: {luck.value}");
        LootGenerator.RandomEquipment(luck);
        LootGenerator.RandomMaterial(luck);
    }
}
