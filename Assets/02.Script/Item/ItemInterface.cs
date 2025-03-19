using UnityEngine;

public interface IInteractable // 상호작용 인터페이스
{
    public string GetInfo();     //설명 표시
    public void Collect();  // 수집
    public bool IsCanCollect { get; }  // 수집 할수있는지
}

public interface IDroppable // 아이템 드롭 인터페이스
{
    public void Drop(Vector3 hit,Vector3 normal);     //아이템 드롭
}

public interface IDamageable
{
    public DamageType DamageType { get; } // 데미지 받는 오브젝트 타입
    public void Damage(float damage);  
}



