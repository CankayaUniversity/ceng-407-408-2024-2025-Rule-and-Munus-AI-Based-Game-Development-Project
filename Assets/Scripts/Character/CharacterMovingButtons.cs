using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovingButtons : MonoBehaviour
{
    [Header("Character Moving Reference")]
    [SerializeField] private CharacterMoving characterMoving;
    [SerializeField] private List<Button> attackButtons;
    [SerializeField] private List<Button> defenceButtons;
    [SerializeField] private List<Button> ArrowButtons;
    [SerializeField] public GameObject ChangeArrowButton;
    [SerializeField] private GameObject ChangeSwordButton;
    [SerializeField] private GameObject SwordPanel;
    [SerializeField] private GameObject ArrowPanel;



    [Header("Weapon Prefabs")]
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject sword;


    [Header("Button Indexes")]
    public int attackIndex;
    public int defenceIndex=-1;
    public int arrowIndex=-1;

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

        characterMoving.OnAttackComplete += OnAttackComplete;
        characterMoving.OnDefenceComplete += OnDefenceComplete;
    }

    public void ChangeWeaponButton()
    {
        if(ChangeArrowButton.activeSelf)
        {
            Debug.Log("Change Sword");
            ChangeArrowButton.SetActive(false);
            ChangeSwordButton.SetActive(true);
            SwordPanel.SetActive(false);
            ArrowPanel.SetActive(true);
            arrow.SetActive(true);
            sword.SetActive(false);
        }
        else
        {
            Debug.Log("Change Sword");
            ChangeArrowButton.SetActive(true);
            ChangeSwordButton.SetActive(false);
            SwordPanel.SetActive(true);
            ArrowPanel.SetActive(false);
            arrow.SetActive(false);
            sword.SetActive(true);
        }
    }
    private void OnDestroy()
    {
        if (characterMoving != null)
        {
            characterMoving.OnAttackComplete -= OnAttackComplete;
            characterMoving.OnDefenceComplete -= OnDefenceComplete;
        }
    }

    private void SetupButtonListeners()
    {
        SetupButtonGroup(attackButtons, (index) => {
            attackIndex = index;
            isSelectedAttack = true;
            Debug.Log($"Attack button {index} selected");
        });

        SetupButtonGroup(defenceButtons, (index) => {
            defenceIndex = index + 1;
            isSelectedDefence = true;
            Debug.Log($"Defence button {index} selected");
        });

        SetupButtonGroup(ArrowButtons, (index) => {
            arrowIndex = index + 1;
            isSelectedAttack = true;
            Debug.Log($"Arrow attack button {index} selected");
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
        Debug.Log("Attack action completed");
    }

    private void OnDefenceComplete()
    {
        Debug.Log("Defence action completed");
    }

    public void TurnButton()
    {
        if (!isTurn) return;

        if (ArrowButtons != null && arrowIndex > 0)
        {
            ExecuteArrow();
        }
        else
        {
            ExecuteAttack();
        }

        StartCoroutine(ExecuteDefenceAfterDelay(4f));
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
            default:
                Debug.LogWarning("Invalid attack index!");
                break;
        }
    }

    private void ExecuteArrow()
    {
        switch (arrowIndex)
        {
            case 1:
                characterMoving.ArrowAttack1();
                break;
            case 2:
                characterMoving.ArrowAttack2();
                break;
            case 3:
                characterMoving.ArrowAttack3();
                break;
            default:
                Debug.LogWarning("Invalid arrow index!");
                break;
        }
    }

    private void ExecuteDefence()
    {
        if (defenceIndex < 1 || defenceIndex > 3)
        {
            Debug.LogWarning("Invalid defence index, skipping defence execution.");
            return;
        }

        switch (defenceIndex)
        {
            case 1:
                characterMoving.Defence1();
                break;
            case 2:
                characterMoving.Defence2();
                break;
            case 3:
                characterMoving.Defence3();
                break;
        }
    }

    private IEnumerator ExecuteDefenceAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ExecuteDefence();
        Debug.Log("Defence executed after delay");

        ResetButtonIndex(); 
    }

    public void ResetButtonIndex()
    {
        isSelectedAttack = false;
        isSelectedDefence = false;
        attackIndex = -1;
        arrowIndex = -1;
        defenceIndex = -1;
    }

    public void ToggleAttackSelection()
    {
        isSelectedAttack = !isSelectedAttack;
    }

    public void ToggleDefenceSelection()
    {
        isSelectedDefence = !isSelectedDefence;
    }

    public void OnForwardStepButtonClicked()
    {
        isSelectedAttack = true;
        characterMoving.ForwardStep();
    }

    public void OnBackwardStepButtonClicked()
    {
        isSelectedAttack = true;
        characterMoving.BackwardStep();
    }
}
