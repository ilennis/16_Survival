[System.Serializable]
public struct UnlockCondition //레시피 해방 조건 구조체
{
    public ConditionType ConditionType; // 해방 조건 타입
    public int Vaule; // 해방 조건 해당값
}

[System.Serializable]
public struct CraftingMaterial //필요한 재료 구조체
{
    public ItemData Item; // 제작에 필요한 아이템
    public int Amount; //제작에 필요한 개수
}

[System.Serializable]
public struct FoodEffect //음식 섭취 효과 구조체
{
    public FoodEffectType FoodEffectType; //음식 섭취효과
    public int Amount; //효과량
}
