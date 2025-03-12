using UnityEngine;

public interface IInteractable // 상호작용 인터페이스
{
    public string GetInfo();     //설명 표시
    public void Interact();      // 상호작용을 실행
}

public interface IDroppable // 아이템 드롭 인터페이스
{
    public void Drop(Vector3 hit,Vector3 normal);     //아이템 드롭
}

public interface IEatable // 음식 섭취 인터페이스
{
    public void Eat();    //음식 섭취
}

public interface IEquippable //장착 인터페이스
{
    public void Equip();     //장착
    public void UnEquip();    //장착 해제
}
