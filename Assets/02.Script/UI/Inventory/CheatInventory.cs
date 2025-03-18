using UnityEngine;
using UnityEngine.UI;

public class CheatInventory : MonoBehaviour
{
    [SerializeField] private Button debugToolActiveButton;
    [SerializeField] private ScrollRect debugTool;

    private bool isActive = false;

    private void Start()
    {
        debugToolActiveButton.onClick.AddListener(ActiveDebugTool);
    }

    private void ActiveDebugTool()
    {
        isActive = !isActive;
        debugTool.gameObject.SetActive(isActive);
    }
}
