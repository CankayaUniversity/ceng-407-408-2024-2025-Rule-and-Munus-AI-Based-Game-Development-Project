using System;
using System.Collections;
using UnityEngine;

public class CharacterMoving : MonoBehaviour, ICharacterMover, ICharacterCombat
{
    [Header("References")]
    [SerializeField] private GameObject targetObject;
    
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float attackDistance = 13.2f;
    
    [Header("Combat Timing")]
    [SerializeField] private float attackDuration = 1f;
    [SerializeField] private float defence1Duration = 1f;
    [SerializeField] private float defence2Duration = 0.5f;
    [SerializeField] private float defence3Duration = 1f;
    [SerializeField] private float stepForwardDuration = 0.6f;
    [SerializeField] private float stepBackwardDuration = 0.4f;

    private Vector3 targetPosition;
    private Vector3 startingPosition;
    private IAnimatorController animatorController;
    public CharacterState currentState = CharacterState.Idle;
    
    public event Action OnAttackComplete;
    public event Action OnDefenceComplete;
    public event Action OnMoveComplete;
    public event Action OnStepComplete;
    
    public bool isTurn { get; set; }

    // Dependency injection through Unity inspector
    private void Awake()
    {
        if (targetObject == null)
        {
            Debug.LogError("Target object missing on CharacterMoving!");
        }
    }

    void Start()
    {
        isTurn = true;
        startingPosition = transform.position;
        animatorController = new AnimatorController(GetComponent<Animator>());
    }

    #region ICharacterMover Implementation
    
    public void MoveTo(Vector3 targetPosition)
    {
        if (currentState != CharacterState.Idle && currentState != CharacterState.MovingToTarget)
            return;
            
        currentState = CharacterState.MovingToTarget;
        animatorController.SetIdle(false);
        animatorController.SetRunning(true);
        StartCoroutine(MoveToCoroutine(targetPosition));
    }

    private IEnumerator MoveToCoroutine(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
            LookAt(targetPosition);
            
            if (currentState == CharacterState.MovingToTarget && Vector3.Distance(transform.position, targetPosition) < attackDistance)
            {
                animatorController.SetRunning(false);
                animatorController.SetAttacking0(true);
                currentState = CharacterState.Attacking;
                yield return new WaitForSeconds(attackDuration);
                break;
            }
            
            yield return null;
        }
        
