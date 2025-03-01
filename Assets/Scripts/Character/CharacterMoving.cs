using System.Collections;
using UnityEngine;

public class CharacterMoving : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private float speed = 5f;
    //public AIMoving aiMoving;
    private Vector3 targetPos;
    private Vector3 startingPos;
    private bool isAttacking = false;
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

        if (isAttacking)
        {
            targetPos = new Vector3(targetObject.transform.position.x, transform.position.y, transform.position.z);
            MoveTo(targetPos);
            if (Vector3.Distance(transform.position, targetPos) < 1.2f)
            {
                isAttacking = false;
                StartCoroutine(ComeBack());
            }
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



    public void Attack()
    {

        isAttacking = true;

    }

    public void Attacking()
    {
        animatorController.SetRunning(false);
        animatorController.SetAttacking(true);
        LookAt(targetPos); // targetPos kullan�yoruz
    }

    public void ForwardStep()
    {
        animatorController.StepForward(true);
        //gameObject.transform.position = new Vector3(gameObject.transform.position.x + 1f, gameObject.transform.position.y, gameObject.transform.position.z);
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

        // Karakterin y�n�n� g�ncelle
        LookAt(targetPosition);
    }

    public void MoveBack(Vector3 startingPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, startingPosition, Time.deltaTime * speed);
        animatorController.SetAttacking(false);
        animatorController.SetBackwarding(true);

        // Karakterin y�n�n� g�ncelle
        LookAt(targetPos); // startingPosition kullan�yoruz
    }

    public void Returned()
    {
        animatorController.SetBackwarding(false);
        animatorController.SetIdle(true);

    }

    private IEnumerator ComeBack()
    {
        Attacking();
        yield return new WaitForSeconds(1f);
        isComeBack = true;
    }

    private void LookAt(Vector3 targetPosition)
    {
        // Karakterin y�n�n� g�ncelleme
        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero) // S�f�r vekt�r kontrol�
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