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

    private void HealthBar(int value)
    {
        healthBar.value = value;
    }
}
