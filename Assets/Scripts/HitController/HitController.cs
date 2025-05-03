using UnityEngine;
using System.Collections.Generic;
using Types;


public class HitController : MonoBehaviour
{
    public Attributes attributes;
    public EquipmentManager equipmentManager;
    public CharacterMovingButtons characterMovingButton;
    //public GameObject gameObject;


    private void Start()
    {
        //equipmentManager = GetComponent<EquipmentManager>();
        attributes = GetComponent<Attributes>();
    }

    /// <summary>
    /// Sald�r� yap�ld���nda �a�r�l�r (�rn. animasyon eventinden veya butondan).
    /// </summary>
    /// <param name="attackerInventory">Sald�ran karakterin envanteri</param>
    /// <param name="isArrow">True ise ok sald�r�s�, False ise k�l��</param>
    public void ApplyHit(EquipmentManager equipmentManager, bool isArrow)
    {
        Debug.Log("ApplyHit e geldi");
        if (equipmentManager.currentEquipment == null)
        {
            Debug.LogWarning("Sald�ran Inventory bo�!");
            return;
        }

        foreach (var eq in equipmentManager.currentEquipment)
        {
            Debug.Log($"Ekipman: {eq.name}, Slot: {eq.equipSlot}");
        }
            


        Equipment weapon = null;
        Equipment armor = null;

        
        int flag = 0;

        // EnemyAI�den savunulan yeri al
        EnemyAI enemyAI = FindAnyObjectByType<EnemyAI>();
        int defenceIndex = (int)enemyAI.defecencedSlot;
        Debug.Log($"Savunulan b�lge: {defenceIndex}");
        armor = GetArmorByIndex(defenceIndex);

        Debug.Log(armor != null ? $"Savunulan z�rh: {armor.name}" : "Savunulan z�rh yok!");


        weapon = equipmentManager.currentEquipment.Find(x =>
           x.equipSlot == (isArrow ? EquipmentSlot.Secondary : EquipmentSlot.Weapon));

        Debug.Log(weapon != null ? $"Sald�r�lan silah: {weapon.equipSlot}" : "Sald�r�lan silah yok!");

        
        int attackIndex = isArrow ? characterMovingButton.arrowIndex : characterMovingButton.attackIndex;

        if (attackIndex == defenceIndex)
        {
            flag = 1;
            Debug.Log((isArrow ? "Ok" : "K�l��") + " sald�r�s� engellendi!");
        }

        calculateDamage(weapon, armor, flag);
    }

    private Equipment GetArmorByIndex(int index)
    {
        switch (index)
        {
            case 1:
                return equipmentManager.currentEquipment.Find(x => x.equipSlot == EquipmentSlot.Head);
            case 2:
                return equipmentManager.currentEquipment.Find(x => x.equipSlot == EquipmentSlot.Body);
            case 3:
                return equipmentManager.currentEquipment.Find(x => x.equipSlot == EquipmentSlot.Legs);
            default:
                Debug.LogWarning("Ge�ersiz savunma b�lgesi!");
                return null;
        }
    }

    private void calculateDamage(Equipment weapon, Equipment armor, int flag)
    {
        if (weapon == null || armor == null)
        {
            Debug.LogWarning("Silah veya z�rh eksik!");
            return;
        }

        int damage = weapon.damageModifier - armor.armorModifier;

        Debug.Log("Hasarü: " + damage);

        if (weapon.damageType == armor.damageType)
        {
            damage += damage / 2;
        }

        if (flag == 1)
        {
            damage = 0;
        }

        attributes.UpdateHealth(attributes.currentHealth - damage);
        Debug.Log($"Verilen Hasar: {damage}, Kalan Can: {attributes.currentHealth}");
    }
}
