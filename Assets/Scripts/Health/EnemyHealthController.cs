using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int currentHealth;
    public readonly int maxHealth = 100;
    public readonly int minHealth = 0;

    private void Awake() {
        currentHealth = maxHealth;
    }


    public void IncreaseHealth(int value)
    {
        currentHealth += value;
        MaxHealthController();

    }

    public void DecreaseHealth(int value)
    {
        currentHealth -= value;
        MinHealthController();

    }

    private void MaxHealthController()
    {
        if(currentHealth>maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void MinHealthController()
    {
        if(currentHealth<minHealth)
        {
            currentHealth = minHealth;
        }
    }
}
