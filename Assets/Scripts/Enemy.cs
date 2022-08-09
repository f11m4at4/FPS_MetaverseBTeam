using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 적이 유한한 상태를 갖도록 하고 싶다.
// 필요속성 : 상태정의
// 애니메이션의 상태를 Move 로 전환하고 싶다.
// 필요속성 : Animator
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
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
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
                Damage();
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
        }
    }

    // 타겟쪽으로 이동하고 싶다.
    // 타겟쪽으로 회전하고 싶다.
    // 공격범위에 타겟이 들어오면 상태를 공격으로 전환하고 싶다.
    // 필요속성 : 공격범위
    public float attackRange = 2;
    private void Move()
    {
        // 타겟쪽으로 이동하고 싶다.
        // 1. 방향이 필요
        // -> target - me
        Vector3 dir = target.position - transform.position;
        float distance = dir.magnitude;
        dir.Normalize();
        dir.y = 0;
        // 타겟쪽으로 회전하고 싶다.
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 5 * Time.deltaTime);
        // 2. 이동하고 싶다.
        cc.SimpleMove(dir * speed);
        // 공격범위에 타겟이 들어오면 상태를 공격으로 전환하고 싶다.
        if(distance < attackRange)
        {
            m_State = EnemyState.Attack;
            currentTime = attackDelayTime;
        }
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
        }
    }

    // 피격당했을 때 호출되는 이벤트 함수
    // 3 대 맞으면 죽도록 처리하자
    public int hp = 3;
    public float damageSpeed = 0.5f;
    Vector3 knockBackPos;
    public void OnDamageProcess(Vector3 shootDirection)
    {
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
        }
        currentTime = 0;
    }

    // 일정시간 기다렸다가 상태를 Idle 로 전환하고 싶다.
    // 필요속성 : 피격대기시간
    public float damageDelayTime = 2;
    private void Damage()
    {
        transform.position = Vector3.Lerp(transform.position, knockBackPos, 15 * Time.deltaTime);

        currentTime += Time.deltaTime;
        if(currentTime > damageDelayTime)
        {
            currentTime = 0;
            m_State = EnemyState.Idle;
        }
    }

    public float dieSpeed = 0.5f;
    private void Die()
    {
        // 아래로 사라지도록 하자.
        transform.position += Vector3.down * dieSpeed * Time.deltaTime;
        // 완전히 사라지면 제거 -2
        if(transform.position.y < -3)
        {
            Destroy(gameObject);
        }
    }
}
