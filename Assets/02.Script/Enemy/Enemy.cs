using UnityEngine;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;  // NavMeshAgent 변수
    private Transform targetPlayer;     // 플레이어의 위치를 나타내는 변수 이름을 변경

    public float detectionRange = 10f;  // 플레이어를 감지할 거리
    private bool isChasing = false;     // 추적 여부
    private float attackDistance = 1.5f; // 공격 범위

    public enum State
    {
        Idle,
        Walk,
        Run,
        Attack,
        Die
    }

    public State currentState;  // 현재 상태
    private Animator animator;  // Animator 컴포넌트

    private float walkSpeed = 2f;   // 걷는 속도
    private float runSpeed = 5f;    // 달리는 속도
    private float idleWalkSwitchTime = 4f; // Idle과 Walk을 번갈아가며 전환할 시간 간격
    private float timeSinceLastSwitch = 0f; // 마지막 상태 전환 시간
    private Vector3 randomPatrolPosition; // 랜덤 서성거리 위치
    private float patrolTime = 3f;  // 서성거리기 시간 간격
    private float timeSinceLastPatrol = 0f; // 마지막 서성거리 시간

    void Start()
    {
        // 컴포넌트 초기화
        animator = GetComponent<Animator>();
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

        // 초기 상태 설정
        currentState = State.Idle;
        navMeshAgent.speed = walkSpeed;

        // 초기 랜덤 서성거리 위치 설정
        SetRandomPatrolPosition();
    }

    void Update()
    {
        // targetPlayer나 navMeshAgent가 초기화되지 않았다면 Update를 종료
        if (targetPlayer == null || navMeshAgent == null)
        {
            return;
        }

        // 상태에 따른 동작 처리
        switch (currentState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Walk:
                Walk();
                break;
            case State.Run:
                Run();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Die:
                Die();
                break;
        }
    }

    private void Idle()
    {
        timeSinceLastSwitch += Time.deltaTime;

        // Idle 상태에서 일정 시간이 지나면 Walk 상태로 전환
        if (timeSinceLastSwitch >= idleWalkSwitchTime)
        {
            currentState = State.Walk;
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
            navMeshAgent.isStopped = false;
            timeSinceLastSwitch = 0f;
            SetRandomPatrolPosition();
            navMeshAgent.SetDestination(randomPatrolPosition);
        }
    }

    private void Walk()
    {
        timeSinceLastPatrol += Time.deltaTime;

        // 플레이어를 감지하면 Run 상태로 전환
        float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);
        if (distanceToPlayer <= detectionRange)
        {
            currentState = State.Run;
            navMeshAgent.SetDestination(targetPlayer.position);  // 플레이어를 향해 이동
        }
        else if (timeSinceLastPatrol >= patrolTime)  // 일정 시간이 지나면 랜덤 위치로 이동
        {
            SetRandomPatrolPosition();
            navMeshAgent.SetDestination(randomPatrolPosition);
            timeSinceLastPatrol = 0f;  // 시간 초기화
        }

        // 애니메이션 전환
        animator.SetBool("isWalking", true);
        animator.SetBool("isRunning", false);
        navMeshAgent.speed = walkSpeed;
    }

    private void Run()
    {
        // 플레이어와 거리가 가까워지면 공격 상태로 전환
        float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);
        if (distanceToPlayer <= attackDistance)
        {
            currentState = State.Attack;
            animator.SetTrigger("Attack");  // 공격 애니메이션 실행
        }
        else
        {
            navMeshAgent.SetDestination(targetPlayer.position);  // 플레이어를 향해 계속 달려감
        }

        // 애니메이션 전환
        animator.SetBool("isRunning", true);
        animator.SetBool("isWalking", false);
        navMeshAgent.speed = runSpeed;
    }

    private void Attack()
    {
        // 공격 애니메이션 트리거
        animator.SetTrigger("Attack");

        // 공격 후 잠시 대기 후 Idle 상태로 돌아가기
        Invoke("FinishAttack", 2f);  // 공격 후 Idle로 돌아가기
    }

    private void FinishAttack()
    {
        currentState = State.Idle;
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
    }

    private void Die()
    {
        // 죽음 상태 처리
        animator.SetTrigger("Die");
        navMeshAgent.isStopped = true;  // 죽으면 더 이상 이동하지 않음
    }

    private void SetRandomPatrolPosition()
    {
        // 주변에서 랜덤한 위치를 서성거릴 수 있도록 설정
        randomPatrolPosition = transform.position + new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
    }
}
