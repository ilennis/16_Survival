using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Boss : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;    // NavMeshAgent
    private Animator animator;            // Animator
    private Transform targetPlayer;       // 플레이어의 위치

    public float detectionRange = 10f;    // 플레이어를 감지할 거리
    private float attackDistance = 1.6f;  // 공격 거리 (플레이어와의 거리)
    private float stopDistance = 2f;      // 공격 전 멈추는 거리 (attackDistance보다 약간 크게 설정)
    private float runSpeed = 5f;          // 달리는 속도
    private float patrolTime = 5f;        // 대기 후 서성거리기 시간
    private float timeInIdle = 0f;        // Idle 상태에서 보낸 시간

    private int normalAttackCount = 0;    // 일반 공격 횟수
    private int maxNormalAttackCount = 3; // 3번 공격 후 특수 공격
    private bool isSpecialAttackReady = false; // 특수 공격 준비 상태

    // 상태를 나타내는 열거형
    public enum State
    {
        Idle,
        Run,
        Attack,
        SpecialAttack,
        Die
    }

    public State currentState;            // 현재 상태

    private Coroutine attackCoroutine;     // 공격 반복을 위한 Coroutine
    private Coroutine specialAttackCoroutine;  // 특수 공격 반복을 위한 Coroutine

    // 체력, 공격력, 경험치
    public float health = 100f;            // 보스의 체력
    public float attackPower = 10f;        // 보스의 공격력
    public float experiencePoints = 100f;  // 보스가 주는 경험치
    public float attackSpeed = 1f;         // 공격 속도 (초 단위)
    public float specialAttackCooldown = 4f; // 특수 공격 쿨다운 시간

    public ItemData DropItemData;      // 죽을 때 획득할 아이템 데이터
    public int DropItemAmount;      // 획득할 아이템 개수

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
        navMeshAgent.speed = runSpeed;  // 달리기 속도 설정
        navMeshAgent.stoppingDistance = stopDistance;  // 멈추는 거리 설정
    }

    void Update()
    {
        if (targetPlayer == null || navMeshAgent == null)
        {
            Idle(); // 플레이어가 없다면 Idle 상태로 계속 유지
            return;
        }

        // 플레이어와의 거리 계산
        float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);

        // 감지 범위 내에서 플레이어를 추적
        if (distanceToPlayer <= detectionRange)
        {
            // 플레이어가 감지 범위 안에 있으면 Run 상태로 전환
            if (currentState != State.Run && currentState != State.Attack && currentState != State.SpecialAttack)
            {
                currentState = State.Run;
                animator.SetBool("isRunning", true);
                animator.SetBool("isIdle", false);
                animator.ResetTrigger("Attack"); // Attack 애니메이션 리셋
            }

            navMeshAgent.SetDestination(targetPlayer.position);  // 플레이어를 향해 이동

            // 일정 거리 이하로 가까워지면 Attack 상태로 전환
            if (distanceToPlayer <= stopDistance)
            {
                if (currentState != State.Attack && currentState != State.SpecialAttack)
                {
                    currentState = State.Attack;
                    navMeshAgent.isStopped = true;
                    animator.SetBool("isRunning", false);
                    animator.SetBool("isIdle", false);

                    if (attackCoroutine == null)
                        attackCoroutine = StartCoroutine(AttackRepeat());
                }
            }
            else if (distanceToPlayer > stopDistance)
            {
                // 플레이어와의 거리가 stopDistance보다 멀어지면 Run 상태로 전환
                if (currentState == State.Attack || currentState == State.SpecialAttack)
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
        }
        else if (distanceToPlayer > detectionRange)
        {
            // 플레이어가 감지 범위를 벗어나면 Idle 상태로 전환
            currentState = State.Idle;
            animator.SetBool("isIdle", true);
            animator.SetBool("isRunning", false);
        }

        // 특수 공격을 일정 시간마다 실행
        if (currentState != State.SpecialAttack && !isSpecialAttackReady && distanceToPlayer <= attackDistance)
        {
            if (specialAttackCoroutine == null)
            {
                specialAttackCoroutine = StartCoroutine(SpecialAttackCooldown());
            }
        }
    }

    private void Idle()
    {
        timeInIdle += Time.deltaTime;

        if (timeInIdle >= patrolTime)
        {
            currentState = State.Run;
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdle", false);
            timeInIdle = 0f;
            navMeshAgent.SetDestination(targetPlayer.position);  // 플레이어를 향해 이동
        }
        else
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isRunning", false);
        }
    }

    private IEnumerator AttackRepeat()
    {
        while (currentState == State.Attack)
        {
            animator.SetTrigger("Attack");

            // 플레이어에게 일반 공격
            if (targetPlayer != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);
                if (distanceToPlayer <= attackDistance)
                {
                    PlayerCondition player = targetPlayer.GetComponent<PlayerCondition>();
                    if (player != null)
                    {
                        player.TakeDamage((int)attackPower); // 플레이어에게 데미지 주기
                    }
                }
            }

            // 3번 일반 공격 후 특수 공격
            if (normalAttackCount >= maxNormalAttackCount && !isSpecialAttackReady)
            {
                normalAttackCount = 0;
                isSpecialAttackReady = true;
                currentState = State.SpecialAttack;
                animator.SetTrigger("SpecialAttack"); // 특수 공격 애니메이션 실행
            }

            // 공격 속도에 따라 대기
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    private IEnumerator SpecialAttackCooldown()
    {
        yield return new WaitForSeconds(specialAttackCooldown);

        if (currentState != State.SpecialAttack)
        {
            currentState = State.SpecialAttack;
            animator.SetTrigger("SpecialAttack");

            // 특수 공격을 진행 (4초마다 한번)
            if (targetPlayer != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);
                if (distanceToPlayer <= attackDistance)
                {
                    PlayerCondition player = targetPlayer.GetComponent<PlayerCondition>();
                    if (player != null)
                    {
                        float specialAttackDamage = attackPower * 2; // 특수 공격은 공격력의 두 배
                        player.TakeDamage((int)attackPower); // 플레이어에게 데미지 주기
                    }
                }
            }

            // 특수 공격 후 다시 Run 상태로 돌아가도록 설정
            isSpecialAttackReady = false;
            specialAttackCoroutine = null;
            currentState = State.Run;
        }
    }

    // 체력, 공격력, 경험치 등
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            currentState = State.Die;
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("Die");
        navMeshAgent.isStopped = true;

        if (DropItemData != null)
        {
            // 아이템 획득
            GameManager.Instance.Inventory.AddItem(DropItemData, DropItemAmount);
        }
        // 적 오브젝트 제거
        Destroy(gameObject, 2f); // 2초 후에 오브젝트 제거
    }

}