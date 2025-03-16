using UnityEngine;
using UnityEngine.Rendering;
using System;
using System.Collections.Generic;

/* The base item class. All items should derive from this. */

[CreateAssetMenu(fileName = "New Material", menuName = "Inventory/Material")]
public class Material : ScriptableObject 
{

    public string Name = "Default";
    public int Count = 0;
    public int Limit = 100;
    public Material(string name, int count)
    {
        Name = name;
        Count = Math.Clamp(count, 0, 100);
    }

    public void AddCount(int amount)
    {
        Count = (int)Math.Clamp(Count + amount, 0, Limit);
    }

}
