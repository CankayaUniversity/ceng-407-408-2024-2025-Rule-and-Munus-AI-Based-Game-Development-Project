using System.Collections.Generic;
using Types;
using Equipments;
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
        Equipment head = ItemGenerator.Generate(EquipmentType.headLeatherArmor, Rarity.Common);


        Equipment body = ItemGenerator.Generate(EquipmentType.bodyLeatherArmor, Rarity.Common);


        Equipment legs = ItemGenerator.Generate(EquipmentType.legLeatherArmor, Rarity.Common);


        
        Equipment feet = ItemGenerator.Generate(EquipmentType.feetLeatherArmor, Rarity.Common);


        enemyEquipments.Add(head);
        enemyEquipments.Add(body);
        enemyEquipments.Add(legs);
        enemyEquipments.Add(feet);

        // Ba�lang�� silahlar� (her hasar tipi i�in 1 adet Common silah)
        foreach (DamageType type in System.Enum.GetValues(typeof(DamageType)))
        {
            if (type == DamageType.Default)
                continue;

            Equipment shortSword = ItemGenerator.Generate(EquipmentType.shortSword, Rarity.Common);
            Equipment bow = ItemGenerator.Generate(EquipmentType.bow, Rarity.Common);

            enemyEquipments.Add(shortSword);
            enemyEquipments.Add(bow);
        }
    }


    private void GenerateEnemyEquipment(Equipment Weapon)
    {
        enemyEquipments =new List<Equipment>();
        Equipment head = ItemGenerator.Generate(EquipmentType.headLeatherArmor, Rarity.Common);


        Equipment body = ItemGenerator.Generate(EquipmentType.bodyLeatherArmor, Rarity.Common);


        Equipment legs = ItemGenerator.Generate(EquipmentType.legLeatherArmor, Rarity.Common);


        
        Equipment feet = ItemGenerator.Generate(EquipmentType.feetLeatherArmor, Rarity.Common);


        enemyEquipments.Add(head);
        enemyEquipments.Add(body);
        enemyEquipments.Add(legs);
        enemyEquipments.Add(feet);

        enemyEquipments.Add(Weapon);
    }
//her 10 fazda 1 rarity değişicek 
    private bool ShouldUpgradeEquipment()
    {
        return progressLevel >= 1;
    }

    private void UpgradeEquipment(int level)
    {
        int index = Dice.RollaDice();
        Rarity rarity = (Rarity)index;
       
        Equipment weapon = ItemGenerator.Generate(EquipmentType.shortSword, rarity);


        Debug.Log($"D��man�n ekipmanlar� g�ncellendi! Yeni seviye: {progressLevel}");
        progressLevel++;
        GenerateEnemyEquipment(weapon);
    }

    public Equipment GetEquippedWeapon(int flag)
    {
        
        if(flag==1){
            Equipment weapon = enemyEquipments.Find(e => e.equipSlot == EquipmentSlot.Weapon);

            if (weapon == null)
            {
                Debug.LogWarning("No weapon found in equipped items.");
            }

            return weapon;
        }
        else{
            Equipment weapon = enemyEquipments.Find(e => e.equipSlot == EquipmentSlot.Secondary);

            if (weapon == null)
            {
                Debug.LogWarning("No weapon found in equipped items.");
            }

            return weapon;
        }
        
    }
}
