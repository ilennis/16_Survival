using UnityEngine;

public class Food : MonoBehaviour, IInteractable
{
    [SerializeField] FoodData foodData;

    private Inventory inventory;

    private void Start()
    {
        inventory = GameManager.Instance.Inventory;
    }

    public void Collect()
    {
        if (!inventory.GetCanAddItemSlot(foodData)) return;
        inventory.AddItem(foodData, 1);
        Debug.Log($"인벤토리에{foodData.ItemName}을 인벤토리에 추가하였습니다!");
        Destroy(gameObject); //오브젝트 파괴
    }

    public string GetInfo()
    {
        return "F를 눌러 채집";
    }
}
