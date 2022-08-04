using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ������ ���¸� ������ �ϰ� �ʹ�.
// �ʿ�Ӽ� : ��������
public class Enemy : MonoBehaviour
{
    // �ʿ�Ӽ� : ��������
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

    // �����ð��� �帣�� ���¸� �̵����� �ٲٰ� �ʹ�.
    // �ʿ�Ӽ� : ���ð�, ����ð�
    public float idleDelayTime = 2;
    float currentTime = 0;
    private void Idle()
    {
        // �����ð��� �帣�� ���¸� �̵����� �ٲٰ� �ʹ�.
        // 1. �ð��� �귶���ϱ�
        currentTime += Time.deltaTime;
        // 2. �ð��� �����ϱ�
        if (currentTime > idleDelayTime)
        {
            // 3. ���¸� �̵����� ��ȯ
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
