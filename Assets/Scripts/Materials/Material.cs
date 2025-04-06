using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Types;

// [CreateAssetMenu(fileName = "New Material", menuName = "Inventory/Material")]
[Serializable]
public class Material 
{

    public MaterialType type;
    public string Name = "Default";
    public int Count = 0;
    public int Limit = 100;
    public Material(string name, int count)
    {
        this.Init(name, count);
    }
    public void Init(string name, int count)
    {
        Name = name;
        // type = MaterialType.Default;
        Count = Math.Clamp(count, 0, 100);
    }
    public void AddCount(int amount)
    {
        Count = Math.Clamp(Count + amount, 0, Limit);
    }
    public Material(MaterialType type, int count)
    {
        this.Init(type, count);
    }
    public void Init(MaterialType type, int count)
    {
        Name = type.ToString();
        Count = Math.Clamp(count, 0, 100);
    }
    public Material InitMaterial(MaterialType type, int count)
    {
        this.Init(type, count);
        return this;
    }
}
