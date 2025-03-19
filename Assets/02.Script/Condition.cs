using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;
    public float maxValue;
    public float startValue;
    public float passiveValue;
    public Image uiBar;
    public float speed = 10f;
    public bool isLevel; // 최대값을 넘을 수 있음 (레벨)

    private void Start()
    {
        curValue = startValue;
    }

    private void Update()
    {
        if (isLevel)
        {
            if (uiBar.fillAmount > GetPercentage())
            {
                uiBar.fillAmount = Mathf.Lerp(0f, GetPercentage(), Time.deltaTime * speed);
            }
            else
            {
                uiBar.fillAmount = Mathf.Lerp(uiBar.fillAmount, GetPercentage(), Time.deltaTime * speed);
            }
        }
        else
        {
            uiBar.fillAmount = GetPercentage();
        }
    }

    public void Add(float amount)
    {
        if (isLevel)
        {
            curValue += amount;
        }
        else
        {
            curValue = Mathf.Min(curValue + amount, maxValue);
        }
    }

    public void Subtract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }
}