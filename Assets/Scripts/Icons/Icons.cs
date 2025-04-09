using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Types;

namespace Icons
{   
    public static class icons{
        public static readonly Dictionary<EquipmentSlot, Sprite> slotSprite = new Dictionary<EquipmentSlot, Sprite>() {
		    { EquipmentSlot.Weapon, Resources.Load<Sprite> ("Platelegs")},
            { EquipmentSlot.Armor, Resources.Load<Sprite> ("Platebody")},
            { EquipmentSlot.Accessoire, Resources.Load<Sprite> ("Helmet")},
            { EquipmentSlot.Default, Resources.Load<Sprite> ("Default")},

	    };
        public static readonly Dictionary<EquipmentSlot, Image> slotImage = new Dictionary<EquipmentSlot, Image>() {
		    { EquipmentSlot.Weapon, Resources.Load<Image> ("Platelegs")},
            { EquipmentSlot.Armor, Resources.Load<Image> ("Platebody")},
            { EquipmentSlot.Accessoire, Resources.Load<Image> ("Helmet")},
            { EquipmentSlot.Default, Resources.Load<Image> ("Default")},

        };
        public static readonly Dictionary<EquipmentSlot, SkinnedMeshRenderer> slotMesh = new Dictionary<EquipmentSlot, SkinnedMeshRenderer>() {
		    { EquipmentSlot.Weapon, Resources.Load<SkinnedMeshRenderer> ("Platelegs")},
            { EquipmentSlot.Armor, Resources.Load<SkinnedMeshRenderer> ("Platebody")},
            { EquipmentSlot.Accessoire, Resources.Load<SkinnedMeshRenderer> ("Helmet")},
            { EquipmentSlot.Default, Resources.Load<SkinnedMeshRenderer> ("Default")},
        };
    }
}


