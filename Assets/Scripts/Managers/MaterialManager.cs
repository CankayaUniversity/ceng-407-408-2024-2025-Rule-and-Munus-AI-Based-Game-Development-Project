using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Keep track of equipment. Has functions for adding and removing items. */

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

		stock.materials.Add("Wood", new Material("Wood", 0));
        stock.materials.Add("Stone", new Material("Stone", 0));
        stock.materials.Add("Iron", new Material("Iron", 0));
        stock.materials.Add("Cloth", new Material("Cloth", 0));
	}

	public void Gather (string gatheredMaterial, int gatheredAmount)
	{
		stock.materials.Add(gatheredMaterial, gatheredAmount);
	}

	// Unequip an item with a particular index
	public void Spend (string givenMaterial, int givenAmount)
	{
		stock.materials.Add(gatheredMaterial, (-1)*givenAmount);
	}

}
