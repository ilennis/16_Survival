using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler,IPointerEnterHandler,IPointerExitHandler, IPointerMoveHandler
{
    [SerializeField] private SlotDragAndDrop slotDragAndDrop;
    [SerializeField] private UIItemInfo Test;
    [SerializeField] private Image itemIcon; // 아이템 아이콘
    [SerializeField] private TextMeshProUGUI haveCountText; // 아이템 소지수

    private UIItemInfo uiItemInfo;

    private void Start()
    {
        uiItemInfo = GameManager.Instance.Inventory.UIItemInfo;
    }

    public ItemData ItemData
    {
        get => itemData;
        set
        {
            itemData = value;
            UpdateCellIcon(); // 인벤토리 슬롯 아이콘 갱신
        }
    }

    private ItemData itemData;

    public int HaveAmount
    {
        get => haveAmount;
        set
        {
            haveAmount = value;
            UpdateHaveAmount(); // 인벤토리 슬롯 소지수 갱신
        }
    }

    private int haveAmount = 0;
    
    //드래그 시작시 호출되는 이벤트
    public void OnBeginDrag(PointerEventData eventData) => slotDragAndDrop.OnBeginDragEvent?.Invoke(this, eventData);
    //드래그 중 호출되는 이벤트
    public void OnDrag(PointerEventData eventData) => slotDragAndDrop.OnDragEvent?.Invoke(this, eventData);
    //드롭시 호출되는 이벤트
    public void OnDrop(PointerEventData eventData) => slotDragAndDrop.OnDropEvent?.Invoke(this, eventData);
    //드래그 앤 드롭이 끝났을때 호출되는 함수
    public void OnEndDrag(PointerEventData eventData) => slotDragAndDrop.OnEndDragEvent?.Invoke(this, eventData);
    ////아이템 클릭시 호출되는 이벤트
    //public void OnPointerClick(PointerEventData eventData) => slotDragAndDrop.OnClickEvent?.Invoke(this, eventData);
    ////아이템 안에 마우스가 들어왔을때 호출되는 이벤트
    public void OnPointerEnter(PointerEventData eventData) => uiItemInfo.OnEnterEvent?.Invoke(ItemData, eventData);
    ////아이템 안에 마우스가 나갔을때 호출되는 이벤트
    public void OnPointerExit(PointerEventData eventData) => uiItemInfo.OnExitEvent?.Invoke(ItemData, eventData);
    ////아이템 안에 마우스가 움직일때 호출되는 이벤트
    public void OnPointerMove(PointerEventData eventData) => uiItemInfo.OnMoveEvent?.Invoke(ItemData, eventData);

    // 아이템 아이콘 갱신
    public void UpdateCellIcon()
    {
        if (ItemData != null)
        {
            itemIcon.color = Color.white;
            itemIcon.sprite = ItemData.Icon;
        }
        else
        {
            itemIcon.color = Color.clear;
            itemIcon.sprite = null;
        }
    }

    // 아이템 소지수 갱신
    public void UpdateHaveAmount()
    {
        haveCountText.text = (ItemData != null && HaveAmount > 1) ? $"x{HaveAmount}" : string.Empty;
    }

    //슬롯에 아이템을 추가할수있는지 판단
    public bool IsCanAddItem(ItemData data)
    {
        //아이템 데이터가 null 이거나 슬롯의 아이템의 소지수가 최대소지수가 아닐때
        return ItemData == null || (data == ItemData && HaveAmount < ItemData.CellMaxHaveCount);
    }

    //슬롯에 아이템을 추가할수있는지 판단
    public bool IsCanUseItem(ItemData data ,int useAmount)
    {
        //아이템 데이터가 null 이거나 슬롯의 아이템의 소지수가 최대소지수가 아닐때
        return data == ItemData && HaveAmount - useAmount >=0;
    }
}

