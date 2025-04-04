using System.Collections.Generic;
using UnityEngine;
using Odds;

public class Dice
{
    public int value = 1;
    private List<int> _1d6 = odds._1d6;

    public Dice()
    {
        value = 1;
        _1d6 = odds._1d6;
    }
    public int RollaDice()
    {
        value = _1d6[Random.Range( 0, _1d6.Count - 1)];
        return value;
    }
    public int RollaDice(Stat LUCK)
    {
        List<int> temp_posb = _1d6;
        if(LUCK.value > 3 && LUCK.value < 5)
        {
            temp_posb.Add(5);
            temp_posb.Add(4);
            temp_posb.Add(4);
            temp_posb.Add(3);
        }
        else if(LUCK.value >= 5 && LUCK.value < 10)
        {
            temp_posb.Add(6);
            temp_posb.Add(5);
            temp_posb.Add(4);
            temp_posb.Add(3);
        }
        else if(LUCK.value >= 10 && LUCK.value < 15)
        {
            temp_posb.Add(6);
            temp_posb.Add(5);
            temp_posb.Add(4);
            temp_posb.Add(4);
        }
        else if(LUCK.value >= 15)
        {
            temp_posb.Add(6);
            temp_posb.Add(6);
            temp_posb.Add(5);
            temp_posb.Add(5);
        }
        value = temp_posb[Random.Range( 0, _1d6.Count - 1)];
        return value;
    }
}
