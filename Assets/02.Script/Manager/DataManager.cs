using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public Dictionary<ItemType, int> HaveItemCountList = new();     // 각 아이템 소지수
    public Dictionary<RecipeData, bool> CraftedOnceRecipeList = new();     // 제작 완료된 일회성 레시피 리스트

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
