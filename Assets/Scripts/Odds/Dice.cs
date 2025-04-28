using System.Collections.Generic;
using UnityEngine;
using Odds;

public static class Dice
{
    public static int value = 1;
    private static List<int> _1d6 = odds._1d6;
    public static int RollaDice()
    {
        value = _1d6[Random.Range( 0, _1d6.Count - 1)];
        return value;
    }
    public static int RollaDice(Stat luck)
    {
        List<int> temp_posb = _1d6;
        if(luck.value > 3 && luck.value < 5)
        {
            temp_posb.Add(5);
            temp_posb.Add(4);
            temp_posb.Add(4);
            temp_posb.Add(3);
        }
        else if(luck.value >= 5 && luck.value < 10)
        {
            temp_posb.Add(6);
            temp_posb.Add(5);
            temp_posb.Add(4);
            temp_posb.Add(3);
        }
        else if(luck.value >= 10 && luck.value < 15)
        {
            temp_posb.Add(6);
            temp_posb.Add(5);
            temp_posb.Add(4);
            temp_posb.Add(4);
        }
        else if(luck.value >= 15)
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
