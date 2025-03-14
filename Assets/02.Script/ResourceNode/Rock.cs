using UnityEngine;

public class Rock : MonoBehaviour, IInteractable
{
    [SerializeField]
    private ItemData resourceData; //획득 자원 데이터

    [SerializeField]
    private ResourceNodeData NodeData; //채집 가능한 오브젝트 데이터

    private int rockHp;
    private int rockYield;

    private void Start()
    {
        rockHp = NodeData.Hp;
        rockYield = NodeData.Yield;
    }

    public string GetInfo()
    {
        //TODO : 인벤토리에 아이템을 최대소지수 만큼 가지고 있으면 더이상 채집 못한다고 표시
        return $" F키를 눌러 채집";
    }

    //상호 작용 실행
    public void Interact()
    {
        rockHp -= rockYield;
        //TODO : 인벤토리에 추가
        //TODO : 인벤토리에 아이템을 최대소지수 만큼 가지고 있으면 채집 불가
        Debug.Log($"인벤토리에{resourceData.ItemName}을 인벤토리에 추가하였습니다!");

        if (rockHp <= 0) //더이상 채집 불가능하면
        {
            Destroy(gameObject); //오브젝트 파괴
        }
    }
}

