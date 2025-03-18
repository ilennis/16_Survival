using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tent : Building
{
    public float healRadius = 5f;
    public float healAmount = 10f;
    private float healInterval = 3f;
    private float nextHealTime = 0f;
    private void FixedUpdate()
    {
        if(Time.deltaTime >= nextHealTime)
        {
            HealNearbyPlayers();
            nextHealTime = Time.deltaTime + healInterval;
        }
    }
    void HealNearbyPlayers()
    {
        Collider[] players = Physics.OverlapSphere(transform.position, healRadius); // 콜라이더 대신에 OverlapSphere 사용

        foreach (Collider player in players)
        {
            if (player.CompareTag("Player"))
            {
                Debug.Log("체력 회복: " + healAmount);
                
            }
        }
    }
    public override void Upgrade()
    {
        base.Upgrade();
        healRadius += 2f; // 레벨업 할때마다 힐 거리 높이기
        healAmount += 5f; // 레벨업 하면 힐 양 늘리기
        Debug.Log("업그레이드 완료. 힐범위: " + healRadius + ", 힐량: " + healAmount + "증가");
    }
}
