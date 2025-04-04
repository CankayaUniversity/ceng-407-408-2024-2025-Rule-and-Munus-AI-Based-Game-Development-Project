using UnityEngine;
using Types;
using System;

public static class LootGenerator
{
    public static void RandomEquipment(Stat luck) 
    {
        EquipmentSlot random = (EquipmentSlot) UnityEngine.Random.Range(0,  Enum.GetNames(typeof(EquipmentSlot)).Length);
        ItemGenerator.Generate(random, luck);
    }
    public static void EquipmentbyType(EquipmentSlot type, Stat luck) 
    {
        ItemGenerator.Generate(type, luck);
    }
    public static void RandomMaterial(Stat luck) 
    {
        MaterialType random = (MaterialType) UnityEngine.Random.Range(0,  Enum.GetNames(typeof(MaterialType)).Length);
        MaterialGenerator.Generate(random, luck);
    }
    public static void MaterialbyType(MaterialType type, Stat luck) 
    {
        MaterialGenerator.Generate(type, luck);
    }
}
