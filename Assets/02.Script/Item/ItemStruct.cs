[System.Serializable]
public struct CraftingRecipe //제작 레시피 구조체
{
    public ItemData Item; // 제작 아이템
    public int Amount; //제작에 필요한 개수
}

[System.Serializable]
public struct FoodEffect //음식 섭취 효과 구조체
{
    public FoodEffectType FoodEffectType; //음식 섭취효과
    public int Amount; //효과량
}
