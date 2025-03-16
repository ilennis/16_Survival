using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingCell : MonoBehaviour
{
    [SerializeField] private Image craftItemImage;
    [SerializeField] private Button craftButton;
    [SerializeField] private Image craftedPanel;
    [SerializeField] private Image lockedPanel;
    [SerializeField] private TextMeshProUGUI unlockConditionText;
    [SerializeField] private MaterialIcon[] materialIcons;

    private Inventory inventory;

    private void Start()
    {
        inventory = GameManager.Instance.Inventory;
        craftButton.onClick.AddListener(CraftItem);
    }

    public RecipeData CraftingRecipe
    {
        get => craftingRecipe;
        set
        {
            craftingRecipe = value;
            SetCrafringCell();
        }
    }

    private RecipeData craftingRecipe;

    private void SetCrafringCell()
    {
        bool isCanUnlock = craftingRecipe.IsCanUnlock(); 
        lockedPanel.gameObject.SetActive(!isCanUnlock); //해방 가능 여부에 따라 해방이미지 활성화여부 결정
        if (craftingRecipe.IsCrafted()) //제작이 완료되어있으면
        {
            ChangeCraftedState(); //제작 완료상태로 변환
            return;
        }
        SetIcon(); // 아이콘 설정
        if (!isCanUnlock) //해방이 불가능하면
        {           
            unlockConditionText.text = $"{craftingRecipe.GetUnlockConditionText()}"; //해방 조건 표시
            return;
        }
        craftButton.interactable = craftingRecipe.IsCanCraft(); // 제작 가능 여부에 따라 제작 버튼 활성화 여부 결정
    }

    private void SetIcon(bool isCrafted=false)
    {
        craftItemImage.sprite = craftingRecipe.CraftItem.Icon;

        for (int i = 0; i < craftingRecipe.Materials.Length; i++)
        {
            materialIcons[i].gameObject.SetActive(true);
            materialIcons[i].SetIcon(craftingRecipe.Materials[i], isCrafted);
        }
    }

    //아이템 제작
    private void CraftItem()
    {
        foreach(var material in CraftingRecipe.Materials)
        {
            inventory.UseItem(material.Item, material.Amount);  //재료 사용
        }
        inventory.AddItem(CraftingRecipe.CraftItem, 1);  //인벤토리에 추가
        //제작이 한번만 가능하면
        if (CraftingRecipe.IsCraftOnce)
        {
            craftButton.interactable = false; //제작 버튼 비활성화
            DataManager.Instance.CraftedOnceRecipeList.Add(CraftingRecipe, true); //제작 완료 레시피 리스트에 추가
            ChangeCraftedState(); //제작 완료 상태로 변환
        }
    }

    private void ChangeCraftedState()
    {
        craftedPanel.gameObject.SetActive(true);
        SetIcon(craftingRecipe.IsCrafted());
    }
}
