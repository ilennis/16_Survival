using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public string buildingName;
    public int cost; // 이거 나중에 수정.
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
