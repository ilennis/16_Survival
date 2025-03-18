using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;    // NavMeshAgent
    private Animator animator;            // Animator
    private Transform targetPlayer;       // 플레이어의 위치

    public float detectionRange = 10f;    // 플레이어를 감지할 거리
    private float attackDistance = 2f;    // 공격 거리 (플레이어와의 거리)
    private float stopDistance = 2f;      // 공격 전 멈추는 거리 (attackDistance보다 약간 크게 설정)
    private float walkSpeed = 2f;         // 걷는 속도
    private float runSpeed = 5f;          // 달리는 속도

    private float idleWalkSwitchTime = 4f; // Idle과 Walk 상태 전환 시간
    private float timeSinceLastSwitch = 0f; // 마지막 상태 전환 시간
    private Vector3 randomPatrolPosition;  // 랜덤 서성거리 위치
    private float patrolTime = 5f;         // 서성거리기 시간 간격
    private float timeSinceLastPatrol = 0f; // 마지막 서성거리 시간

    private float idleTime = 3f;           // Idle 상태에서 멈추는 시간
    private float timeInIdle = 0f;         // Idle 상태에서 멈춘 시간

    // 상태를 나타내는 열거형
    public enum State
    {
        Idle,
        Walk,
        Run,
        Attack,
        Die
    }

    public State currentState;            // 현재 상태

    private Coroutine attackCoroutine;     // 공격 반복을 위한 Coroutine

    // 체력, 공격력, 경험치
    public float health = 100f;            // 적의 체력
    public float attackPower = 10f;        // 적의 공격력
    public float experiencePoints = 50f;   // 적이 주는 경험치
    public float attackSpeed = 1f;         // 공격 속도 (초 단위)

    public GameObject itemDropPrefab;      // 죽을 때 떨어뜨릴 아이템 프리팹

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent == null || animator == null)
        {
            Debug.LogError("NavMeshAgent 또는 Animator가 없습니다.");
            return;
        }

        targetPlayer = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (targetPlayer == null)
        {
            Debug.LogError("플레이어를 찾을 수 없습니다.");
            return;
        }

        // 초기 상태 설정
        currentState = State.Idle;
        navMeshAgent.speed = walkSpeed;  // 기본 속도 설정
        navMeshAgent.stoppingDistance = stopDistance;  // 멈추는 거리 설정
        SetRandomPatrolPosition();

        // 시작하자마자 플레이어가 감지 범위 안에 있으면 Run 상태로 전환
        float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);
        if (distanceToPlayer <= detectionRange)
        {
            currentState = State.Run;
            navMeshAgent.SetDestination(targetPlayer.position);
            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);
            animator.SetBool("isIdle", false);
        }
        else
        {
            // 플레이어가 감지 범위 밖에 있으면 Idle 상태로 시작
            currentState = State.Idle;
            animator.SetBool("isIdle", true);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
    }

    void Update()
    {
        if (targetPlayer == null || navMeshAgent == null)
            return;

        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage(health); // 체력을 즉시 0으로 만들기
        }

        // 플레이어와의 거리 계산
        float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);

        if (distanceToPlayer <= detectionRange && currentState != State.Attack)
        {
            // 플레이어가 감지 범위 안에 있으면 Run 상태로 전환
            if (currentState != State.Run)
            {
                currentState = State.Run;
                animator.SetBool("isRunning", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("isIdle", false);
            }
            navMeshAgent.SetDestination(targetPlayer.position);  // 플레이어를 향해 이동
        }
        else if (distanceToPlayer > detectionRange)
        {
            // 플레이어가 범위 밖에 있으면 Idle과 Walk 상태를 반복
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

        // Run 상태에서 플레이어와의 거리가 stopDistance 이내로 가까워지면 Attack 상태로 전환
        if (currentState == State.Run && distanceToPlayer <= stopDistance)
        {
            currentState = State.Attack;
            navMeshAgent.isStopped = true;
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", false);

            if (attackCoroutine == null)
                attackCoroutine = StartCoroutine(AttackRepeat());
        }

        // Attack 상태에서 플레이어가 멀어지면 Run 상태로 전환
        if (currentState == State.Attack && distanceToPlayer > stopDistance)
        {
            currentState = State.Run;
            navMeshAgent.isStopped = false;
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdle", false);

            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
    }

    private void Idle()
    {
        timeInIdle += Time.deltaTime;

        if (timeInIdle >= idleTime)
        {
            currentState = State.Walk;
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", false);
            navMeshAgent.isStopped = false;
            timeInIdle = 0f;
            SetRandomPatrolPosition();
            navMeshAgent.SetDestination(randomPatrolPosition);
        }

        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isIdle", true);
    }

    private void Walk()
    {
        timeSinceLastPatrol += Time.deltaTime;

        if (timeSinceLastPatrol >= patrolTime)
        {
            SetRandomPatrolPosition();
            navMeshAgent.SetDestination(randomPatrolPosition);
            timeSinceLastPatrol = 0f;
        }

        if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", false);
            navMeshAgent.speed = walkSpeed;
        }
        else
        {
            currentState = State.Idle;
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", true);
        }
    }

    private void Run()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);

        if (distanceToPlayer <= stopDistance)
        {
            navMeshAgent.isStopped = true;
            currentState = State.Attack;
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", false);

            if (attackCoroutine == null)
                attackCoroutine = StartCoroutine(AttackRepeat());
        }
        else
        {
            navMeshAgent.SetDestination(targetPlayer.position);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdle", false);
            navMeshAgent.speed = runSpeed;
        }
    }

    private IEnumerator AttackRepeat()
    {
        while (currentState == State.Attack)
        {
            animator.SetTrigger("Attack");

            // 플레이어의 체력을 직접 수정하는 코드
            if (targetPlayer != null)
            {
                // 플레이어와의 거리 확인
                float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);
                if (distanceToPlayer <= attackDistance)
                {
                    // 플레이어의 체력을 직접 수정
                    PlayerCondition player = targetPlayer.GetComponent<PlayerCondition>();
                    if (player != null)
                    {
                        player.TakeDamage((int)attackPower); // 플레이어에게 데미지 주기
                    }
                }
            }

            // 공격 속도에 따라 대기
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
    }

    private void Die()
    {
        animator.SetTrigger("Die");
        navMeshAgent.isStopped = true;

        // 아이템 드롭
        if (itemDropPrefab != null)
        {
            Instantiate(itemDropPrefab, transform.position, Quaternion.identity);
        }

        // 경험치 지급
        PlayerCondition player = targetPlayer.GetComponent<PlayerCondition>();
        if (player != null)
        {
            player.GetExp(experiencePoints);
        }

        // 적 오브젝트 제거
        Destroy(gameObject, 1f); // 2초 후에 오브젝트 제거
    }

    // 데미지를 받는 함수
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            currentState = State.Die;
            Die();
        }
    }

    private void SetRandomPatrolPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 10f;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 10f, NavMesh.AllAreas);
        randomPatrolPosition = hit.position;
    }
}
