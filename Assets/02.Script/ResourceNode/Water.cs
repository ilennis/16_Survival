using UnityEngine;

public class Water : MonoBehaviour,IInteractable
{
    [SerializeField]
    private FoodData waterData; //획득 자원 데이터

    private Inventory inventory;

    private void Start()
    {
        inventory = GameManager.Instance.Inventory;
    }

    public string GetInfo()
    {
        //TODO : 인벤토리에 아이템을 최대소지수 만큼 가지고 있으면 더이상 채집 못한다고 표시
        return $"F키를 눌러서 물 뜨기";
    }

    public void Collect()
    {
        if (!inventory.GetCanAddItemSlot(waterData)) return;
        inventory.AddItem(waterData,1);
    }
}
