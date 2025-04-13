using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

[Serializable]
public class Stat
{
    public float baseValue = 1.0f;
    public virtual float value { 
        get {
            if(flag || baseValue != finalBaseValue) {
                finalBaseValue = baseValue;
                _value = CalculateValue();
                flag = false;
            }
            return _value;
        } 
        }
    protected bool flag = true;
    protected float _value = 0.0f; 
    protected float finalBaseValue = float.MinValue;
    protected readonly List<StatModifier> statModifiers;
    public readonly ReadOnlyCollection<StatModifier> StatModifiers;
    public Stat()
    {
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly();
    }
    public Stat(float bValue) : this()
    {
        baseValue = bValue;
    }
    public virtual void AddModifier(StatModifier mod)
    {
        flag = true;
        statModifiers.Add(mod);
        statModifiers.Sort();
    }
    public virtual void IncreaseBase()
    {
        flag = true;
        baseValue += 1;
    }
    public void DecreaseBase()
    {
        flag = true;
        if(baseValue>0) { baseValue -= 1; }
        else { baseValue = 0; }
    }
    protected virtual int CompareOrder(StatModifier first, StatModifier second)
    {
        if(first.order < second.order)
        {
            return -1;
        }
        else if(first.order > second.order)
        {
            return 1;
        }
        return 0;
    }
    public virtual bool RemoveModifier(StatModifier mod)
    {
        if(statModifiers.Remove(mod))
        {
            flag = true;
            return true;
        }
        return false;
    }
    public virtual bool RemoveAllModifier(object source)
    {
        bool didRemove = false;
        for(int i = statModifiers.Count -1; i >= 0; --i)
        {
            if(statModifiers[i].source == source)
            {
                flag = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }
    protected virtual float CalculateValue()
    {
        float sum = baseValue;
        for(int i = 0; i < statModifiers.Count; ++i)
        {
            StatModifier mod = statModifiers[i];
            if(mod.type == StatModType.Flat)
            {
                sum += mod.value;
            }
            else if(mod.type == StatModType.Percent)
            {
                sum *= 1 + mod.value;
            }
        }  
        return (float)Math.Round(sum, 4);
    }
}
