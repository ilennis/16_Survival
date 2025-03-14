using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]  private Transform inventoryList;
    public UIItemInfo UIItemInfo; 
    public UIUseItem UIUseItem;
    public SlotDragAndDrop SlotDragAndDrop;

    private Player player;
    private DataManager dataManager;
    private InventorySlot[] inventorySlotList;
    private Dictionary<ItemType, int> haveItemCountList = new();
    private Dictionary<ItemType, List<InventorySlot>> notEmptySlotList = new();

    private void Awake()
    {
        GameManager.Instance.Inventory = this;
        player = GameManager.Instance.Player;
        dataManager = DataManager.Instance;
        inventorySlotList = inventoryList.GetComponentsInChildren<InventorySlot>();
        haveItemCountList = dataManager.HaveItemCountList;
    }
    //아이템을 인벤토리에 추가
    public void AddItem(ItemData itemData, int amount)
    {
        int remainingAmount = amount;

        while (remainingAmount > 0)
        {
            var slot = GetCanAddItemSlot(itemData); //추가 가능한 슬롯 가져오기
            if (slot == null)
            {
                Debug.LogWarning("인벤토리가 꽉 찼습니다!");
                return;
            }
            int addAmount = CalculateAddAmount(slot, itemData, remainingAmount); // 추가 가능한 아이템수 계산
            AddItemToSlot(slot, itemData, addAmount); // 슬롯에 아이템 추가
            UpdateNotEmptySlotList(slot, itemData); //슬롯 아이템 리스트 업데이트
            remainingAmount -= addAmount; 
        }
        UpdateHaveItemCountList(itemData, amount); //가지고 있는 아이템수 리스트 업데이트
    }   
    //추가할 아이템 개수 계산
    private int CalculateAddAmount(InventorySlot slot, ItemData itemData, int remainingAmount)
    {
        int slotMaxAmount = (slot.ItemData == null) ? itemData.CellMaxHaveCount : itemData.CellMaxHaveCount - slot.HaveAmount;
        return Mathf.Min(remainingAmount, slotMaxAmount);
    }
    // 슬롯에 아이템 데이터 추가
    private void AddItemToSlot(InventorySlot slot, ItemData itemData, int addAmount)
    {
        slot.ItemData = itemData;
        slot.HaveAmount += addAmount;
    }
    private void UpdateNotEmptySlotList(InventorySlot slot, ItemData itemData)
    {
        //인벤토리에 지정 아이템이 추가되어있다면 
        if (notEmptySlotList.ContainsKey(itemData.ItemType))
        {
            notEmptySlotList[itemData.ItemType].Add(slot);    //수량 증가
        }
        else
        {
            notEmptySlotList.Add(itemData.ItemType, new List<InventorySlot> { slot }); 
        }
    }
    private void UpdateHaveItemCountList(ItemData itemData, int amount)
    {
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
        //사용할 개수가 충분하지 않다면 사용불가
        if (!IsCanUseItem(itemData, useAmount)) return;

        switch (itemData.ItemType)
        {
            case ItemType.Resource:
                ConsumeItem(itemData, useAmount);
                break;
            case ItemType.Food:       
                // 아이템 사용 로직
                if (itemData is FoodData food)
                {
                    food.Eat(player);
                    ConsumeItem(itemData, useAmount);
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
    //아이템을 사용가능한지 판단
    private bool IsCanUseItem(ItemData data, int useAmount)
    {
        return haveItemCountList.ContainsKey(data.ItemType) && haveItemCountList[data.ItemType] - useAmount >= 0;
    }
    //아이템 소비
    private void ConsumeItem(ItemData data, int useAmount)
    {
        int remainAmount = useAmount;

        var list = notEmptySlotList[data.ItemType];

        int index = list.Count - 1;
        while (remainAmount > 0)
        {
            int canUseAmount = Mathf.Min(remainAmount, list[index].HaveAmount);
            list[index].HaveAmount -= canUseAmount;
            if (list[index].HaveAmount == 0)
            {
                list[index].ItemData = null;
            }
            remainAmount -= canUseAmount;
            index--;
        }

        haveItemCountList[data.ItemType] -= useAmount;
    }
}
