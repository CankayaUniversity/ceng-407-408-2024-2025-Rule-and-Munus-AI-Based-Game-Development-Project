using JetBrains.Annotations;
using Unity.AppUI.UI;
using UnityEngine.UI;
using UnityEngine;

namespace Types
{
    public enum EquipmentSlot
    {
        Head = 1,
        Body = 2,
        Legs = 3,
        Feet = 4,
        Secondary = 5,
        Weapon = 6,
        Hand = 7,
        Accessoire = 8,
        Default = 9,
    }

    public enum DamageType
    {
        Piercing,
        Slashing,
        Bludgeoning,
        Fire,
        Cold,
        Poision,
        Default,
    }
    public enum Rarity
    {
        Common = 1,
        Advenced = 2,
        Uncommon = 3,
        Rare = 4,
        Epic = 5,
        Legendary = 6,
        Default = 7,
    }
    public enum StatType
    {
        STR = 1,
        DEX = 2,
        CON = 3,
        INT = 4,
        WIS = 5,
        CHA = 6,
        LUCK = 7,
        Default = 8,
    }
    public enum MaterialType
    {
        Wood,
        Stone,
        Iron,
        Cloth,
        Default,
    }
        // public enum EquipmentType
        // {
        //     HeadLeatherArmor,
        //     BodyLeatherArmor,
        //     LegsLeatherArmor,
        //     FeetLeatherArmor,
        //     Bow,
        //     ShortSword,
        //     LongSword,
        //     Hammer,
        //     Default,
        // }
}
