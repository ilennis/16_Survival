using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentDamage : MonoBehaviour
{
    public int damage;
    public float damageRate;
    public Sprite zoneMark;
    public Image UIMark;

    private List<IDamage> things = new List<IDamage>();

    private void Start()
    {
        InvokeRepeating("DealDamage", 0, damageRate);
        UIMark.gameObject.SetActive(false);
    }
    void DealDamage()
    {
        for (int i = 0; i < things.Count; i++)
        {
            things[i].TakeDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamage damagable))
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                things.Add(damagable);
                UIMark.sprite = zoneMark;
                UIMark.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamage damagable))
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                things.Remove(damagable);
                UIMark.gameObject.SetActive(false);
            }
        }
    }
}
