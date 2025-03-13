using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField]  private Transform inventoryList;
    public UIItemInfo UIItemInfo;

    private InventorySlot[] inventorySlotList;
    private Dictionary<ItemType, int> haveItemCountList = new Dictionary<ItemType, int>();
    private Dictionary<ItemType, List<InventorySlot>> NotEmptySlotList = new Dictionary<ItemType, List<InventorySlot>>();

    private void Awake()
    {
        GameManager.Instance.Inventory = this;
        inventorySlotList = inventoryList.GetComponentsInChildren<InventorySlot>();
    }

    //아이템 추가
    public void AddItem(ItemData itemData, int amount)
    {
        int totalAmount = amount;

        // 아이템을 추가할 수 있는 슬롯 찾기
        var emptySlot = GetCanAddItemSlot(itemData);
        if (emptySlot == null)
        {
            Debug.Log("인벤토리가 꽉 찼습니다!");
            return;
        }

        while (totalAmount > 0)
        {
            var slot = GetCanAddItemSlot(itemData);

            //추가 가능한 아이템수 계산
            int canAddAmount = (slot.ItemData == null) ? itemData.CellMaxHaveCount : itemData.CellMaxHaveCount - slot.HaveAmount;
            int addAmount = Mathf.Min(totalAmount, canAddAmount);

            if (NotEmptySlotList.ContainsKey(itemData.ItemType))
            {
                NotEmptySlotList[itemData.ItemType].Add(slot);
            }
            else
            {
                List<InventorySlot> tests = new List<InventorySlot>();
                tests.Add(slot);
                NotEmptySlotList.Add(itemData.ItemType, tests);
            }

            //데이터 반영
            slot.ItemData = itemData;
            slot.HaveAmount += addAmount;
            totalAmount -= addAmount;
        }

        if (haveItemCountList.ContainsKey(itemData.ItemType))
        {
            haveItemCountList[itemData.ItemType] += amount;
        }
        else
        {
            haveItemCountList.Add(itemData.ItemType, amount);
        }
    }

    //아이템 사용 (가공,건축,장착,음식 먹기)
    public void UseItem(ItemData itemData, int useAmount)
    {
        if (!IsCanUseItem(itemData, useAmount)) return;

        switch (itemData.ItemType)
        {
            case ItemType.Resource:
                Use(itemData, useAmount);
                break;
            case ItemType.Food:
                if (itemData.ItemPrefab.TryGetComponent(out IEatable item))
                {
                    Use(itemData, useAmount);
                    item.Eat();
                }
                break;
        }
    }

    //아이템 추가 가능한 슬롯 찾기
    private InventorySlot GetCanAddItemSlot(ItemData itemData)
    {
        foreach (var cell in inventorySlotList)
        {
            if (cell.IsCanAddItem(itemData))
            {
                return cell;
            }
        }
        return null;
    }

    private bool IsCanUseItem(ItemData data, int useAmount)
    {
        return haveItemCountList.ContainsKey(data.ItemType) && haveItemCountList[data.ItemType] - useAmount >= 0;
    }

    private void Use(ItemData data, int useAmount)
    {
        int totalUseAmount = useAmount;

        var list = NotEmptySlotList[data.ItemType];

        int index = list.Count - 1;
        while (totalUseAmount > 0)
        {
            int canUseAmount = Mathf.Min(totalUseAmount, list[index].HaveAmount);
            list[index].HaveAmount -= canUseAmount;
            if (list[index].HaveAmount == 0)
            {
                list[index].ItemData = null;
            }
            totalUseAmount -= canUseAmount;
            index--;
        }

        haveItemCountList[data.ItemType] -= useAmount;
    }
}
