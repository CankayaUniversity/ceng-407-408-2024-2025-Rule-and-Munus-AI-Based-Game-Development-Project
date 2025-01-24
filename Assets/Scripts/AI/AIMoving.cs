using System.Collections;
using UnityEngine;

public class AIMoving : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    public CharacterMovingButtons characterMoving;

    private Vector3 targetPos;
    private Vector3 startingPos;
    private bool isAttacking = false;
    private bool isComeBack = false;

    


    void Start()
    {
        characterMoving.isTurn = true;
    }


    void Update()
    {
        if(characterMoving.isTurn==false)
        {
            StartCoroutine(PerformRandomAction());
        }
        
    }

    public IEnumerator PerformRandomAction()
    {
        int randomAction = Random.Range(0, 3);
        switch (randomAction)
        {
            case 0:
                animator.SetBool("isAttacking", true);
                StartCoroutine(ResetTrigger("isAttacking"));
                break;

            case 1:
                animator.SetBool("isStepForward", true);
                StartCoroutine(ResetTrigger("isStepForward"));
                break;

            case 2:
                animator.SetBool("isStepBackward", true);
                StartCoroutine(ResetTrigger("isStepBackward"));
                break;
        }
        yield return new WaitForSeconds(1f); // AI hareket süresi
        characterMoving.isTurn = true; // AI sırasını tamamlar
    }

    private IEnumerator ResetTrigger(string triggerName)
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool(triggerName, false);
        characterMoving.isTurn = true;
    }
}
