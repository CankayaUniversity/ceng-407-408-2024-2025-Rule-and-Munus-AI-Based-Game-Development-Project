using System.Collections.Generic;
using Types;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public List<Equipment> enemyEquipments = new List<Equipment>();

    private int progressLevel = 0;
    private float upgradeCheckTimer = 0f;
    private const float upgradeCheckInterval = 10f;

    private void Start()
    {
        GenerateEnemyEquipment();
    }

    private void Update()
    {
        upgradeCheckTimer += Time.deltaTime;

        if (upgradeCheckTimer >= upgradeCheckInterval)
        {
            upgradeCheckTimer = 0f;
            if (ShouldUpgradeEquipment())
            {
                UpgradeEquipment(progressLevel);
            }
        }
    }

    private void GenerateEnemyEquipment()
    {
        // Ba�lang�� z�rhlar� (Bludgeoning tipinde)
        Equipment head = new Equipment(
            EquipmentSlot.Head,
            Rarity.Common,
            DamageType.Bludgeoning,
            armorModifier: 2,
            damageModifier: 0,
            mesh: null,
            icon: null
        );

        Equipment body = new Equipment(
            EquipmentSlot.Body,
            Rarity.Common,
            DamageType.Bludgeoning,
            armorModifier: 5,
            damageModifier: 0,
            mesh: null,
            icon: null
        );

        Equipment legs = new Equipment(
            EquipmentSlot.Legs,
            Rarity.Common,
            DamageType.Bludgeoning,
            armorModifier: 1,
            damageModifier: 0,
            mesh: null,
            icon: null
        );

        enemyEquipments.Add(head);
        enemyEquipments.Add(body);
        enemyEquipments.Add(legs);

        // Ba�lang�� silahlar� (her hasar tipi i�in 1 adet Common silah)
        foreach (DamageType type in System.Enum.GetValues(typeof(DamageType)))
        {
            if (type == DamageType.Default)
                continue;

            Equipment weapon = new Equipment(
                EquipmentSlot.Weapon,
                Rarity.Common,
                type,
                armorModifier: 0,
                damageModifier: 5, // ba�lang�� hasar� d���k
                mesh: null,
                icon: null
            );

            enemyEquipments.Add(weapon);
        }
    }

    private bool ShouldUpgradeEquipment()
    {
        return progressLevel >= 1;
    }

    private void UpgradeEquipment(int level)
    {
        foreach (Equipment equipment in enemyEquipments)
        {
            equipment.rarirty = GetUpgradedRarity(equipment.rarirty, level);

            // Ayr�ca armorModifier'� art�r
            switch (equipment.rarirty)
            {
                case Rarity.Rare:
                    equipment.armorModifier += 5; // +5 z�rh
                    break;
                case Rarity.Epic:
                    equipment.armorModifier += 10; // +10 z�rh
                    break;
                case Rarity.Legendary:
                    equipment.armorModifier += 15; // +15 z�rh
                    break;
            }
        }

        Debug.Log($"D��man�n ekipmanlar� g�ncellendi! Yeni seviye: {progressLevel}");
        progressLevel++;
    }

    private Rarity GetUpgradedRarity(Rarity currentRarity, int level)
    {
        switch (currentRarity)
        {
            case Rarity.Common:
                return level >= 2 ? Rarity.Rare : Rarity.Common;
            case Rarity.Rare:
                return level >= 4 ? Rarity.Epic : Rarity.Rare;
            case Rarity.Epic:
                return level >= 6 ? Rarity.Legendary : Rarity.Epic;
            case Rarity.Legendary:
                return Rarity.Legendary; // Daha �st� yok
            default:
                return currentRarity;
        }
    }


    public Equipment GetEquippedWeapon()
    {
        // Weapon slotuna sahip ekipman� buluyoruz
        Equipment weapon = enemyEquipments.Find(e => e.equipSlot == EquipmentSlot.Weapon);

        if (weapon == null)
        {
            Debug.LogWarning("No weapon found in equipped items.");
        }

        return weapon;
    }
}
