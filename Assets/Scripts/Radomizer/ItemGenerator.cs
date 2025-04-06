using System.Collections.Generic;
using UnityEngine;
using Types;
using Odds;
public static class ItemGenerator
{
    public static GameObject gameObject =  GameObject.Find("GameManager");
    public static Inventory inventory = gameObject.GetComponent<Inventory>();
    public static Equipment generated;
    public static Dictionary<Rarity, int[]> rarityValues = odds.rarityValues;
    public static Dictionary<Rarity, int[]> rarityModifiers = odds.rarityModifiers;
    public static void Generate(EquipmentSlot type, Stat luck)
    {
        int fate = (int)luck.value;
        Rarity rarity = Craft(type, fate); 
        Dictionary<StatType, StatModifier> statTypeModifier= new Dictionary<StatType, StatModifier>();
        switch(type)
        {
            case EquipmentSlot.Weapon:
                statTypeModifier = PrimaryWeapon(rarity);
                break;
            case EquipmentSlot.Armor:
                statTypeModifier = Armor(rarity);
                break;
            case EquipmentSlot.Accessoire:
                statTypeModifier = Accessoire(rarity);
                break;
            // case EquipmentSlot.Shield:
            //     generated = SecondaryWeapon(dice.value);       
            //     break;
            // case "Scroll":
            //     generated = Scroll(dice.value);       
            //     break;
            default:
                Debug.Log("Unrecognized Equipment type!");  
                Debug.Log($"Type: {type} Fate: {luck.value}");  
                statTypeModifier.Add(StatType.Default, new StatModifier(0.0f, StatModType.Flat, 0, generated));
                break;
        }
        generated.AdjustStatModifiers(statTypeModifier);
        inventory.Add(generated);
        // inventory.ShowItems();
    }
    public static Dictionary<StatType, StatModifier> PrimaryWeapon(Rarity rarity)
    {
        StatModifier modifier;
        Dictionary<StatType, StatModifier> keyValuePairs= new Dictionary<StatType, StatModifier>();
        int value = rarityModifiers[rarity][Random.Range(0, rarityModifiers[rarity].Length)];

        modifier = new StatModifier(value, StatModType.Flat, 1, generated);
        keyValuePairs.Add(StatType.STR, modifier);

        return keyValuePairs;
    }
    // public static Equipment SecondaryWeapon(int fate)
    // {
    //     Equipment defaultEquipment;
    //     Dictionary<StatType, StatModifier> keyValuePairs= new Dictionary<StatType, StatModifier>();
    //     StatModifier modifier;
    //     int value;
    //     switch(fate)
    //     {
    //         // Common
    //         case 1:
    //             defaultEquipment = Craft(EquipmentSlot.Weapon, Rarity.Common);
    //             value = rarityModifiers[Rarity.Common][Random.Range(0, rarityModifiers[Rarity.Common].Length)];
    //             modifier = new StatModifier(value, StatModType.Flat, 1, defaultEquipment);
    //             keyValuePairs.Add(StatType.STR, modifier);
    //             defaultEquipment.AdjustStatModifiers(keyValuePairs);
    //             break;
    //         // Advenced
    //         case 2:
    //             defaultEquipment = Craft(EquipmentSlot.Weapon, Rarity.Advenced);
    //             value = rarityModifiers[Rarity.Common][Random.Range(0, rarityModifiers[Rarity.Common].Length)];
    //             modifier = new StatModifier(value, StatModType.Flat, 1, defaultEquipment);
    //             keyValuePairs.Add(StatType.STR, modifier);
    //             defaultEquipment.AdjustStatModifiers(keyValuePairs);
    //             break;
    //         // Uncommon
    //         case 3:
    //             defaultEquipment = Craft(EquipmentSlot.Weapon, Rarity.Uncommon);
    //             value = rarityModifiers[Rarity.Common][Random.Range(0, rarityModifiers[Rarity.Common].Length)];
    //             modifier = new StatModifier(value, StatModType.Flat, 1, defaultEquipment);
    //             keyValuePairs.Add(StatType.STR, modifier);
    //             defaultEquipment.AdjustStatModifiers(keyValuePairs);
    //             break;
    //         // Rare
    //         case 4:
    //             defaultEquipment = Craft(EquipmentSlot.Weapon, Rarity.Rare);
    //             value = rarityModifiers[Rarity.Common][Random.Range(0, rarityModifiers[Rarity.Common].Length)];
    //             modifier = new StatModifier(value, StatModType.Flat, 1, defaultEquipment);
    //             keyValuePairs.Add(StatType.STR, modifier);
    //             defaultEquipment.AdjustStatModifiers(keyValuePairs);
    //             break;
    //         // Epic
    //         case 5:
    //             defaultEquipment = Craft(EquipmentSlot.Weapon, Rarity.Epic);
    //             value = rarityModifiers[Rarity.Common][Random.Range(0, rarityModifiers[Rarity.Common].Length)];
    //             modifier = new StatModifier(value, StatModType.Flat, 1, defaultEquipment);
    //             keyValuePairs.Add(StatType.STR, modifier);
    //             defaultEquipment.AdjustStatModifiers(keyValuePairs);
    //             break;
    //         // Legendary
    //         case 6:
    //             defaultEquipment = Craft(EquipmentSlot.Weapon, Rarity.Legendary);
    //             value = rarityModifiers[Rarity.Common][Random.Range(0, rarityModifiers[Rarity.Common].Length)];
    //             modifier = new StatModifier(value, StatModType.Flat, 1, defaultEquipment);
    //             keyValuePairs.Add(StatType.STR, modifier);
    //             defaultEquipment.AdjustStatModifiers(keyValuePairs);
    //             break;
    //         default:
    //             Debug.Log($"Uncrognized fate value: {fate}");
    //             defaultEquipment = Craft(EquipmentSlot.Weapon, Rarity.Default);
    //             keyValuePairs.Add(StatType.STR, new StatModifier(0, StatModType.Flat, 1, defaultEquipment));
    //             break;
    //     }
    //     return defaultEquipment;
    // }
    public static Dictionary<StatType, StatModifier> Armor(Rarity rarity)
    {
        StatModifier modifier;
        Dictionary<StatType, StatModifier> keyValuePairs= new Dictionary<StatType, StatModifier>();
        int value = rarityModifiers[rarity][Random.Range(0, rarityModifiers[rarity].Length)];

        modifier = new StatModifier(value, StatModType.Flat, 1, generated);
        keyValuePairs.Add(StatType.CON, modifier);

        return keyValuePairs;
    }
    public static Dictionary<StatType, StatModifier> Accessoire(Rarity rarity)
    {
        StatModifier modifier;
        Dictionary<StatType, StatModifier> keyValuePairs= new Dictionary<StatType, StatModifier>();
        int value = rarityModifiers[rarity][Random.Range(0, rarityModifiers[rarity].Length)];
        value = (int)(value * 0.75);
        modifier = new StatModifier(value, StatModType.Flat, 1, generated);
        keyValuePairs.Add(StatType.STR, modifier);
        keyValuePairs.Add(StatType.CON, modifier);

        return keyValuePairs;
    }
    // public static Equipment Scroll(int fate)
    // {

