using UnityEngine;
using UnityEngine.Rendering;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string id = "Null";
    public string name = "Null";
    public string description = "Null";
    public Sprite icon;
    public GameObject prefab;
    // public enum Type = {"Default", "Consumable", "Weapon", "Armor"};
    // public Type type;
    public string tag = "Item";
    public int attack = 0;
    public int defence = 0;
    public int STR = 0;
    public int DEX = 0;
    public int INT = 0;
    public int WIS = 0;
    public int CON = 0;
    public int CHA = 0;

}
