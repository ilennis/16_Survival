using UnityEngine;

public class Pill : MonoBehaviour,IInteractable
{
    [SerializeField] ItemData pillData;
    private Inventory inventory;
    public bool IsCanCollect => inventory.GetCanAddItemSlot(pillData);

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
        return "F키를 눌러서 채집";
    }

    public void Collect()
    {
        if (!IsCanCollect) return;
        inventory.AddItem(pillData,1);
        Destroy(gameObject);
    }
}