    // }
    public static Rarity Craft(EquipmentSlot slot, int fate)
    {
        Rarity rarity = RarityFactor(fate);
        int value = DamageFactor(slot, rarity);
        switch(slot)
        {
            case EquipmentSlot.Weapon:
                // generated = new Equipment(slot, rarity, 0, value);
                generated = ScriptableObject.CreateInstance<Equipment>();
                generated.InitEquipment(slot, rarity, 0, value);
                break;
            case EquipmentSlot.Armor:
                // generated = new Equipment(slot, rarity, value, 0);
                generated = ScriptableObject.CreateInstance<Equipment>();
                generated.InitEquipment(slot, rarity, value, 0);
                break;
            case EquipmentSlot.Accessoire:
                //generated = new Equipment(slot, rarity, value, value);
                generated = ScriptableObject.CreateInstance<Equipment>();
                generated.InitEquipment(slot, rarity, value, value);
                break;
            default:
                Debug.Log($"Uncrognized Equipment Slot: {slot} Default Equipment crafted with value 0");
                // generated = new Equipment(EquipmentSlot.Default, Rarity.Default, 0, 0);
                generated = ScriptableObject.CreateInstance<Equipment>();
                generated.InitEquipment(EquipmentSlot.Default, Rarity.Default, 0, 0);
                break;
        }

        return rarity;
    }
    public static Rarity RarityFactor(int fate)
    {
        int random = Random.Range(1, 101) + fate*5;
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
    public static int DamageFactor(EquipmentSlot slot, Rarity rarity)
    {
        int value;
        switch(rarity)
        {
            // Common
            case Rarity.Common:
                value = rarityValues[Rarity.Common][Random.Range(0, rarityValues[Rarity.Common].Length)];
                break;
            // Advenced
            case Rarity.Advenced:
                value = rarityValues[Rarity.Advenced][Random.Range(0, rarityValues[Rarity.Advenced].Length)];
                break;
            // Uncommon
            case Rarity.Uncommon:
                value = rarityValues[Rarity.Uncommon][Random.Range(0, rarityValues[Rarity.Uncommon].Length)];
                break;
            // Rare
            case Rarity.Rare:
                value = rarityValues[Rarity.Rare][Random.Range(0, rarityValues[Rarity.Rare].Length)];
                break;
            // Epic
            case Rarity.Epic:
                value = rarityValues[Rarity.Epic][Random.Range(0, rarityValues[Rarity.Epic].Length)];
                break;
            // Legendary
            case Rarity.Legendary:
                value = rarityValues[Rarity.Legendary][Random.Range(0, rarityValues[Rarity.Legendary].Length)];
                break;
            default:
                Debug.Log($"Uncrognized rarity: {rarity} Value setted to 0");
                return 0;
        }
        if(slot == EquipmentSlot.Armor)
        {
            value = (int)(value*0.75);
        }
        else if(slot == EquipmentSlot.Accessoire)
        {
            value = (int)(value*0.5);
        }
        return value;
    }
}
