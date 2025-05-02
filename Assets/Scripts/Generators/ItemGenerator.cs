using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Types;
using Odds;
using Icons;
using Equipments;
using Unity.VisualScripting;
// using Microsoft.Unity.VisualStudio.Editor;
public static class ItemGenerator
{
    public static GameObject gameObject =  GameObject.Find("Player");
    public static Inventory inventory = gameObject.GetComponent<Inventory>();
    public static Equipment generated;
    public static Rarity equipmentRarity;
    public static EquipmentType equipmentType;
    public static int fate;
    public static Dictionary<EquipmentSlot, Sprite> slotSprite = icons.slotSprite;
    public static void Generate(EquipmentType type, Rarity rarity, Stat luck)
    {
        Dictionary<StatType, StatModifier> statTypeModifier= new Dictionary<StatType, StatModifier>();
        generated = ScriptableObject.CreateInstance<Equipment>();
        fate = (int)luck.value;
        equipmentType = type;
        equipmentRarity = rarity;
        //Crafts an equipment with damageFactor/defenceFactor but statModifiers
        Craft(); 
        //Polishs the equipment for adjusting the statModifiers
        Polish(ref statTypeModifier);
        /* switch(type.slot)
        {
            case EquipmentSlot.Weapon:
                statTypeModifier = PrimaryWeapon(rarity);
                break;
            case EquipmentSlot.Secondary:
                statTypeModifier = SecondaryWeapon(rarity);       
                break;
                //head + body + legs + feet)
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
        } */

        generated.AdjustStatModifiers(statTypeModifier);
        inventory.Add(generated);
        // inventory.ShowItems();
    }

    public static Equipment Generate(EquipmentType type, Rarity rarity)
    {
        Dictionary<StatType, StatModifier> statTypeModifier= new Dictionary<StatType, StatModifier>();
        generated = ScriptableObject.CreateInstance<Equipment>();
        fate = 1;
        equipmentType = type;
        equipmentRarity = rarity;
        //Crafts an equipment with damageFactor/defenceFactor but statModifiers
        Craft(); 
        //Polishs the equipment for adjusting the statModifiers
        Polish(ref statTypeModifier);
        generated.AdjustStatModifiers(statTypeModifier);
        return generated;
    }
    public static void Polish(ref Dictionary<StatType, StatModifier> sTM)
    {
        int value = odds.rarityModifiers[equipmentRarity][Random.Range(0, odds.rarityModifiers[equipmentRarity].Length)];
        // value = (int)(value * type.factor); //Open this if balance is necessary
        StatModifier modifier = new StatModifier(value, StatModType.Flat, 1, generated);

        for(int i = 0; i < equipmentType.statTypes.Count; i++)
        {
            sTM.Add(equipmentType.statTypes[i], modifier);
        }
    }
/*     public static Dictionary<StatType, StatModifier> PrimaryWeapon(Rarity rarity)
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
    } */
    // public static Equipment Scroll(int fate)
    // {

    // }
    public static void Craft()
    {
        int value = DamageFactor();
        generated.InitEquipment(equipmentType.slot, equipmentRarity, equipmentType.damageType, 0, 0, equipmentType.mesh, slotSprite[equipmentType.slot]);
        generated.name = equipmentType.ToString();
        switch(equipmentType.slot)
        {
            case EquipmentSlot.Weapon:
                generated.setDamageModifier(value);
                break;
            case EquipmentSlot.Secondary:
                generated.setDamageModifier(value);
                break;
            case EquipmentSlot.Head:
                generated.setArmorModifier(value);
                break;
            case EquipmentSlot.Body:
                generated.setArmorModifier(value);
                break;
            case EquipmentSlot.Legs:
                generated.setArmorModifier(value);
                break;
            case EquipmentSlot.Feet:
                generated.setArmorModifier(value);
                break;
            case EquipmentSlot.Accessoire:
                generated.setDamageModifier(value);
                generated.setArmorModifier(value);
                break;
            default:
                Debug.Log($"Uncrognized Equipment Slot: {equipmentType.slot} Default Equipment crafted with value 0");
                break;
        }
    }
    public static int DamageFactor()
    {
        int value = odds.rarityValues[equipmentRarity][Random.Range(0, odds.rarityValues[equipmentRarity].Length)];
        value = (int)(value*equipmentType.factor);
        return value;
    }
}
