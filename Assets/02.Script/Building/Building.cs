using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public string buildingName;
    public BuildingCost[] costRequired; // 이제 코스트에 아이템 + 양 추가 가능. 각 BuildingCost마다 필요양.
    // public int cost; // 코스트는 숫자만
    public int level = 1; // 기본 레벨 1
    public bool isUpgradable = true;

    public virtual void Upgrade()
    {
        if(isUpgradable)
        {
            level++;
            Debug.Log(buildingName + "upgraded to Level" + level);
        }
    }
    public virtual void Dismantle()
    {
        Debug.Log(buildingName + " dismantled.");
        Destroy(gameObject);
    }
}
