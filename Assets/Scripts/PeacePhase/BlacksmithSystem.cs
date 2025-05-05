using UnityEngine;
using TMPro;

public class BlacksmithSystem : MonoBehaviour
{
    public GameObject uiPanel;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI resultText;

    public InventorySystem inventorySystem;

    public string craftedItemName = "Sword";
    private int level = 1;

    private void Start()
    {
        if (uiPanel != null)
            uiPanel.SetActive(false);

        UpdateLevelUI();
    }

    private void OnMouseDown()
    {
        if (uiPanel != null)
            uiPanel.SetActive(true);
    }

    public void UpgradeBuilding()
    {
        if (inventorySystem.HasItem("Wood") && inventorySystem.HasItem("Stone"))
        {
            inventorySystem.RemoveItem("Wood");
            inventorySystem.RemoveItem("Stone");
            level++;
            UpdateLevelUI();
            resultText.text = $"Upgraded to Level {level}";
        }
        else
        {
            resultText.text = "Not enough materials to upgrade!";
        }
    }

    public void CraftItem()
    {
        if (inventorySystem.HasItem("Wood") && inventorySystem.HasItem("Iron"))
        {
            inventorySystem.RemoveItem("Wood");
            inventorySystem.RemoveItem("Iron");
            inventorySystem.AddItem(craftedItemName);
            resultText.text = $"{craftedItemName} crafted!";
        }
        else
        {
            resultText.text = "Not enough materials to craft!";
        }
    }

    public void ClosePanel()
    {
        if (uiPanel != null)
            uiPanel.SetActive(false);
    }

    private void UpdateLevelUI()
    {
        if (levelText != null)
            levelText.text = $"Level: {level}";
    }
}
