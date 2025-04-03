using System.Collections.Generic;
using UnityEngine;

public class Dice
{
    public int value;
    private List<int> possibilities;

    public Dice()
    {
        value = 1;
        possibilities = new List<int>();
        for(int i = 1; i < 7; ++i)
        {
            possibilities.Add(i);
        }
    }
    public int RollaDice()
    {
        value = possibilities[Random.Range( 0, possibilities.Count - 1)];
        return value;
    }
    public int RollaDice(Stat LUCK)
    {
        List<int> temp_posb = possibilities;
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
        value = temp_posb[Random.Range( 0, possibilities.Count - 1)];
        return value;
    }
}
