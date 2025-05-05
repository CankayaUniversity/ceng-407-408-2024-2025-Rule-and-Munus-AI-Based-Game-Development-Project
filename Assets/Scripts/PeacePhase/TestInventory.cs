using UnityEngine;

public class TestInventory : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private InventorySystem Inventory;
    void Start()
    {
        Inventory = GameObject.Find("GameManager").GetComponent<InventorySystem>();
        Inventory.AddItem("Stone");
        Inventory.AddItem("Wood");
        Inventory.AddItem("Iron");
        Inventory.PrintInventory();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
