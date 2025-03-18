using UnityEngine;

public class Water : MonoBehaviour,IInteractable
{
    [SerializeField]
    private FoodData waterData; //획득 자원 데이터
    private Inventory inventory;
    private Interaction interaction;
    public bool IsCanCollect => inventory.GetCanAddItemSlot(waterData);

    private void Start()
    {
        inventory = GameManager.Instance.Inventory;
        interaction = GameManager.Instance.Player.Interaction;
    }

    public string GetInfo()
    {
        return $"F키를 눌러서 물 뜨기";
    }

    public void Collect()
    {
        if (!IsCanCollect)
        {
            NotificationManager.Instance.ShowFullInventory();
            return;
        }
        inventory.AddItem(waterData,1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interaction.CheckedWater = this;
            interaction.OnCheckWaterEvent(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어 나감");
            interaction.CheckedWater = null;
            interaction.OnCheckWaterEvent(this);
        }
    }
}
