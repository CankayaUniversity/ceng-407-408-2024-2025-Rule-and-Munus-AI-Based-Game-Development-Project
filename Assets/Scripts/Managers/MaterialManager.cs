using UnityEngine;

public class MaterialManager : MonoBehaviour {

	#region Singleton

	public static MaterialManager instance;
	// public SkinnedMeshRenderer targetMesh;

    // SkinnedMeshRenderer[] currentMeshes;

	void Awake ()
	{
		instance = this;
	}

	#endregion

	Stock stock;	// Reference to our stock
	void Start ()
	{
		stock = Stock.instance;		// Get a reference to our stock

		stock.Add(new Material("Wood", 0));
        stock.Add(new Material("Stone", 0));
        stock.Add(new Material("Iron", 0));
        stock.Add(new Material("Cloth", 0));
	}

	public void Gather (string gatheredMaterial, int gatheredAmount)
	{
		stock.Add(gatheredMaterial, gatheredAmount);
	}

	// Unequip an item with a particular index
	public void Spend (string givenMaterial, int givenAmount)
	{
		stock.Add(givenMaterial, (-1)*givenAmount);
	}

}
