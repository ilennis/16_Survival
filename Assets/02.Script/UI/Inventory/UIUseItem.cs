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
        if (data.ItemType != ItemType.Food) return;
        useItem = data;
        useItemWindow.gameObject.SetActive(true);
        itemInfoText.text = $"<color=yellow>{data.ItemName}</color>을(를)\n사용하시겠습니까?";
    }

    //아이템 사용
    private void UseItem()
    {
        if (useItem == null) return;
        inventory.UseItem(useItem, 1);
        useItemWindow.gameObject.SetActive(false);
    }

    //아이템 사용 UI 닫기
    private void CloseWindow()
    {
        useItem = null;
        useItemWindow.gameObject.SetActive(false);
    }
}
