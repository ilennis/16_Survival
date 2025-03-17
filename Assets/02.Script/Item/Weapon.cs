using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData Data;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    public void OnHit()
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 0.1f))
        {
            if (hit.collider.TryGetComponent(out IDamageable damageable))
            {
                float damageAmount = GetDamageAmount(damageable.DamageType);
                if (damageAmount == 0) return;
                damageable.Damage(damageAmount);
            }
        }
    }

    //데미지를 받는 오브젝트 타입에 따라 데미지 값 반환
    private float GetDamageAmount(DamageType type)
    {
        if (type == DamageType.Enemy)
        {
            return Data.AttackDamage;
        }
        else if(type == DamageType.Resource)
        {
            return Data.ResourceDamage;
        }
        return 0;
    }
}
