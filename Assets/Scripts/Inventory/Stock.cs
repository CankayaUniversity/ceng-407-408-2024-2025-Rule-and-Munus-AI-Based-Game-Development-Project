using System.Collections.Generic;
using UnityEngine;
using Types;
using System.Linq;

public class Stock : MonoBehaviour {

	#region Singleton

	public static Stock instance;
	public Dictionary<MaterialType, Material> materials = new Dictionary<MaterialType, Material>();

	void Awake()
	{
    	instance = this;
		Material[] prefabs = Material.FindObjectsByType<Material>(FindObjectsSortMode.None);
		for(int i = 0; i < prefabs.Length; ++i)
		{
			Debug.Log($"Prefabs: {prefabs[i].name}");
			materials.Add(prefabs[i].type, prefabs[i]);
		}
	}

	#endregion
	public void ExpandStock(Material material)
	{
		materials.Add(material.type, material);
	}
	public void Show()
	{
        for(int i = 0; i < materials.Count; ++i)
        {
        Debug.Log($"{materials.ElementAt(i).Key}: {materials.ElementAt(i).Value.type.ToString()}");
        }
	}
	public bool Add(MaterialType type, int amount)
    {
        if (!materials.ContainsKey(type))
        {
            Debug.Log("Invalid material type!");
            return false;
        }

		// if (materials[name].Count + amount < MaxMaterialAmount)
		// {
		// 	materials[name].AddCount(amount);
		// 	mat.Add(new Material(name, amount));
		// }
		// // If old amount + gathered amount exceed the max amount and updated amount does not exceed max amount.
		// else if (materials[name].Count + amount > MaxMaterialAmount && materials[name].Count + amount - MaxMaterialAmount < MaxMaterialAmount)
		// {
		// 	materials[name].AddCount(materials[name].Count + amount - MaxMaterialAmount);
		// 	mat.Add(new Material(name, amount));
		// }

		// While math.clamp is used for AddCount in Material class, we don't need to check whether old count + gathered amount exceed the max value or not
		
		
		
		
		//materials[name].AddCount(amount);
		//mat.Add(new Material(name, amount));

		// else
		// {
		// 	Debug.Log("Exceed the max amount of: " + name);
		// 	return false;
		// }
		materials[type].AddCount(amount);
		return true;
    }
	public bool Add(Material gatheredMaterial)
    {
        if (!materials.ContainsKey(gatheredMaterial.type))
        {
            Debug.Log("Invalid material type!");
            return false;
        }

		materials[gatheredMaterial.type].AddCount(gatheredMaterial.Count);
        
		
        return true;
    }

}
