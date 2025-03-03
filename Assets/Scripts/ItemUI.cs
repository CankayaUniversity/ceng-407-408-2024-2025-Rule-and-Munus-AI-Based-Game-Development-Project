using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(Button))]
public class ItemUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    Image image;
    [SerializeField]
    Button button;

    public void Initialize(string inventoryId, Item item, Action<string> removeItemAction)
    {
        image.sprite = item.icon;
        transform.localScale = Vector3.one;
        button.onClick.AddListener(() => removeItemAction.Invoke(inventoryId));
    }

    void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

}
