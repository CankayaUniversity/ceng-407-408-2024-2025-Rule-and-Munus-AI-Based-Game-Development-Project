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
        EquipmentSlot[] slots = new EquipmentSlot[]
        {
            EquipmentSlot.Head,
            EquipmentSlot.Body,
            EquipmentSlot.Legs,
            EquipmentSlot.Weapon,
            EquipmentSlot.Secondary,
            EquipmentSlot.Feet,
            EquipmentSlot.Accessoire
        };

        foreach (EquipmentSlot slot in slots)
        {
            Equipment newEquipment = ScriptableObject.CreateInstance<Equipment>();
            newEquipment.equipSlot = slot;
            newEquipment.rarirty = GetInitialRarity();

            enemyEquipments.Add(newEquipment);
        }

        Debug.Log("Enemy's initial equipment has been generated!");
    }

    private Rarity GetInitialRarity()
    {
        int randomValue = Random.Range(0, 100);

        if (randomValue < 40)
            return Rarity.Common;     
        else if (randomValue < 70)
            return Rarity.Uncommon;   
        else if (randomValue < 85)
            return Rarity.Advenced;    
        else if (randomValue < 95)
            return Rarity.Rare;        
        else if (randomValue < 99)
            return Rarity.Epic;        
        else
            return Rarity.Legendary;   
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
        }

        Debug.Log($"Düþmanýn ekipmanlarý güncellendi! Yeni seviye: {progressLevel}");
        progressLevel++; 
    }

    private Rarity GetUpgradedRarity(Rarity current, int level)
    {
        
        switch (current)
        {
            case Rarity.Common:
                return (level >= 1) ? Rarity.Uncommon : current;
            case Rarity.Uncommon:
                return (level >= 2) ? Rarity.Advenced : current;
            case Rarity.Advenced:
                return (level >= 3) ? Rarity.Rare : current;
            case Rarity.Rare:
                return (level >= 4) ? Rarity.Epic : current;
            case Rarity.Epic:
                return (level >= 5) ? Rarity.Legendary : current;
            case Rarity.Legendary:
                return Rarity.Legendary; 
            default:
                return current;
        }
    }

    
}
