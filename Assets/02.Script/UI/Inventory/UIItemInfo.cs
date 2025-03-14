using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItemInfo : MonoBehaviour
{
    [SerializeField] private Image itemInfoWindow;
    [SerializeField] private TextMeshProUGUI itemInfoText;

    public Action<ItemData, PointerEventData> OnEnterEvent = delegate { };
    public Action<ItemData, PointerEventData> OnExitEvent = delegate { };
    public Action<ItemData, PointerEventData> OnMoveEvent = delegate { };

    private void Start()
    {
        OnEnterEvent += OnPointerEnter;
        OnExitEvent += OnPointerExit;
        OnMoveEvent += OnPointerMove;
    }

    private void OnDisable()
    {
        OnEnterEvent -= OnPointerEnter;
        OnExitEvent -= OnPointerExit;
        OnMoveEvent -= OnPointerMove;
    }

    private void OnPointerEnter(ItemData data , PointerEventData eventData)
    {
        if (data == null) return;
        itemInfoWindow.gameObject.SetActive(true);
        itemInfoText.text = $"{data.ItemName}\n{data.Description}";
    }

    private void OnPointerMove(ItemData data, PointerEventData eventData)
    {
        if (data == null) return;
        //마우스가 설명창을 감지 못하게 떨어지게 배치
        itemInfoWindow.transform.position = eventData.position + new Vector2(20, -20);
    }

    private void OnPointerExit(ItemData data, PointerEventData eventData)
    {
        if (data == null) return;
        itemInfoWindow.gameObject.SetActive(false);
        itemInfoText.text = string.Empty;
    }
}
