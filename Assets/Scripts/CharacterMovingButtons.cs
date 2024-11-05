using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovingButtons : MonoBehaviour
{
    //[Serializefield] private CharacterMoving characterMoving;
    [SerializeField] private CharacterMoving characterMoving;


    public void AttackButton()
    {
        characterMoving.Attack();

    }
}
