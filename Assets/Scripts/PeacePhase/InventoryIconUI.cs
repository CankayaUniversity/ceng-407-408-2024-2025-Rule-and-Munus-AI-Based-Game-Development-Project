using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryIconUI : MonoBehaviour
{
    public InventorySystem inventorySystem;
    public GameObject iconPrefab; // prefab (64x64 Image)
    public Transform gridParent; // GridLayout olan panel

    private List<GameObject> currentIcons = new List<GameObject>();

    void Update()
    {
        if (inventorySystem == null || iconPrefab == null || gridParent == null)
            return;

        // Eski ikonlarý destroy et - temizlemek için
        foreach (GameObject icon in currentIcons)
        {
            Destroy(icon);
        }
        currentIcons.Clear();

        // Yeni item ikonlarýný oluþturuyor
        foreach (string item in inventorySystem.items)
        {
            GameObject iconGO = Instantiate(iconPrefab, gridParent);

            // icon atamak için Resources klasöründen Sprite yüklemek için
            Image image = iconGO.GetComponent<Image>();
            Sprite sprite = Resources.Load<Sprite>($"Icons/{item}");

            if (sprite != null)
                image.sprite = sprite;
            else
                Debug.LogWarning($"No sprite found for item: {item}");

            currentIcons.Add(iconGO);
        }
    }
}
