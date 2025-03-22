using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovingButtons : MonoBehaviour
{

    [SerializeField] private CharacterMoving characterMoving;
    public bool isTurn;
    public bool isSelectedAttack;
    public bool isSelectedDefence;
    public List<Button> attackButtons;
    public List<Button> defenceButtons;
    private int attackIndex;
    private int defenceIndex;


    void Start()
    {
        isTurn = true;
        isSelectedAttack = false;
        isSelectedDefence = false;
    }

    private void Update()
    {
        if(isSelectedAttack && isSelectedDefence)
        {
            isTurn= true;
        }
        else
        {
            isTurn = false;
        }
    }

    public void ButtonAttackIndex()
    {
        for (int i = 0; i < attackButtons.Count; i++)
        {
            int index = i;
            attackButtons[i].onClick.RemoveAllListeners(); 
            attackButtons[i].onClick.AddListener(() =>
            {
                attackIndex = index; 
                OnButtonClicked(index); 
            });
        }
    }

    public void ButtonDefenceIndex()
    {
        for(int i=0; i < defenceButtons.Count; i++)
        {
            int index = i;
            defenceButtons[i].onClick.RemoveAllListeners();
            defenceButtons[i].onClick.AddListener(() =>
            {
                defenceIndex = index; 
                OnButtonClicked(index); 
            });
        }
    }

    public void TurnButton()
    {
        if (isTurn == true)
        {
            AttackAnimCase();
            StartCoroutine(WaitAndExecute());
            
        }
    }
    IEnumerator WaitAndExecute()
    {
        yield return new WaitForSeconds(4f); 
        DefenceAnimCase();
        Debug.Log("4 saniye sonra çalýþtý!");
    }

    public void AttackAnimCase()
    {
        switch (attackIndex)
        {
            case 0:
                characterMoving.Attack0();
                break;
            case 1:
                characterMoving.Attack1();
                break;
            case 2:
                characterMoving.Attack2();
                break;
            case 3:
                characterMoving.Attack3();
                break;
        }
    }

    public void DefenceAnimCase()
    {
        switch (defenceIndex)
        {
            case 0:
                characterMoving.Defence1();
                break;
            case 1:
                characterMoving.Defence2();
                break;
            case 2:
                characterMoving.Defence3();
                break;
            
        }
    }

    public void OnButtonClicked(int index)
    {
        Debug.Log("Týklanan butonun indexi: " + index);
    }

    public void isSelectedAttack_()
    {
        isSelectedAttack = !isSelectedAttack; 
    }

    public void isSelectedDefence_()
    {
        isSelectedDefence = !isSelectedDefence;
    }

    public void AttackButton0()
    {
        isSelectedAttack_();
        ButtonAttackIndex();
       

    }

    public void AttackButton1()
    {
        isSelectedAttack_();
        ButtonAttackIndex();
        
    }

    public void AttackButton2()
    {
        isSelectedAttack_();
        ButtonAttackIndex();
       
    }

    public void AttackButton3()
    {
        isSelectedAttack_();
        ButtonAttackIndex();
        
    }
    public void DefenceButton1()
    {
        isSelectedDefence_();
        ButtonDefenceIndex();
        
    }

    public void DefenceButton2()
    {
        isSelectedDefence_();
        ButtonDefenceIndex();
        
    }

    public void DefenceButton3()
    {
        isSelectedDefence_();
        ButtonDefenceIndex();
        
    }

    public void forwardStepButton()
    {
        isSelectedAttack_();
        characterMoving.ForwardStep();
        
    }

    public void backwardStepButton()
    {
        isSelectedAttack_();
        characterMoving.BackwardStep();
        
    }
    
}