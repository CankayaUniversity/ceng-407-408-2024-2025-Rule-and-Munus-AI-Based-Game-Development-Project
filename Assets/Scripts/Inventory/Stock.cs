using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Types;
using Materials;
using System.Linq;
using TMPro;
using System.Dynamic;
using System;
using UnityEngine.InputSystem.Interactions;

public class Stock : MonoBehaviour {

	#region Singleton

	public static Stock instance;
	public Dictionary<MaterialType, Material> typeMaterial;
	public List<GameObject> textList;
	public bool isUpdated = false;
	void Awake()
	{
    	instance = this;
		typeMaterial = materials.typeMaterial;
		for(int i = 0; i < typeMaterial.Count; ++i)
		{
			textList[i].GetComponent<TextMeshProUGUI>().text = typeMaterial.ElementAt(i).Value.Count.ToString();
			Debug.Log($"{textList[i].GetComponent<TextMeshProUGUI>().text}");
		}
		Debug.Log($"Stock Created!");
	}
    // public void Update()
    // {
    //     if(isUpdated)
	// 	{
	// 		UpdateText();
	// 		isUpdated = false;
	// 	}
	// 	// UpdateText();
    // }
    public void UpdateText()
	{
		for(int i = 0; i < textList.Count; ++i)
		{
			textList[i].GetComponent<TextMeshProUGUI>().text = typeMaterial.ElementAt(i).Value.Count.ToString();
			Debug.Log($"{textList[i].GetComponent<TextMeshProUGUI>().text}");
		}
	}

	#endregion
	public void ExpandStock(Material material)
	{
		typeMaterial.Add(material.type, material);
		isUpdated = true;
		UpdateText();
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
		typeMaterial[type].AddCount(amount);
		isUpdated = true;
		UpdateText();
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
        isUpdated = true;
		UpdateText();
        return true;
    }

}
