using UnityEngine;

public class Water : MonoBehaviour,IInteractable
{
    [SerializeField]
    private FoodData waterData; //획득 자원 데이터

    public string GetInfo()
    {
        //TODO : 인벤토리에 아이템을 최대소지수 만큼 가지고 있으면 더이상 채집 못한다고 표시
        return $"F키를 눌러서 물 뜨기";
    }

    public void Interact()
    {
        //TODO : 인벤토리에 추가
        //TODO : 인벤토리에 아이템을 최대소지수 만큼 가지고 있으면 채집 불가
        Debug.Log($"인벤토리에{waterData.ItemName}을 인벤토리에 추가하였습니다!");
    }
}
