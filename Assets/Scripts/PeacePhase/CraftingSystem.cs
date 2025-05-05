using UnityEngine;
using TMPro;

public class CraftingSystem : MonoBehaviour
{
    public InventorySystem inventorySystem;
    public TextMeshProUGUI resultText;

    // Craft için gerekli bazý item'lar: (daha detaylý olacak)
    public string requiredItem1 = "Wood";
    public string requiredItem2 = "Iron";
    public string craftedItem = "Sword";

    public void CraftItem()
    {
        if (inventorySystem.HasItem(requiredItem1) && inventorySystem.HasItem(requiredItem2))
        {
            inventorySystem.RemoveItem(requiredItem1);
            inventorySystem.RemoveItem(requiredItem2);
            inventorySystem.AddItem(craftedItem);

            resultText.text = $"{craftedItem} crafted!";
            Debug.Log($"{craftedItem} crafted!");
        }
        else
        {
            resultText.text = "Missing materials!";
            Debug.Log("Not enough materials to craft.");
        }
    }
}
