using UnityEngine;

public class Water : MonoBehaviour,IInteractable
{
    [SerializeField]
    private FoodData waterData; //획득 자원 데이터

    private Inventory inventory;

    public bool IsCanCollect => inventory.GetCanAddItemSlot(waterData);

    private void Start()
    {
        inventory = GameManager.Instance.Inventory;
    }

    public string GetInfo()
    {
        if (!IsCanCollect)
        {
            return "인벤토리가 꽉찼습니다";
        }
        return $"F키를 눌러서 물 뜨기";
    }

    public void Collect()
    {
        if (!IsCanCollect) return;
        inventory.AddItem(waterData,1);
    }
}
