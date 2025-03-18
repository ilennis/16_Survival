using UnityEngine;
using UnityEngine.UI;

public class CheatButton : MonoBehaviour
{
    private Button button;
    private Inventory inventory;
    [SerializeField] ItemData addItem;
    [SerializeField] int addAmount;

    private void Start()
    {
        button = GetComponent<Button>();
        inventory = GameManager.Instance.Inventory;
        button.onClick.AddListener(AddItem);
    }

    private void AddItem()
    {
        inventory.AddItem(addItem, addAmount);
    }
}
