using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingUI : MonoBehaviour
{
    public static BuildingUI Instance;

    [Header("UI Elements")]
    public GameObject buildingMenu;
    public GameObject[] buildingSlots; // Slots for 1-9 selection
    public Image[] materialIcons; // Icons for materials
    public TextMeshProUGUI[] materialCosts; // Text for material amount


    private bool isMenuOpen = false;
    private BuildManager buildManager;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleBuildingMenu();
        }

        if (isMenuOpen)
        {
            for (int i = 0; i < 9; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    SelectBuilding(i);
                }
            }
        }
    }

    // Press B to open. 
    // While pressing B, use 1~9 number key to select.

    void ToggleBuildingMenu()
    {
        isMenuOpen = !isMenuOpen;
        buildingMenu.SetActive(isMenuOpen);
    }

    void SelectBuilding(int index)
    {
        if (buildManager != null)
        {
            buildManager.SetBuilding(index);
            ToggleBuildingMenu();
        }
    }
}
