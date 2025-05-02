using UnityEngine;

public class EnemyHitController : MonoBehaviour
{
    public EnemyHealthController enemyhealthController;
    public ActionIndexController actionIndexController;
    //public void OnTriggerEnter(Collider other) {
    //    if(other.tag == "Arrow") 
    //    {
    //        Destroy(other.gameObject);
    //        Debug.Log("Hit by enemy!");
    //        actionIndexController.IndexController();
    //        //healthController.DecreaseHealth(10);
    //    }
    //    else if(other.tag == "Sword")
    //    {
    //        Debug.Log("Hit by enemy!");
    //        actionIndexController.IndexController();
    //        //healthController.DecreaseHealth(10);
    //    }
    //}
}
