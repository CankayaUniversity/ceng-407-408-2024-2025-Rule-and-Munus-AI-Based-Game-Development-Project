using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class EnemyHealthController : MonoBehaviour
{
    public int currentHealth;
    public readonly int maxHealth = 100;
    public readonly int minHealth = 0;
    public Slider healthBar; 
    
    public bool isDead = false;
    

    private void Awake() {
        currentHealth = maxHealth;
        HealthBar(currentHealth);
        isDead = false;
    }


    public void IncreaseHealth(int value)
    {
        currentHealth += value;
        MaxHealthController();
        HealthBar(currentHealth);

    }

    public void DecreaseHealth(int value)
    {
        currentHealth -= value;
        MinHealthController();
        HealthBar(currentHealth);

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
            isDead = true;
            Debug.Log("Character is dead!");
        }
       
    }

    private void HealthBar(int value)
    {
        healthBar.value = value;
    }
}
