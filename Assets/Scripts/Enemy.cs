using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 적이 유한한 상태를 갖도록 하고 싶다.
// 필요속성 : 상태정의
// 애니메이션의 상태를 Move 로 전환하고 싶다.
// 필요속성 : Animator
//죽는 애니메이션이 끝나고 나면 바닥으로 사라지도록 하고싶다.
public class Enemy : MonoBehaviour
{
    #region Enemy 속성정의
    // 필요속성 : 상태정의
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Damage,
        Die
    };
    EnemyState m_State = EnemyState.Idle;
    #endregion

    #region Idle 속성
    // 필요속성 : 대기시간, 경과시간
    public float idleDelayTime = 2;
    float currentTime = 0;
    #endregion

    #region Move 속성
    // 필요속성 : 이동속도, 타겟, CharacterController
    public float speed = 5;
    public Transform target;
    CharacterController cc;
    #endregion

    // 필요속성 : Animator
    Animator anim;

    // Path Finding 을 위한 네비게이션
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        print("state : " + m_State);
        switch (m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Damage:
                //Damage();
                break;
            case EnemyState.Die:
                Die();
                break;
        }
    }

    // 일정시간이 흐르면 상태를 이동으로 바꾸고 싶다.
    
    private void Idle()
    {
        // 일정시간이 흐르면 상태를 이동으로 바꾸고 싶다.
        // 1. 시간이 흘렀으니까
        currentTime += Time.deltaTime;
        // 2. 시간이 됐으니까
        if (currentTime > idleDelayTime)
        {
            // 3. 상태를 이동으로 전환
            m_State = EnemyState.Move;
            currentTime = 0;
            // 애니메이션의 상태를 Move 로 전환하고 싶다.
            anim.SetTrigger("Move");
            agent.enabled = true;
        }
    }

    // 타겟쪽으로 이동하고 싶다.
    // 타겟쪽으로 회전하고 싶다.
    // 공격범위에 타겟이 들어오면 상태를 공격으로 전환하고 싶다.
    // 필요속성 : 공격범위
    public float attackRange = 2;

    // 타겟이 이동가능한 범위 밖에 있으면 패트롤 하고 싶다.
    // 필요속성 : 이동가능한 범위
    public float moveToTargetRange = 5;
    Vector3 randPos;
    bool canMove = false;
    private void Move()
    {
        // 타겟쪽으로 이동하고 싶다.
        // 1. 방향이 필요
        // -> target - me
        Vector3 dir = target.position - transform.position;
        float distance = dir.magnitude;
        dir.Normalize();
        dir.y = 0;

        // 내가 향하는 벡터
        Debug.DrawLine(transform.position, transform.position + transform.forward * 5, Color.red);
        // 타겟쪽으로 향하는 벡터
        Debug.DrawLine(transform.position, transform.position + dir * 5, Color.yellow);

        float dot = Vector3.Dot(transform.forward, dir);
        
        // 이동범위 안에 있다면 그리고 시야범위 안에 있을 때
        if (true)//distance < moveToTargetRange && dot > 0)
        {
            // -> 타겟쪽으로 이동
            agent.destination = target.position;
        }
        // 그렇지 않으면
        // 타겟이 이동가능한 범위 밖에 있으면 패트롤 하고 싶다.
        else
        {
            // 패트롤 하고 싶다.

            // 만약 아직 이동할 곳을 못찾았다면
            if (canMove == false)
            {
                // -> 이동할 곳을 찾자.
                canMove = GetRandomPosition(transform.position, out randPos, 10);
            }
            // 만약 찾았다면
            // -> 그렇지 않다면
            else
            {
                // 이동
                agent.destination = randPos;
                // 만약 목적지에 거의 도착했다면
                if (Vector3.Distance(randPos, transform.position) < 0.1f)
                {
                    // 다시 찾아야 한다고 알려주기
                    canMove = false;
                }
            }
        }
        // 타겟쪽으로 회전하고 싶다.
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 5 * Time.deltaTime);
        // 2. 이동하고 싶다.
        //cc.SimpleMove(dir * speed);
        // 공격범위에 타겟이 들어오면 상태를 공격으로 전환하고 싶다.
        if(distance < attackRange)
        {
            m_State = EnemyState.Attack;
            currentTime = attackDelayTime;
            agent.enabled = false;
        }
    }

    private bool GetRandomPosition(Vector3 position, out Vector3 randPos, float range = 3)
    {
        // 랜덤한 위치찾기
        Vector3 center = Random.insideUnitSphere * range;
        center.y = 0;
        center += position;
        NavMeshHit hitInfo;
        // 랜덤 위치로부터 움직일 수 있는지 여부 조사
        bool result = NavMesh.SamplePosition(center, out hitInfo, range, 1);
        // randPos 에 위치 할당
        randPos = hitInfo.position;
        // 가능 여부 반환
        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    // 일정시간에 한번씩 공격하고 싶다.
    // 필요속성 : 공격대기시간
    // 타겟이 공격범위를 벗어나면 상태를 이동으로 전환하고 싶다.
    public float attackDelayTime = 2;
    private void Attack()
    {
        Vector3 dir = target.position - transform.position;
        dir.Normalize();
        dir.y = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);

        currentTime += Time.deltaTime;
        if(currentTime > attackDelayTime)
        {
            currentTime = 0;
            print("attack!!!");
            anim.SetTrigger("Attack");
        }
        // 타겟이 공격범위를 벗어나면 상태를 이동으로 전환하고 싶다.
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance > attackRange)
        {
            m_State = EnemyState.Move;
            anim.SetTrigger("Move");
            agent.enabled = true;
        }
    }

    // 피격당했을 때 호출되는 이벤트 함수
    // 3 대 맞으면 죽도록 처리하자
    public int hp = 3;
    public float damageSpeed = 0.5f;
    Vector3 knockBackPos;
    public void OnDamageProcess(Vector3 shootDirection)
    {
        agent.enabled = false;

        StopAllCoroutines();
        // 3 대 맞으면
        hp--;
        if (hp <= 0)
        {
            // 죽도록 처리하자
            m_State = EnemyState.Die;
            // 충돌체 꺼버리자
            cc.enabled = false;
            anim.SetTrigger("Die");

        }
        else
        {
            m_State = EnemyState.Damage;
            // 애니메이션 상태 전환
            anim.SetTrigger("Damage");
            //transform.position += shootDirection * damageSpeed;
            //cc.Move(shootDirection * damageSpeed);
            shootDirection.y = 0;
            knockBackPos = transform.position + shootDirection * damageSpeed;
            StartCoroutine(Damage());
        }
        currentTime = 0;
    }

    // 일정시간 기다렸다가 상태를 Idle 로 전환하고 싶다.
    // 필요속성 : 피격대기시간
    public float damageDelayTime = 2;
    private IEnumerator Damage()
    {
        float damageEndCheckTime = 0;
        // 원하는 위치로 계속 이동하고 싶다.
        // 시간이 2초가 아직 안됐다면
        while (damageEndCheckTime < 2)
        {
            // 시간이 흘러야 한다.
            damageEndCheckTime += Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, knockBackPos, 15 * Time.deltaTime);

            // 코루틴 종료시키기
            //yield break;
            // delta Time 만큼 기다리기
            yield return null;
        }
        transform.position = knockBackPos;

        m_State = EnemyState.Idle;
        //currentTime += Time.deltaTime;
        //if (currentTime > damageDelayTime)
        //{
        //    currentTime = 0;
        //    m_State = EnemyState.Idle;
        //}
    }

    public float dieSpeed = 0.5f;
    private void Die()
    {
        //죽는 애니메이션이 끝나고 나면 바닥으로 사라지도록 하고싶다.
        currentTime += Time.deltaTime;
        if(currentTime > 2)
        {
            // 아래로 사라지도록 하자.
            transform.position += Vector3.down * dieSpeed * Time.deltaTime;
            // 완전히 사라지면 제거 -2
            if (transform.position.y < -3)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
