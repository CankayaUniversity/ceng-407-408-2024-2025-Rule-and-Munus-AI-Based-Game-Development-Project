using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using Types;

public static class ItemGenerator
{
    public static Equipment generated;
    public static Dictionary<Rarity, int[]> rarityValues = new Dictionary<Rarity, int[]>() {
        { Rarity.Common, new int[8] {1, 1, 2, 2, 2, 3, 4, 5}},
        { Rarity.Advenced, new int[12] {3, 4, 5, 5, 5, 6, 6, 7, 7, 8, 9, 10}},
        { Rarity.Uncommon, new int[11] {5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}},
        { Rarity.Rare, new int[18] {6, 7, 8, 8, 9, 9, 9, 10, 10, 11, 11, 11, 12, 12, 13, 13, 14, 15}},
        { Rarity.Epic, new int[16] {10, 11, 12, 13, 14, 15, 16, 16, 16, 17, 17, 18, 18, 19, 19, 20}},
        { Rarity.Legendary, new int[13] {16, 17, 17, 17, 18, 18, 18, 18, 19, 19, 19, 20, 20}}
    };
    // public static Dictionary<StatType, int[]> rarityModifiers = new Dictionary<StatType, int[]>() {
    //     { StatType.STR, new int[8] {1, 1, 2, 2, 2, 3, 4, 5}},
    //     { StatType.DEX, new int[12] {3, 4, 5, 5, 5, 6, 6, 7, 7, 8, 9, 10}},
    //     { StatType.INT, new int[11] {5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}},
    //     { StatType.WIS, new int[18] {6, 7, 8, 8, 9, 9, 9, 10, 10, 11, 11, 11, 12, 12, 13, 13, 14, 15}},
    //     { StatType.CON, new int[16] {10, 11, 12, 13, 14, 15, 16, 16, 16, 17, 17, 18, 18, 19, 19, 20}},
    //     { StatType.CHA, new int[13] {16, 17, 17, 17, 18, 18, 18, 18, 19, 19, 19, 20, 20}},
    //     { StatType.LUCK, new int[13] {16, 17, 17, 17, 18, 18, 18, 18, 19, 19, 19, 20, 20}}
    // };
    public static Dictionary<Rarity, int[]> rarityModifiers = new Dictionary<Rarity, int[]>() {
        { Rarity.Common, new int[8] {0, 0, 0, 0, 1, 1, 1, 2}},
        { Rarity.Advenced, new int[7] {1, 1, 1, 2, 2, 3, 4}},
        { Rarity.Uncommon, new int[10] {2, 2, 2, 3, 3, 4, 4, 5, 6, 7}},
        { Rarity.Rare, new int[14] {3, 4, 4, 5, 5, 5, 6, 6, 6, 7, 8, 9, 10}},
        { Rarity.Epic, new int[25] {5, 5, 6, 6, 6, 6, 7, 7, 7, 8 ,8, 8, 8, 9, 9, 9, 10, 11, 11, 12, 12, 13}},
        { Rarity.Legendary, new int[24] {8, 8, 8, 9, 9, 9, 10, 10, 10, 10, 11, 11, 11, 11, 12, 12, 12, 12, 13, 13, 13, 14, 14, 15, 15, 15}}
    };

