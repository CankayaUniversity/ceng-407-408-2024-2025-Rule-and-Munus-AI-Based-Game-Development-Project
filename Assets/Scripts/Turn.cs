using UnityEngine;

public class Turn : MonoBehaviour
{

    private CharacterMovingButtons characterMovingButtons;
    private EnemyAI enemyAI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterMovingButtons=GetComponent<CharacterMovingButtons>();
        enemyAI=GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(characterMovingButtons.isTurn==true){
            
        }
        
    }
}
