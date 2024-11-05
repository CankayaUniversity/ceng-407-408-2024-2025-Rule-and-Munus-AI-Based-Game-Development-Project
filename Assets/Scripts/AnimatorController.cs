using UnityEngine;

public class AnimatorController
{
    private Animator animator;

    public AnimatorController(Animator animator)
    {
        this.animator = animator;
    }

    public void SetIdle(bool isIdle)
    {
        animator.SetBool("isIdle", isIdle);
    }

    public void SetRunning(bool isRunning)
    {
        animator.SetBool("isRunning", isRunning);
    }

    public void SetAttacking(bool isAttacking)
    {
        animator.SetBool("isAttacking", isAttacking);

    }

    public void SetBackwarding(bool isBackwarding)
    {
        animator.SetBool("isBackwarding", isBackwarding);
        
    }
}
