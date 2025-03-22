using System.Collections;
using UnityEngine;

public class CharacterMoving : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private float speed = 5f;
    //public AIMoving aiMoving;
    private Vector3 targetPos;
    private Vector3 startingPos;
    private bool isAttacking0 = false;
    private bool isAttacking1 = false;
    private bool isAttacking2 = false;
    private bool isAttacking3 = false;
    private bool isComeBack = false;
    private AnimatorController animatorController;

    public bool isTurn;




    void Start()
    {
        isTurn = true;
        targetPos = new Vector3(targetObject.transform.position.x, transform.position.y, transform.position.z);
        startingPos = transform.position;
        animatorController = new AnimatorController(GetComponent<Animator>());
    }

    void FixedUpdate()
    {

        if (isAttacking0)
        {
            targetPos = new Vector3(targetObject.transform.position.x, transform.position.y, transform.position.z);
            MoveTo(targetPos);
            if (Vector3.Distance(transform.position, targetPos) < 13.2f)
            {
                isAttacking0 = false;
                StartCoroutine(ComeBack());
            }
        }
        else if (isAttacking1)
        {
            Attacking1();
            isAttacking1 = false;

        }
        else if(isAttacking2)
        {
            Attacking2();
            isAttacking2 = false;
            

        }
        else if(isAttacking3)
        {
            Attacking3();
            isAttacking3 = false;
            
        }

        if (isComeBack)
        {
            MoveBack(startingPos);
            if (Vector3.Distance(transform.position, startingPos) < 0.1f)
            {
                isComeBack = false;
                Returned();

            }
        }

    }



    public void Attack0()
    {

        isAttacking0 = true;

    }

    public void Attack1()
    {
        isAttacking1 = true;
    }
    public void Attack2()
    {
        isAttacking2 = true;
    }

    public void Attack3()
    {
        isAttacking3 = true;
    }   

    public void Attacking0()
    {
        animatorController.SetRunning(false);
        animatorController.SetAttacking0(true);
        LookAt(targetPos); 
    }

    public void Attacking1()
    {
        animatorController.SetAttacking1(true);
        LookAt(targetPos);
        
    }

    public void Attacking2()
    {
        animatorController.SetAttacking2(true);
        LookAt(targetPos);
        
    }
   

    public void Attacking3()
    {
        animatorController.SetAttacking3(true);
        LookAt(targetPos);
        
    }
    
    public void Defence1()
    {
        animatorController.SetDefence1(true);
        StartCoroutine(ResetDefence());
        LookAt(targetPos);
    }

    public void Defence2()
    {
        animatorController.SetDefence2(true);
        StartCoroutine(ResetDefence2());
        LookAt(targetPos);
    }

    public void Defence3()
    {
        animatorController.SetDefence3(true);
        StartCoroutine(ResetDefence3());
        LookAt(targetPos);
    }
    public void ForwardStep()
    {
        animatorController.StepForward(true);
        StartCoroutine(ResetStepForward());
    }

    private IEnumerator ResetStepForward()
    {
        yield return new WaitForSeconds(0.6f);
        animatorController.StepForward(false);

    }

    public void BackwardStep()
    {
        animatorController.StepBackward(true);
        StartCoroutine(ResetStepBack());
    }

    private IEnumerator ResetDefence()
    {
        yield return new WaitForSeconds(1f);
        animatorController.SetDefence1(false);
    }

    private IEnumerator ResetDefence2()
    {
        yield return new WaitForSeconds(.5f);
        animatorController.SetDefence2(false);
    }

    private IEnumerator ResetDefence3()
    {
        yield return new WaitForSeconds(1f);
        animatorController.SetDefence3(false);
    }

    private IEnumerator ResetStepBack()
    {
        yield return new WaitForSeconds(0.4f);
        animatorController.StepBackward(false);

    }

    public void MoveTo(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
        animatorController.SetRunning(true);
        animatorController.SetIdle(false);

       
        LookAt(targetPosition);
    }

    public void MoveBack(Vector3 startingPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, startingPosition, Time.deltaTime * speed);
        animatorController.SetAttacking0(false);
        animatorController.SetBackwarding(true);

        
        LookAt(targetPos); 
    }

    public void Returned()
    {
        animatorController.SetBackwarding(false);
        animatorController.SetIdle(true);

    }

    private IEnumerator ComeBack()
    {
        Attacking0();
        yield return new WaitForSeconds(1f);
        isComeBack = true;
    }

    private void LookAt(Vector3 targetPosition)
    {
       
        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero) 
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
        }
    }



    //private IEnumerator AIMove()
    //{
    //    // Perform AI's random action
    //    //yield return StartCoroutine(aiMoving.PerformRandomAction());
    //    //currentTurn = Turn.Player;
    //}


}