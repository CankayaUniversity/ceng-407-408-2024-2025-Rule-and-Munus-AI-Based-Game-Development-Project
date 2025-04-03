using System.Collections.Generic;
using UnityEngine;

public class Stock : MonoBehaviour {

	#region Singleton

	public static Stock instance;

	void Awake()
	{
    	instance = this;
	}

	#endregion


	// Current list of items in inventory
	public Dictionary<string, Material> materials = new Dictionary<string, Material>();
	public void ExpandStock(Material material)
	{
		materials.Add(material.name, material);
	}
	public bool Add(string name, int amount)
    {
        if (!materials.ContainsKey(name))
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

		else
		{
			Debug.Log("Exceed the max amount of: " + name);
			return false;
		}
    }
	public bool Add(Material gatheredMaterial)
    {
        if (!materials.ContainsKey(gatheredMaterial.name))
        {
            Debug.Log("Invalid material type!");
            return false;
        }

		materials[gatheredMaterial.Name].AddCount(gatheredMaterial.Count);
        
		
        return true;
    }
	public void Add(Material gatheredMaterial, int temp)
    {
		// Special case for adding new Material
		materials.Add(gatheredMaterial.name, gatheredMaterial);
    }

}
