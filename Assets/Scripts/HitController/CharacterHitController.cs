using UnityEngine;

public class CharacterHitController : MonoBehaviour
{
    public CharacterHealthController characterhealthController;
    public ActionIndexController actionIndexController;
    public void OnTriggerEnter(Collider other) {
        if(other.tag == "Enemy") 
        {
            Debug.Log("Hit by player!");
            actionIndexController.IndexController();
            //healthController.DecreaseHealth(10);
        }
    }
}
