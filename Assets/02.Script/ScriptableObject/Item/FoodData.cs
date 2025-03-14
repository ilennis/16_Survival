using UnityEngine;

[CreateAssetMenu(fileName = "FoodData", menuName = "Scriptable Object/FoodData", order = int.MaxValue)]
public class FoodData : ItemData
{
    public FoodEffect[] FoodEffects;

    public void Eat(Player player)
    {
        foreach(var foodEffect in FoodEffects)
        {
            //음식 효과 적용
            //player.Eat(foodEffect);
        }
    }
}
