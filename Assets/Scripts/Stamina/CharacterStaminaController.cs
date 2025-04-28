using UnityEngine;
using UnityEngine.UI;

public class CharacterStaminaController : MonoBehaviour
{
    public int currentStamina;
    public readonly int maxStamina = 80;
    public readonly int minStamina = 0;

    public Slider staminaBar; 

    private void Awake() {
        currentStamina = maxStamina;
        StaminaBar(currentStamina);
    }


    public void IncreaseStamina(int value)
    {
        currentStamina += value;
        MaxStaminaController();
        StaminaBar(currentStamina);

    }

    public void DecreaseStamina(int value)
    {
        currentStamina -= value;
        MinStaminaController();
        StaminaBar(currentStamina);

    }

    private void MaxStaminaController()
    {
        if(currentStamina>maxStamina)
        {
            currentStamina = maxStamina;
        }
    }

    private void MinStaminaController()
    {
        if(currentStamina<minStamina)
        {
            currentStamina = minStamina;
        }
    }

    private void StaminaBar(int value)
    {
        staminaBar.value = value;
    }
}
