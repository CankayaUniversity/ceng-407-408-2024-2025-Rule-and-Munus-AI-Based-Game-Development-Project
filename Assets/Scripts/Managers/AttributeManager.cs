using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class AttributeManager : MonoBehaviour
{
    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;
    public TMP_Text text4;
    public TMP_Text text5;

    public int text1Value = 0;
    public int text2Value = 0;
    public int text3Value = 0;
    public int text4Value = 0;
    public int text5Value = 0;
    
    public void ButtonLeft1()
    {
        text1Value--;
        if(text1Value < 0)
        {
            text1Value = 0;
        }
        text1.text = text1Value.ToString();

    }

    public void ButtonRight1()
    {
        Debug.Log("ButtonRight1");
        text1Value++;
        text1.text = text1Value.ToString();
    }

    public void ButtonLeft2()
    {
        text2Value--;
        if (text2Value < 0)
        {
            text2Value = 0;
        }
        text2.text = text2Value.ToString();

    }

    public void ButtonRight2()
    {
        text2Value++;
        text2.text = text2Value.ToString();
    }   

    public void ButtonLeft3()
    {
        text3Value--;
        if (text3Value < 0)
        {
            text3Value = 0;
        }
        text3.text = text3Value.ToString();
    }

    public void ButtonRight3()
    {
        text3Value++;
        text3.text = text3Value.ToString();
    }

    public void ButtonLeft4()
    {
        text4Value--;
        if (text4Value < 0)
        {
            text4Value = 0;
        }
        text4.text = text4Value.ToString();
    }

    public void ButtonRight4()
    {
        text4Value++;
        text4.text = text4Value.ToString();
    }

    public void ButtonLeft5()
    {
        text5Value--;
        if(text5Value < 0)
        {
            text5Value = 0;
        }
        text5.text = text5Value.ToString();
    }
    public void ButtonRight5()
    {
        text5Value++;
        text5.text = text5Value.ToString();
    }




}
