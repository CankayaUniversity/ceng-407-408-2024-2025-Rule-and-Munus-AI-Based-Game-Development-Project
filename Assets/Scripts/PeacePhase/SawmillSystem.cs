using UnityEngine;
using TMPro;

public class SawmillSystem : MonoBehaviour
{
    public GameObject uiPanel;
    public TextMeshProUGUI resultText;
    public InventorySystem inventorySystem;

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

    public void ProduceWood()
    {
        inventorySystem.AddItem("Wood");
        resultText.text = "Wood produced!";
    }

    public void ClosePanel()
    {
        if (uiPanel != null)
            uiPanel.SetActive(false);
    }
}
