using UnityEngine;
using UnityEngine.UI;

public class InventoryToggleManager : MonoBehaviour
{
    [SerializeField] private Toggle inventoryToggle;
    [SerializeField] private Toggle craftingToggle;

    [SerializeField] private GameObject inventoryTab;
    [SerializeField] private GameObject craftingTab;

    private void Start()
    {
        inventoryToggle.onValueChanged.AddListener(ShowInventory);
        craftingToggle.onValueChanged.AddListener(ShowCrafting);
        ShowInventory(inventoryToggle.isOn);
        ShowCrafting(craftingToggle.isOn);
    }

    private void ShowInventory(bool isOn)
    {
        inventoryTab.SetActive(isOn);
    }

    private void ShowCrafting(bool isOn)
    {
        craftingTab.SetActive(isOn);
    }
}
