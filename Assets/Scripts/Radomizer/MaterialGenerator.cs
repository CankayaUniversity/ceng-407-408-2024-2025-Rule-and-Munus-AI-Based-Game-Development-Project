using System.Collections.Generic;
using UnityEngine;
using Types;

public static class MaterialGenerator
{
    public static Stock stock = Stock.instance;
    public static List<Material> ifNeeded;
    public static void Generate(MaterialType type, Stat luck)
    {
        int amount = Random.Range(1, 20) + (int)(luck.value * 0.75);
        stock.Add(type, amount);
    }
    public static void Generate(MaterialType type, int amount)
    {
        stock.Add(type, amount);
    }
}
