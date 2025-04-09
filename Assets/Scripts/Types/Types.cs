using UnityEngine;

namespace Types
{   
        public enum EquipmentSlot 
        { 
            Head, 
            Body, 
            Legs, 
            Weapon, 
            // Shield, 
            Feet, 
            Accessoire,
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
