using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;
using Types;
using static UnityEngine.Rendering.DebugUI;
public class HitController : MonoBehaviour
{
    public ActionIndexController actionIndexController;
    public Attributes attributes;
    public EquipmentManager equipmentManager;
    public CharacterMovingButtons characterMovingButton;
    private EnemyAI enemyAI;



    private void Start()
    {
        attributes = GetComponent<Attributes>();
    }
    public void OnTriggerEnter(Collider other)
    {
        int flag = 0; // Flag to check if attack index is equal to defence index
        Equipment armor = null; 
        if (other.tag == "Arrow")
        {
            int index = IndexFind();
            int index2 = (int)enemyAI.defencedSlot;

            if (index2 == 1)
            {
                armor = equipmentManager.currentEquipment.Find(x => x.equipSlot == EquipmentSlot.Head);
            }
            else if (index2 == 2)
            {
                armor = equipmentManager.currentEquipment.Find(x => x.equipSlot == EquipmentSlot.Body);
            }
            else if (index2 == 3)
            {
                armor = equipmentManager.currentEquipment.Find(x => x.equipSlot == EquipmentSlot.Legs);
            }

            Equipment weapon = other.gameObject.GetComponent<Inventory>().equipments.Find(x => x.equipSlot == EquipmentSlot.Secondary);

            Destroy(other.gameObject);
            if(characterMovingButton.attackIndex == index)
            {
                flag = 1;
                Debug.Log("Attack index is equal to defence index!");
                calculateDamage(weapon, armor,flag);
            }
            calculateDamage(weapon, armor, flag);
        }
    }

    public int IndexFind()
    {
        int index = 0;   
        switch (characterMovingButton.attackIndex)
        {
            case 0:
                index = 0;
                break;
            case 1:
                index = 1;
                Debug.Log("Attack index is 1");
                break;
            case 2:
                index = 2;
                Debug.Log("Attack index is 2");
                break;
            case 3:
                index = 3;
                Debug.Log("Attack index is 3");
                break;
            default:
                Debug.Log("Invalid attack index");
                break;
        }
        return index;
    }



    public void calculateDamage(Equipment weapon, Equipment armor, int flag)
    {
        int damage = weapon.damageModifier-armor.armorModifier;
        if(weapon.damageType == armor.damageType)
        {
            damage = damage + damage / 2;
        }
        if(flag == 1)
        {
            damage = 0;
        }

        //actionIndexController.IndexController(attributes,damage);
        attributes.UpdateHealth(-damage);
    }
}
