using UnityEngine;
using Types;

public class MaterialManager : MonoBehaviour {

	#region Singleton

	public static MaterialManager instance;
	public static Material[] materials;
	// public SkinnedMeshRenderer targetMesh;

    // SkinnedMeshRenderer[] currentMeshes;

	void Awake ()
	{
		instance = this;
		materials = Material.FindObjectsByType<Material>(FindObjectsSortMode.None); 
	}

	#endregion

	Stock stock;	// Reference to our stock
	void Start ()
	{
		stock = Stock.instance;		// Get a reference to our stock
		
		// If prefabs can be automaticly added through FindObjectsByType
		// no need for below code parts

		// stock.Add(new Material(MaterialType.Wood, 0));
        // stock.Add(new Material(MaterialType.Stone, 0));
        // stock.Add(new Material(MaterialType.Iron, 0));
        // stock.Add(new Material(MaterialType.Cloth, 0));
	}

	public void Gather (MaterialType gatheredType, int gatheredAmount)
	{
		stock.Add(gatheredType, gatheredAmount);
	}

	public void Spend (MaterialType givenType, int givenAmount)
	{
		stock.Add(givenType, (-1)*givenAmount);
	}

}