    public static void Generate(string type, Dice dice)
    {
        switch(type)
        {
            case "Primary":
                generated = PrimaryWeapon(dice.value);       
                break;
            case "Armor":
                generated = Armor(dice.value);       
                break;
            case "Accessoire":
                // generated = Accessoire(dice.value);       
                break;
            case "Secondary":
                generated = SecondaryWeapon(dice.value);       
                break;
            case "Scroll":
                // generated = Scroll(dice.value);       
                break;
            default:
                Debug.Log("Unrecognized Equipment type!");  
                Debug.Log($"Type: {type} Fate: {dice.value}");  
                break;
        }
    }
    public static Equipment PrimaryWeapon(int fate)
    {
        Equipment defaultEquipment;
        Dictionary<StatType, StatModifier> keyValuePairs= new Dictionary<StatType, StatModifier>();
        StatModifier modifier;
        int value;
        switch(fate)
        {
            // Common
            case 1:
                Craft(EquipmentSlot.Weapon, Rarity.Common, &defaultEquipment);
                value = rarityModifiers[Rarity.Common][Random.Range(0, rarityModifiers[Rarity.Common].Length)];
                modifier = new StatModifier(value, StatModType.Flat, 1, defaultEquipment);
                keyValuePairs.Add(StatType.STR, modifier);
                defaultEquipment.AdjustStatModifiers(keyValuePairs);
                break;
            // Advenced
            case 2:
                defaultEquipment = Craft(EquipmentSlot.Weapon, Rarity.Advenced);
                value = rarityModifiers[Rarity.Common][Random.Range(0, rarityModifiers[Rarity.Common].Length)];
                modifier = new StatModifier(value, StatModType.Flat, 1, defaultEquipment);
                keyValuePairs.Add(StatType.STR, modifier);
                defaultEquipment.AdjustStatModifiers(keyValuePairs);
                break;
            // Uncommon
            case 3:
                defaultEquipment = Craft(EquipmentSlot.Weapon, Rarity.Uncommon);
                value = rarityModifiers[Rarity.Common][Random.Range(0, rarityModifiers[Rarity.Common].Length)];
                modifier = new StatModifier(value, StatModType.Flat, 1, defaultEquipment);
                keyValuePairs.Add(StatType.STR, modifier);
                defaultEquipment.AdjustStatModifiers(keyValuePairs);
                break;
            // Rare
            case 4:
                defaultEquipment = Craft(EquipmentSlot.Weapon, Rarity.Rare);
                value = rarityModifiers[Rarity.Common][Random.Range(0, rarityModifiers[Rarity.Common].Length)];
                modifier = new StatModifier(value, StatModType.Flat, 1, defaultEquipment);
                keyValuePairs.Add(StatType.STR, modifier);
                defaultEquipment.AdjustStatModifiers(keyValuePairs);
                break;
            // Epic
            case 5:
                defaultEquipment = Craft(EquipmentSlot.Weapon, Rarity.Epic);
                value = rarityModifiers[Rarity.Common][Random.Range(0, rarityModifiers[Rarity.Common].Length)];
                modifier = new StatModifier(value, StatModType.Flat, 1, defaultEquipment);
                keyValuePairs.Add(StatType.STR, modifier);
                defaultEquipment.AdjustStatModifiers(keyValuePairs);
                break;
            // Legendary
            case 6:
                defaultEquipment = Craft(EquipmentSlot.Weapon, Rarity.Legendary);
                value = rarityModifiers[Rarity.Common][Random.Range(0, rarityModifiers[Rarity.Common].Length)];
                modifier = new StatModifier(value, StatModType.Flat, 1, defaultEquipment);
                keyValuePairs.Add(StatType.STR, modifier);
                defaultEquipment.AdjustStatModifiers(keyValuePairs);
                break;
            default:
                Debug.Log($"Uncrognized fate value: {fate}");
                defaultEquipment = Craft(EquipmentSlot.Weapon, Rarity.Default);
                keyValuePairs.Add(StatType.STR, new StatModifier(0, StatModType.Flat, 1, defaultEquipment));
                break;
        }
        return defaultEquipment;
    }
    public static Equipment SecondaryWeapon(int fate)
    {
        Equipment defaultEquipment;
        Dictionary<StatType, StatModifier> keyValuePairs= new Dictionary<StatType, StatModifier>();
        StatModifier modifier;
        int value;
        switch(fate)
        {
            // Common
            case 1:
                defaultEquipment = Craft(EquipmentSlot.Weapon, Rarity.Common);
                value = rarityModifiers[Rarity.Common][Random.Range(0, rarityModifiers[Rarity.Common].Length)];
                modifier = new StatModifier(value, StatModType.Flat, 1, defaultEquipment);
                keyValuePairs.Add(StatType.STR, modifier);
                defaultEquipment.AdjustStatModifiers(keyValuePairs);
                break;
            // Advenced
            case 2:
                defaultEquipment = Craft(EquipmentSlot.Weapon, Rarity.Advenced);
                value = rarityModifiers[Rarity.Common][Random.Range(0, rarityModifiers[Rarity.Common].Length)];
                modifier = new StatModifier(value, StatModType.Flat, 1, defaultEquipment);
                keyValuePairs.Add(StatType.STR, modifier);
                defaultEquipment.AdjustStatModifiers(keyValuePairs);
                break;
            // Uncommon
            case 3:
                defaultEquipment = Craft(EquipmentSlot.Weapon, Rarity.Uncommon);
                value = rarityModifiers[Rarity.Common][Random.Range(0, rarityModifiers[Rarity.Common].Length)];
                modifier = new StatModifier(value, StatModType.Flat, 1, defaultEquipment);
                keyValuePairs.Add(StatType.STR, modifier);
                defaultEquipment.AdjustStatModifiers(keyValuePairs);
                break;
            // Rare
            case 4:
                defaultEquipment = Craft(EquipmentSlot.Weapon, Rarity.Rare);
                value = rarityModifiers[Rarity.Common][Random.Range(0, rarityModifiers[Rarity.Common].Length)];
                modifier = new StatModifier(value, StatModType.Flat, 1, defaultEquipment);
                keyValuePairs.Add(StatType.STR, modifier);
                defaultEquipment.AdjustStatModifiers(keyValuePairs);
                break;
            // Epic
            case 5:
                defaultEquipment = Craft(EquipmentSlot.Weapon, Rarity.Epic);
                value = rarityModifiers[Rarity.Common][Random.Range(0, rarityModifiers[Rarity.Common].Length)];
                modifier = new StatModifier(value, StatModType.Flat, 1, defaultEquipment);
                keyValuePairs.Add(StatType.STR, modifier);
                defaultEquipment.AdjustStatModifiers(keyValuePairs);
                break;
            // Legendary
            case 6:
                defaultEquipment = Craft(EquipmentSlot.Weapon, Rarity.Legendary);
                value = rarityModifiers[Rarity.Common][Random.Range(0, rarityModifiers[Rarity.Common].Length)];
                modifier = new StatModifier(value, StatModType.Flat, 1, defaultEquipment);
                keyValuePairs.Add(StatType.STR, modifier);
                defaultEquipment.AdjustStatModifiers(keyValuePairs);
                break;
            default:
                Debug.Log($"Uncrognized fate value: {fate}");
                defaultEquipment = Craft(EquipmentSlot.Weapon, Rarity.Default);
                keyValuePairs.Add(StatType.STR, new StatModifier(0, StatModType.Flat, 1, defaultEquipment));
                break;
        }
        return defaultEquipment;
    }
    public static Equipment Armor(int fate)
    {
        Equipment defaultEquipment;
        Dictionary<StatType, StatModifier> keyValuePairs= new Dictionary<StatType, StatModifier>();
        StatModifier modifier;
        int value;
        switch(fate)
        {
            // Common
            case 1:
                defaultEquipment = Craft(EquipmentSlot.Armor, Rarity.Common);
                value = rarityModifiers[Rarity.Common][Random.Range(0, rarityModifiers[Rarity.Common].Length)];
                modifier = new StatModifier(value, StatModType.Flat, 1, defaultEquipment);
                keyValuePairs.Add(StatType.CON, modifier);
                defaultEquipment.AdjustStatModifiers(keyValuePairs);
                break;
            // Advenced
            case 2:
                defaultEquipment = Craft(EquipmentSlot.Armor, Rarity.Advenced);
                value = rarityModifiers[Rarity.Common][Random.Range(0, rarityModifiers[Rarity.Common].Length)];
                modifier = new StatModifier(value, StatModType.Flat, 1, defaultEquipment);
                keyValuePairs.Add(StatType.CON, modifier);
                defaultEquipment.AdjustStatModifiers(keyValuePairs);
                break;
            // Uncommon
            case 3:
                defaultEquipment = Craft(EquipmentSlot.Armor, Rarity.Uncommon);
                value = rarityModifiers[Rarity.Common][Random.Range(0, rarityModifiers[Rarity.Common].Length)];
                modifier = new StatModifier(value, StatModType.Flat, 1, defaultEquipment);
                keyValuePairs.Add(StatType.CON, modifier);
                defaultEquipment.AdjustStatModifiers(keyValuePairs);
                break;
            // Rare
            case 4:
                defaultEquipment = Craft(EquipmentSlot.Armor, Rarity.Rare);
                value = rarityModifiers[Rarity.Common][Random.Range(0, rarityModifiers[Rarity.Common].Length)];
                modifier = new StatModifier(value, StatModType.Flat, 1, defaultEquipment);
                keyValuePairs.Add(StatType.CON, modifier);
                defaultEquipment.AdjustStatModifiers(keyValuePairs);
                break;
            // Epic
            case 5:
                defaultEquipment = Craft(EquipmentSlot.Armor, Rarity.Epic);
                value = rarityModifiers[Rarity.Common][Random.Range(0, rarityModifiers[Rarity.Common].Length)];
                modifier = new StatModifier(value, StatModType.Flat, 1, defaultEquipment);
                keyValuePairs.Add(StatType.CON, modifier);
                defaultEquipment.AdjustStatModifiers(keyValuePairs);
                break;
            // Legendary
            case 6:
                defaultEquipment = Craft(EquipmentSlot.Armor, Rarity.Legendary);
                value = rarityModifiers[Rarity.Common][Random.Range(0, rarityModifiers[Rarity.Common].Length)];
                modifier = new StatModifier(value, StatModType.Flat, 1, defaultEquipment);
                keyValuePairs.Add(StatType.CON, modifier);
                defaultEquipment.AdjustStatModifiers(keyValuePairs);
                break;
            default:
                Debug.Log($"Uncrognized fate value: {fate}");
                defaultEquipment = Craft(EquipmentSlot.Armor, Rarity.Default);
                keyValuePairs.Add(StatType.CON, new StatModifier(0, StatModType.Flat, 1, defaultEquipment));
                break;
        }
        return defaultEquipment;
    }
    // public static Equipment Accessoire(int fate)
    // {

