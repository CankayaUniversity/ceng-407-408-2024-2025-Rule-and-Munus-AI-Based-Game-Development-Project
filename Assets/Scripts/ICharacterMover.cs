using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterMover
{
    void MoveTo(Vector3 targetPosition);
    void MoveBack(Vector3 startingPosition);

    void Attacking();
}

