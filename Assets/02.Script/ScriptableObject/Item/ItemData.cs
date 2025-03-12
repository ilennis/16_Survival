using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/ItemData", order = int.MaxValue)]
public class ItemData : ScriptableObject
{
    public string ItemName; //아이템 이름
    public string Description; //아이템 설명
    public Sprite Icon; //아이템 이미지
    public ItemType ItemType; // 아이템 타입
    public int MaxHaveCount; //최대 소지수
}


