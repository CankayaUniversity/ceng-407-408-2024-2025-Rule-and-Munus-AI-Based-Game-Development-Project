using System.Collections.Generic;
using UnityEngine;
using Types;

public static class MaterialGenerator
{
    public static Stock stock = Stock.instance;
    public static List<Material> ifNeeded;
    public static Material[] materials = Material.FindObjectsByType<Material>(FindObjectsSortMode.None); 
    public static void Generate(MaterialType type, Stat luck)
    {
        int amount = Random.Range(1, 20) + (int)(luck.baseValue * 0.75);
        int fate = Random.Range(0, materials.Length);
        stock.Add(type, amount);
    }
}
