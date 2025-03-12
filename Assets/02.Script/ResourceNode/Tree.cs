using UnityEngine;

public class Tree : MonoBehaviour,IInteractable
{
    [SerializeField]
    private ItemData resourceData; //획득 자원 데이터

    [SerializeField]
    private ResourceNodeData NodeData; //채집 가능한 오브젝트 데이터

    private int treeHp;
    private int treeYield;

    private void Start()
    {
        treeHp = NodeData.Hp;
        treeYield = NodeData.Yield;
    }

    public string GetInfo()
    {
        //TODO : 인벤토리에 아이템을 최대소지수 만큼 가지고 있으면 더이상 채집 못한다고 표시
        return $"마우스 왼쪽버튼을 눌러 나무베기";
    }

    //상호 작용 실행
    public void Interact()
    {
        treeHp -= treeYield;
        //TODO : 인벤토리에 추가
        //TODO : 인벤토리에 아이템을 최대소지수 만큼 가지고 있으면 채집 불가
        Debug.Log($"인벤토리에{resourceData.ItemName}을 인벤토리에 추가하였습니다!");

        if (treeHp <= 0) //더이상 채집 불가능하면
        {
            Destroy(gameObject); //오브젝트 파괴
        }
    }

}
