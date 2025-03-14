using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotDragAndDrop : MonoBehaviour
{
    [SerializeField]
    private Image dragImage;
    private InventorySlot dragCell;

    public Action<InventorySlot, PointerEventData> OnBeginDragEvent = delegate { };
    public Action<InventorySlot, PointerEventData> OnDragEvent = delegate { };
    public Action<InventorySlot, PointerEventData> OnEndDragEvent = delegate { };
    public Action<InventorySlot, PointerEventData> OnDropEvent = delegate { };

    private void OnEnable()
    {
        OnDropEvent += OnDrop;
        OnDragEvent += OnDrag;
        OnBeginDragEvent += OnBeginDrag;
        OnEndDragEvent += OnEndDrag;
    }

    private void OnDisable()
    {
        OnDropEvent -= OnDrop;
        OnDragEvent -= OnDrag;
        OnBeginDragEvent -= OnBeginDrag;
        OnEndDragEvent -= OnEndDrag;
    }

    private void OnDrag(InventorySlot cell, PointerEventData eventData)
    {
        if (cell.ItemData == null) return;
        dragImage.transform.position = eventData.position;
    }

    private void OnDrop(InventorySlot cell, PointerEventData eventData)
    {
        if (cell.ItemData != null || dragCell==null) return;
        cell.ItemData = dragCell.ItemData;
        cell.HaveAmount = dragCell.HaveAmount;
        dragCell.ItemData = null;
        dragCell.HaveAmount = 0;
    }

    private void OnBeginDrag(InventorySlot cell, PointerEventData eventData)
    {
        if (cell.ItemData == null) return;
        dragImage.sprite = cell.ItemData.Icon;
        dragImage.gameObject.SetActive(true);
        dragImage.transform.position = cell.transform.position;
        dragCell = cell;
        //이미지가 겹쳐서 EventSystem이 정상적으로 작동되지않는것을 방지
        dragImage.raycastTarget = false;
    }

    private void OnEndDrag(InventorySlot cell, PointerEventData eventData)
    {
        dragCell = null;
        dragImage.gameObject.SetActive(false);
        //원래대로 되돌리기
        dragImage.raycastTarget = true;
    }
}
