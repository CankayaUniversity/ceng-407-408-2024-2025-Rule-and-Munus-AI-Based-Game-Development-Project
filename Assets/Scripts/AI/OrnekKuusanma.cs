using System.Collections.Generic;
using UnityEngine;
using Types;
using Stats;

public class Player : MonoBehaviour
{
    public string playerName;
    public int baseDamage;
    public int baseArmor;

    public Dictionary<EquipmentSlot, Equipment> equippedItems = new Dictionary<EquipmentSlot, Equipment>();

    private void Start()
    {
        playerName = "Knight";
        baseDamage = 10;
        baseArmor = 5;

        Debug.Log($"Before Equipping => Damage: {baseDamage}, Armor: {baseArmor}");

        // Bütün slotlara ekipman yarat ve kuþan
        EquipAllSlots();

        Debug.Log($"After Full Equipping => Damage: {baseDamage}, Armor: {baseArmor}");
    }

    public void Equip(Equipment equipment)
    {
        if (equippedItems.ContainsKey(equipment.equipSlot))
        {
            // Eski ekipmanýn etkisini geri al
            baseDamage -= equippedItems[equipment.equipSlot].damageModifier;
            baseArmor -= equippedItems[equipment.equipSlot].armorModifier;
            equippedItems[equipment.equipSlot] = equipment;
        }
        else
        {
            equippedItems.Add(equipment.equipSlot, equipment);
        }

        baseDamage += equipment.damageModifier;
        baseArmor += equipment.armorModifier;

        Debug.Log($"{equipment.equipSlot} equipped! Damage +{equipment.damageModifier}, Armor +{equipment.armorModifier}");
    }

    private void EquipAllSlots()
    {
        // Tüm slotlar için farklý ekipmanlar yarat
        List<Equipment> equipments = new List<Equipment>()
        {
            CreateEquipment(EquipmentSlot.Head, Rarity.Rare, DamageType.Piercing, 5, 2),
            CreateEquipment(EquipmentSlot.Body, Rarity.Epic, DamageType.Slashing, 15, 5),
            CreateEquipment(EquipmentSlot.Legs, Rarity.Advenced, DamageType.Slashing, 10, 2),
            CreateEquipment(EquipmentSlot.Feet, Rarity.Common, DamageType.Bludgeoning, 3, 1),
            CreateEquipment(EquipmentSlot.Hand, Rarity.Uncommon, DamageType.Bludgeoning, 2, 1),
            CreateEquipment(EquipmentSlot.Weapon, Rarity.Legendary, DamageType.Slashing, 0, 25),
            CreateEquipment(EquipmentSlot.Secondary, Rarity.Rare, DamageType.Piercing, 5, 5),
            CreateEquipment(EquipmentSlot.Accessoire, Rarity.Epic, DamageType.Fire, 0, 10)
        };

        foreach (var equipment in equipments)
        {
            Equip(equipment);
        }
    }

    // Basit ekipman oluþturucu
    private Equipment CreateEquipment(EquipmentSlot slot, Rarity rarity, DamageType dmgType, int armorMod, int dmgMod)
    {
        Equipment equipment = ScriptableObject.CreateInstance<Equipment>();
        equipment.InitEquipment(slot, rarity, dmgType, armorMod, dmgMod, null, null);
        return equipment;
    }
}
