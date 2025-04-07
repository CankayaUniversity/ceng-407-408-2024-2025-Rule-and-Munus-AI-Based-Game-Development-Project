using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Types;

namespace Icons
{   
    public static class icons{
        public static readonly Dictionary<EquipmentSlot, Sprite> slotSprite = new Dictionary<EquipmentSlot, Sprite>() {
		    { EquipmentSlot.Weapon, Resources.Load<Sprite> ("Assets/Scripts/Icons/Platelegs")},
            { EquipmentSlot.Armor, Resources.Load<Sprite> ("Assets/Scripts/Icons/Platebody")},
            { EquipmentSlot.Accessoire, Resources.Load<Sprite> ("Assets/Scripts/Icons/Helmet")},
	    };
        public static readonly Dictionary<EquipmentSlot, Image> slotImage = new Dictionary<EquipmentSlot, Image>() {
		    { EquipmentSlot.Weapon, Resources.Load<Image> ("Assets/Scripts/Icons/Platelegs")},
            { EquipmentSlot.Armor, Resources.Load<Image> ("Assets/Scripts/Icons/Platebody")},
            { EquipmentSlot.Accessoire, Resources.Load<Image> ("Assets/Scripts/Icons/Helmet")},
        };
        public static readonly Dictionary<EquipmentSlot, SkinnedMeshRenderer> slotMesh = new Dictionary<EquipmentSlot, SkinnedMeshRenderer>() {
		    { EquipmentSlot.Weapon, Resources.Load<SkinnedMeshRenderer> ("Assets/Scripts/Icons/Platelegs")},
            { EquipmentSlot.Armor, Resources.Load<SkinnedMeshRenderer> ("Assets/Scripts/Icons/Platebody")},
            { EquipmentSlot.Accessoire, Resources.Load<SkinnedMeshRenderer> ("Assets/Scripts/Icons/Helmet")},
        };
    }
}


