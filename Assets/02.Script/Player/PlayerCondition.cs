using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public interface IDamage
{
    void TakeDamage(int damageAmount);
}
public class PlayerCondition : MonoBehaviour, IDamage
{
    public UICondition uiCondition;
    public TextMeshProUGUI levelText;

    Condition Health { get { return uiCondition.health; } }
    Condition Hunger { get { return uiCondition.hunger; } }
    Condition Thirst { get { return uiCondition.thirst; } }
    Condition Stamina { get { return uiCondition.stamina; } }
    Condition Level { get { return uiCondition.level; } }

    public int level = 1;
    public float levelUpBonus = 20f;
    public float noHungerHealthDecay;
    public float noThirstHealthDecay;
    public bool useStamina;
    public event Action onTakeDamage;

    private void Update()
    {
        Hunger.Subtract(Hunger.passiveValue * Time.deltaTime);
        Thirst.Subtract(Thirst.passiveValue * Time.deltaTime);
        Stamina.Add(Stamina.passiveValue * Time.deltaTime);

        if (useStamina)
        {
            Stamina.Subtract(Stamina.passiveValue * 3f * Time.deltaTime);
        }

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

        levelText.text = $"Lv. {level}";

        if (Input.GetKeyDown(KeyCode.Return))
        {
            GetExp(20f);
        }
    }

    public void Damage(float amount)
    {
        Health.Subtract(amount);
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
        
        if (Level.curValue >= Level.maxValue)
        {
            Level.curValue = Level.curValue - Level.maxValue;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        Level.maxValue *= 1.1f;

        // 필요할 경우 레벨업 관련 기능 추가
        Health.maxValue += levelUpBonus;
        Health.Add(levelUpBonus);

        Hunger.maxValue += levelUpBonus;
        Hunger.Add(levelUpBonus);

        Thirst.maxValue += levelUpBonus;
        Thirst.Add(levelUpBonus);

        Stamina.maxValue += levelUpBonus;
        Stamina.Add(levelUpBonus);
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TakeDamage(int damageAmount)
    {
        Health.Subtract(damageAmount);
    }
}