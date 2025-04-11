using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Types;
using Odds;
using Icons;
// using Microsoft.Unity.VisualStudio.Editor;
public static class ItemGenerator
{
    public static GameObject gameObject =  GameObject.Find("GameManager");
    public static Inventory inventory = gameObject.GetComponent<Inventory>();
    public static Equipment generated;
    public static Dictionary<Rarity, int[]> rarityValues = odds.rarityValues;
    public static Dictionary<Rarity, int[]> rarityModifiers = odds.rarityModifiers;
    public static Dictionary<EquipmentSlot, Sprite> slotSprite = icons.slotSprite;
    public static Dictionary<EquipmentSlot, Image> slotImage = icons.slotImage;
    public static Dictionary<EquipmentSlot, SkinnedMeshRenderer> slotMesh = icons.slotMesh;
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
            case EquipmentSlot.Secondary:
                generated = SecondaryWeapon(dice.value);       
                break;
                //head + torso + legs + feet)
            case EquipmentSlot.Head:
                statTypeModifier = HeadArmor(rarity);
                break;
            case EquipmentSlot.Body:
                statTypeModifier = BodyArmor(rarity);
                break;
            case EquipmentSlot.Legs:
                statTypeModifier = LegArmor(rarity);
                break;
            case EquipmentSlot.Feet:
                statTypeModifier = FeetArmor(rarity);
                break;
            case EquipmentSlot.Accessoire:
                statTypeModifier = Accessoire(rarity);
                break;
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
    public static Dictionary<StatType, StatModifier> SecondaryWeapon(Rarity rarity)
    {
        StatModifier modifier;
        Dictionary<StatType, StatModifier> keyValuePairs= new Dictionary<StatType, StatModifier>();
        int value = rarityModifiers[rarity][Random.Range(0, rarityModifiers[rarity].Length)];
        value = (int)(value * 0.60);
        modifier = new StatModifier(value, StatModType.Flat, 1, generated);
        keyValuePairs.Add(StatType.STR, modifier);

        return keyValuePairs;
    }
    public static Dictionary<StatType, StatModifier> HeadArmor(Rarity rarity)
    {
        StatModifier modifier;
        Dictionary<StatType, StatModifier> keyValuePairs= new Dictionary<StatType, StatModifier>();
        int value = rarityModifiers[rarity][Random.Range(0, rarityModifiers[rarity].Length)];
        value = (int)(value * 0.60);
        modifier = new StatModifier(value, StatModType.Flat, 1, generated);
        keyValuePairs.Add(StatType.CON, modifier);

        return keyValuePairs;
    }
    public static Dictionary<StatType, StatModifier> BodyArmor(Rarity rarity)
    {
        StatModifier modifier;
        Dictionary<StatType, StatModifier> keyValuePairs= new Dictionary<StatType, StatModifier>();
        int value = rarityModifiers[rarity][Random.Range(0, rarityModifiers[rarity].Length)];
        value = (int)(value * 0.75);
        modifier = new StatModifier(value, StatModType.Flat, 1, generated);
        keyValuePairs.Add(StatType.CON, modifier);

        return keyValuePairs;
    }
    public static Dictionary<StatType, StatModifier> LegArmor(Rarity rarity)
    {
        StatModifier modifier;
        Dictionary<StatType, StatModifier> keyValuePairs= new Dictionary<StatType, StatModifier>();
        int value = rarityModifiers[rarity][Random.Range(0, rarityModifiers[rarity].Length)];
        value = (int)(value * 0.75);
        modifier = new StatModifier(value, StatModType.Flat, 1, generated);
        keyValuePairs.Add(StatType.CON, modifier);

        return keyValuePairs;
    }
    public static Dictionary<StatType, StatModifier> FeetArmor(Rarity rarity)
    {
        StatModifier modifier;
        Dictionary<StatType, StatModifier> keyValuePairs= new Dictionary<StatType, StatModifier>();
        int value = rarityModifiers[rarity][Random.Range(0, rarityModifiers[rarity].Length)];
        value = (int)(value * 0.50);
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
                generated = ScriptableObject.CreateInstance<Equipment>();
                generated.InitEquipment(slot, rarity, 0, value, slotMesh[slot], slotSprite[slot]);
                break;
            case EquipmentSlot.Head:
                generated = ScriptableObject.CreateInstance<Equipment>();
                generated.InitEquipment(slot, rarity, value, 0, slotMesh[slot], slotSprite[slot]);
                break;
            case EquipmentSlot.Accessoire:
                generated = ScriptableObject.CreateInstance<Equipment>();
                generated.InitEquipment(slot, rarity, value, value, slotMesh[slot], slotSprite[slot]);
                break;
            default:
                Debug.Log($"Uncrognized Equipment Slot: {slot} Default Equipment crafted with value 0");
                generated = ScriptableObject.CreateInstance<Equipment>();
                generated.InitEquipment(EquipmentSlot.Default, Rarity.Default, 0, 0, slotMesh[slot], slotSprite[slot]);
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
        switch(slot)
        {
            case EquipmentSlot.Secondary:
                break;
            case EquipmentSlot.Head:
                break;
            case EquipmentSlot.Body:
                break;
            case EquipmentSlot.Legs:
                break;
            case EquipmentSlot.Feet:
                break;
            case EquipmentSlot.Accessoire:
                break;
            default:
                break;
        }
        if(slot == EquipmentSlot.Head)
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
