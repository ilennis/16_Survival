using UnityEngine;
using System.Collections;

public class TreeDate : MonoBehaviour, IInteractable, IDamageable
{
    [SerializeField] private ItemData resourceData; //획득 자원 데이터
    [SerializeField] private ResourceNodeData NodeData; //채집 가능한 오브젝트 데이터
    [SerializeField] private int GatheringCooldown = 180; //채집 쿨다운
    
    public DamageType DamageType => DamageType.Resource;

    public bool IsCanCollect => inventory.GetCanAddItemSlot(resourceData);

    private int treeHp;
    private int treeYield;

    private bool isCanGathering = true;
    private Inventory inventory;

    private void Start()
    {
        treeHp = NodeData.Hp;
        treeYield = NodeData.Yield;

        inventory = GameManager.Instance.Inventory;
    }

    public string GetInfo()
    {
        //무기를 장착하고 있지않으면 표시하지않기
        if (GameManager.Instance.Player.WeaponController.CurrentWeapon == null) return string.Empty;
        if(!IsCanCollect)
        {
            return "인벤토리가 꽉찼습니다";
        }
        if (!isCanGathering)
        {
            return "더이상 채집이 불가합니다";
        }
        return $"마우스 왼쪽버튼을 눌러 나무베기";
    }

    private IEnumerator ICooldownGathering()
    {
        isCanGathering = false;
        yield return new WaitForSecondsRealtime(GatheringCooldown);
        isCanGathering = true;
    }

    public void Damage(float damage)
    {
        if (!isCanGathering) return;
        if (!IsCanCollect) return;

        var addAmount = treeYield * (int)damage;
        treeHp -= addAmount;
        inventory.AddItem(resourceData, addAmount);
        Debug.Log($"인벤토리에{resourceData.ItemName}을 {addAmount}만큼 추가하였습니다!");

        if (treeHp <= 0) //더이상 채집 불가능하면
        {
            StartCoroutine(ICooldownGathering()); //채집 쿨다운
        }
    }

    public void Collect() { }
}
