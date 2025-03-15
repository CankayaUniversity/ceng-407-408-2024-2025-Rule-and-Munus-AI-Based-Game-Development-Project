using UnityEngine;
using UnityEngine.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

public class Attributes : MonoBehaviour 
{
    public string Name = "Lorem";
    public string _class = "Fighter";
    public string race = "Ipsum";
    public int BaseSTR = 0;
    public int BaseDEX = 0;
    public int BaseCON = 0;
    public int BaseINT = 0;

    private Dictionary<string, Item> equippedItems = new Dictionary<string, Item> ();
    private Inventory inventory = new Inventory();

    public int TotalSTR = 0;
    public int TotalDEX = 0;
    public int TotalCON = 0;
    public int TotalINT = 0;

    #region Singleton

	public static Attributes instance;

	void Awake()
	{
		instance = this;
	}

	#endregion

    public bool EquipItem(Item item)
    {
        if (equippedItems.ContainsKey(item.name))
        {
            Console.WriteLine("You can't equip more than 1 " + item.name);
            return false;
        }
        equippedItems.Add(item.name, item);
        Console.WriteLine($"{Name} equipped {item.name}!");
        return true;
    }

    public bool UnequipItem(Item item)
    {
        if (equippedItems.ContainsKey(item.name))
        {
            Console.WriteLine(item.name + "slot is empty!");
            return false;
        }
        equippedItems.Remove(item.name);
        equippedItems.Remove(item.name);
        Console.WriteLine($"{Name} unequipped {item.name}.");
        return true;
    }

    // public void AutoEquipBestItem(List<Item> availableItems)
    // {
    //     var bestItem = availableItems.OrderByDescending(i => i.STR + i.DEX + i.CON + i.INT).FirstOrDefault();
    //     if (bestItem != null)
    //     {
    //         EquipItem(bestItem);
    //     }
    // }

    public void CollectMaterial(string material, int amount)
    {
        inventory.Add(material, amount);
    }

    public void ShowStats()
    {
        Console.WriteLine($"{Name} - STR: {TotalSTR}, DEX: {TotalDEX}, CON: {TotalCON}, INT: {TotalINT}");
    }

	
}
