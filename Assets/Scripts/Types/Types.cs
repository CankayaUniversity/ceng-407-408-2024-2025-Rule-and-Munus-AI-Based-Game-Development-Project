using JetBrains.Annotations;
using Unity.AppUI.UI;
using UnityEngine.UI;
using UnityEngine;

namespace Types
{   
        public enum EquipmentSlot 
        { 
            Head, 
            Body, 
            Legs, 
            Weapon,
            Secondary,
            Feet, 
            Hand,
            Accessoire,
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
            Common,
            Advenced,
            Uncommon,
            Rare,
            Epic,
            Legendary,
            Default,
        }
        public enum StatType
        {
            STR,
            DEX,
            INT,
            WIS,
            CON,
            CHA,
            LUCK,
            Default,
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
