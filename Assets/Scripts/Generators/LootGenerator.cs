using UnityEngine;
using Types;
using System;
using Equipments;

public static class LootGenerator
{
    public static void RandomEquipment(Stat luck) 
    {
        int randomIndex = UnityEngine.Random.Range(0, EquipmentType.equipmentList.Count);
        var randomType = EquipmentType.equipmentList[randomIndex];
        Rarity rarity = RarityFactor((int) luck.value); 
        Debug.Log($"Chosen Type: {randomType}");
        ItemGenerator.Generate(randomType, rarity, luck);
    }
    // public static void EquipmentbyType(EquipmentType type, Stat luck) 
    // {
    //     ItemGenerator.Generate(type, luck);
    // }
    public static void RandomMaterial(Stat luck) 
    {
        MaterialType random = (MaterialType) UnityEngine.Random.Range(0,  Enum.GetNames(typeof(MaterialType)).Length);
        Debug.Log($"Chosen Material: {random}");
        MaterialGenerator.Generate(random, luck);
    }
    public static void MaterialbyType(MaterialType type, Stat luck) 
    {
        MaterialGenerator.Generate(type, luck);
    }
    public static Rarity RarityFactor(int fate)
    {
        int random = UnityEngine.Random.Range(1, 101) + fate*5;
        Rarity rarity;
        // 1 to 42 -> 42
        if(random < 43)
        {
            rarity = Rarity.Common;
        }
        // 43 to 70 -> 28
        else if(random > 42 && random < 71)
        {
            rarity = Rarity.Advenced;
        }
        // 71 to 85 -> 15
        else if(random > 70 && random < 86)
        {
            rarity = Rarity.Rare;
        }
        // 86 to 95 -> 10
        else if(random > 85 && random < 96)
        {
            rarity = Rarity.Epic;
        }
        // 96 to 100-> 5
        else
        {
            rarity = Rarity.Legendary;
        }
        return rarity;
    }
}
