using System.Collections.Generic;
using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int phase;
    public AttributeManager attributeManager;
    public List<Button> attrbiuteIncreaseButtons;
    public List<Button> attrbiuteDecreaseButtons;
    public void Awake()
    {
        phase = 0;
        attributeManager = GetComponent<AttributeManager>();
    }
    public void UpdatePhase()
    {
        phase++;
        if(phase/10 == 0)
        {
            ActivateIncreaseButtons();
            ActivateDecreaseButtons();
        }
    }
    public void ActivateIncreaseButtons()
    {
        for(int i = 0; i < attrbiuteIncreaseButtons.Count; i++)
        {
            attrbiuteIncreaseButtons[i].visible = true;
        }
    }
    public void DeactivateIncreaseButtons()
    {
        for(int i = 0; i < attrbiuteIncreaseButtons.Count; i++)
        {
            attrbiuteIncreaseButtons[i].visible = false;
        }
    }
    public void ActivateDecreaseButtons()
    {
        for(int i = 0; i < attrbiuteDecreaseButtons.Count; i++)
        {
            attrbiuteDecreaseButtons[i].visible = true;
        }
    }
    public void DeactivateDecreaseButtons()
    {
        for(int i = 0; i < attrbiuteDecreaseButtons.Count; i++)
        {
            attrbiuteDecreaseButtons[i].visible = false;
        }
    }
    public void StartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void SettingsMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void CreditsMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void CreditsBackButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
    public void SettingsBackButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void CollesiumButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void VillageButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}