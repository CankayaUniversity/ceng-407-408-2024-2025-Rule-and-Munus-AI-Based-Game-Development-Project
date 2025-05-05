using UnityEngine;
using Types;
public class MaterialManager : MonoBehaviour {

	#region Singleton
	public static MaterialManager instance;
	public Stock stock;	
	void Awake ()
	{
		instance = this;
	}

	#endregion
	void Start ()
	{

	}

	public void Gather (MaterialType gatheredType, int gatheredAmount)
	{
		stock.Add(gatheredType, gatheredAmount);	
		// UpdateMaterialTexts();
	}

	public void Spend (MaterialType givenType, int givenAmount)
	{
		stock.Add(givenType, (-1)*givenAmount);
		// UpdateMaterialTexts();
	}
}

