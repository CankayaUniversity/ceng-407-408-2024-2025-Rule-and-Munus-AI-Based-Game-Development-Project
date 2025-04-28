using UnityEngine.UI;
using UnityEngine;
using Types;
using Icons;
using System.Collections.Generic;

namespace Equipments
{
    public struct EquipmentType
    {
        public EquipmentSlot slot;
        public DamageType damageType;
        public Image sprite;
        public float factor;
        public SkinnedMeshRenderer mesh;
        public List<StatType> statTypes;
        public EquipmentType(EquipmentSlot slot = EquipmentSlot.Default, DamageType damageType = DamageType.Default, float factor = 1.0f, Image sprite = null, SkinnedMeshRenderer mesh = null, List<StatType> statTypes = null)
        {
            this.slot = slot;
            this.damageType = damageType;
            this.sprite = sprite;
            this.mesh = mesh;
            this.factor = factor;
            this.statTypes = statTypes;
        }
        public static readonly EquipmentType headLeatherArmor = new EquipmentType(EquipmentSlot.Head, DamageType.Piercing, 0.60f, 
            Resources.Load<Image> ("Helmet"), Resources.Load<SkinnedMeshRenderer> ("Helmet"), new List<StatType>(){StatType.CON});
        public static readonly EquipmentType bodyLeatherArmor = new EquipmentType(EquipmentSlot.Body, DamageType.Bludgeoning, 0.75f, 
            Resources.Load<Image> ("Platedody"), Resources.Load<SkinnedMeshRenderer> ("Platebody"), new List<StatType>(){StatType.CON});
        public static readonly EquipmentType legLeatherArmor = new EquipmentType(EquipmentSlot.Legs, DamageType.Slashing, 0.75f, 
            Resources.Load<Image> ("Platelegs"), Resources.Load<SkinnedMeshRenderer> ("Platelegs"), new List<StatType>(){StatType.CON});
        public static readonly EquipmentType feetLeatherArmor = new EquipmentType(EquipmentSlot.Feet, DamageType.Bludgeoning, 0.50f, 
            Resources.Load<Image> ("Default"), Resources.Load<SkinnedMeshRenderer> ("Default"), new List<StatType>(){StatType.CON});
        public static readonly EquipmentType shortSword = new EquipmentType(EquipmentSlot.Weapon, DamageType.Slashing, 1.0f, 
            Resources.Load<Image> ("Default"), Resources.Load<SkinnedMeshRenderer> ("Default"), new List<StatType>(){StatType.STR});
        public static readonly EquipmentType longSword = new EquipmentType(EquipmentSlot.Weapon, DamageType.Slashing, 1.5f, 
            Resources.Load<Image> ("Default"), Resources.Load<SkinnedMeshRenderer> ("Default"), new List<StatType>(){StatType.STR});
        public static readonly EquipmentType bow = new EquipmentType(EquipmentSlot.Weapon, DamageType.Piercing, 1.0f, 
            Resources.Load<Image> ("Default"), Resources.Load<SkinnedMeshRenderer> ("Default"), new List<StatType>(){StatType.DEX});
        public static readonly EquipmentType hammer = new EquipmentType(EquipmentSlot.Weapon, DamageType.Bludgeoning, 1.0f, 
            Resources.Load<Image> ("Default"), Resources.Load<SkinnedMeshRenderer> ("Default"), new List<StatType>(){StatType.STR});
        public static readonly EquipmentType accessoire = new EquipmentType(EquipmentSlot.Accessoire, DamageType.Default, 0.75f, 
            Resources.Load<Image> ("Default"), Resources.Load<SkinnedMeshRenderer> ("Default"), new List<StatType>(){StatType.WIS, StatType.LUCK});
        public static readonly EquipmentType defaultEquipment = new EquipmentType(EquipmentSlot.Default, DamageType.Default, 0.0f, 
            Resources.Load<Image> ("Default"), Resources.Load<SkinnedMeshRenderer> ("Default"), new List<StatType>(){StatType.Default});
        public static readonly List<EquipmentType> equipmentList = new List<EquipmentType>() {
            {headLeatherArmor},
            {bodyLeatherArmor},
            {legLeatherArmor},
            {feetLeatherArmor},
            {shortSword},
            {longSword},
            {bow},
            {hammer},
            {accessoire},
            {defaultEquipment},
        };
    };

}
