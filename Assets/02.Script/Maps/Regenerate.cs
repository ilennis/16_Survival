using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Regenerate : MonoBehaviour
{


    [Header("RegenerateArea")]
    [SerializeField] private List<Bounds> regenerateAreaA;
    [SerializeField] private List<Bounds> regenerateAreaB;
    [SerializeField] private List<Bounds> regenerateAreaC;
    [SerializeField] private List<GameObject> regeneratePrefab;

    public int resourceMaxAmountA;
    public int resourceMaxAmountB;
    public int resourceMaxAmountC;

    public int resourceAmountA;
    public int resourceAmountB;
    public int resourceAmountC;

    private void Start()
    {
        Respwon();
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
            GameObject randomPrefab = regeneratePrefab[Random.Range(0, regeneratePrefab.Count)];
            Bounds randomArea = regenerateAreaA[Random.Range(0, regenerateAreaA.Count)];

            Vector3 randomPosition = new Vector3(Random.Range(randomArea.min.x, randomArea.max.x), 0, Random.Range(randomArea.min.z, randomArea.max.z));

            Instantiate(randomPrefab, randomPosition, Quaternion.identity);
            //Debug.Log($"생성 성공!{randomPosition}");
        }
        for (; resourceAmountB < resourceMaxAmountB; resourceAmountB++)
        {
            GameObject randomPrefab = regeneratePrefab[Random.Range(0, regeneratePrefab.Count)];
            Bounds randomArea = regenerateAreaB[Random.Range(0, regenerateAreaB.Count)];

            Vector3 randomPosition = new Vector3(Random.Range(randomArea.min.x, randomArea.max.x), 0, Random.Range(randomArea.min.z, randomArea.max.z));

            Instantiate(randomPrefab, randomPosition, Quaternion.identity);
            //Debug.Log($"생성 성공!{randomPosition}");
        }
        for (; resourceAmountC < resourceMaxAmountC; resourceAmountC++)
        {
            GameObject randomPrefab = regeneratePrefab[Random.Range(0, regeneratePrefab.Count)];
            Bounds randomArea = regenerateAreaC[Random.Range(0, regenerateAreaC.Count)];

            Vector3 randomPosition = new Vector3(Random.Range(randomArea.min.x, randomArea.max.x), 0, Random.Range(randomArea.min.z, randomArea.max.z));

            Instantiate(randomPrefab, randomPosition, Quaternion.identity);
            //Debug.Log($"생성 성공!{randomPosition}");
        }
    }

    private void OnDrawGizmosSelected()
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
