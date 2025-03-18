using UnityEngine;

public class Pill : MonoBehaviour,IInteractable
{
    [SerializeField] ItemData pillData;
    private Inventory inventory;

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
        if (!inventory.GetCanAddItemSlot(pillData)) return;
        inventory.AddItem(pillData,1);
        Destroy(gameObject);
    }
}
