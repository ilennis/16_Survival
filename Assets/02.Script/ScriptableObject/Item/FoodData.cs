using UnityEngine;

[CreateAssetMenu(fileName = "FoodData", menuName = "Scriptable Object/FoodData", order = int.MaxValue)]
public class FoodData : ItemData
{
    public FoodEffect[] FoodEffects;
}
