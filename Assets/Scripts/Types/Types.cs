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
            Hand,
            Accessoire = 7,
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
            Default,
        }
        public enum StatType
        {
            STR = 1,
            DEX = 2,
            INT = 3,
            WIS = 4,
            CON = 5,
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
}
