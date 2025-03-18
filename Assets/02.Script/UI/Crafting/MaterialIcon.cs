using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialIcon : MonoBehaviour
{
    [SerializeField] private Image materialIconImage;
    [SerializeField] private TextMeshProUGUI materialAmountText;

    private CraftingMaterial materialData;

    public void SetIcon(CraftingMaterial data,bool isCanUnlock ,bool isCrafted)
    {
        materialData = data;
        materialIconImage.sprite = materialData.Item.Icon;
        if (isCrafted || !isCanUnlock) //제작 완료 상태이면
        {
            materialAmountText.gameObject.SetActive(false);
            return;
        }
        materialAmountText.gameObject.SetActive(true);
        UpdateAmount(); //아이템 소지수 / 필요한 아이템수 텍스트 업데이트
    }

    private void UpdateAmount()
    {
        //아이템 소지수 가져오기
        int haveAmount = GameManager.Instance.Inventory.GetHasItemAmount(materialData.Item.ItemType);

        //아이템 소지수를 판단해서 충분하면 초록색,부족하면 빨간색으로 텍스트 표시
        var haveItemText = haveAmount >= materialData.Amount ?
            $"<color=green>{haveAmount}</color>"
            : $"<color=red>{haveAmount}</color>";
        materialAmountText.text = $"{haveItemText}/{materialData.Amount}";
    }
}
