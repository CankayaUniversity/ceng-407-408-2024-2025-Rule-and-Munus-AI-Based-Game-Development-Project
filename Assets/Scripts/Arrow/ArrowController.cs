using Types;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private int arrowNum = 10;
    [SerializeField] private CharacterMovingButtons characterMovingButtons;
    private EquipmentManager equipmentManager;

    private void Awake()
    {
        equipmentManager = FindObjectOfType<EquipmentManager>();

        Rarity weapon = Rarity.Common; // Varsayýlan deðer (ya da enum'un en düþük deðeri)

        if (equipmentManager != null)
        {
            var secondaryEquipment = equipmentManager.currentEquipment
                .Find(x => x.equipSlot == EquipmentSlot.Secondary);

            if (secondaryEquipment != null)
            {
                weapon = secondaryEquipment.rarirty;
                arrowNum *= (int)weapon;
            }
        }
        else
        {
            Debug.LogWarning("EquipmentManager not found in scene!");
        }

        Debug.Log("ArrowNum: " + arrowNum);
        Debug.Log("rarity: " + weapon);
    }

    public void arrowCounter()
    {
        arrowNum--;
        if (arrowNum <= 0)
        {
            characterMovingButtons.ChangeArrowButton.SetActive(false);
            characterMovingButtons.ChangeWeaponButton();
        }
    }
}
