using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftingRecipes", menuName = "Scriptable Object/CraftingRecipes", order = int.MaxValue)]

public class CraftingRecipes : ScriptableObject
{
    public List<RecipeData> CraftingRecipeList;
}
