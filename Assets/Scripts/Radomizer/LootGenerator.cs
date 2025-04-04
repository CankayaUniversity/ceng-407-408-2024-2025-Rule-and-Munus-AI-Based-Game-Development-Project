using UnityEngine;
using Types;
using System;

public static class LootGenerator
{
    public static void Loot(Stat luck) 
    {
        EquipmentSlot random = (EquipmentSlot) UnityEngine.Random.Range(0,  Enum.GetNames(typeof(EquipmentSlot)).Length);
        ItemGenerator.Generate(random, luck);
    }
}
