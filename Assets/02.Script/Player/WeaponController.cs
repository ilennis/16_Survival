using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [SerializeField] Animator animator;

    private Dictionary<int, WeaponData> setWeaponDataList = new();
    private Dictionary<WeaponData, Weapon> createdWeapons = new();

    public Weapon CurrentWeapon;

    private int currentWeaponIndex = 1;
    private int slotCount = 3; //무기 슬롯수

    private void Start()
    {
        setWeaponDataList = DataManager.Instance.SetWeaponDataList;
        createdWeapons = DataManager.Instance.CreatedWeapons;
    }

    private void Update()
    {
        float scrollValue = Mouse.current.scroll.ReadValue().y;

        if (scrollValue > 0)
        {
            ChangeWeapon(-1);
        }
        else if (scrollValue < 0)
        {
            ChangeWeapon(1);
        }
    }

    //무기 변경
    private void ChangeWeapon(int direction)
    {
        if (createdWeapons.Count < 2) return; // 무기가 2개 미만이면 변경 불가

        int previousIndex = currentWeaponIndex;
        int maxIndex = setWeaponDataList.ContainsKey(slotCount) ? slotCount : setWeaponDataList.Count;

        //현재 무기 슬롯이 최솟값이나 최댓값일때 무기 변경 불가
        if ((direction < 0 && currentWeaponIndex == 1) || (direction > 0 && currentWeaponIndex == maxIndex))
        {
            return;
        }
        // 이전 무기 해제
        if (setWeaponDataList.TryGetValue(previousIndex, out WeaponData prevWeapon))
        {
            ActiveWeapon(prevWeapon, false);
        }
        // 인덱스 변경 (direction에 따라 증가/감소)
        currentWeaponIndex += direction;
        // 새 무기 장착
        if (setWeaponDataList.TryGetValue(currentWeaponIndex, out WeaponData newWeapon))
        {
            ActiveWeapon(newWeapon, true);
        }
        Debug.Log($"현재 무기 슬롯: {currentWeaponIndex}");
    }

    //무기 슬롯에 장착
    public void EquipWeapon(WeaponData data, int index)
    {
        if (!createdWeapons.ContainsKey(data))
        {
            var weaponPrefab = Instantiate(data.ItemPrefab, Camera.main.transform);
            Weapon weapon = weaponPrefab.GetComponent<Weapon>();
            createdWeapons.Add(data, weapon);
        }
        if (index == currentWeaponIndex)
        {
            ActiveWeapon(data, true);
        }
    }

    //무기 활성화 비활성화
    public void ActiveWeapon(WeaponData data, bool isEquip)
    {
        createdWeapons[data].gameObject.SetActive(isEquip);
        CurrentWeapon = isEquip ? createdWeapons[data] : null;
    }

    //무기 공격 애니메이션
    public void Attack()
    {
        if (CurrentWeapon == null) return;
        CurrentWeapon.PlayAttackAnimation();
    }
}
