using System;
using System.Collections;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public LayerMask InteractionLayer; // 감지할 레이어
    public float MaxCheckDistance = 3.0f; // 레이의 최대 감지 거리
    public IInteractable Checkedtem; // 감지한 아이템

    private GameObject checkObject; // 감지한 물체

    public Action<IInteractable> OnCheckItemEvent = delegate { }; // 아이템 감지 이벤트

    private void Start()
    {
        StartCoroutine(ICheckItem());
    }

    private IEnumerator ICheckItem()
    {
        while (true)
        {
            CheckItem();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void CheckItem()
    {
        //레이를 쏠 위치 설정
        Vector3 rayPos = transform.position + Vector3.up * 0.35f;
        //레이의 감지 범위 설정
        float sphereRadius = 2.0f;
        Ray ray = new Ray(rayPos, transform.forward);
        //레이 표시
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * MaxCheckDistance, Color.red);
        //레이 충돌
        //SphereCast를 이용해 레이 폭을 늘리기
        if (Physics.SphereCast(ray, sphereRadius, out RaycastHit hit, MaxCheckDistance, InteractionLayer))
        {
            if (hit.collider.gameObject != checkObject)
            {
                if (hit.collider.TryGetComponent(out IInteractable item))
                {
                    checkObject = hit.collider.gameObject;
                    Checkedtem = item;
                }
                else
                {
                    NotCheckedItem();
                }
            }
        }
        else
        {
            NotCheckedItem();
        }
        OnCheckItemEvent?.Invoke(Checkedtem);
    }

    //감지가 안되면
    private void NotCheckedItem()
    {
        checkObject = null;
        Checkedtem = null;
    }
}
