using System.Collections.Generic;
using UnityEngine;
using Types;

namespace Odds
{   
    public static class odds{
        public static readonly Dictionary<Rarity, int[]> rarityValues = new Dictionary<Rarity, int[]>() {
        { Rarity.Common, new int[8] {1, 1, 2, 2, 2, 3, 4, 5}},
        { Rarity.Advenced, new int[12] {3, 4, 5, 5, 5, 6, 6, 7, 7, 8, 9, 10}},
        { Rarity.Uncommon, new int[11] {5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}},
        { Rarity.Rare, new int[18] {6, 7, 8, 8, 9, 9, 9, 10, 10, 11, 11, 11, 12, 12, 13, 13, 14, 15}},
        { Rarity.Epic, new int[16] {10, 11, 12, 13, 14, 15, 16, 16, 16, 17, 17, 18, 18, 19, 19, 20}},
        { Rarity.Legendary, new int[13] {16, 17, 17, 17, 18, 18, 18, 18, 19, 19, 19, 20, 20}},
        { Rarity.Default, new int[1] {1}}
        };
        public static readonly Dictionary<Rarity, int[]> rarityModifiers = new Dictionary<Rarity, int[]>() { 
        { Rarity.Common, new int[8] {0, 0, 0, 0, 1, 1, 1, 2}},
        { Rarity.Advenced, new int[7] {1, 1, 1, 2, 2, 3, 4}},
        { Rarity.Uncommon, new int[10] {2, 2, 2, 3, 3, 4, 4, 5, 6, 7}},
        { Rarity.Rare, new int[13] {3, 4, 4, 5, 5, 5, 6, 6, 6, 7, 8, 9, 10}},
        { Rarity.Epic, new int[22] {5, 5, 6, 6, 6, 6, 7, 7, 7, 8 ,8, 8, 8, 9, 9, 9, 10, 11, 11, 12, 12, 13}},
        { Rarity.Legendary, new int[26] {8, 8, 8, 9, 9, 9, 10, 10, 10, 10, 11, 11, 11, 11, 12, 12, 12, 12, 13, 13, 13, 14, 14, 15, 15, 15}},
        { Rarity.Default, new int[1] {1}}
        };
        public static readonly List<int> _1d6 = new List<int> {1, 2, 3, 4, 5, 6};
    }
    
    
}
