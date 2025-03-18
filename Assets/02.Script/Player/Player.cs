using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition Condition;
    public WeaponController WeaponController;

    private void Awake()
    {
        GameManager.Instance.Player = this;
        CharacterManager.Instance.Player = this;

        controller = GetComponent<PlayerController>();
        Condition = GetComponent<PlayerCondition>();
        WeaponController = GetComponent<WeaponController>();
    }

    public void Eat(FoodEffect foodEffect)
    {
        switch (foodEffect.FoodEffectType)
        {
            case FoodEffectType.HungerRecovery:
                Condition.Eat(foodEffect.Amount);
                break;
            case FoodEffectType.ThirstRecovery:
                Condition.Drink(foodEffect.Amount);
                break;
        }
    }
}
