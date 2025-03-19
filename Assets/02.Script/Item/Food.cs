using UnityEngine;

public class Food : MonoBehaviour, IInteractable
{
    [SerializeField] FoodData foodData;
    private Inventory inventory;
    public bool IsCanCollect => inventory.GetCanAddItemSlot(foodData);

    private void Start()
    {
        inventory = GameManager.Instance.Inventory;
    }

    public void Collect()
    {
        if (!IsCanCollect)
        {
            NotificationManager.Instance.ShowFullInventory();
            return;
        }
        inventory.AddItem(foodData, 1);
        Debug.Log($"인벤토리에{foodData.ItemName}을 추가하였습니다!");
        Destroy(gameObject); //오브젝트 파괴
    }

    public string GetInfo()
    {
        return "F를 눌러 채집";
    }
}
