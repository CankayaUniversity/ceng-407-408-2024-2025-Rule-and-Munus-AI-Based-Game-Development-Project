using UnityEngine;
using TMPro;

public class MarketplaceSystem : MonoBehaviour
{
    public GameObject uiPanel;
    public InventorySystem inventorySystem;
    public TextMeshProUGUI resultText;

    private void Start()
    {
        if (uiPanel != null)
            uiPanel.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (uiPanel != null)
            uiPanel.SetActive(true);
    }

    public void BuyWood()
    {
        inventorySystem.AddItem("Wood");
        resultText.text = "You bought Wood!";
    }

    public void BuyIron()
    {
        inventorySystem.AddItem("Iron");
        resultText.text = "You bought Iron!";
    }

    public void ClosePanel()
    {
        if (uiPanel != null)
            uiPanel.SetActive(false);
    }
}
