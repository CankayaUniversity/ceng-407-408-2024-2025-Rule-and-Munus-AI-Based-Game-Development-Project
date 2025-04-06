using System.Collections.Generic;
using UnityEngine;
using Types;

namespace Stats
{   
    public static class _stats{
        public static Dictionary<StatType, StatModifier> statModifiers = new Dictionary<StatType, StatModifier>() {
		{ StatType.STR, new StatModifier(0, StatModType.Flat)},
		{ StatType.DEX, new StatModifier(0, StatModType.Flat)},
		{ StatType.INT, new StatModifier(0, StatModType.Flat)},
		{ StatType.WIS, new StatModifier(0, StatModType.Flat)},
		{ StatType.CON, new StatModifier(0, StatModType.Flat)},
		{ StatType.CHA, new StatModifier(0, StatModType.Flat)},
		{ StatType.LUCK, new StatModifier(0, StatModType.Flat)},
	    };
        public static Dictionary<StatType, Stat> stats = new Dictionary<StatType, Stat>() {
        { StatType.STR, new Stat(1)},
        { StatType.DEX, new Stat(1)},
        { StatType.INT, new Stat(1)},
        { StatType.WIS, new Stat(1)},
        { StatType.CON, new Stat(1)},
        { StatType.CHA, new Stat(1)},
        { StatType.LUCK, new Stat(1)},
        { StatType.Default, new Stat(1)}
    };
    }
    
    
}
