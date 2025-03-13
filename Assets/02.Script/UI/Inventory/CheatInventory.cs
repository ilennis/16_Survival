using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatInventory : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;
    
    [SerializeField]
    private Button[] cheatButtons;

    [SerializeField]
    private ItemData[] itemDatas;

    [SerializeField]
    private int addAxeCount = 1;
    [SerializeField]
    private int addWoodCount = 23;
    [SerializeField]
    private int addRockCount = 7;
    [SerializeField]
    private int UseRockCount = 7;

    private void Start()
    {
        cheatButtons[0].onClick.AddListener(AddAxe);
        cheatButtons[1].onClick.AddListener(AddWood);
        cheatButtons[2].onClick.AddListener(AddRock);
        cheatButtons[3].onClick.AddListener(UseRock);
    }

    private void AddAxe()
    {
        inventory.AddItem(itemDatas[0], addAxeCount);
    }

    private void AddWood()
    {
        inventory.AddItem(itemDatas[1], addWoodCount);
    }

    private void AddRock()
    {
        inventory.AddItem(itemDatas[2], addRockCount);
    }

    private void UseRock()
    {
        inventory.UseItem(itemDatas[2], UseRockCount);
    }
}
