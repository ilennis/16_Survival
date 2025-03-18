using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "Scriptable Object/QuestData", order = int.MaxValue)]
public class EnoughItemQuestData : ScriptableObject
{
    public string QuestText;
    public ItemData ConditionItem;
    public int ConditionAmount;
}
