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


    public void AttackButton()
    {
        characterMoving.Attack();
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