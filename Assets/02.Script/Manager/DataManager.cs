using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    
    public Dictionary<ItemType, int> HaveItemCountList = new();     // 각 아이템 소지수

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
