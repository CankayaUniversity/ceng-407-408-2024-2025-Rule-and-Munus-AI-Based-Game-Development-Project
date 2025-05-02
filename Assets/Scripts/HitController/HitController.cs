using UnityEngine;
using System.Collections.Generic;
using Types;


public class HitController : MonoBehaviour
{
    public Attributes attributes;
    public EquipmentManager equipmentManager;
    public CharacterMovingButtons characterMovingButton;


    private void Start()
    {
        attributes = GetComponent<Attributes>();
    }

    /// <summary>
    /// Sald�r� yap�ld���nda �a�r�l�r (�rn. animasyon eventinden veya butondan).
    /// </summary>
    /// <param name="attackerInventory">Sald�ran karakterin envanteri</param>
    /// <param name="isArrow">True ise ok sald�r�s�, False ise k�l��</param>
    public void ApplyHit(Inventory attackerInventory, bool isArrow)
    {
        if (attackerInventory == null)
        {
            Debug.LogWarning("Sald�ran Inventory bo�!");
            return;
        }

        Equipment weapon = null;
        Equipment armor = null;
        int flag = 0;

        // EnemyAI�den savunulan yeri al
        EnemyAI enemyAI = FindAnyObjectByType<EnemyAI>();
        int defenceIndex = (int)enemyAI.defecencedSlot;
        
        armor = GetArmorByIndex(defenceIndex);

        // Silah t�r�n� belirle
        weapon = attackerInventory.equipments.Find(x =>
            x.equipSlot == (isArrow ? EquipmentSlot.Secondary : EquipmentSlot.Weapon));

        // E�er sald�r� y�n� savunulan yerse, hasar s�f�rlan�r
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

        if (weapon.damageType == armor.damageType)
        {
            damage += damage / 2;
        }

        if (flag == 1)
        {
            damage = 0;
        }

        attributes.UpdateHealth(-damage);
        Debug.Log($"Verilen Hasar: {damage}, Kalan Can: {attributes.currentHealth}");
    }
}
