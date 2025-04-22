
using UnityEngine;
public interface IAnimatorController
{
    void SetIdle(bool value);
    void SetRunning(bool value);
    void SetBackwarding(bool value);
    void SetAttacking0(bool value);
    void SetAttacking1();
    void SetAttacking2();
    void SetAttacking3();
    void StepForward();
    void StepBackward();
    void SetDefence1(bool value);
    void SetDefence2(bool value);
    void SetDefence3(bool value);
    bool IsIdle();
}
  

public class AnimatorController : IAnimatorController
{
    private readonly Animator animator;
    private readonly int idleHash;
    private readonly int runningHash;
    private readonly int backwardingHash;
    private readonly int attackingHash;
    private readonly int attacking1Hash;
    private readonly int attacking2Hash;
    private readonly int attacking3Hash;
    private readonly int stepForwardHash;
    private readonly int stepBackwardHash;
    private readonly int defenceHash;
    private readonly int defence2Hash;
    private readonly int defence3Hash;

    public AnimatorController(Animator animator)
    {
        this.animator = animator;
        
        idleHash = Animator.StringToHash("isIdle");
        runningHash = Animator.StringToHash("isRunning");
        backwardingHash = Animator.StringToHash("isBackwarding");
        attackingHash = Animator.StringToHash("isAttacking");
        attacking1Hash = Animator.StringToHash("isAttacking1");
        attacking2Hash = Animator.StringToHash("isAttacking2");
        attacking3Hash = Animator.StringToHash("isAttacking3");
        stepForwardHash = Animator.StringToHash("isStepForward");
        stepBackwardHash = Animator.StringToHash("isStepBackward");
        defenceHash = Animator.StringToHash("isDefence");
        defence2Hash = Animator.StringToHash("isDefence2");
        defence3Hash = Animator.StringToHash("isDefence3");
    }

    public void SetIdle(bool value) => animator.SetBool(idleHash, value);
    public void SetRunning(bool value) => animator.SetBool(runningHash, value);
    public void SetBackwarding(bool value) => animator.SetBool(backwardingHash, value);
    public void SetAttacking0(bool value) => animator.SetBool(attackingHash, value);
    
    public void SetAttacking1() => animator.SetTrigger(attacking1Hash);
    public void SetAttacking2() => animator.SetTrigger(attacking2Hash);
    public void SetAttacking3() => animator.SetTrigger(attacking3Hash);
    
    public void StepForward() => animator.SetTrigger(stepForwardHash);
    public void StepBackward() => animator.SetTrigger(stepBackwardHash);
    
    public void SetDefence1(bool value) => animator.SetBool(defenceHash, value);
    public void SetDefence2(bool value) => animator.SetBool(defence2Hash, value);
    public void SetDefence3(bool value) => animator.SetBool(defence3Hash, value);
    
    public bool IsIdle() => animator.GetCurrentAnimatorStateInfo(0).IsName("Idle");
}