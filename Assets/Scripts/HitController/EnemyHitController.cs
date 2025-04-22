using UnityEngine;

public class EnemyHitController : MonoBehaviour
{
    public HealthController healthController;
    public void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") 
        {
            Debug.Log("Hit by enemy!");
            actionIndexController.IndexController();
            //healthController.DecreaseHealth(10);
        }
    }
}
