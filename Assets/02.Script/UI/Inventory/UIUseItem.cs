using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIUseItem : MonoBehaviour
{
    [SerializeField] private Image useItemWindow;
    [SerializeField] private TextMeshProUGUI itemInfoText;
    [SerializeField] private Button useItemButton;
    [SerializeField] private Button closeButton;

    private Inventory inventory;
    private ItemData useItem;

    public Action<ItemData, PointerEventData> OnClickEvent = delegate { };

    private void Start()
    {
        inventory = GameManager.Instance.Inventory;

        useItemButton.onClick.AddListener(UseItem);
        closeButton.onClick.AddListener(CloseWindow);
    }

    private void OnEnable()
    {
        OnClickEvent += OnClick;
    }

    private void OnDisable()
    {
        OnClickEvent -= OnClick;
    }

    private void OnClick(ItemData data, PointerEventData eventData)
    {
        if (data == null || !IsCanActiveUseUI(data)) return;
        useItem = data;
        useItemWindow.gameObject.SetActive(true);
        itemInfoText.text = $"<color=yellow>{data.ItemName}</color>을(를)\n사용하시겠습니까?";
    }

    //아이템 사용
    private void UseItem()
    {
        if (useItem == null) return;
        switch (useItem.ItemType)
        {
            case ItemType.Vaccine:
                GameManager.Instance.IsClear = true;
                break;
            case ItemType.Pill:
                GameManager.Instance.Player.Condition.Damage(10);
                break;
            default:
                inventory.UseItem(useItem, 1);
                break;
        }
        useItemWindow.gameObject.SetActive(false);
    }

    //아이템 사용 UI 닫기
    private void CloseWindow()
    {
        useItem = null;
        useItemWindow.gameObject.SetActive(false);
    }

    private bool IsCanActiveUseUI(ItemData data)
    {
        bool isCanUseVaccine = data.ItemType == ItemType.Vaccine && inventory.IsHasItem(ItemType.Syringe);
        bool isPill = data.ItemType == ItemType.Pill;
        bool isFood = data.ItemType == ItemType.Food;
        bool isWater = data.ItemType == ItemType.Water;
        return isCanUseVaccine || isPill || isFood || isWater;
    }
}