    // }
    // public static Equipment Scroll(int fate)
    // {

    // }
    public static Equipment Craft(EquipmentSlot slot, Rarity rarity, Equipment& equipment)
    {
        Equipment defaultEquipment;
        int value = 0;
        switch(rarity)
        {
            // Common
            case Rarity.Common:
                value = rarityValues[Rarity.Common][Random.Range(0, rarityValues[Rarity.Common].Length)];
                break;
            // Advenced
            case Rarity.Advenced:
                value = rarityValues[Rarity.Common][Random.Range(0, rarityValues[Rarity.Advenced].Length)];
                break;
            // Uncommon
            case Rarity.Uncommon:
                value = rarityValues[Rarity.Common][Random.Range(0, rarityValues[Rarity.Uncommon].Length)];
                break;
            // Rare
            case Rarity.Rare:
                value = rarityValues[Rarity.Common][Random.Range(0, rarityValues[Rarity.Rare].Length)];
                break;
            // Epic
            case Rarity.Epic:
                value = rarityValues[Rarity.Common][Random.Range(0, rarityValues[Rarity.Epic].Length)];
                break;
            // Legendary
            case Rarity.Legendary:
                value = rarityValues[Rarity.Common][Random.Range(0, rarityValues[Rarity.Legendary].Length)];
                break;
            default:
                Debug.Log($"Uncrognized rarity: {rarity}");
                defaultEquipment = new Equipment(EquipmentSlot.Weapon, Rarity.Default, 1, 1);
                return defaultEquipment;
        }
        if(slot == EquipmentSlot.Armor)
        {
            value = (int)(value*0.75);
        }
        equipment = new Equipment(slot, rarity, 0, value);
    }
}
