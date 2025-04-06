using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Types;
using Materials;
using System.Linq;

public class Stock : MonoBehaviour {

	#region Singleton

	public static Stock instance;
	public Dictionary<MaterialType, Material> typeMaterial;
	public List<Text> texts;
	void Awake()
	{
    	instance = this;
		typeMaterial = materials.typeMaterial;
		Debug.Log($"Stock Created");

	}

	#endregion
	public void ExpandStock(Material material)
	{
		typeMaterial.Add(material.type, material);
	}
	public void Show()
	{
        for(int i = 0; i < typeMaterial.Count; ++i)
        {
        	Debug.Log($"{typeMaterial.ElementAt(i).Key}: {typeMaterial.ElementAt(i).Value.Count}");
        }
	}
	public bool Add(MaterialType type, int amount)
    {
        if (!typeMaterial.ContainsKey(type))
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
		typeMaterial[type].AddCount(amount);
		return true;
    }
	public bool Add(Material gatheredMaterial)
    {
        if (!typeMaterial.ContainsKey(gatheredMaterial.type))
        {
            Debug.Log("Invalid material type!");
            return false;
        }

		typeMaterial[gatheredMaterial.type].AddCount(gatheredMaterial.Count);
        
		
        return true;
    }

}
