using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public string buildingName;
    public int level = 1;
    public GameObject uiPanel;

    private InventorySystem inventory;

    private void Start()
    {
        // InventorySystem'i GameManager'dan alýnacak
        GameObject manager = GameObject.Find("GameManager");
        if (manager != null)
        {
            inventory = manager.GetComponent<InventorySystem>();
        }
        else
        {
            Debug.LogWarning("GameManager not found! InventorySystem cannot be accessed.");
        }
    }

    private void OnMouseDown()
    {
        Debug.Log($"{buildingName} clicked!");
        if (uiPanel != null)
        {
            uiPanel.SetActive(true);
        }
    }

    public void UpgradeBuilding()
    {
        // Envanterden Wood ve Stone vs vs. kontrolü (örnektir)
        if (inventory != null && inventory.HasItem("Wood") && inventory.HasItem("Stone"))
        {
            inventory.RemoveItem("Wood");
            inventory.RemoveItem("Stone");

            level++;
            Debug.Log($"{buildingName} upgraded to level {level}");
        }
        else
        {
            Debug.LogWarning("Not enough materials to upgrade the building.");
        }
    }

    public void ClosePanel()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }
    }
}