        OnMoveComplete?.Invoke();
    }

    public void MoveBack(Vector3 startingPosition)
    {
        if (currentState != CharacterState.Attacking)
            return;
            
        currentState = CharacterState.ReturningToStart;
        animatorController.SetAttacking0(false);
        animatorController.SetBackwarding(true);
        StartCoroutine(MoveBackCoroutine(startingPosition));
    }

    private IEnumerator MoveBackCoroutine(Vector3 startingPosition)
    {
        while (Vector3.Distance(transform.position, startingPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPosition, Time.deltaTime * speed);
            LookAt(targetPosition);
            yield return null;
        }
        
        animatorController.SetBackwarding(false);
        animatorController.SetIdle(true);
        currentState = CharacterState.Idle;
        OnMoveComplete?.Invoke();
    }
    
    public void LookAt(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
        }
    }
    
    #endregion

    #region ICharacterCombat Implementation
    
    public void Attack0()
    {
        if (currentState != CharacterState.Idle)
            return;
            
        targetPosition = new Vector3(targetObject.transform.position.x, transform.position.y, transform.position.z);
        MoveTo(targetPosition);
        StartCoroutine(CompleteAttackSequence());
    }

    private IEnumerator CompleteAttackSequence()
    {
        // Wait for attack animation to complete
        while (currentState != CharacterState.Attacking)
        {
            yield return null;
        }
        
        yield return new WaitForSeconds(attackDuration);
        MoveBack(startingPosition);
        
        // Wait for return to complete
        while (currentState != CharacterState.Idle)
        {
            yield return null;
        }
        
        OnAttackComplete?.Invoke();
    }

    public void Attack1()
    {
        if (currentState != CharacterState.Idle)
            return;
            
        currentState = CharacterState.Attacking;
        targetPosition = new Vector3(targetObject.transform.position.x, transform.position.y, transform.position.z);
        LookAt(targetPosition);
        animatorController.SetAttacking1();
        StartCoroutine(ResetState(attackDuration, OnAttackComplete));
    }

    public void Attack2()
    {
        if (currentState != CharacterState.Idle)
            return;
            
        currentState = CharacterState.Attacking;
        targetPosition = new Vector3(targetObject.transform.position.x, transform.position.y, transform.position.z);
        LookAt(targetPosition);
        animatorController.SetAttacking2();
        StartCoroutine(ResetState(attackDuration, OnAttackComplete));
    }

    public void Attack3()
    {
        if (currentState != CharacterState.Idle)
            return;
            
        currentState = CharacterState.Attacking;
        targetPosition = new Vector3(targetObject.transform.position.x, transform.position.y, transform.position.z);
        LookAt(targetPosition);
        animatorController.SetAttacking3();
        StartCoroutine(ResetState(attackDuration, OnAttackComplete));
    }

    public void Defence1()
    {
        if (currentState != CharacterState.Idle)
            return;
            
        currentState = CharacterState.Defending;
        targetPosition = new Vector3(targetObject.transform.position.x, transform.position.y, transform.position.z);
        LookAt(targetPosition);
        animatorController.SetDefence1(true);
        StartCoroutine(ResetDefence(animatorController.SetDefence1, defence1Duration, OnDefenceComplete));
    }

    public void Defence2()
    {
        if (currentState != CharacterState.Idle)
            return;
            
        currentState = CharacterState.Defending;
        targetPosition = new Vector3(targetObject.transform.position.x, transform.position.y, transform.position.z);
        LookAt(targetPosition);
        animatorController.SetDefence2(true);
        StartCoroutine(ResetDefence(animatorController.SetDefence2, defence2Duration, OnDefenceComplete));
    }

    public void Defence3()
    {
        if (currentState != CharacterState.Idle)
            return;
            
        currentState = CharacterState.Defending;
        targetPosition = new Vector3(targetObject.transform.position.x, transform.position.y, transform.position.z);
        LookAt(targetPosition);
        animatorController.SetDefence3(true);
        StartCoroutine(ResetDefence(animatorController.SetDefence3, defence3Duration, OnDefenceComplete));
    }

    public void ForwardStep()
    {
        if (currentState != CharacterState.Idle)
            return;
            
        // Treating forward step as an attack action
        currentState = CharacterState.Attacking;
        targetPosition = new Vector3(targetObject.transform.position.x, transform.position.y, transform.position.z);
        LookAt(targetPosition);
        animatorController.StepForward();
        StartCoroutine(ResetAttackingStep(stepForwardDuration));
    }

    public void BackwardStep()
    {
        if (currentState != CharacterState.Idle)
            return;
            
        // Treating backward step as an attack action
        currentState = CharacterState.Attacking;
        targetPosition = new Vector3(targetObject.transform.position.x, transform.position.y, transform.position.z);
        LookAt(targetPosition);
        animatorController.StepBackward();
        StartCoroutine(ResetAttackingStep(stepBackwardDuration));
    }
    
    #endregion

    #region Helper Coroutines
    
    private IEnumerator ResetState(float duration, Action onComplete = null)
    {
        yield return new WaitForSeconds(duration);
        currentState = CharacterState.Idle;
        onComplete?.Invoke();
    }

    private IEnumerator ResetDefence(Action<bool> resetAction, float duration, Action onComplete = null)
    {
        yield return new WaitForSeconds(duration);
        resetAction(false);
        currentState = CharacterState.Idle;
        onComplete?.Invoke();
    }
    
    // Updated to treat steps as attacking actions
    private IEnumerator ResetAttackingStep( float duration)
    {
        yield return new WaitForSeconds(duration);
       
        currentState = CharacterState.Idle;
        OnAttackComplete?.Invoke(); // Notify that an attack action is complete
    }
    
    #endregion
}

// Updated CharacterMovingButtons to handle steps as attack actions
