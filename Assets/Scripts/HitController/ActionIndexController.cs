using UnityEngine;

public class ActionIndexController : MonoBehaviour
{
    public CharacterMovingButtons characterMovingButton;
    public CharacterMoving characterMoving;
    public Attributes enemy;
    public Attributes character;
    


    public void IndexController()
    {
        Debug.Log("IndexController called!");
        if(characterMoving.currentState.ToString()=="Attacking")
        {
            Debug.Log("Character is Attacking!/IndexController");
            /*if(characterMovingButton.attackIndex == enemyMovingButton.defenceIndex)
             {
            Debug.Log("Attack index is equal to defence index!");
            CorrectDefenceAction();
             }*/

        }
        else if(characterMoving.currentState.ToString()=="Defending")
        {
            Debug.Log("Character is Defending!/IndexController");
            /*if(characterMovingButton.defenceIndex == enemyMovingButton.attackIndex)
            {
                Debug.Log("Defence index is equal to attack index!");
                CorrectDefenceAction();
            }*/
        }
        else{
            Debug.Log("Character is not in a valid state!/IndexController");
        }
        
        
    }

    public void CorrectDefenceAction(int value)
    {
        value = value - value*3/4;
        enemy.UpdateHealth(value);
    }
}
