using UnityEngine;
using Types;
public class LootManager : MonoBehaviour
{
	# region Singleton
	public static LootManager instance;
    public Attributes attributes;
    public Stock stock;
    public Stat luck;
    void Awake ()
	{
		instance = this;
    }

	#endregion
    void Start()
    {
        luck = attributes.stats[StatType.LUCK];
    }

    public void GenerateLoot()
    {
        Debug.Log($"Luck is --------->: {luck.value}");
        LootGenerator.RandomEquipment(luck);
        LootGenerator.RandomMaterial(luck);
    }
    public void GenerateMEquipment()
    {
        LootGenerator.RandomEquipment(luck);
    }
    public void GenerateMaterial()
    {
        LootGenerator.RandomMaterial(luck);
    }
}
