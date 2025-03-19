using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICrafting : MonoBehaviour
{
    [SerializeField] private ScrollRect recipeList; //제작 리스트 스크롤
    [SerializeField] private CraftingRecipes craftingRecipes; //제작 레시피 리스트 데이터
    [SerializeField] private CraftingCell craftingCellPrefab; //제작 레시피 셀 프리펩

    private List<CraftingCell> craftingCells = new(); // 생성된 셀 리스트

    private void Awake()
    {
        CreateCell(); //제작 레시피 셀 생성
    }
    private void OnEnable()
    {
        SetData(); //데이터 셋팅
        recipeList.verticalNormalizedPosition = 1;
    }

    private void CreateCell()
    {
        var count = recipeList.content.GetComponentsInChildren<CraftingCell>().Length;
        for (int i = count; i < craftingRecipes.CraftingRecipeList.Count; i++)
        {
            var cell = Instantiate(craftingCellPrefab, recipeList.content);
            craftingCells.Add(cell);
        }
    }

    private void SetData()
    {
        for (int i = 0; i < craftingCells.Count; i++)
        {
            var data = craftingRecipes.CraftingRecipeList[i];
            craftingCells[i].CraftingRecipe = data;
        }
    }
}
