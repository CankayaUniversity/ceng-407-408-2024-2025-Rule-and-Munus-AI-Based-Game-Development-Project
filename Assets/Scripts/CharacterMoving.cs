using System.Collections;
using UnityEngine;

public class CharacterMoving : MonoBehaviour, ICharacterMover
{
    [SerializeField] private GameObject targetObject; 
    [SerializeField] private float speed = 5f; 
    private Vector3 targetPos; 
    private Vector3 startingPos; 
    private bool isAttacking = false; 
    private bool isComeBack = false;
    private AnimatorController animatorController;

    void Start()
    {
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
        //Attacking();
    }

    public void Attacking()
    {
        animatorController.SetRunning(false);
        animatorController.SetAttacking(true);
        LookAt(targetPos); // targetPos kullanıyoruz
    }

    public void MoveTo(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
        animatorController.SetRunning(true);
        animatorController.SetIdle(false);
        
        // Karakterin yönünü güncelle
        LookAt(targetPosition);
    }

    public void MoveBack(Vector3 startingPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, startingPosition, Time.deltaTime * speed);
        animatorController.SetAttacking(false);
        animatorController.SetBackwarding(true);
        
        // Karakterin yönünü güncelle
        LookAt(targetPos); // startingPosition kullanıyoruz
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
        // Karakterin yönünü güncelleme
        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero) // Sıfır vektör kontrolü
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
        }
    }
}
