using UnityEngine;
using Types;
using Unity.VisualScripting;
using Unity.AppUI.UI;
using System.Collections.Generic;
using TMPro;

public class MaterialManager : MonoBehaviour {

	#region Singleton
	public static new GameObject gameObject;
	public static MaterialManager instance;
	// public SkinnedMeshRenderer targetMesh;

    // SkinnedMeshRenderer[] currentMeshes;

	void Awake ()
	{
		instance = this;
		gameObject = GameObject.Find("Player");
	}

	#endregion

	Stock stock;	// Reference to our stock
	[Header("UI Elements")]
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI stoneText;
    public TextMeshProUGUI ironText;

    private Dictionary<MaterialType, TextMeshProUGUI> materialTexts;
	void Start ()
	{
		stock = gameObject.GetComponent<Stock>();		// Get a reference to our stock
		materialTexts = new Dictionary<MaterialType, TextMeshProUGUI>
        {
            { MaterialType.Wood, woodText },
            { MaterialType.Stone, stoneText },
            { MaterialType.Iron, ironText }
        };

        UpdateMaterialTexts();
	}

	public void Gather (MaterialType gatheredType, int gatheredAmount)
	{
		stock.Add(gatheredType, gatheredAmount);	
		UpdateMaterialTexts();
	}

	public void Spend (MaterialType givenType, int givenAmount)
	{
		stock.Add(givenType, (-1)*givenAmount);
		UpdateMaterialTexts();
	}
	private void UpdateMaterialTexts()
    {
        foreach (var material in materialTexts.Keys)
        {
            int amount = stock.typeMaterial[material].Count; // Assuming your Stock class has Get(MaterialType)
            materialTexts[material].text = amount.ToString();
        }
    }
}

