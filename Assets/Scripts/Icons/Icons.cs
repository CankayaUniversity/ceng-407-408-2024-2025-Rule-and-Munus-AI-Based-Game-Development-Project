using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Types;
using Equipments;

namespace Icons
{   
    public static class icons{
        public static readonly Dictionary<EquipmentSlot, Sprite> slotSprite = new Dictionary<EquipmentSlot, Sprite>() {
		    { EquipmentSlot.Weapon, Resources.Load<Sprite> ("Platelegs")},
            { EquipmentSlot.Body, Resources.Load<Sprite> ("Platebody")},
            { EquipmentSlot.Head, Resources.Load<Sprite> ("Helmet")},
            { EquipmentSlot.Legs, Resources.Load<Sprite> ("Default")},
            { EquipmentSlot.Feet, Resources.Load<Sprite> ("Default")},
            { EquipmentSlot.Accessoire, Resources.Load<Sprite> ("Default")},
            { EquipmentSlot.Secondary, Resources.Load<Sprite> ("Default")},
            { EquipmentSlot.Default, Resources.Load<Sprite> ("Default")},
	    };
        public static readonly Dictionary<EquipmentSlot, Image> slotImage = new Dictionary<EquipmentSlot, Image>() {
		    { EquipmentSlot.Weapon, Resources.Load<Image> ("Platelegs")},
            { EquipmentSlot.Body, Resources.Load<Image> ("Platebody")},
            { EquipmentSlot.Head, Resources.Load<Image> ("Helmet")},
            { EquipmentSlot.Legs, Resources.Load<Image> ("Default")},
            { EquipmentSlot.Feet, Resources.Load<Image> ("Default")},
            { EquipmentSlot.Accessoire, Resources.Load<Image> ("Default")},
            { EquipmentSlot.Default, Resources.Load<Image> ("Default")},

        };
        public static readonly Dictionary<EquipmentSlot, SkinnedMeshRenderer> slotMesh = new Dictionary<EquipmentSlot, SkinnedMeshRenderer>() {
		    { EquipmentSlot.Weapon, Resources.Load<SkinnedMeshRenderer> ("Platelegs")},
            { EquipmentSlot.Body, Resources.Load<SkinnedMeshRenderer> ("Platebody")},
            { EquipmentSlot.Head, Resources.Load<SkinnedMeshRenderer> ("Helmet")},
            { EquipmentSlot.Legs, Resources.Load<SkinnedMeshRenderer> ("Default")},
            { EquipmentSlot.Feet, Resources.Load<SkinnedMeshRenderer> ("Default")},
            { EquipmentSlot.Accessoire, Resources.Load<SkinnedMeshRenderer> ("Default")},
            { EquipmentSlot.Default, Resources.Load<SkinnedMeshRenderer> ("Default")},
        };
    }
}


