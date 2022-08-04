using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 적이 유한한 상태를 갖도록 하고 싶다.
// 필요속성 : 상태정의
public class Enemy : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print("state : " + m_State);
        switch(m_State)
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
    // 필요속성 : 대기시간, 경과시간
    public float idleDelayTime = 2;
    float currentTime = 0;
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
        }
    }

    private void Move()
    {
        throw new NotImplementedException();
    }

    private void Attack()
    {
        throw new NotImplementedException();
    }

    private void Damage()
    {
        throw new NotImplementedException();
    }

    private void Die()
    {
        throw new NotImplementedException();
    }
}
