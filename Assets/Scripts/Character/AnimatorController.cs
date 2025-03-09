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

    public void SetAttacking0(bool isAttacking)
    {
        animator.SetBool("isAttacking", isAttacking);
        isTurn = false;

    }

    public void SetAttacking1(bool isAttacking)
    {
        //animator.SetBool("isAttacking2", isAttacking);
        animator.SetTrigger("isAttacking1");
        isTurn = false;
    }
    public void SetAttacking2(bool isAttacking)
    {
        //animator.SetBool("isAttacking1", isAttacking);
        animator.SetTrigger("isAttacking2");
        isTurn = false;
    }

    public void SetAttacking3(bool isAttacking)
    {
        //animator.SetBool("isAttacking3", isAttacking);
        animator.SetTrigger("isAttacking3");
        isTurn = false;
    }

    public void SetBackwarding(bool isBackwarding)
    {
        animator.SetBool("isBackwarding", isBackwarding);
        isTurn = false;

    }

    public void StepForward(bool isStepForward)
    {
        animator.SetBool("isStepForward", isStepForward);
        isTurn = false;
    }

    public void StepBackward(bool isStepBackward)
    {
        animator.SetBool("isStepBackward", isStepBackward);
        isTurn = false;
    }

    public void SetDefence1(bool isDefence)
    {
        animator.SetBool("isDefence", isDefence);
        isTurn = false;
    }

    public void SetDefence2(bool isDefence)
    {
        animator.SetBool("isDefence2", isDefence);
        isTurn = false;
    }

    public void SetDefence3(bool isDefence)
    {
        animator.SetBool("isDefence3", isDefence);
        isTurn = false;
    }

    public bool IsIdle()
    {
        isTurn = true;
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Idle");
    }

}