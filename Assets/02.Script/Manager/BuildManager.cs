using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject[] buildingPrefabs; // 빌딩 Prefab. 리스트
    public GameObject placementIndicator; // 가능하면 구현. 투명한 모양으로 빌딩 건설 Preview
    public LayerMask groundLayerMask; // 땅에 Raycast 닿으면 구현
    public int selectedBuildingIndex = 0;
    public float maxPlacementDistance = 5f;
    public Text errorText;

    private GameObject currentPreview;
    private bool canPlace = false; // true 여야지 된다.

    public float spaceSize = 1.0f; // 건물 사이 뭔가 없어야하는 간격.
    public float rotationAngle = 45f; // 돌리는 각도 일단 45도로. 
    private Quaternion currentRotation = Quaternion.identity; // 지금 현제 각도

    // Update is called once per frame
    void Update()
    {
        // 문제: 어디에다가 해야지 이 버튼을 누르면 이렇게 들어가는가 정의하지?
        AimBuilding();
    }

    void AimBuilding()
    {
        // Raycast로 카메라 조준하는 점 확인.
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)); // 스크린 한가운데
        RaycastHit hit;

       
        // Raycast에 맞으면 설치할 위치 표기하기. 
        if (Physics.Raycast(ray, out hit, maxPlacementDistance, groundLayerMask))
        {
            Vector3 snappedPosition = GetSnappedPosition(hit.point);
            Debug.Log("Hitting");
            if (currentPreview == null) 
            {
                currentPreview = Instantiate(placementIndicator);
            }

            currentPreview.transform.position = snappedPosition;
            currentPreview.transform.rotation = currentRotation;
            canPlace = !Physics.CheckBox(hit.point, currentPreview.transform.localScale / 2); // Check for collisions
        }
        else // 해당 사항 안되면 Preview 없에기 (Raycast가 땅에 닿지 않을때)
        {
            canPlace = false;
            if (currentPreview != null) 
            {
                Destroy(currentPreview);
            }
        }

    }
    void PlaceBuilding()
    {
        if (!CheckResources(selectedBuildingIndex))
        {
            Debug.Log("Not enough materials!");
            return;
        }

        if (currentPreview != null)
        {
            Instantiate(buildingPrefabs[selectedBuildingIndex], currentPreview.transform.position, currentRotation);
            DeductResources(selectedBuildingIndex);
            Destroy(currentPreview);
        }
    }
    Vector3 GetSnappedPosition(Vector3 rawPosition) // 정수 값으로만 딱딱 이동할 수 있도록 만드는 Vector3 값
    {
        float x = Mathf.Round(rawPosition.x / spaceSize) * spaceSize;
        float z = Mathf.Round(rawPosition.z / spaceSize) * spaceSize;
        return new Vector3(x, rawPosition.y, z);
    }

    bool CheckResources(int buildingIndex)
    {
        // 인벤토리 확인 용도. 
        return true;
    }
    bool DeductResources(int buildingIndex)
    {
        // 인벤토리에서 재료 빼기 용도.
        return true;
    }
}
