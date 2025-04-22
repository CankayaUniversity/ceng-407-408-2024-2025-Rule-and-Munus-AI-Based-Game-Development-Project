using UnityEngine;

public class EnemyHitController : MonoBehaviour
{
    public EnemyHealthController enemyhealthController;
    public ActionIndexController actionIndexController;
    public void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") 
        {
            Debug.Log("Hit by enemy!");
            actionIndexController.IndexController();
            //healthController.DecreaseHealth(10);
        }
    }
}
