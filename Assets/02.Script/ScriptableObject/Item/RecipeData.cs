using UnityEngine;

[CreateAssetMenu(fileName = "RecipeData", menuName = "Scriptable Object/RecipeData", order = int.MaxValue)]
public class RecipeData : ScriptableObject
{
    public ItemData CraftItem; // 제작 아이템
    public CraftingMaterial[] Materials; //필요한 재료
    public UnlockCondition UnlockCondition; //해방 조건
    public bool IsCraftOnce; //한번만 제작가능한지

    //레시피 해방 가능한지
    public bool IsCanUnlock()
    {
        switch (UnlockCondition.ConditionType)
        {
            case ConditionType.PlayerLevel:
                //TODO:플레이어 레벨 참조
                return 1 >= UnlockCondition.Vaule;
        }
        return false;
    }

    //제작 가능한지
    public bool IsCanCraft()
    {
        var inventory = GameManager.Instance.Inventory;

        foreach (var material in Materials)
        {
            var haveAmount = inventory.GetHasItemAmount(material.Item.ItemType);
            if (haveAmount == 0)
            {
                return false;
            }
        }
        return true;
    }

    //해방 조건 텍스트 반환
    public string GetUnlockConditionText()
    {
        switch (UnlockCondition.ConditionType)
        {
            case ConditionType.PlayerLevel:
                return $"플레이어 레벨<color=yellow>{UnlockCondition.Vaule}</color>이상 해방";
            default:
                return string.Empty;
        }
    }

    //일회성 제작 레시피 한정 제작이 완료되었는지
    public bool IsCrafted()
    {
        var craftedOnceRecipeList = DataManager.Instance.CraftedOnceRecipeList;
        return craftedOnceRecipeList.ContainsKey(this) && craftedOnceRecipeList[this];
    }
}
