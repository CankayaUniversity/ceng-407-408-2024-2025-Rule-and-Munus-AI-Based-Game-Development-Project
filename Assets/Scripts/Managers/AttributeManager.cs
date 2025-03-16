using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Keep track of equipment. Has functions for adding and removing items. */

public class AttributeManager : MonoBehaviour {

	#region Singleton

	public static AttributeManager instance;

	void Awake ()
	{
		instance = this;
	}

	#endregion

	Attributes attributes;	// Reference to our stock
	void Start ()
	{
		attributes = Attributes.instance;		// Get a reference to our stock

		attributes.stats.Add(STR, 0);
        attributes.stats.Add(DEX, 0);
        attributes.stats.Add(CON, 0);
        attributes.stats.Add(INT, 0);
		attributes.stats.Add(WIS, 0);
        attributes.stats.Add(CON, 0);
	}

	public void Gather (string gatheredMaterial, int gatheredAmount)
	{
		stock.materials.Add(gatheredMaterial, gatheredAmount);
	}
	
	public void Spend (string givenMaterial, int givenAmount)
	{
		stock.materials.Add(gatheredMaterial, (-1)*givenAmount);
	}

}
