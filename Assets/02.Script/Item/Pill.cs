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
        return "F키를 눌러서 채집";
    }

    public void Collect()
    {
        if (!IsCanCollect)
        {
            NotificationManager.Instance.ShowFullInventory();
            return;
        }
        inventory.AddItem(pillData,1);
        Destroy(gameObject);
    }
}
