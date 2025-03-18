using TMPro;
using UnityEngine;

public class InfoText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI infoText;
    private Interaction interaction;

    private void Start()
    {
        interaction = GameManager.Instance.Player.Interaction;
        interaction.OnCheckItemEvent += UpdateInfoText;
    }

    private void OnDestroy()
    {
        interaction.OnCheckItemEvent -= UpdateInfoText;
    }

    private void UpdateInfoText(IInteractable checkItem)
    {
        infoText.gameObject.SetActive(checkItem != null);
        if (checkItem != null)
        {
            infoText.text = checkItem.GetInfo();
        }
    }
}
