using UnityEngine;
using System;
public enum StatModType
{
    Flat = 100,
    Percent = 200,
}
public class StatModifier
{
    public readonly float value;
    public readonly StatModType type;
    public readonly int order;
    public readonly object source;

    public StatModifier(float value, StatModType type, int order, object source)
    {
        this.value = value;
        this.type = type;
        this.order = order;
        this.source = source;
    }

    public StatModifier(float value, StatModType type) : this (value, type, (int)type, null) { }
    public StatModifier(float value, StatModType type, int order) : this (value, type, order, null) { }
    public StatModifier(float value, StatModType type, object source) : this (value, type, (int)type, source) { }
}
