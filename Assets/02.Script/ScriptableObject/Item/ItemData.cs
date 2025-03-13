using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/ItemData", order = int.MaxValue)]
public class ItemData : ScriptableObject
{
    public string ItemName; //아이템 이름
    public string Description; //아이템 설명
    public Sprite Icon; //아이템 이미지
    public ItemType ItemType; // 아이템 타입
    public int CellMaxHaveCount; //인벤토리 슬롯 한칸 최대 소지수
    public GameObject ItemPrefab; //아이템 프리펩 오브젝트
}


