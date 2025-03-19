using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Regenerate : MonoBehaviour
{
    [Header("RegenerateArea")]
    [SerializeField] private List<Bounds> regenerateAreaA;//각 구역 별 위치를 지정해서 저장하는 리스트
    [SerializeField] private List<Bounds> regenerateAreaB;
    [SerializeField] private List<Bounds> regenerateAreaC;//위의 List를 다시 List에 넣을 수 있을까?
    [SerializeField] private List<GameObject> regeneratePrefab;//생성되는 프리펩 리스트
    [SerializeField] private float _regenerateStartTime;//씬 스타트 후 생성될 때 까지 지연 시간

    public int resourceMaxAmountA;//에리어 별 최대 생성 갯수
    public int resourceMaxAmountB;
    public int resourceMaxAmountC;

    public int resourceAmountA;//에리어 별 현재 오브젝트 갯수
    public int resourceAmountB;
    public int resourceAmountC;

    private void Start()
    {
        InvokeRepeating("Respwon", _regenerateStartTime, RegenerateManager.Instance.RegenerateAttribute.dayCycle.fullDayLength);//리젠 반복, 주야 주기로 한 번 리젠
    }
    void Respwon()
    {
        if (regeneratePrefab == null)
        {
            Debug.LogError("프리펩이 없습니다!");
            return;
        }
        for (; resourceAmountA < resourceMaxAmountA; resourceAmountA++)
        {
            GameObject randomPrefab = regeneratePrefab[Random.Range(0, regeneratePrefab.Count)];//프리펩 리스트에서 생성할 프리펩 선택
            Bounds randomArea = regenerateAreaA[Random.Range(0, regenerateAreaA.Count)];//구역 리스트에서 생성할 구역 선택 

            Vector3 randomPosition = new Vector3(Random.Range(randomArea.min.x, randomArea.max.x), randomArea.center.y, Random.Range(randomArea.min.z, randomArea.max.z));//구역에서 랜덤 좌표 선택

            GameObject RegenObject = Instantiate(randomPrefab, randomPosition, Quaternion.identity);//프리팹 인스턴스 생성
            ObjectAmount regenObject = RegenObject.AddComponent<ObjectAmount>();//인스턴스에 스크립트 추가

            regenObject.regenerate = this;
            regenObject.areaCode = 1001;//구역별 갯수를 카운트할 코드
            //Debug.Log($"생성 성공!{randomPosition}");
        }
        for (; resourceAmountB < resourceMaxAmountB; resourceAmountB++)
        {
            GameObject randomPrefab = regeneratePrefab[Random.Range(0, regeneratePrefab.Count)];
            Bounds randomArea = regenerateAreaB[Random.Range(0, regenerateAreaB.Count)];

            Vector3 randomPosition = new Vector3(Random.Range(randomArea.min.x, randomArea.max.x), randomArea.center.y, Random.Range(randomArea.min.z, randomArea.max.z));

            GameObject RegenObject = Instantiate(randomPrefab, randomPosition, Quaternion.identity);//프리팹 인스턴스 생성
            ObjectAmount regenObject = RegenObject.AddComponent<ObjectAmount>();//인스턴스에 스크립트 추가

            regenObject.regenerate = this;
            regenObject.areaCode = 1002;
            //Debug.Log($"생성 성공!{randomPosition}");
        }
        for (; resourceAmountC < resourceMaxAmountC; resourceAmountC++)
        {
            GameObject randomPrefab = regeneratePrefab[Random.Range(0, regeneratePrefab.Count)];
            Bounds randomArea = regenerateAreaC[Random.Range(0, regenerateAreaC.Count)];

            Vector3 randomPosition = new Vector3(Random.Range(randomArea.min.x, randomArea.max.x), randomArea.center.y, Random.Range(randomArea.min.z, randomArea.max.z));

            GameObject RegenObject = Instantiate(randomPrefab, randomPosition, Quaternion.identity);//프리팹 인스턴스 생성
            ObjectAmount regenObject = RegenObject.AddComponent<ObjectAmount>();//인스턴스에 스크립트 추가

            regenObject.regenerate = this;
            regenObject.areaCode = 1003;
            //Debug.Log($"생성 성공!{randomPosition}");
        }
    }

    private void OnDrawGizmosSelected()//에리어 가시화, 색깔 별로 나눠 표시
    {
        if (regenerateAreaA == null)
        {
            return;
        }
        if (regenerateAreaB == null)
        {
            return;
        }
        if (regenerateAreaC == null)
        {
            return;
        }
        foreach (var area in regenerateAreaA)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(area.center, area.size);
        }
        foreach (var area in regenerateAreaB)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(area.center, area.size);
        }
        foreach (var area in regenerateAreaC)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(area.center, area.size);
        }
    }
}
