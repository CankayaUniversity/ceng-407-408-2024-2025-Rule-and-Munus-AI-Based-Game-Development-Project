using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovingButtons : MonoBehaviour
{
    [SerializeField] private CharacterMoving characterMoving;
    [SerializeField] private List<Button> attackButtons;
    [SerializeField] private List<Button> defenceButtons;
    
    private int attackIndex;
    private int defenceIndex;
    private bool isSelectedAttack;
    private bool isSelectedDefence;
    
    public bool isTurn => isSelectedAttack && isSelectedDefence;

    void Start()
    {
        if (characterMoving == null)
        {
            Debug.LogError("CharacterMoving reference not set in CharacterMovingButtons!");
            return;
        }
        
        ResetButtonIndex();
        SetupButtonListeners();
        
        // Register for character events
        characterMoving.OnAttackComplete += OnAttackComplete;
        characterMoving.OnDefenceComplete += OnDefenceComplete;
    }

    private void OnDestroy()
    {
        // Clean up event subscriptions
        if (characterMoving != null)
        {
            characterMoving.OnAttackComplete -= OnAttackComplete;
            characterMoving.OnDefenceComplete -= OnDefenceComplete;
        }
    }

    private void SetupButtonListeners()
    {
        // Set up attack button listeners
        SetupButtonGroup(attackButtons, (index) => {
            attackIndex = index;
            isSelectedAttack = true;
            Debug.Log($"Attack button {index} selected");
        });
        
        // Set up defence button listeners
        SetupButtonGroup(defenceButtons, (index) => {
            defenceIndex = index;
            isSelectedDefence = true;
            Debug.Log($"Defence button {index} selected");
        });
    }
    
    private void SetupButtonGroup(List<Button> buttons, System.Action<int> onClick)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i;
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(() => onClick(index));
        }
    }

    private void OnAttackComplete()
    {
        // Handle attack completion logic
        Debug.Log("Attack action completed");
    }
    
    private void OnDefenceComplete()
    {
        // Handle defence completion logic
        Debug.Log("Defence completed");
    }

    public void TurnButton()
    {
        if (!isTurn) return;
        
        ExecuteAttack();
        StartCoroutine(ExecuteDefenceAfterDelay(4f));
        ResetButtonIndex();
    }

    private void ExecuteAttack()
    {
        switch (attackIndex)
        {
            case 0:
                characterMoving.Attack0();
                break;
            case 1:
                characterMoving.Attack1();
                break;
            case 2:
                characterMoving.Attack2();
                break;
            case 3:
                characterMoving.Attack3();
                break;
            case 4:
                characterMoving.ForwardStep();
                break;
            case 5:
                characterMoving.BackwardStep();
                break;
        }
    }

    private void ExecuteDefence()
    {
        switch (defenceIndex)
        {
            case 0:
                characterMoving.Defence1();
                break;
            case 1:
                characterMoving.Defence2();
                break;
            case 2:
                characterMoving.Defence3();
                break;
        }
    }

    private IEnumerator ExecuteDefenceAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ExecuteDefence();
        Debug.Log("Defence executed after delay");
    }

    public void ResetButtonIndex()
    {
        isSelectedAttack = false;
        isSelectedDefence = false;
    }

    public void ToggleAttackSelection()
    {
        isSelectedAttack = !isSelectedAttack;
    }

    public void ToggleDefenceSelection()
    {
        isSelectedDefence = !isSelectedDefence;
    }

    // Updated step button handlers - now they properly set the attack selection
    public void OnForwardStepButtonClicked()
    {
        isSelectedAttack = true; // Mark attack as selected since step counts as an attack
        characterMoving.ForwardStep();
    }
    
    public void OnBackwardStepButtonClicked()
    {
        isSelectedAttack = true; // Mark attack as selected since step counts as an attack
        characterMoving.BackwardStep();
    }
}