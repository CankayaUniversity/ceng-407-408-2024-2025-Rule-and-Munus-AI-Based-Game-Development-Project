using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Types;
using Materials;
using System.Linq;
using TMPro;
using System.Dynamic;
using System;

public class Stock : MonoBehaviour {

	#region Singleton

	public static Stock instance;
	public Dictionary<MaterialType, Material> typeMaterial;
	public List<TextMeshProUGUI> texts;
	public List<GameObject> textList;
	public bool isUpdated = false;
	public TextMeshProUGUI temp;
	void Awake()
	{
    	instance = this;
		typeMaterial = materials.typeMaterial;
		texts = new List<TextMeshProUGUI>();
		textList = new List<GameObject>(){
			GameObject.Find("Wood_Count"), 
			GameObject.Find("Stone_Count"),
			GameObject.Find("Iron_Count"),
			GameObject.Find("Cloth_Count")
			};
		for(int i = 0; i < typeMaterial.Count; ++i)
		{
			temp.text = typeMaterial.ElementAt(i).Value.Count.ToString();
			texts.Add(temp);
			Debug.Log($"Awake: {texts[i].text}");
			textList[i].GetComponent<TextMeshProUGUI>().text = temp.text;
		}
		Debug.Log($"Stock Created");
	}
    public void Update()
    {
        if(this.isUpdated)
		{
			UpdateText();
			isUpdated = false;
		}
		UpdateText();
    }
    public void UpdateText()
	{
		for(int i = 0; i < texts.Count; ++i)
		{
			texts[i].text = typeMaterial.ElementAt(i).ToString();
			Debug.Log(texts[i].text);
		}
		// foreach (Material material in typeMaterial.Values)
		// {
		// 	Debug.Log(material.type.ToString());
		// 	temp.text = typeMaterial[material.type].Count.ToString();
		// }
		// texts.Add(temp);
	}

	#endregion
	public void ExpandStock(Material material)
	{
		typeMaterial.Add(material.type, material);
		isUpdated = true;
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
		isUpdated = true;
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
        return true;
    }

}
