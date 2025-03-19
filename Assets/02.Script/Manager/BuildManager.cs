using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{

    public static BuildManager Instance;

    public Camera playerCamera;
    public GameObject[] buildingPrefabs; // 빌딩 Prefab. 리스트
    public GameObject placementIndicator; // 가능하면 구현. 투명한 모양으로 빌딩 건설 Preview

    public LayerMask groundLayerMask; // 땅에 Raycast 닿으면 구현
    public int selectedBuildingIndex = 0;
    public float maxPlacementDistance = 5f;
    // public Text errorText;

    private GameObject currentPreview;

    public float spaceSize = 1.0f; // 건물 사이 뭔가 없어야하는 간격.
    public float rotationAngle = 45f; // 돌리는 각도 일단 45도로. 
    private Quaternion currentRotation = Quaternion.identity; // 지금 현제 각도

    private bool isBuilding = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (isBuilding)
            {
                Debug.Log("Canceling Buliding");
                CancelBuilding();
            }
            else
            {
                Debug.Log("starting Buliding");
                StartBuilding();
            }
        }

        if (isBuilding)
        {
            // UpdatePreviewPosition();

            if (Input.GetMouseButtonDown(0))
            {
                PlaceBuilding();
            }
        }
    }
    void StartBuilding()
    {
        isBuilding = true;
        
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 20, groundLayerMask))
        {
            currentPreview.transform.position = hit.point;

            currentPreview = Instantiate(buildingPrefabs[selectedBuildingIndex], hit.transform);
        }
       
       
        // currentPreview.GetComponent<Collider>().enabled = false; // 콜리션 끄기
    }
    void CancelBuilding()
    {
        isBuilding = false;
        
        Destroy(currentPreview);
    }

    void PlaceBuilding()
    {
        Instantiate(buildingPrefabs[selectedBuildingIndex], currentPreview.transform.position, Quaternion.identity);
        CancelBuilding();
    }
 

    public void SetBuilding(int index)
    {
        if (index >= 0 && index < buildingPrefabs.Length)
        {
            selectedBuildingIndex = index;
        }
    }


    
    bool CheckResources(int buildingIndex)
    {
        // 인벤토리 확인 용도. 이건 좀 나중에 인벤토리 시스템 어떻게 돌아가는지 확인 필요
        return true;
    }
    bool DeductResources(int buildingIndex)
    {
        // 인벤토리에서 재료 빼기 용도.
        return true;
    }
    
}
