using TMPro;
using UnityEngine;

public class UIQuestText : MonoBehaviour
{
    [SerializeField] private EnoughItemQuestData questData;
    private TextMeshProUGUI questText;
    private Inventory inventory;

    private void Start()
    {
        questText = GetComponent<TextMeshProUGUI>();
        inventory = GameManager.Instance.Inventory;
        inventory.OnUpdateInventory += UpdateQuestText;
        UpdateQuestText(questData.ConditionItem.ItemType);
    }

    private void UpdateQuestText(ItemType type)
    {
        if (type != questData.ConditionItem.ItemType) return;
        int haveAmount = inventory.GetHasItemAmount(type);
        string text = $"{questData.QuestText} ({haveAmount}/{questData.ConditionAmount})";
        if(haveAmount == questData.ConditionAmount)
        {
            text = $"<color=green>{text}</color>";
        }
        questText.text = text;
    }
}
