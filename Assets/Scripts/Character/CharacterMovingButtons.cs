using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovingButtons : MonoBehaviour
{

    [SerializeField] private CharacterMoving characterMoving;
    public bool isTurn;

    void Start()
    {
        isTurn = true;
    }


    public void AttackButton0()
    {
        characterMoving.Attack0();
        StartCoroutine(WaitAndResetCounter());

    }

    public void AttackButton1()
    {
        characterMoving.Attack1();
        StartCoroutine(WaitAndResetCounter());
    }

    public void AttackButton2()
    {
        characterMoving.Attack2();
        StartCoroutine(WaitAndResetCounter());
    }

    public void AttackButton3()
    {
        characterMoving.Attack3();
        StartCoroutine(WaitAndResetCounter());
    }
    public void DefenceButton1()
    {
        characterMoving.Defence1();
        StartCoroutine(WaitAndResetCounter());
    }

    public void DefenceButton2()
    {
        characterMoving.Defence2();
        StartCoroutine(WaitAndResetCounter());
    }

    public void DefenceButton3()
    {
        characterMoving.Defence3();
        StartCoroutine(WaitAndResetCounter());
    }

    public void forwardStepButton()
    {
        characterMoving.ForwardStep();
        StartCoroutine(WaitAndResetCounter());
    }

    public void backwardStepButton()
    {
        characterMoving.BackwardStep();
        StartCoroutine(WaitAndResetCounter());
    }
    private IEnumerator WaitAndResetCounter()
    {
        isTurn = true;
        yield return new WaitForSeconds(2f);
        isTurn = false;


    }
}