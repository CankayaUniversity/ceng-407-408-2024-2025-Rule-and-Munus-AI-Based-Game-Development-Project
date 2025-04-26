
using UnityEngine;
public interface ICharacterCombat
{
    void Attack0();
    void Attack1();
    void Attack2();
    void Attack3();
    void ArrowAttack1();
    void ArrowAttack2();
    void ArrowAttack3();
    void Defence1();
    void Defence2();
    void Defence3();
    void ForwardStep();
    void BackwardStep();
}

// CharacterState.cs
public enum CharacterState
{
    Idle,
    MovingToTarget,
    Attacking,
    Defending,
    Stepping,

    Dead,
    ReturningToStart
}



public interface ICharacterMover
{
    void MoveTo(Vector3 targetPosition);
    void MoveBack(Vector3 startingPosition);
    void LookAt(Vector3 targetPosition);
}