using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;  // NavMeshAgent 변수
    private Transform targetPlayer;     // 플레이어의 위치를 나타내는 변수 이름을 변경

    public float detectionRange = 10f;  // 플레이어를 감지할 거리
    private bool isChasing = false;     // 추적 여부

    void Start()
    {
        // NavMeshAgent 컴포넌트 가져오기
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent is missing from the GameObject.");
            return;  // NavMeshAgent가 없으면 실행을 멈춤
        }

        // 플레이어의 위치 찾기
        targetPlayer = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (targetPlayer == null)
        {
            Debug.LogError("Player object with tag 'Player' not found.");
            return;  // 플레이어가 없으면 실행을 멈춤
        }

        Debug.Log("NavMeshAgent and Player are properly assigned.");
    }

    void Update()
    {
        // targetPlayer나 navMeshAgent가 초기화되지 않았다면 Update를 종료
        if (targetPlayer == null || navMeshAgent == null)
        {
            return;
        }

        // targetPlayer와의 거리 계산
        float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);

        // 플레이어가 범위 내에 있으면 추적
        if (distanceToPlayer < detectionRange)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        // 추적 중일 때
        if (isChasing)
        {
            navMeshAgent.SetDestination(targetPlayer.position);  // 플레이어를 향해 이동
        }
        else
        {
            PatrolArea();  // 플레이어를 추적하지 않으면 배회
        }
    }

    void PatrolArea()
    {
        // 예시: 주기적으로 이동할 랜덤 지점 설정
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            Vector3 randomPoint = new Vector3(
                Random.Range(-10f, 10f),
                transform.position.y,
                Random.Range(-10f, 10f)
            );
            navMeshAgent.SetDestination(randomPoint);
        }
    }
}
