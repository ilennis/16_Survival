using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; //싱글턴은 당연... 나중에 인벤토리에 확장시키기

    [SerializeField] private GameObject inventoryUI;
    public bool IsOpen = false;  // 인벤토리 상태

    private Dictionary<string, int> materials = new Dictionary<string, int>(); // 찾기 위해 Dictionary 사용

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public bool HasMaterials(BuildingCost[] costs) // 빌딩 코스트 비교 true/false
    {
        foreach (var cost in costs)
        {
            if (!materials.ContainsKey(cost.materialName) || materials[cost.materialName] < cost.requiredAmount)
            {
                return false;
            }
        }
        return true;
    }

    public void DeductMaterials(BuildingCost[] costs) // 이름 있는거에 빼기
    {
        foreach (var cost in costs)
        {
            if (materials.ContainsKey(cost.materialName))
            {
                materials[cost.materialName] -= cost.requiredAmount;
            }
        }
    }

    public void AddMaterial(string materialName, int amount)
    {
        if (!materials.ContainsKey(materialName))
        {
            materials[materialName] = 0;
        }
        materials[materialName] += amount;
    }

    public int GetMaterialAmount(string materialName)
    {
        return materials.ContainsKey(materialName) ? materials[materialName] : 0;
    }

    public void ToggleInventory()
    {
        IsOpen = !IsOpen;
        inventoryUI.SetActive(IsOpen);
        // UI가 활성화되면 마우스 커서 표시
        Cursor.visible = IsOpen;
        Cursor.lockState = IsOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }
}