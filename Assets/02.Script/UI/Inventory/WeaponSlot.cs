using System.Collections.Generic;
using UnityEngine.EventSystems;

public class WeaponSlot : InventorySlot
{
    public int SlotIndex;

    private Dictionary<int, WeaponData> setWeaponDataList = new();
    private WeaponController weaponController;

    protected override void Start()
    {
        base.Start();
        weaponController = GameManager.Instance.Player.WeaponController;
        setWeaponDataList = DataManager.Instance.SetWeaponDataList;
    }

    public override void OnDrop(PointerEventData eventData) => slotDragAndDrop.OnDropWeaponEvent?.Invoke(this, eventData);

    //슬롯에 무기 장착
    public void SetWeapon(int slotNumber, WeaponData weapon)
    {            
        setWeaponDataList.Add(slotNumber, weapon);
        weaponController.EquipWeapon(weapon, slotNumber);
    }

    //슬롯에서 무기 장착해제
    public void ClearWeapon(int slotNumber, WeaponData weapon)
    {
        setWeaponDataList.Remove(slotNumber);
        weaponController.ActiveWeapon(weapon,false);
    }
}
