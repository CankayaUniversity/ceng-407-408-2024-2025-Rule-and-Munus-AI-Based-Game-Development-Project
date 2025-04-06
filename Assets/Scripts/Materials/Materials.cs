using System.Collections.Generic;
using UnityEngine;
using Types;

namespace Materials
{   
    public static class materials{
        public static Dictionary<MaterialType, Material> typeMaterial = new Dictionary<MaterialType, Material>() {
		{ MaterialType.Wood, new Material(MaterialType.Wood, 0)},
		{ MaterialType.Cloth, new Material(MaterialType.Cloth, 0)},
        { MaterialType.Iron, new Material(MaterialType.Iron, 0)},
        { MaterialType.Stone, new Material(MaterialType.Stone, 0)},
	    };
    }
    
    
}
