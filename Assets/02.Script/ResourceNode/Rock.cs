using UnityEngine;

public class Rock : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData resourceData; //획득 자원 데이터
    [SerializeField] private ResourceNodeData NodeData; //채집 가능한 오브젝트 데이터

    private Inventory inventory;

    private int rockHp;
    private int rockYield;

    public bool IsCanCollect => inventory.GetCanAddItemSlot(resourceData);

    private void Start()
    {
        rockHp = NodeData.Hp;
        rockYield = NodeData.Yield;

        inventory = GameManager.Instance.Inventory;
    }

    public string GetInfo()
    {
        return $" F키를 눌러 채집";
    }

    public void Collect()
    {
        if (!IsCanCollect)
        {
            NotificationManager.Instance.ShowFullInventory();
            return;
        }
        rockHp -= rockYield;
        inventory.AddItem(resourceData, rockYield);
        if (rockHp <= 0) //더이상 채집 불가능하면
        {
            Destroy(gameObject); //오브젝트 파괴
        }
    }
}

