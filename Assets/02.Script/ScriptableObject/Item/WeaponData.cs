using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Object/WeaponData", order = int.MaxValue)]
public class WeaponData : ItemData
{
    public float AttackDamage; // 공격데미지
    public float ResourceDamage; //자원을 채집할때 가하는 피해량
    public float AttackSpeed; // 공격 속도
    public CraftingRecipe[] CraftingRecipes; // 무기 제작 레시피
}
