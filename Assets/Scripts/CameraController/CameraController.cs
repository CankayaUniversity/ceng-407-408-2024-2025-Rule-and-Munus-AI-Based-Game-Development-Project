using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Animator animator;
    private bool isMainCamera;

    private void Awake()
    {
        isMainCamera = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isMainCamera)
            {
                animator.Play("MainCamera_");
                isMainCamera = false;
            }
            else
            {
                animator.Play("PlayerCamera_");
                isMainCamera = true;
            }
        }
    }
}
