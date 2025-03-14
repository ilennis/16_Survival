using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;
    public TextMeshProUGUI levelText;

    Condition Health { get { return uiCondition.health; } }
    Condition Hunger { get { return uiCondition.hunger; } }
    Condition Thirst { get { return uiCondition.thirst; } }
    Condition Stamina { get { return uiCondition.stamina; } }
    Condition Level { get { return uiCondition.level; } }

    private int level = 1;
    private int exp = 0;
    public float noHungerHealthDecay;
    public float noThirstHealthDecay;
    public event Action onTakeDamage;

    private void Update()
    {
        Hunger.Subtract(Hunger.passiveValue * Time.deltaTime);
        Thirst.Subtract(Thirst.passiveValue * Time.deltaTime);
        Stamina.Add(Stamina.passiveValue * Time.deltaTime);

        if (Hunger.curValue <= 0f)
        {
            Health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (Health.curValue <= 0f)
        {
            Die();
        }

        if (Thirst.curValue <= 0f)
        {
            Health.Subtract(noThirstHealthDecay * Time.deltaTime);
        }

        if (Thirst.curValue <= 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        Health.Add(amount);
    }

    public void Eat(float amount)
    {
        Hunger.Add(amount);
    }

    public void Drink(float amount)
    {
        Thirst.Add(amount);
    }

    public void GetExp(float amount)
    {
        Level.Add(amount);
        levelText.text = $"Lv. {level}";
    }

    public void Die()
    {
        Debug.Log("플레이어가 죽었다.");
    }
}