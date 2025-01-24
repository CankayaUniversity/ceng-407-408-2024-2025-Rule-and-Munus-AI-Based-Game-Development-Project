using System;
using UnityEngine;

public class AnimatorController
{
    private Animator animator;

    public bool isTurn;

    public AnimatorController(Animator animator)
    {
        this.animator = animator;
        isTurn = false;
    }

    public void SetIdle(bool isIdle)
    {
        animator.SetBool("isIdle", isIdle);
        isTurn = false;
    }

    public void SetRunning(bool isRunning)
    {
        animator.SetBool("isRunning", isRunning);
        isTurn = false;
    }

    public void SetAttacking(bool isAttacking)
    {
        animator.SetBool("isAttacking", isAttacking);
        isTurn = false;

    }

    public void SetBackwarding(bool isBackwarding)
    {
        animator.SetBool("isBackwarding", isBackwarding);
        isTurn = false;
        
    }

    public void StepForward(bool isStepForward)
    {
        animator.SetBool("isStepForward",isStepForward);
        isTurn = false;
    }

    public void StepBackward(bool isStepBackward)
    {
        animator.SetBool("isStepBackward", isStepBackward);
        isTurn = false;
    }

    public bool IsIdle()
    {
        isTurn = true;
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Idle");
    }
    
}
