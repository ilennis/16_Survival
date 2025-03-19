using TMPro;
using UnityEngine;

public class InfoText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI infoTextWater;
    private Interaction interaction;

    private void Start()
    {
        interaction = GameManager.Instance.Player.Interaction;
        interaction.OnCheckItemEvent += UpdateInfoText;
        interaction.OnCheckWaterEvent += UpdateWaterInfoText;
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

    private void UpdateWaterInfoText(IInteractable checkItem)
    {
        infoTextWater.gameObject.SetActive(checkItem != null);
        if (checkItem != null)
        {
            infoTextWater.text = checkItem.GetInfo();
        }
    }
}
